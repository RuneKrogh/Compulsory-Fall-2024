# Compulsory-Fall-2024
## Setup

```sh
npm ci --prefix client
docker compose up -d
```

Start server

```sh
dotnet run --project server/Api
```

Start client

```sh
npm run dev --prefix client
```

Setup database
```sh
Run "testdata.sql" in "server/DataAccess/Database" to setup test data
```
