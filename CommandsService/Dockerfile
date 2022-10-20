
# pulling dotnet sdk image from docker hub
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

# set working directory to app
WORKDIR /app

# copy csproj - dotnet restore is not required (called implicitly on dotnet build)
COPY *.csproj ./
RUN dotnet restore

# copy everything and build
COPY . ./
# build the project using the release config and copy it inside out folder
RUN dotnet publish -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "CommandsService.dll" ]