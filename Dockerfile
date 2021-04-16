FROM mcr.microsoft.com/dotnet/sdk:5.0.202-alpine3.13-amd64

RUN apk add libc-dev 

WORKDIR /src

COPY src/*.sln .
COPY src/Dcr/. ./Dcr/
COPY src/Dcr.CommandHandler/. ./Dcr.CommandHandler/
COPY src/Dcr.Config/. ./Dcr.Config/
COPY src/Dcr.Services/. ./Dcr.Services/
COPY src/Dcr.Utils/. ./Dcr.Utils/
COPY src/Dcr.Tests/. ./Dcr.Tests/

RUN dotnet build -c Release -o /app

WORKDIR /app

ENV DISCORD_TOKEN=TOKEN
ENV PREFIX=!

ENTRYPOINT [ "dotnet", "Dcr.dll" ]
