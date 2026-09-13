# Character Select

Pick your player appearance in Sons of the Forest and keep it across sessions.

The game assigns one of eight player appearances at random when you join a server,
and only remembers it if the world gets saved. This mod lets you choose, and
reapplies your choice every time you spawn.

## Notes

Client side only. Nothing needs to be installed on a dedicated server.

Your appearance replicates through normal game networking, so other players see
your choice even if they do not have the mod installed. They will not be able to
change their own appearance without it.

## Requirements

RedLoader

## Installation

1. Download the latest release
2. Extract into your Sons of the Forest folder so you end up with:
   - `Mods/CharacterSelect.dll`
   - `Mods/CharacterSelect/manifest.json`

## Usage

Press F9 to cycle through the eight appearances. Your choice is saved to
`_RedLoader/UserData/CharacterSelect.txt` and applied automatically on every spawn.

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

Requires the .NET 8 SDK and a RedLoader install that has been launched once, so
the interop assemblies exist. The script offers to install both if they are missing.
