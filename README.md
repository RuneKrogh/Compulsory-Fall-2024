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



## Missing Requirements
- Server-side Data Validation
- xUnit Tests
- User story requirements:
    - As a customer I want to be able to place an order with X order entries of products.
    - As a customer I want to be able to see my own order history.
    - As a customer I want to have a product overview with filtering, ordering and full-text search preferences.
    - As a business admin I want to create new products, discontinue products and restock products.
    - As a business admin I want to be able to create custom properties for paper products (water-resistant, study, etc).
    - As a business admin I want to be able to change the status of an order.
