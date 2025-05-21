# Documentação oficial

## Comandos iniciais para gerar os bancos de dados
1. Gerar o banco Geral, com usuários e empresas: <br>
``` dotnet ef database update --context GeneralDbContext --verbose ```

2. Gerar o banco de design interno, que serve de base para os outros bancos de dados: <br>
``` dotnet ef database update --context InternalDbContext --verbose ```

## Comandos para gerenciar o banco de dados

comando para criar novas migrations nos bancos internos:<br>
``` dotnet ef migrations add MigrationName --context InternalDbContext --output-dir Migrations/Internal --verbose ```

atualizar bancos de dados internos: <br>
``` HTTP::GET /api/Config/Database-update ```

## Informações relacionados aos banco de dados.
* Existem dois esquemas de banco de dados, o ``` GeneralDbContext - Banco geral de usuários e empresas``` e o ``` InternalDbContext - Banco específico para guardar os dados de cada empresa separadamente. ```
* Cada empresa tem seu próprio banco de dados.
* Cada vez que uma empresa é criada, automaticamente um novo banco de dados para a empresa é gerado.
* O parametro usado para definir o nome do banco de dados é ```Database=nome_empresa```.
* Na tabela de empresas ``` companies ``` existe um atributo chamado StringConnection. Ex: ``` "Host=localhost;Port=5432;Database=dummy_empresa;Username=postgres;Password=1234" ``` 
* O comando usado para criar migrations para o ``` InternalDbContext ``` é ```dotnet ef migrations add InitialCreate --context InternalDbContext --output-dir Migrations/Internal```
* O comando usado para rodar uma migration em um contexto específico é ``` dotnet ef database update --context InternalDbContext ```

## Informações relacionadas as Roles e níveis de acesso.

1. ``` admin ``` - Controle total do sistema, acesso a todas as funcionalidades do sistema.
2. ``` admin_internal ``` - Administrador geral de cada empresa específica, acesso a todos os dados e funcionalidades de cada empresa específica.

## Variáveis de ambiente
### Variáveis relacionadas ao token JWT (Exemplos)
* JWT_SECRET_KEY = "MinhaChaveRSASuperSegura@2025"
* JWT_ISSUER = "http://localhost:7151"
* JWT_AUDIENCE = "http://localhost:4200"
### Variáveis relacionadas ao banco de dados geral (Exemplos)
* SERVER = "localhost"
* DATABASE_NAME = "general"
* USER = "sa"
* PASSWORD = "12345678"