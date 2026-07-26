# NANDCommand
Additional commands for App24's Sailwind Console Mod
## Settings
- Patch Port teleport
  - fix SailwindConsole's Teleport command so it puts you on the ground
## How to use this ReadMe
- less than/greater than brackets '<' and '>' mean the argument is required
- parentheses '(' and ')' mean the words inside them are interchangable options e.g. "island" or "boat"
- square brackets '[' and ']' mean an argument is optional e.g. boat index for teleports; if it's not supplied, a default will be assumed
- "int" means it requires a whole number e.g. 10, not 10.4
- "float" means it can handle more precision
- flags are an optional argument, used to change the behavior of commands that accept them
- "vanilla boat name" means the community's short names: cog, dhow, sanbuq, etc
- port names are the full name seen in the mission list, complete with spaces
- options in quotes are "pick one" options and are used without the quotes
## Commands
### BringToShipyard
- usage: BringToShipyard [boat name or index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest shipyard (if loaded). Will set boat as owned
- alias: BTS
### BringToPort 
- usage: BringToPort [boat name or index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest recovery port (if loaded). Will set boat as owned
- alias: BTP
### MoveBoat
- usage: MoveBoat \<lat> \<long> [boat name or index] [flag]
- Teleport a boat (or current/last boat if unspecified) to lat/long.
- flags: -y (also teleport all owned boats within 100m of the primary boat)
### MovePlayer
- usage: MovePlayer \<lat> \<long>
- teleport to globe coords
### TeleportTo
- usage: TeleportTo \<target type ("island", "boat", "object", "port")> \<target (island index, port name, boat index or boat name)>
- boat names for vanilla boats, others must be referenced by index
- useful for teleporting to islands with no port
- when using "object", only accepts index, not name.
  - can target boats, anchors, mooring ropes, storms
- alias: TpTo
### SetTimeScale
- usage: SetTimeScale [multiplier] [flag]
- sets day/night time scale.
- if unspecified, resets to default (0.008 aka 28.8)
- flags:
  - \-r (treat multiplier as the ratio of game time to real)
  - \-p (treat multiplier as a percentage of default)
### ExportInfo
- usage: ExportInfo \<item type ("parts", "boats", "food")> [scene index or vanilla boat name]
- exports item info and indexes
  - "parts" exports boat part info. Expects a boat (if unspecified it will do all of them)
  - "objects" exports all occupied indexes in `SaveLoadManager.currentObjects` (boats, mooring ropes, npc boats, storms)
  - "food" exports food items info (name, mass, energy, etc.)
  - "items" exports all items info (index, prefab name, item name, mass,value, description, category)
  - "islands" exports index, name, lat, long, port index, port name, has carrier, has currency exchange, main import, main export
  - "boats" exports index, object name, price, base mass, water capacity, water intake rate, water drain rate, durability days, impact multiplier, wear steepness, impact threshold
### CheatSpeed
- usage: CheatSpeed [speed]
- if speed is unspecified, resets to 0
- if set to a positive number, enables holding W/S while using a boat's steering wheel to push forward/back.
### SetWindKnots
- usage: SetWindKnots \<knots>
- sets the wind speed to the specified speed
### SetWeather
- usage: SetWeather <weather ("clear", "cloudy", "rain", "storm")> [seconds]
- force the weather for the specified seconds, or 10 if unspecified
### GetDistance
- usage: GetDistance <target type ("island", "boat", "port")> <target (island index, boat index or vanilla boat name, port name)>
- gets the distance from the player to the specified target
- alias: GetDist
### FixMe
- usage: FixMe [distance]
- teleports the player upward to hopefully get out of sticky situations
- if distance is omitted, defaults to 5m
### BringToMe
- usage: BringToMe [index or vanilla boat name]
- teleport a boat (or current/last boat if unspecified) to you. Will set boat as owned. DO NOT USE WHILE ON LAND!
- alias: BTM
### CookFood
- cook currently held food item
### SmokeFood
- smoke currently held food item
### GetDamage
 - usage: GetDamage [index or vanilla boat name]
 - returns the current damage percentage of the targeted boat (or current/last boat if unspecified)
### SetDamage
- usage: SetDamage [index or vanilla boat name] \<damage percent>
- set the current damage percentage of the targeted boat (or current/last boat if unspecified)
### GetWater
- usage: GetWater [index or vanilla boat name]
- returns the current water level and capacity of the targeted boat.
- alias: getBilge
### SetWater
- usage: SetWater [index or vanilla boat name] \<water level>
- set the bilge water level of the targeted boat (or current/last boat if unspecified). Accepts "units" or percentage ("setWater 20" will assume units, "setWater 20%" will be percentage).
- alias: setBilge
### ToggleDamage
- enable/disable damage and wear.
### SetOwned
- usage: SetOwned [index or vanilla boat name] [true, false]
- change the owned status of the targeted boat (or current/last boat if unspecified)
- if true/false is omitted, assumes true
### List
- usage: List \<type (boat, island)>
- lists all present objects of the specified types and their IDs
### AddMoney
- usage: AddMoney \<region (0, 1, 2)> \<amount>
- add or remove money
- works like AddGold, but can also accept negative numbers
### CleanBoat
- cleans the targeted boat (or current/last boat if unspecified)
### SetLevel
- usage SetLevel \<region(0, 1, 2)> \<level>
- Set reputation level for a specific region
### SpawnItem
- usage: SpawnItem \<int id>
- item IDs can be seen with the List command
### GetHeading
- usage: GetHeading <target type (island, boat, port)> <target (island index, boat index or vanilla boat name, port name)>
- returns the degree heading from player to target
- alias: getAngle
### SetWalkSpeed
- usage: SetWalkSpeed \<float speed>
- Set player's walking speed multiplier. 1 is default
- aliases: "setMoveSpeed", "sws", "sms"
### List
- usage: List <type("boats", "islands", "items")>
- returns the names and IDs of all objects of the specified type
