Steps to run:
1. Specify Db connection string in appsettings.json ConnectionStrings
2. Specify JWT Issuer in appsettings.json JwtSettings
3. Specify JWT Audience in appsettings.json JwtSettings
4. Specify JWT Secret Key in appsettings.json JwtSettings
5. Specify url of IdentityAPI service in appsettings.json ExternalServices
6. Run RabbitMQ container in Docker
7. Specify RabbitMQ queue name and host name in appsettings.json
8. Run Identity API
