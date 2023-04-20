namespace NumCheckLib {
    public static class TypeCheck {
        public static bool IsPositiveNumber(int number) {
            return number >= 0;
        }
        public static bool IsOddNumber(int number) {
            return IsPositiveNumber(number) && number % 2 == 1;
        }
        public static bool IsEvenNumber(int number) {
            return IsPositiveNumber(number) && number % 2 == 0;
        }
        public static bool IsPrimeNumber(int number) {
            if(number <= 1) return false;
            if(number <= 3) return true;
            if(number % 2 == 0 || number % 3 == 0) return false;
            for(int i = 5; i * i <= number; i = i + 6)
                if(number % i == 0 || number % (i + 2) == 0) return false;
            return true;
        }
        public static bool IsCompositeNumber(int number) {
            return IsPositiveNumber(number) && !IsPrimeNumber(number);
        }
    }
}