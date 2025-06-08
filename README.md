# NANDCommand

## Bring To Shipyard 
- usage: BringToShipyard [boat name or scene index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest shipyard (if loaded). Will set boat as owned
- alias: BTS
## Move Boat
- usage: MoveBoat \<lat> \<long> [boat name or scene index] [flags]
- Teleport a boat (or current/last boat if unspecified) to lat/long.
- flags: -y (also teleport all owned boats within 100m of the primary boat)
## Move Player
- usage: MovePlayer \<lat> \<long>
- teleport to globe coords
## Teleport To
- usage: TeleportTo \<island index>
- useful for teleporting to islands with no port
- alias: TpTo
## Set Time Scale
- usage: SetTimeScale [multiplier] [flags]
- sets day/night time scale.
- if unspecified, resets to default (0.008)
- flags: -r (treat multiplier as the ratio of game time to real e.g. 28.8 is default)
## Export Info
- usage: ExportInfo \<item type (parts, boats, food)> [scene index or vanilla boat name]
- exports item info and indexes
  - "parts" exports boat part info. Expects a boat (if unspecified it will do all of them)
  - "objects" exports all occupied indexes in `SaveLoadManager.currentObjects` (boats, mooring ropes, npc boats, storms)
  - "food" exports food items info (name, mass, energy, etc.)
  - "items" exports all items info (name, mass, value)
