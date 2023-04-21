using GcdLcmLib;
namespace GcdLcm {
    internal class GcdLcmFinder {
        internal static (int,int) GcdLcmCalculate(int x, int y) {
            return (GcdLcmAlgorithms.CalculateGcd(x, y), GcdLcmAlgorithms.CalculateLcm(x, y));
        }
    }
}
