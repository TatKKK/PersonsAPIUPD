## **1. Database Commands**

dotnet ef database drop --force --project PersonsDAL --startup-project WebApi

dotnet ef migrations add TestCreate --project PersonsDAL --startup-project WebApi

dotnet ef database update --project PersonsDAL --startup-project WebApi

