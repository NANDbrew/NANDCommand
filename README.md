# NANDCommand
Additional commands for App24's Sailwind Console Mod
## Settings
- Patch Port teleport
  - fix SailwindConsole's Teleport command so it puts you on the ground
## How to use this ReadMe
- less than/greater than signs '<' and '>' mean the argument is required
- parentheses '(' and ')' mean the words inside them are interchangable options e.g. "island" or "boat"
- quotation marks, brackets, etc. are not part of the command. They're just here to tell you what they are
- square brackets '[' and ']' mean an argument is optional e.g. boat index for teleports; if it's not supplied, the default will be used
- "int" means it requires a whole number e.g. 10, not 10.4
- "float" means it can handle more precision
- flags are an optional argument, used to change the behavior of commands that accept them
- "boat name" means the community's short names for the base game boats: cog, dhow, sanbuq, etc.
- port names are the full name seen in the mission list, complete with spaces
## Commands
### BringToShipyard
- usage: BringToShipyard [boat name or index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest shipyard (if loaded)
- alias: BTS
- example: `BrigToShipyard brig`
### BringToPort 
- usage: BringToPort [boat name or index]
- boat names for vanilla boats, others must be referenced by index
- teleport a boat (or current/last boat if unspecified) to the nearest recovery port
- alias: BTP
- example using HappyBayBoat mod: `BringToPort 144`
- example vanilla boat: `BringToPort junk`
### MoveBoat
- usage: MoveBoat \<float lat> \<float long> [boat name or index] [flag]
- Teleport a boat (or current/last boat if unspecified) to lat/long.
- flags: -y (also teleport all owned boats within 100m of the primary boat)
- example: `MoveBoat 32 4.5 sanbuq`
### TeleportTo
- usage: TeleportTo \<target type ("island", "boat", "object", "coords")> \<target (island index, boat index or boat name, lat long)>
- boat names for vanilla boats, others must be referenced by index
- useful for teleporting to islands with no port
- when using "object", only accepts index, not name.
  - can target boats, anchors, mooring ropes, storms
- alias: TpTo
- example: `TeleportTo island 15`
- example: `TeleportTo coords 40.34 0.63`
### SetTimeScale
- usage: SetTimeScale [float multiplier] [flag]
- sets day/night time scale.
- if unspecified, resets to default (0.008 aka 28.8)
- flags:
  - \-r (treat multiplier as the ratio of game time to real time)
  - \-p (treat multiplier as a percentage of default)
- example: `setTimeScale 20 -r`
  - result: 20 game minutes per real world minute
- example: `setTimeScale 200 -p`
  - result: game time moves twice normal speed aka 57.6 game minutes per real minute
### ExportInfo
- usage: ExportInfo \<item type ("parts", "boats", "food", "items", "islands")> [index or boat name]
- exports item info and indexes to a `.csv` file. On windows it will go in: "documents/sailwind info dump/", but it will say where it put it
  - "parts" exports boat part info. Expects a boat (if unspecified it will do all of them)
  - "objects" exports all occupied indexes in `SaveLoadManager.currentObjects` (boats, mooring ropes, npc boats, storms)
  - "food" exports food items info (name, mass, energy, etc.)
  - "items" exports all items info (index, prefab name, item name, mass,value, description, category)
  - "islands" exports index, name, lat, long, port index, port name, has carrier, has currency exchange, main import, main export
  - "boats" exports index, object name, price, base mass, water capacity, water intake rate, water drain rate, durability days, impact multiplier, wear steepness, impact threshold
### CheatSpeed
- usage: CheatSpeed [float speed]
- if speed is unspecified, resets to 0
- if set to a positive number, enables driving the boat forward/backward with W/S while holding a boat's steering wheel
### SetWindKnots
- usage: SetWindKnots \<float knots>
- sets the wind speed to the specified speed
- will go back to normal after a short time, due to the game's wind randomizing timer
### SetWeather
- usage: SetWeather <weather ("clear", "cloudy", "rain", "storm")> [seconds]
- force the weather for the specified seconds, or 10 if unspecified
- example: `SetWeather clear 30`
### GetDistance
- usage: GetDistance <target type ("island", "boat", "port")> <target (island index, boat index or boat name, port name)>
- gets the distance from the player to the specified target
- alias: GetDist
- example 1 targeting Gold Rock City: `getdist island 1`
- example 2 targeting Gold Rock City: `getdist port gold rock city`
### FixMe
- usage: FixMe [distance]
- teleports the player upward to hopefully get out of sticky situations
- if distance is not specified, defaults to 5m
### BringToMe
- usage: BringToMe [index or vanilla boat name]
- teleport a boat (or current/last boat if unspecified) to you. DO NOT USE WHILE ON LAND!
- defaults to current/last boat you were on if unspecified
- alias: BTM
### CookFood
- cook currently held food item
### SmokeFood
- smoke currently held food item
### GetDamage
 - usage: GetDamage [index or boat name]
 - returns the current damage percentage of the targeted boat (or current/last boat if unspecified)
- defaults to current/last boat you were on if unspecified
### SetDamage
- usage: SetDamage [index or vanilla boat name] \<damage percent>
- set the current damage percentage of the targeted boat (or current/last boat if unspecified)
- defaults to current/last boat you were on if unspecified
### GetWater
- usage: GetWater [index or boat name]
- returns the current water level and capacity of the targeted boat.
- defaults to current/last boat you were on if unspecified
- alias: getBilge
### SetWater
- usage: SetWater [index or boat name] \<water level>
- set the bilge water level of the targeted boat (or current/last boat if unspecified). Accepts "units" or percentage ("setWater 20" will assume units, "setWater 20%" will be percentage).
- alias: setBilge
- example percent: `setWater kakam 20%`
- example units: `setWater dhow 60`
### ToggleDamage
- enable/disable damage and wear globally
### SetOwned
- usage: SetOwned [index or boat name] [true, false]
- change the owned status of the targeted boat (or current/last boat if unspecified)
- if true/false is unspecified, assumes true
- example: `setowned jong`
### List
- usage: List \<type ("boats", "islands", "items", "ports")>
- lists all present objects of the specified types and their IDs
- example: `List boats`
### AddMoney
- usage: AddMoney \<region (0, 1, 2)> \<amount>
- add or remove money
- works like AddGold, but can also accept negative numbers
- example subtracting Crowns: `AddMoney 2 -50`
### CleanBoat
- cleans the targeted boat (or current/last boat if unspecified)
### SetLevel
- usage SetLevel \<region(0, 1, 2)> \<level>
- set reputation level for a specific region
- example level ten for Emerald: `SetLevel 1 10`
### SpawnItem
- usage: SpawnItem \<int id>
- item IDs can be seen with the List command
- example spawning a lantern hook: `SpawnItem 79`
### GetHeading
- usage: GetHeading <target type (island, boat, port)> <target (island index, boat index or boat name, port name)>
- returns the degree heading from player to target
- alias: getAngle
- example: `GetHeading boat sanbuq`
### SetWalkSpeed
- usage: SetWalkSpeed \<float speed>
- set player's walking speed multiplier. 1 is default
- aliases: "setMoveSpeed", "sws", "sms"
### GetPrice
- usage: GetPrice \<good name>
- get the price of a good at all ports
- prices are internal values, not any particular currency
- example: `GetPrice north fish`
### GetPriceReport
- usage: GetPriceReport \<port name>
- get a full price report from a port
- prices are internal values, not any particular currency
- example: `GetPriceReport sen'na`
### GetNeeds
- lists player health stats: sleep, sleep debt, water, food, food debt, protein, vitamins
### SetVitamins
- usage: SetVitamins \<int amount>
- set your vitamins %
### SetProtein
- usage: SetProtein \<int amount>
- set your protein %