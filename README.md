# APIExampleBillExchange
An API that receives a payment from an user, the amount due and then returns the change. It returns the minimum amount of bills and coins in order to give the correct change.

Application environment
```
-Microsoft Windows (in my case, Windows 11 Pro)

-Visual Studio Community 2022 (System minimun requirements are needed as well)

-Entity Framework
  
-ORM Dapper

-.NET 6.0

-SQL Server Management Studio 2019 (19.0.2)

-Swagger
```

Needed for testing purposes

```
-In order to work correctly, please change the Data Source in the appsettings.json file when going to run the application. To do so, use the information provided by SQL Server Management Studio.

-The SQL Database script is the 'MoneyExchangeScript' file. It has the database settings needed for testing and some data.
```

## IMPORTANT
I used Dapper for making queries to the Database but I wasn't sure if that was what the exercise asked for. So, **I added in every DA (Data Acces) class some regions: Dapper and Entity Framework Core.** All the regions associated with Entity Framework are commented so as the Dapper code work but feel free to uncomment the Entity Framework regions to check them out. Both codes do the same! But bear in mind that you should comment one set of regions and uncomment the other one so as to make the necessary queries to the DB just once. If not, there could be inconsistencies.



Feel free to check it out it!
