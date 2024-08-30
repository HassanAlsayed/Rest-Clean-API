FROM mcr.microsoft.com/dotnet/sdk:8.0 As build

WORKDIR /app

COPY "HR.LeaveManagmant.Clean.sln" .
COPY "HR.LeaveManagmant.Api/HR.LeaveManagmant.Api.csproj" "HR.LeaveManagmant.Api/"
COPY "HR.LeaveManagmant.Application/HR.LeaveManagmant.Application.csproj" "HR.LeaveManagmant.Application/"
COPY "HR.LeaveManagmant.Application.IntegrationTest/HR.LeaveManagmant.Application.IntegrationTest.csproj" "HR.LeaveManagmant.Application.IntegrationTest/"
COPY "HR.LeaveManagmant.Application.UnitTest/HR.LeaveManagmant.Application.UnitTest.csproj" "HR.LeaveManagmant.Application.UnitTest/"
COPY "HR.LeaveManagmant.Domain/HR.LeaveManagmant.Domain.csproj" "HR.LeaveManagmant.Domain/"
COPY "HR.LeaveManagmant.Persistence/HR.LeaveManagmant.Persistence.csproj" "HR.LeaveManagmant.Persistence/"

RUN dotnet restore "HR.LeaveManagmant.Clean.sln"

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "HR.LeaveManagmant.Api.dll"]

