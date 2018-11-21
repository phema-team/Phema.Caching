# phema.caching
Strongly typed IDistributedCache wrapper using DI

# Core library
```csharp
services.AddDistributedMemoryCache()
  .AddDistributedCaching(caching =>
    caching.AddCache<Model, ModelDistributedCache>());
```

# Redis
```csharp
services.AddDistributedRedisCaching(caching =>
  caching.AddCache<Model, ModelDistributedCache>());
```

# Json
```
services.AddDistributedRedisCaching(caching =>
  caching.AddCache<Model, ModelDistributedCache>())
  .WithJsonSerialization();
```
