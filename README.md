# Phema.Caching

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Caching/status.svg)](https://cloud.drone.io/phema-team/Phema.Caching)
[![Nuget](https://img.shields.io/nuget/v/Phema.Caching.svg)](https://www.nuget.org/packages/Phema.Caching)

Strongly typed `IDistributedCache` wrapper using `Microsoft.Extensions.DependencyInjection`. Working with objects, not bytes!

## Installation

```bash
$> dotnet add package Phema.Caching
```

## Features

- Used modular `Phema.Serialization` serialization packages
- `IDistributedCache<TValue>` and `IDistributedCache<TKey, TValue>` interfaces

## Usage

```csharp
// Add
services.AddDistributedMemoryCache() // Microsoft.Extensions.Caching.Memory
  .AddJsonSerializer() // Phema.Serialization.Json
  .AddDistributedCache(); // Phema.Caching

// Get or inject
var cache = provider.GetRequiredService<IDistributedCache<TestModel>>();

// Use
await cache.SetAsync("test", new TestModel());
```

- Add `Microsoft.Extensions.Caching.*` package
- Add `Phema.Serialization.*` package
- Add `Phema.Caching`
