# NikiforovAll.CopyPaster

[![Build](https://github.com/NikiforovAll/copy-paster/actions/workflows/build.yml/badge.svg)](https://github.com/NikiforovAll/copy-paster/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/dt/NikiforovAll.CopyPaster.svg)](https://nuget.org/packages/NikiforovAll.CopyPaster)
[![NuGet](https://img.shields.io/nuget/v/NikiforovAll.CopyPaster.svg)](https://www.nuget.org/packages/NikiforovAll.CopyPaster/)
[![NuGet](https://img.shields.io/nuget/vpre/NikiforovAll.CopyPaster.svg)](https://www.nuget.org/packages/NikiforovAll.CopyPaster/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/nikiforovall/copy-paster/blob/main/LICENSE)
[![contributionswelcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/nikiforovall/copy-paster)

Download code and saves it to local file system.

Same as `wget https://URL/ --output-document=FILE_NAME`, but you don't have to pass a link to raw version. It rewrites "https://github.com" to "https://raw.githubusercontent.com/" automatically for you.

## Install

`dotnet tool install --global NikiforovAll.CopyPaster`

## Run

```bash
copa https://github.com/NikiforovAll/copy-paster/blob/main/README.md
# creates README.md in current folder
```

## More details

```bash
$ dotnet run -- -h
Description:
  Copies file by url

Usage:
  NikiforovAll.CopyPaster <url> [options]

Arguments:
  <url>  url

Options:
  -o, --output <output>  Output file path
  --dry-run              Dry run
  --raw                  Raw. No panel
  --version              Show version information
  -?, -h, --help         Show help and usage information
```

## TODO

* [X] rewrite "github.com" -> https://raw.githubusercontent.com/
* [] consider: format code according to project styles (namespace refactoring, formatting)
* [] handle folder download
* [] port to npx
* [] cross-platform path handling