# SimpleEssentials

Simple library that wraps other useful libraries. It's purpose is to speed up your development time.

## DbDataProvider

Gives you an easy way to map SQL data to strongly typed objects. Also provides an easy cache management if a cache is necessary.

### Initialization

DbDataProvider can be initialized two ways:
* Pass IDataStore and ICacheManager instances
* Inject the dependencies

```C#
var dbProvider = new DbDataProvider(new DbStore("[CONTECTION_STRING]"), new MemoryCacheManager());
```

```C#
//Somewhere in App_Start
ContainerHelper.Container.Register<IDataStore>(() => new DbStore("[CONTECTION_STRING]"));
ContainerHelper.Container.Register<ICacheManager>(() => new MemoryCacheManager());
ContainerHelper.Container.Verify();

//Somewhere else in project
var dbProvider = new DbDataProvider();
```
You can override the injected values by initializing the DbDataProvider with the proper objects.


### Usage
* Insert
* InsertList
* InsertAndReturnId
* BulkInsert
* Update
* Delete
* Execute
* ExecuteScalar
* Get
* GetByType
* GetByParameters
* GetMultiMap


#### Insert

Inserts a single object into the database

```C#
bool Insert<T>(T data, CacheSettings cacheSettings = null, bool invalidateCache = false) where T : class, new();
```
returns true if successful, false if not.

#### InsertList

Inserts a list into the database.

```C#
int InsertList<T>(IEnumerable<T> data, string sql, CacheSettings cacheSettings = null) where T : class, new();
```
returns records inserted.

#### InsertAndReturnId
Insert a single oject into the database and return the new rows ID.

```C#
int InsertAndReturnId<T>(string sql, T data, CacheSettings cacheSettings = null, bool invalidateCache = false) where T : class, new();
```
returns ID of the new record.

## Built With

* [CsvHelper](https://github.com/JoshClose/CsvHelper) - CSV File Management
* [Dapper](https://github.com/StackExchange/Dapper) - Lightweight ORM
* [Dapper.Contrib](https://github.com/StackExchange/Dapper) - ORM Extensions
* [FastMember](https://github.com/mgravell/fast-member) - Replacement for .NET Reflection
* [Newtonsoft.Json](https://www.newtonsoft.com/json) - JSON framework
* [NuPack](https://github.com/Virtuoze/NuPack) - Generate NuGet Packages on build
* [SimpleInjector](https://simpleinjector.org/index.html) - Dependency Management

## Authors

* **Kevin Suarez** - [ksuarez2](https://github.com/ksuarez2)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Many thanks to my friend Joseoh (https://github.com/TheMofaDe) who has helped me with design patterns
