FROM mcr.microsoft.com/dotnet/sdk:6.0-focal-amd64 AS build

WORKDIR /src

COPY ./src .

RUN dotnet build -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:6.0-focal-amd64

WORKDIR /app

COPY --from=build app .

RUN apt-get update && apt-get install -y libleptonica-dev libtesseract-dev libc6-dev libjpeg62-dev libgdiplus
RUN ln -s /usr/lib/x86_64-linux-gnu/liblept.so.5 x64/liblept.so.5
# RUN ln -s /usr/lib/x86_64-linux-gnu/libleptonica.so x64/libleptonica-1.80.0.so

ENV DISCORD_TOKEN TOKEN
ENV INSTALL_TESSDATA true
ENV PREFIX !

ENTRYPOINT [ "dotnet", "Dcr.dll" ]
