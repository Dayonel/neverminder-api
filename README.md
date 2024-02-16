# ![Neverminder icon](https://github.com/Dayonel/neverminder-api/assets/10290812/b64fdd09-5297-4cf5-bb68-3d7249a9c147) Neverminder API

Neverminder is an app that sends notifications for your plans.

## Installation

Install [Visual Studio](https://visualstudio.microsoft.com/downloads/)
 - .NET 8

Install [Docker](https://www.docker.com/products/docker-desktop/)

## Firebase
Create a project in [Firebase](https://firebase.com/)

Generate credentials for your service account
	
> Place the json file in the api project root `neverminder-me-firebase-adminsdk-pq10q-ab46b52acc.json`

## Digital ocean
Create a dropplet in digital ocean, select from marketplace `Docker 23.0.6 on Ubuntu 22.04`
You will need at a machine at least of `1GB / 1CPU` for `$6/mo`.
> Configure your SSH keys.

Login into your dropplet, execute the following command
```
sudo ufw allow ssh
```
This is necessary for github actions step `Transfer files to Droplet`

## Domain
Buy a domain for example in [Namecheap](https://www.namecheap.com/)
Point your domain DNS to digitalocean
> ns1.digitalocean.com\
> ns2.digitalocean.com\
> ns3.digitalocean.com

Check that your DNS is propagated [dnschecker](https://dnschecker.org/)

## Github secrets
Configure your project secrets, you will need at least these
> DOCKER_IMAGE_NAME\
> DOCKER_PASSWORD\
> DOCKER_USERNAME\
> DROPLET_IP\
> DROPLET_USERNAME\
> DROPPLET_FOLDER_PATH\
> FIREBASE_CREDENTIALS\
> PASSPHRASE\
> SSH_PRIVATE_KEY\
> LITESTREAM_KEY_ID\
> LITESTREAM_SECRET\
> S3_REPLICAS_HOSTNAME

## Let's encrypt
Copy a simple nginx.conf configuration from commit `663897e3e1bf26e950a5ae5f7587531e8fc5fe41`.

Request your first https certificate using let's encrypt.
Replace `--email` by your email and `-d` by your domain.
```
CERTBOT_COMMAND="certonly --webroot -w /var/www/certbot --force-renewal --email your_email -d your_domain.com -d www.your_domain.com -d api.your_domain.com --agree-tos" /usr/bin/docker compose -f /neverminder-api/docker-compose.yml up certbot
```

## Auto renew
Configure your `cron` job to auto renew your ssl certificate.
```
crontab -e
```

Replace `--email` by your email and `-d` by your domain.
```
0 5 1 */2 * CERTBOT_COMMAND="certonly --webroot -w /var/www/certbot --force-renewal --email your_email -d your_domain.com -d www.your_domain.com -d api.your_domain.com --agree-tos" /usr/bin/docker compose -f /neverminder-api/docker-compose.yml up certbot
```

## Database
`Statup.cs` file will apply all pending migrations by this instruction `db.Database.Migrate()`.\
A database is automatically created if none exist.

Create a file `appsettings.development.json` in the api project root with a local connection string.
```
"ConnectionStrings": {
  "DbContext": "DataSource=neverminder.db"
}
```

In production, file `appsettings.json` is used and a volume is mounted, path has to match `docker-compose.yml`
```
"ConnectionStrings": {
  "DbContext": "DataSource=/var/lib/sqlite/db-file.db"
}
```

## SQLite
Journaling mode is set to [WAL](https://www.sqlite.org/wal.html), is significantly faster in most scenarios.\
Database consists of up to 3 files:
> Database file `.db`\
> WAL file `.db-wal`\
> Shared memory file `.db-shm`

## Litestream
Database is replicated using [Litestream](https://litestream.io/) to a S3-compatible storage.\
Litestream only works with the SQLite `WAL` journaling mode.\
Litestream requires periodic write locks, we handle this scenario using `PRAGMA busy_timeout = 5000;`\
Checkpoints to the main database are set to `PRAGMA synchronous = NORMAL;`, this setting is a good choice for most applications running in WAL mode.\
Litestream control checkpoints maintaining a read lock on the database due to this instruction `PRAGMA wal_autocheckpoint = 0;`

## Restore a db replica
To restore your database, download litestream in your server/local machine
```
wget https://github.com/benbjohnson/litestream/releases/download/v0.3.13/litestream-v0.3.13-linux-amd64.deb
```

Then install it using dpkg:
```
sudo dpkg -i litestream-v0.3.13-linux-amd64.deb
```

Next, set environment variables
```
export LITESTREAM_ACCESS_KEY_ID=YOUR_ID
```
```
export LITESTREAM_SECRET_ACCESS_KEY=YOUR_SECRET
```

Clear console history
```
history -c
```
```
clear
```

Restore now database
```
litestream restore -o neverminder.db s3://S3_REPLICAS_HOSTNAME/neverminder-db
```

## Access server via FTP
If you are using windows you can access server files using [WinSCP](https://winscp.net/eng/download.php)

## Copy database from inside a container
Copy database: *up to 3 files*
```
docker cp neverminder-api:/var/lib/sqlite /neverminder-api
```

## Copy logs folder from inside a container
Copy all logs
```
docker cp neverminder-api:/app/logs /neverminder-api
```

## Log a running Docker container
> docker logs neverminder-api\
> docker logs litestream\
> docker logs nginx-reverse-proxy

## Setup
Follow the commits of the `setup` branch for a detailed step by step.