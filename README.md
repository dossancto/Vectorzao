# Vectorzao

> This project is just a Quick Test using `pgvector` extension 
    for postgres and EntityFramework extensions. This code is not so beautiful.

## About

The main focus of this project, is to  test the using of Semantic Search for Postgres Databases.
Using a lightweight ia model. This test aims to practice the usage of embeddings generation in dotnet.

## Dependencies:

This projects uses the following dependencies:

- Dotnet 8
- Dotnet EF Tools
- Docker and Docker Compose
- Ollama (with `snowflake-arctic-embed:latest` model)

## Setup:

- Up the container

```sh
docker compose up -d
```

- Run migrations

```sh
dotnet ef datbase update -s Vetorzao.API
```

- Run the application

```sh
dotnet run --project Vetorzao.API
```
