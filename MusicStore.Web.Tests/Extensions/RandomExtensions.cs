using System;

namespace MusicStore.Web.Tests.Extensions
{
    public static class RandomExtensions
    {
        public static string NextString(this Random random)
        {
            return Guid.NewGuid().ToString();
        }

        public static int NextPositive(this Random random)
        {
            return random.Next(1, int.MaxValue);
        }

        public static byte[] NextByteArray(this Random random)
        {
            return Guid.NewGuid().ToByteArray();
        }
    }
}