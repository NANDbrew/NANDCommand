# NANDCommand

## Bring To Shipyard 
- usage: BringToShipyard [boat name or index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest shipyard (if loaded). Will set boat as owned
- alias: BTS
## Move Boat
- usage: MoveBoat \<lat> \<long> [boat name or index] [flag]
- Teleport a boat (or current/last boat if unspecified) to lat/long.
- flags: -y (also teleport all owned boats within 100m of the primary boat)
## Move Player
- usage: MovePlayer \<lat> \<long>
- teleport to globe coords
## Teleport To
- usage: TeleportTo \<target type (island, boat)> \<target (island index, boat index or boat name)>
- boat names for vanilla boats, others must be referenced by index
- useful for teleporting to islands with no port
- alias: TpTo
## Set Time Scale
- usage: SetTimeScale [multiplier] [flag]
- sets day/night time scale.
- if unspecified, resets to default (0.008 aka 28.8)
- flags:
  - \-r (treat multiplier as the ratio of game time to real)
  - \-p (treat multiplier as a percentage of default) *upcoming*
## Export Info
- usage: ExportInfo \<item type (parts, boats, food)> [scene index or vanilla boat name]
- exports item info and indexes
  - "parts" exports boat part info. Expects a boat (if unspecified it will do all of them)
  - "objects" exports all occupied indexes in `SaveLoadManager.currentObjects` (boats, mooring ropes, npc boats, storms)
  - "food" exports food items info (name, mass, energy, etc.)
  - "items" exports all items info (name, mass, value)
## settings
- Patch Port teleport
  - fix SailwindConsole's Teleport command so it puts you on the ground
