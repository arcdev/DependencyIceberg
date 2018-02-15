Source code from engram404.net's [The Dependency Iceberg](http://engram404.net/the-dependency-iceberg).

# Full .NET Framework
Uses [Autofac](https://autofac.org).
## DependencyIceberg
The typical dependency injection pattern.

Output:
```
ctor ---> Leaf
ctor ---> Twig
ctor ---> Branch
ctor ---> Trunk
```

## LazyDependencyIcerberg
Dependency Injection via Lazy interfaces.

Output:
```
ctor ---> Leaf
```

# .NET Core
Uses the standard [Dependency Injection framework](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection) from .NET Core.

## CoreDependencyIceberg
The typical dependency injection pattern. <br/>Uses 

Output:
```
ctor ---> Leaf
ctor ---> Twig
ctor ---> Branch
ctor ---> Trunk
```

## LazyDependencyIcerberg
Dependency Injection via Lazy interfaces.

Output:
```
ctor ---> Leaf
```
