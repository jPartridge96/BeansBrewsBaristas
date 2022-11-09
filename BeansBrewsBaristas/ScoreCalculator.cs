using System;

namespace BeansBrewsBaristas
{
    public class ScoreCalculator
    {
        private const int MAX_COMBO = 10;

        public int Score;
        public int Combo
        {
            get { return Combo; }
            set
            {
                if (Combo > MAX_COMBO)
                    Combo = MAX_COMBO;
                else Combo = value;
            }
        }

        public decimal DifficultyMultiplier { get; set; }
        public decimal ComboMultiplier { get => Fibonacci(Combo) * 100; }
        public decimal Multiplier { get => DifficultyMultiplier + ComboMultiplier; }

        public long Fibonacci(int n)
        {
            if (n < 0) throw new Exception("Integer must be positive");
            if (n <= 1) return n;

            long ans = 0;
            long f1 = 1;
            long f2 = 1;

            for (int i = 2; i < n; i++)
            {
                ans = f1 + f2;

                f2 = f1;
                f1 = ans;
            }
            return ans;
        }
    }
}
