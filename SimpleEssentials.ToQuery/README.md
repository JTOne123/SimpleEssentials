 # ToQuery
 A small, lightweight library to convert function expressions into a parameterized database query string. 
 
 ## Supported Databases
 * MSSQL
 * MySqL
 * More To Come
 
 ## Usage
 To use the library, use the appropriate expression converter for your database.
 
 ### Expresison Converters
 * ExpToMsSql
 * ExpToMySql
 
 #### Expression Converter Methods
 * Select
 * SelectTop
 * Where
 * InnerJoinOn
 * LeftJoinOn
 * On
 * Limit
 
 ### Simple Use
 ```C#
 var queryObject = new ExpToMsSql().Select<CustomCampaign>().Where<CustomCampaign>(x => x.Id == 2).Generate();
 var sqlQuery = queryObject.Query;

 //output of sqlQuery:  select * from [CustomCampaign] [CustomCampaign]  where [CustomCampaign].[Id] = 2
 ```
 
 ### Parameterized Use
 When passing in variables into the expression your query will be automatically parameterized.
 ```C#
var testVariable = 50;
var queryObject = new ExpToMsSql().Select<CustomCampaign>().Where<CustomCampaign>(x => x.Id == testVariable).Generate();
var sqlQuery = queryObject.Query;
var sqlParameters = queryObject.Parameters;

 //output of sqlQuery:   select * from [CustomCampaign] [CustomCampaign]  where [CustomCampaign].[Id] = @1
 //output of sqlParameters: [1, 50]
 ```
 sqlQuery will now be a parameterized query string and sqlParameters is a dictionary with the key being the parameter name and the value as the variable originally passed in
 
 
 
 
