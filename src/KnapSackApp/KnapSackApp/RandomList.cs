namespace KnapSackApp {
    public static class RandomList {
        static Random random = new Random();

        public static List<Thing> CreateRandomThingList(int length) {
            List<Thing> list = new List<Thing>();

            for(int i = 0; i < length; i++) {
                int weight = random.Next(1, 101);
                int value = random.Next(100, 1000);

                Thing thing = new Thing {
                    Weight = weight,
                    Value = value
                };
                list.Add(thing);
            }
            return list;
        }
    }
}
