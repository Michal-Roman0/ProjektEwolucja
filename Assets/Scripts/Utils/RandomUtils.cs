using UnityEngine;


namespace Utils
{
    public static class RandomUtils
    {
        public static float GenerateGaussianNoise(float mean, float stdev)
        {
            float u1 = Random.value;
            float u2 = Random.value;
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
            return mean + stdev * randStdNormal;
        }
    }
}