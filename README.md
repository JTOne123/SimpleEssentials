# SimpleEssentials

Simple library that wraps other useful libraries. It's purpose is to speed up development time.
### Features
* Lightweight ORM
* Caching
* IO File Management
* Logging

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


### Methods
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


### Insert

Inserts a single object into the database. Will cache results if cacheSettings are provided.

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

### InsertList

Inserts a list into the database. Will cache results if cacheSettings are provided.

```C#
int InsertList<T>(IEnumerable<T> data, CacheSettings cacheSettings = null);
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

### InsertAndReturnId
Insert a single oject into the database and return the new rows ID. Will cache results if cacheSettings are provided.

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

### BulkInsert
Currently has issues, will document once fixed

### Update
Update a single record in the database and cache if cacheSettings are provided

```C#
bool Update<T>(T data, CacheSettings cacheSettings = null)
```
```C#
var customCampaign = dbProvider.Get<CustomCampaign>(1);
customCampaign.Description = "Updated Description";
var success = dbProvider.Update(customCampaign);
```
returns true is successful, false if not.

### Delete
Delete a single record in the database and cache if cacheSettings are provided

```C#
bool Delete<T>(T data, CacheSettings cacheSettings = null, string fieldKey = null)
```
```C#
var customCampaign = dbProvider.Get<CustomCampaign>(1);
var success = dbProvider.Delete(customCampaign);
```
returns true is successful, false if not.

### Execute
Execute a SQL statement. Will cache results if cacheSettings are provided.

```C#
int Execute(string sql, object param, CacheSettings cacheSettings = null, bool invalidateCache = false)
```
```C#
//TODO
```
returns rows affected

### ExecuteScalar
Execute a scalar SQL statement. Will cache results if cacheSettings are provided.

```C#
int ExecuteScalar(string sql, object param, CacheSettings cacheSettings = null, bool invalidateCache = false)
```
```C#
//TODO
```
returns rows affected

### Get
Gets a list of records from the database by the expression given. Will cache results if cacheSettings are provided.

```C#
IEnumerable<T> Get<T>(Expression<Func<T, bool>> expression, CacheSettings cacheSettings = null)
```
```C#
var campaigns = dbProvider.Get<CustomCampaign>(x => x.CreateDate >= DateTime.Now.AddDays(-5));
```
returns a list of objects that meets the expression. 

### Get
Gets a single record from the database by id and maps it to a strongly typed object. Will cache results if cacheSettings are provided.

```C#
T Get<T>(object id, CacheSettings cacheSettings = null)
```
```C#
var customCampaign = dbProvider.Get<CustomCampaign>(1);
```
returns strongly typed object from the data. Null if it was not found.

### GetByType
Gets all records in a table.

```C#
IEnumerable<T> GetByType<T>(CacheSettings cacheSettings = null)
```
```C#
var campaings = dbProvider.GetByType<CustomCampaign>();
```
returns an IEnumerable of strongly typed objects.

## File Handling
### Methods
* Create
* Get
* Write
* Read
* ReadAll


### Create
File
```C#
var file = Factory.FileHandler.Create("test.txt");
```
Folder
```C#
var folder = Factory.FolderHandler.Create("test");
```

### Get
File
```C#
var file = Factory.FileHandler.Get("test.txt");
```
Folder
```C#
var folder = Factory.FolderHandler.Get("test");
```

### Write
[To be added]

### Read
[To be added]

## Built With

* [CsvHelper](https://github.com/JoshClose/CsvHelper) - CSV File Management
* [Dapper](https://github.com/StackExchange/Dapper) - Lightweight ORM
* [Dapper.Contrib](https://github.com/StackExchange/Dapper) - ORM Extensions
* [FastMember](https://github.com/mgravell/fast-member) - Faster Reflection
* [Newtonsoft.Json](https://www.newtonsoft.com/json) - JSON framework
* [SimpleInjector](https://simpleinjector.org/index.html) - Dependency Management

## Authors

* **Kevin Suarez** - [ksuarez2](https://github.com/ksuarez2)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Many thanks to my friend Joseph (https://github.com/TheMofaDe) who has helped me with design patterns
