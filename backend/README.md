## Registry system backend


## EF Core migrations

Install tooling

~~~bash
dotnet tool update -g dotnet-ef
dotnet tool update -g dotnet-aspnet-codegenerator
~~~

Run from solution folder

~~~bash
dotnet ef migrations --project App.DAL.EF --startup-project backend add initialmigration --context AppDbContext
dotnet ef database   --project App.DAL.EF --startup-project backend update --context AppDbContext
~~~
~~~bash
dotnet ef database   --project App.DAL.EF --startup-project backend drop --context AppDbContext
~~~

## Generate admin controllers
~~~bash
cd backend

dotnet aspnet-codegenerator controller -name ActivityEventController -actions -m App.Domain.ActivityEvent -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MinutesAddedController -actions -m App.Domain.MinutesAdded -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ParticipantEventController -actions -m App.Domain.ParticipantEvent -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RaffleItemController -actions -m App.Domain.RaffleItem -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

cd ..
~~~

## Generate api controllers
~~~bash
cd backend

dotnet aspnet-codegenerator controller -name ActivityEventController -async -api -m App.Domain.ActivityEvent -dc AppDbContext --relativeFolderPath Areas/Api/Controllers
dotnet aspnet-codegenerator controller -name MinutesAddedController -async -api -m App.Domain.MinutesAdded -dc AppDbContext --relativeFolderPath Areas/Api/Controllers
dotnet aspnet-codegenerator controller -name ParticipantEventController -async -api -m App.Domain.ParticipantEvent -dc AppDbContext --relativeFolderPath Areas/Api/Controllers
dotnet aspnet-codegenerator controller -name RaffleItemController -async -api -m App.Domain.RaffleItem -dc AppDbContext --relativeFolderPath Areas/Api/Controllers

cd ..
~~~