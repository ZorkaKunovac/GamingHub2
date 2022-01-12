# GamingHub2

## API launching


* Clone the repository

* Install Docker

* Within the *main directory* of the repository (`GamingHub2`), enter the following commands on the CMD:

```
docker-compose build

docker-compose up
```
>
## Database importing

* Start a new CMD window in the main directory of the repository (`GamingHub2`)

* Enter the following commands:

```
docker exec -it gaminghub2-master_gaminghub2-sql_1 mkdir /var/opt/mssql/backup

docker cp 160089.bak gaminghub2-master_gaminghub2-sql_1:/var/opt/mssql/backup

docker exec -it gaminghub2-master_gaminghub2-sql_1 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "QWElkj132!" -Q "RESTORE DATABASE [160089] FROM DISK = '/var/opt/mssql/backup/160089.bak' WITH MOVE 'GamingHub2DB' TO '/var/opt/mssql/data/GamingHub2DB.mdf', MOVE 'GamingHub2DB_log' TO '/var/opt/mssql/data/GamingHub2DB_log.ldf'"
```

* If necessary replace `gaminghub2-master_gaminghub2-sql_1` with the name of the SQL server docker container

## Application login credentials

#### Role: Administrator
* Username: Administrator
* Password: test12345
* Applications: Desktop, Mobile

### Role: Moderator
* Username: Moderator
* Password: test
* Applications: Desktop, Mobile

### Role: User
* Username: Korisnik
* Password: test
* Applications: Mobile
