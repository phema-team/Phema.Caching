## Phema.Caching
- Strongly typed IDistributedCache wrapper using DI
- You can add IDistributedCache explisitly and then add typed caches
- You can specify key type, using `AddCache<TKey, TModel, TDistributedCache<TKey, TModel>>`
- By default `TKey` is string
```csharp
services.AddDistributedMemoryCache()
  .AddPhemaDistributedCache(caching =>
    caching.AddCache<Model, ModelDistributedCache>());
```

## Phema.Caching.Redis
- You can simplify definition by using `Phema.Caching.Redis` syntactic sugar
- You have to specify `ISerializer` from `Phema.Serialization` package
```csharp
services.AddPhemaDistributedRedisCache(caching =>
  caching.AddCache<Model, ModelDistributedCache>());
```

## Phema.Caching.Redis
```csharp
services.AddPhemaDistributedMemoryCache(caching =>
  caching.AddCache<Model, ModelDistributedCache>());
```
