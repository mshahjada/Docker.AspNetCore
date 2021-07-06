#!/bin/sh

#Author   : Sagor
#Scripts Follows Here:
#DB backup query

echo "Initializing......"

#date=$(date '+%Y%m%d%H%M')

#db_name="CloudApp_${date}.bak"

#server_name="BS-581\SQLEXPRESS"
#userId="SA"
#password="1234"
#database_name="CloudApp"

server_name="localhost"
userId="SA"
password="Docker@1234"
database_name="CloudApp"
container_name="sqlserver"

script_loc="c:\Backup\backup.sql"
backup_log="c:\Backup\log.txt"
#backup_path='C:\Backup\'"${db_name}"''
#backup_path = '/var/opt/mssql/backup'
backup_path="${1}"


echo "......Starting Backup......"

#query="BACKUP DATABASE ["$database_name"] TO DISK = N'"$backup_path"'"
query="BACKUP DATABASE ["$database_name"] TO DISK = N'"$backup_path"'"


#sqlcmd -S BS-581\SQLEXPRESS -U SA -P 1234

#sqlcmd -S "$server_name" -E -i "$script_loc"  -o "$backup_log"

#sqlcmd -S "$server_name" -U "$userId" -P "$password" -Q "$query"

docker exec -i "$container_name" /opt/mssql-tools/bin/sqlcmd -S "$server_name" -U "$userId" -P "$password" -Q "$query"

docker mv "$container_name":/"$backup_path" /var/tmp

#find . -name "CloudApp_*" -type f -delete

echo "......Backup Taken......"
