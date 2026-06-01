using System;

namespace RadRefinements
{
    // "Borrowed" from RadRefinements by Raddude aka bryon82 per MIT license https://github.com/bryon82/SailwindRadRefinements/
    public static class CompassRose
    {
        public static string GetCardinalDirection(float degrees, int precision)
        {
            if (precision == 4)
            {
                string[] directions = { "North", "East", "South", "West", "North" };
                var index = (int)Math.Round(degrees / 90.0) % 4;
                return directions[index];
            }
            else if (precision == 8)
            {
                string[] directions = {
                        "North", "Northeast", "East", "Southeast",
                        "South", "Southwest", "West", "Northwest", "North"
                    };
                var index = (int)Math.Round(degrees / 45.0) % 8;
                return directions[index];
            }
            else if (precision == 16)
            {
                string[] directions = {
                        "North", "North-northeast", "Northeast", "East-northeast",
                        "East", "East-southeast", "Southeast", "South-southeast",
                        "South", "South-southwest", "Southwest", "West-southwest",
                        "West", "West-northwest", "Northwest", "North-northwest", "North"
                    };
                var index = (int)Math.Round(degrees / 22.5) % 16;
                return directions[index];
            }
            else // precision == 32
            {
                string[] directions = {
                        "North", "North by east", "North-northeast", "Northeast by north",
                        "Northeast", "Northeast by east", "East-northeast", "East by north",
                        "East", "East by south", "East-southeast", "Southeast by east",
                        "Southeast", "Southeast by south", "South-southeast", "South by east",
                        "South", "South by west", "South-southwest", "Southwest by south",
                        "Southwest", "Southwest by west", "West-southwest", "West by south",
                        "West", "West by north", "West-northwest", "Northwest by west",
                        "Northwest", "Northwest by north", "North-northwest", "North by west", "North"
                    };
                var index = (int)Math.Round(degrees / 11.25) % 32;
                return directions[index];
            }
        }

        public static string GetAbbreviatedDirection(float degrees, int precision)
        {
            string direction = GetCardinalDirection(degrees, precision);

            return direction
                .ToLower()
                .Replace("north", "N")
                .Replace("east", "E")
                .Replace("south", "S")
                .Replace("west", "W")
                .Replace("-", "")
                .Replace(" by ", "b");
        }
    }
}
