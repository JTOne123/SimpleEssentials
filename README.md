# SimpleEssentials

Simple library that wraps other useful libraries. It's purpose is to speed up your development time.

## DbDataProvider

Gives you an easy way to map SQL data to strongly typed objects. Also provides easy cache management if a cache is necessary.

### Initialization

DbDataProvider can be initialized two ways:
* Pass IDataStore and ICacheManager instances
```C#
var dbProvider = new DbDataProvider(new DbStore("[CONNECTION_STRING]"), new MemoryCacheManager());
```

* Inject the dependencies
```C#
//Somewhere in App_Start
Factory.Container.Register<IDataStore>(() => new DbStore("[CONNECTION_STRING]"));
Factory.Container.Register<ICacheManager>(() => new MemoryCacheManager());
Factory.Container.Verify();

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
bool Insert<T>(T data, CacheSettings cacheSettings = null)
```
```C#
var campaign = new CustomCampaign()
  {
      Description = "Just a test campaign",
      Name = "Github"
  };
var success = dbProvider.Insert(campaign);

```
returns true if successful, false if not.

#### InsertList

Inserts a list into the database.

```C#
int InsertList<T>(IEnumerable<T> data, string sql, CacheSettings cacheSettings = null);
```
```C#
var campaigns = new List<CustomCampaign>()
  {
      new CustomCampaign() {Description = "Desc1", Name = "Test1"},
      new CustomCampaign() {Description = "Desc2", Name = "Test2"},
      new CustomCampaign() {Description = "Desc3", Name = "Test3"},
  };
var rowsAffected = dbProvider.InsertList(campaigns);
```
returns records inserted.

#### InsertAndReturnId
Insert a single oject into the database and return the new rows ID.

```C#
int InsertAndReturnId<T>(T data, CacheSettings cacheSettings = null)
```
```C#
var campaign = new CustomCampaign()
  {
      Description = "Just a test campaign",
      Name = "Github"
  };
var returnId = dbProvider.InsertAndReturnId(campaign);

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

* Many thanks to my friend Joseph (https://github.com/TheMofaDe) who has helped me with design patterns
