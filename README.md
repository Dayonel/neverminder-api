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
> DB_HOST\
> DB_NAME\
> DB_PASSWORD\
> DB_USER\
> DOCKER_IMAGE_NAME\
> DOCKER_PASSWORD\
> DOCKER_USERNAME\
> DROPLET_IP\
> DROPLET_USERNAME\
> DROPPLET_FOLDER_PATH\
> FIREBASE_CREDENTIALS\
> PASSPHRASE\
> SSH_PRIVATE_KEY

## Let's encrypt
Configure your https certificate using let's encrypt.
Replace `--email` by your email and `-d` by your domain.

## Auto renew
Configure your `cron` job to auto renew your ssl certificate.
```
crontab -e
```

Replace `--email` by your email and `-d` by your domain.
```
0 5 1 */2 * CERTBOT_COMMAND="certonly --webroot -w /var/www/certbot --force-renewal --email your_email  -d your_domain.com -d www.your_domain.com -d api.your_domain.com --agree-tos" /usr/bin/docker compose -f /neverminder-api/docker-compose.yml up certbot
```

## Database
Create a file `appsettings.development.json` in the api project root and replace your connection string `DbContext` with your credentials.

## Setup
Follow the commits of the `setup` branch for a detailed step by step.

## SQLite (optional)
Copy file to container
```
docker cp neverminder.db neverminder-api:/tmp/neverminder.db
```

Move file to a volume
```
docker exec -it neverminder-api mv /tmp/neverminder.db /var/lib/sqlite/db-file.db
```

Follow the commits of the `sqlite` branch for a detailed step by step.

## SQLite migrations (optional)
`Statup.cs` file will apply all pending migrations by this instruction `db.Database.Migrate()`

## Copy file from inside a container volume
Copy file from container volume
```
docker cp neverminder-api:/var/lib/sqlite/db-file.db /neverminder-api
```

## Copy logs folder from inside a container
Copy all logs
```
docker cp neverminder-api:/app/logs /neverminder-api
```