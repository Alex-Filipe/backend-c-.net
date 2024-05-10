FROM mcr.microsoft.com/dotnet/sdk:8.0.202

COPY . /backend

WORKDIR /backend

RUN dotnet build
EXPOSE 5086
ENTRYPOINT [ "dotnet", "run" ]