# NANDCommand

## Bring To Shipyard 
- usage: BringToShipyard [boat name or index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest shipyard (if loaded). Will set boat as owned
- alias: BTS
## Bring To Port 
- usage: BringToPort [boat name or index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest recovery port (if loaded). Will set boat as owned
- alias: BTP
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
## Cheat Speed
- usage: CheatSpeed [speed]
- if speed is unspecified, resets to 0
- if set to a positive number, enables holding W/S while using a boat's steering wheel to push forward/back.
## SetWindKnots
- usage: SetWindKnots <wind speed (int knots)>
- sets the wind speed to the specified speed
## SetWeather
- usage: SetWeather <weather (clear, cloudy, rain, storm)> [seconds]
- force the weather for the specified seconds, or 10 if unspecified
## GetDistance
- usage: GetDistance <target type (island, boat, port)> <target (island index, boat index or vanilla boat name, port name)>
- gets the distance from the player to the specified target
- alias: GetDist
## FixMe
- teleports the player 1m upward

## settings
- Patch Port teleport
  - fix SailwindConsole's Teleport command so it puts you on the ground
