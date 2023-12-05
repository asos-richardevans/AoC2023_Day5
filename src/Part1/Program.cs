var input = File.ReadAllLines("./input.txt");

var seeds = input[0].Split(':')[1].Replace("  ", " ").Trim().Split(' ').Select(long.Parse).ToList();

var maps = new List<List<(long, long, long)>>();
maps.AddRange(Enumerable.Range(1, 7).Select(i => new List<(long, long, long)>()).ToArray());

GetData("seed-to-soil map:", maps.ToArray()[0]);
GetData("soil-to-fertilizer map:", maps.ToArray()[1]);
GetData("fertilizer-to-water map:", maps.ToArray()[2]);
GetData("water-to-light map:", maps.ToArray()[3]);
GetData("light-to-temperature map:", maps.ToArray()[4]);
GetData("temperature-to-humidity map:", maps.ToArray()[5]);
GetData("humidity-to-location map:", maps.ToArray()[6]);

var endLocations = new List<long>();

foreach (var seed in seeds)
{
    var endLocation = seed;
    foreach (var map in maps)
    {
        endLocation = GetMapping(endLocation, map);
    }
    endLocations.Add(endLocation);
}

Console.WriteLine(endLocations.Min());

long GetMapping(long value, List<(long, long, long)> map)
{
    foreach (var mapping in map)
    {
        if (value >= mapping.Item2 && value < mapping.Item2 + mapping.Item3)
        {
            return value - mapping.Item2 + mapping.Item1;
        }
    }
    return value;
}

void GetData(string title, List<(long, long, long)> map)
{
    var include = false;
    foreach (var line in input)
    {
        if (line.StartsWith(title))
        {
            include = true;
            continue;
        }

        if (include)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }
            var mapping = line.Split(' ').Select(long.Parse).ToArray();
            map.Add((mapping[0], mapping[1], mapping[2]));
        }
    }
}