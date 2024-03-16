FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN apk add libc6-compat
RUN apk add libcap
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG FEED_URL
ARG PAT
WORKDIR /src
# download and install latest credential provider
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | sh
# Environment variable to enable seesion token cache
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
# Environment variable for adding endpoint credentials
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS {\"endpointCredentials\": [{\"endpoint\":\"${FEED_URL}\", \"username\":\"ArtifactsDocker\", \"password\":\"${PAT}\"}]}
COPY . .
RUN dotnet restore "src/EmployeesAPI.WebApi/EmployeesAPI.WebApi.csproj"
WORKDIR "/src/src/EmployeesAPI.WebApi"
RUN dotnet build "EmployeesAPI.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeesAPI.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
ENV COMPlus_EnableDiagnostics=0
WORKDIR /app
COPY --from=publish /app/publish .
RUN chgrp -R 0 /app && \
    chmod -R g=u /app
RUN setcap 'cap_net_bind_service+ep' /usr/share/dotnet/dotnet  
USER 1001
ENTRYPOINT ["dotnet", "EmployeesAPI.WebApi.dll"]
