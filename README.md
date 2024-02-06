Entity Framework Performans Karşılaştırılması Yapıldı.
Proje .Net Core Minimal Api  Mimarisinde geliştirdi.
Ek olarak dapper ile de performans testi yapıldı.

#Projede Kullanılan Paketler
Dapper : 2.1.28
Microsoft.EntityFrameworkCore.SqlServer : 6.0.26
Microsoft.EntityFrameworkCore.Tools : 6.0.26

veritabanı işlemleri
Connection String : "Data Source=(localdb)\\mssqllocaldb; Database=DbEfCorePerformance; Trusted_Connection=true;"
PM > Add-Migration Add_Company_And_Employee
PM > Update-database

#EndPoints (Put)
https://localhost:7296/increase-salaries?companyId=1
https://localhost:7296/increase-salaries-sql?companyId=1
https://localhost:7296/increase-salaries-sql-dapper?companyId=1
