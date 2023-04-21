namespace NumCheckLib {
    public static class TypeCheck {
        public static bool IsOddNumber(int number) {
            return number % 2 == 1 || number % 2 == -1;
        }
        public static bool IsEvenNumber(int number) {
            return number % 2 == 0;
        }
        public static bool IsPrimeNumber(int number) {
            if(number <= 1)
                return false;

            for(int i = 2; i <= Math.Sqrt(number); i++)
                if(number % i == 0)
                    return false;

            return true;
        }
        public static bool IsCompositeNumber(int number) {
            return number >= 4 && !IsPrimeNumber(number);
        }
    }
}