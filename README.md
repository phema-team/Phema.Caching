# Phema.Caching

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Caching/status.svg)](https://cloud.drone.io/phema-team/Phema.Caching) [![Nuget](https://img.shields.io/nuget/v/Phema.Caching.svg)](https://www.nuget.org/packages/Phema.Caching)

Strongly typed IDistributedCache wrapper using `Microsoft.Extensions.DependencyInjection`

## Installation

```bash
$> dotnet add package Phema.Caching
```

## Usage

```csharp
// Add
services.AddDistributedMemoryCache()
  .AddDistributedCache(options =>
    options.AddCache<TestModel>())
  .AddNewtonsoftJsonSerializer();

// Get
var cache = provider.GetRequiredService<IDistributedCache<TestModel>>();

// Use
await cache.SetAsync("test", new TestModel());
```

- Add `Microsoft.Extensions.Caching.*` package
- Add `Phema.Serialization.*` package
- Add `Phema.Caching`

## Features

- Working with objects, not bytes
- Used modular `Phema.Serialization` serialization package
- `IDistributedCache<TValue>` and `IDistributedCache<TKey, TValue>` interfaces