# Phema.Caching

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Caching/status.svg)](https://cloud.drone.io/phema-team/Phema.Caching)
[![Nuget](https://img.shields.io/nuget/v/Phema.Caching.svg)](https://www.nuget.org/packages/Phema.Caching)
[![Nuget](https://img.shields.io/nuget/dt/Phema.Caching.svg)](https://nuget.org/packages/Phema.Caching)

Strongly typed `IDistributedCache` wrapper. Working with objects, not bytes!

## Installation

```bash
$> dotnet add package Phema.Caching
```

## Usage

```csharp
// Add
services.AddDistributedCache() // Phema.Caching
  .AddDistributedMemoryCache(); // Microsoft.Extensions.Caching.Memory

// Get or inject
var cache = provider.GetRequiredService<IDistributedCache<TestModel>>();

// Use
await cache.SetAsync("test", new TestModel());
```
