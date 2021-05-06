FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY bin/Release/net5.0/ .
RUN apt-get update && apt-get install -y curl
HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail http://localhost || exit 1
ENTRYPOINT ["dotnet", "Timer.dll"]