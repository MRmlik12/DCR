FROM mcr.microsoft.com/dotnet/sdk:5.0.400-focal-amd64

RUN apt-get update && apt-get install -y libleptonica-dev libtesseract-dev libc6-dev libjpeg62-dev libgdiplus

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

RUN ln -s /usr/lib/x86_64-linux-gnu/liblept.so.5 x64/liblept.so.5
RUN ln -s /usr/lib/x86_64-linux-gnu/liblept.so.5 x64/libleptonica-1.80.0.so

ENV DISCORD_TOKEN TOKEN
ENV PREFIX !

ENTRYPOINT [ "dotnet", "Dcr.dll" ]
