# 📘 MockApiIntegration

## 📑 Table of Contents
- [API Endpoints](#api-endpoints)
  - [Create Product](#1-create-product)
  - [Get Products](#2-get-products-with-filtering-and-pagination)
  - [Delete Product](#3-delete-product)
  - [Notes](#notes)
- [Local Setup](#local-setup)



# 🔧 API Endpoints
### 1. Create Product
POST /api/products

Creates a product by forwarding the request to the external mock API, then stores the product's ID and name in memory.

### 2. Get Products (with Filtering and Pagination)
GET /api/products?name=prod&page=1&pageSize=5

Fetches a paginated list of products created through this API. Only IDs stored in memory are queried from the mock API.

### 3. Delete Product
DELETE /api/products/{id}

Deletes the product from the mock API and removes its ID from memory.

### Notes
Products are only tracked if created via this API.
In-memory store is not persistent; restarting the app clears tracked IDs.

# Local Setup 

Open Cmd and run following commands

git clone [https://github.com/ViswanthSwarna/MockApiIntegration.git](https://github.com/ViswanthSwarna/MockApiIntegration)

cd MockApiIntegration/MockApiIntegration.API

dotnet run --urls "http://localhost:5050"

Finally go to url http://localhost:5050/Swagger and test the endpoints



