# DCR 
![example workflow](https://github.com/MRmlik12/DCR/actions/workflows/build.yml/badge.svg)

Discord bot for reading texts from images

## Commands

prefix used default is '!'

* help - redirect to github Commands section
* read - reads text from image and send response to same channel
* ping - sends information about latency from bot
* about - show version, authors and github repository link

## OCR Supported languages

* English (for now)

## For Contributors

If you want to contribute here you must follow the rules:
* branch naming:
    * `feature/example-desc-of-feature` - feature
    * `fix/example-desc-of-fix` - fix
* don't create pr from your branch main (fork)

## Building from source
You must have installed .NET 5.0 on your computer to build project

```bash
    $ dotnet build src -o ./out
    // When build ends up
    $ cd out
    // Before launching bot you must setup configuration.json or envoirements variables DISCORD_TOKEN & PREFIX
    $ dotnet Dcr.dll
```

### Building with docker
```bash
    $ docker build -t mrmlik12/dcr .
    $ docker run mrmlik12/dcr -e DISCORD_TOKEN=TOKEN -e PREFIX=!
```