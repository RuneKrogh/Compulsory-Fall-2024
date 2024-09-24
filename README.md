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
    - Validation for creating/updating products
    - Validation for creating/updating orders
    - Validation for creating/updating customers
    - Validation for creating/updating properties
      
- Data Transfer Objects
    - DTO for creating order (order entries)
    - DTO for updating order (status)
    - DTO for creating/updating customers (name, email, phone, address)
    - DTO for creating products (name, stock, price, properties)
    - DTO for updating products (name, stock, price, discontinued, properties)
    - DTO for creating/updating properties (name)
      
- xUnit Tests
    - Success/Error Creating/Deleting/Updating Products
    - Success/Error Creating/Deleting/Updating Orders
    - Success/Error Creating/Deleting/Updating Customers
    - Success/Error Creating/Deleting/Updating Properties
    - Success/Error Creating/Deleting/Updating Properties

- User story requirements:
    - As a customer I want to be able to place an order with X order entries of products.
    - As a customer I want to be able to see my own order history.
    - As a customer I want to have a product overview with filtering, ordering and full-text search preferences.
    - As a business admin I want to create new products, discontinue products and restock products.
    - As a business admin I want to be able to create custom properties for paper products (water-resistant, study, etc).
    - As a business admin I want to be able to change the status of an order.
