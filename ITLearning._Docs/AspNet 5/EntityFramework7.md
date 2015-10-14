# Readme

#### Entity Framework
[DOCS](http://ef.readthedocs.org/en/latest/)

### EF7 Migrations:


> In EF project's root directory (with project.json):

> dnx ef migrations add [CamelCaseMigrationName]
> dnx ef database update

> dnx ef database update -e Development // update database on local env.

> dnx ef database update -e Production  // update database on Azure env.