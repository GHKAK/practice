namespace GcdLcmLib {
    public class GcdLcmAlgorithms {
        public static int CalculateGcd(int x, int y) {
            x = Math.Abs(x);
            y = Math.Abs(y);
            if(x == 0) return y;
            if(y == 0) return x;
            while(y != 0) {
                int remainder = x % y;
                x = y;
                y = remainder;
            }

            return x;
        }
        public static int CalculateLcm(int x, int y) {
            x = Math.Abs(x);
            y = Math.Abs(y);
            if(x == 0 || y == 0) return 0;

            return (x * y) / CalculateGcd(x, y);
        }
    }
}