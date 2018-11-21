# phema.caching
Strongly typed IDistributedCache wrapper using DI

# Core library
```csharp
services.AddDistributedMemoryCache()
  .AddDistributedCaching(caching =>
    caching.AddCache<Model, ModelDistributedCache>());
```

- You can add IDistributedCache explisitly and then add typed caches
- This will not work, you have to configure Serialization e.g. `.WithJsonSerialization` from `Phema.Caching.Extensions.Json`
- You can specify key type, using `AddCache<TKey, TModel, TDistributedCache<TKey, TModel>>`
- By default `TKey` is string

# Redis
```csharp
services.AddDistributedRedisCaching(caching =>
  caching.AddCache<Model, ModelDistributedCache>());
```

- You can simplify definition by using `Phema.Caching.Redis` syntactic sugar
- You still have to specify `Serialization`
- This pacjage uses `Phema.Configuration` so you need to add `RedisCondiguration` to your configuration tree or add `RedisConfiguration` explisitly by configuring DI

# Json
```csharp
services.AddDistributedRedisCaching(caching =>
  caching.AddCache<Model, ModelDistributedCache>())
  .WithJsonSerialization();
```

- Now we have only one serialization provider
- You have to use one of serialization providers, but i not added default to core library, because you can write your own or add predefined by using this package
