## EF commands (from solution folder)
dotnet ef migrations add NameOfUpdate --project LibraryAssignment.Data --startup-project LibraryAssignment.MinimalApi  
dotnet ef database update --project LibraryAssignment.Data --startup-project LibraryAssignment.MinimalApi

Remove last migration
dotnet ef migrations remove --project LibraryAssignment.Data --startup-project LibraryAssignment.MinimalApi
