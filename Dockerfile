FROM mcr.microsoft.com/dotnet/sdk AS build

WORKDIR /src

COPY src/*.sln .
COPY src/Dcr/*.csproj ./Dcr/
COPY src/Dcr.CommandHandler/*.csproj ./Dcr.CommandHandler/
COPY src/Dcr.Config/*.csproj ./Dcr.Config/
COPY src/Dcr.Services/*.csproj ./Dcr.Services/
COPY src/Dcr.Utils/*.csproj ./Dcr.Utils/
COPY src/Dcr.Tests/*.csproj ./Dcr.Tests/

RUN dotnet restore

COPY src/Dcr/. ./Dcr/
COPY src/Dcr.CommandHandler/. ./Dcr.CommandHandler/
COPY src/Dcr.Config/. ./Dcr.Config/
COPY src/Dcr.Services/. ./Dcr.Services/
COPY src/Dcr.Utils/. ./Dcr.Utils/
COPY src/Dcr.Tests/. ./Dcr.Tests/

RUN dotnet build -c Release -o /app

WORKDIR /app

RUN apt-get update && apt-get install -y tesseract-ocr && apt-get install -y libleptonica-dev

ENV DISCORD_TOKEN=TOKEN
ENV PREFIX=!

ENTRYPOINT [ "dotnet", "Dcr.dll" ]
