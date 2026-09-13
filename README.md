# Character Select

Choose your player appearance in Sons of the Forest and keep it across sessions.

Sons of the Forest gives you a random appearance out of eight every time you join
a server, and there is no way to choose. This mod lets you pick the one you want
and puts it back on every spawn, so you stay the same character.

## Multiplayer

Client side only. Nothing gets installed on a dedicated server.

Your appearance replicates through normal game networking, so other players see
your choice even if they do not have the mod installed.

## Requirements

[RedLoader](https://github.com/ToniMacaroni/RedLoader/releases/latest)

## Installation

1. Download `CharacterSelect.zip` from the
   [latest release](https://github.com/JustinOros/sotf-character-select/releases/latest)
2. Open the `Mods` folder inside your Sons of the Forest install, usually
   `C:\Program Files (x86)\Steam\steamapps\common\Sons Of The Forest\Mods`
3. Extract the contents of the zip into that `Mods` folder

When you are done it should look like this:

```
Sons Of The Forest/
  Mods/
    CharacterSelect.dll
    CharacterSelect/
      manifest.json
```

The `Mods` folder is created by RedLoader, so install RedLoader first if it is
not there.

## Usage

Once you are in the world, press F9 to cycle through the eight appearances. The
current one is shown on screen as you cycle.

Your choice is saved to `_RedLoader/UserData/CharacterSelect.txt` and applied
automatically every time you spawn.

| Value | Name |
| --- | --- |
| 0 | White |
| 1 | Black |
| 2 | Latin |
| 3 | Asian |
| 4 | BlackA |
| 5 | WhiteA |
| 6 | BlackB |
| 7 | LatinA |

## Building

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and a
[RedLoader](https://github.com/ToniMacaroni/RedLoader/releases/latest) install
that has been launched at least once, so the interop assemblies exist. The build
script offers to install both if they are missing.

```
.\build.ps1 -Install
```

The game folder is found through Steam. Override it with `-GameDir "path"` or the
`SOTF_PATH` environment variable.

To produce a release zip:

```
.\build.ps1 -Package
```

## Credits

Built against RedLoader by Toni Macaroni. The player race system was found by
reading the source of ArmorMod by clairenheit.

