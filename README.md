# Learning GraphQL

Building GraphQL API's in .NET to better understand how GraphQL works.

The application is made up of a GraphQL API running in ASP.NET. To demonstrate how resolvers can run in parallel and against backend services properties of the Product object resolve against three different backend services.

``` json
{
    "id": "", // The Product ID
    "details": {}, // Details resolve against product backend
    "stockLevel": {}, // Stock Levels resolve against the stock backend
    "reviews": {} // Reviews resolve against the review backend
}
```

The product, stock and review backend are seperate gRPC services.

## Run Locally

The backend services need to start up before the main GraphQL API. Run the below commands in seperate terminal windows.

### Product Backend

```bash
cd src/Product.Backend
dotnet run --urls=http://localhost:5007/
```

### Stock Level Backend

```bash
cd src/Stock.Backend
dotnet run --urls=http://localhost:5008/
```

### Reviews Backend

```bash
cd src/Reviews.Backend
dotnet run --urls=http://localhost:5009/
```

### GraphQL API

```bash
cd src/GraphQl.DotnetApi
dotnet run --urls=http://localhost:5010/
```

Once all running navigate to `http://localhost:5010/ui/playground` to view the GraphQL playground.

## Using the API

The API's are built around In Memory data stores. Before any query endpoints will run first run the `addProduct` mutation to add a new product record.

