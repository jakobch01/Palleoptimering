#!/bin/bash
# Wait for SQL Server to be up and running
sleep 30s

# Run the SQL script to initialize the database
/opt/mssql-tools/bin/sqlcmd -S db -U sa -P YourStrong@Passw0rd -d master -i init_db.sql
