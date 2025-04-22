# Documentação oficial

## Informações relacionados aos banco de dados.
* Existem dois esquemas de banco de dados, o ``` GeneralDbContext - Banco geral de usuários e empresas``` e o ``` InternalDbContext - Banco específico para guardar os dados de cada empresa separadamente. ```
* Cada empresa tem seu próprio banco de dados.
* Cada vez que uma empresa é criada, automaticamente um novo banco de dados para a empresa é gerado.
* O parametro usado para definir o nome do banco de dados é ```Database=nome_empresa```.
* Na tabela de empresas ``` companies ``` existe um atributo chamado StringConnection. Ex: ``` "Host=localhost;Port=5432;Database=dummy_empresa;Username=postgres;Password=1234" ``` 
* O comando usado para criar migrations para o ``` InternalDbContext ``` é ```dotnet ef migrations add InitialCreate --context InternalDbContext --output-dir Migrations/Internal```

## Informações relacionadas as Roles e níveis de acesso.

1. ``` admin ``` - Controle total do sistema, acesso a todas as funcionalidades do sistema.
2. ``` admin_internal ``` - Administrador geral de cada empresa específica, acesso a todos os dados e funcionalidades de cada empresa específica.