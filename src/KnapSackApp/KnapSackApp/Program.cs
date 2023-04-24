using KnapSackApp;

const int MaxWeight = 125;
List<Thing> things = RandomList.CreateRandomThingList(5);
Console.WriteLine("Input List : \n ");
foreach(Thing thing in things) {
    Console.WriteLine($"Weight: {thing.Weight}, Value: {thing.Value}");
}
Console.WriteLine($" \nResult List in BackPack with MaxWeight {MaxWeight} : \n ");
foreach(Thing thing in things.KnapSackList(MaxWeight)) {
    Console.WriteLine($"Weight: {thing.Weight}, Value: {thing.Value}");
}
Console.Read();