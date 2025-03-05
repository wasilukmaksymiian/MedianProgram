namespace MedianProgram.Services
{
    public class MedianModelsServices
    {
        public List<int> GetPrime(int n)
        {
            if (n < 2)
            {
                return new List<int>();
            }

            bool[] isPrime = new bool[n + 1];
            for (int i = 2; i <= n; i++)
            {
                isPrime[i] = true;
            }
            for (int i = 2; i * i <= n; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= n; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            return Enumerable.Range(2, n-1).Where(i => isPrime[i]).ToList();
        }

        public List<int> GetMedianPrime(List<int> primes)
        {
            int count = primes.Count;
            if (count == 0) return new List<int>();
            if (count % 2 == 1) return new List<int> { primes[count / 2] };
            else return new List<int> { primes[count / 2 - 1], primes[count / 2] };
        }
    }
}
