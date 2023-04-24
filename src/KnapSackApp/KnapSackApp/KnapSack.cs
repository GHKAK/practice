namespace KnapSackApp {
    public static class KnapSack {
        public static List<Thing> KnapSackList(this List<Thing> things, int MaxWeight) {
            List<Thing> result = new List<Thing>();
            int i, w;
            int n = things.Count;
            int[,] K = new int[n + 1, MaxWeight + 1];
            for(i = 0; i <= n; i++) {
                for(w = 0; w <= MaxWeight; w++) {
                    if(i == 0 || w == 0)
                        K[i, w] = 0;
                    else if(things[i - 1].Weight <= w)
                        K[i, w] = Math.Max(things[i - 1].Value + K[i - 1, w - things[i - 1].Weight], K[i - 1, w]);
                    else
                        K[i, w] = K[i - 1, w];
                }
            }
            while(n != 0) {
                if(K[n, MaxWeight] != K[n - 1, MaxWeight]) {
                    result.Add(things[n - 1]);
                    MaxWeight = MaxWeight - things[n - 1].Weight;
                }
                n--;
            }
            return result;
        }
    }
}
