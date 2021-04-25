FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY bin/Release/net5.0/ .
HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail http://localhost || exit 1
ENTRYPOINT ["dotnet", "Timer.dll"]