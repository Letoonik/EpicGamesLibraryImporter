# EpicGamesImportTool
[![.NET](https://github.com/Letoonik/EpicGamesImportTool/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Letoonik/EpicGamesImportTool/actions/workflows/dotnet.yml) ![GitHub all releases](https://img.shields.io/github/downloads/Letoonik/EpicGamesImportTool/total?color=34cf58)

A tool that allows to import previously installed games, mostly useful when they disappeared from the library after a reinstall.

1. Pick a folder with "Select folder" and click "Scan" to search for Epic Game entries in all subfolders.
2. Select all games (Ctrl + A) and click "Import selected" to add all found games to your Epic Games Launcher library.

When you open the Epic Game Launcher afterwards and go into your library, it will take a little while to scan, validate, and fix missing metadata for every game you just added.

Note: Probably doesn't work for Unreal Engine Installations (at least it didnt work for me).

## This repo was forked from https://github.com/FFouetil/EpicGamesImportTool. It was a little broken, so I fixed it (by randomly adding some imports in the C# files) and provided propper binaries in a zip file. All credit goes to FFouetil.
![-](https://raw.githubusercontent.com/Letoonik/EpicGamesImportTool/main/.github/workflows/pic.png)
