Entity Framework Performans Karşılaştırılması Yapıldı.
<br/>
Proje .Net Core Minimal Api  Mimarisinde geliştirdi.
<br/>
Ek olarak dapper ile de performans testi yapıldı.
<br/>
#Projede Kullanılan Paketler<br/>
Dapper : 2.1.28<br/>
Microsoft.EntityFrameworkCore.SqlServer : 6.0.26<br/>
Microsoft.EntityFrameworkCore.Tools : 6.0.26<br/>

veritabanı işlemleri<br/>
Connection String : "Data Source=(localdb)\\mssqllocaldb; Database=DbEfCorePerformance; Trusted_Connection=true;"<br/>
PM > Add-Migration Add_Company_And_Employee<br/>
PM > Update-database<br/>

#EndPoints (Put)<br/>
https://localhost:7296/increase-salaries?companyId=1<br/>
https://localhost:7296/increase-salaries-sql?companyId=1<br/>
https://localhost:7296/increase-salaries-sql-dapper?companyId=1<br/>
