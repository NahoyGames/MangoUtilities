namespace MangoUtilities.Serializing
{
    public static class Compressor
    {
        public static byte ToByte(float value, float min, float max)
        {
            return (byte) Remap(value, min, max, byte.MinValue, byte.MaxValue);
        }

        public static float ToFloat(byte value, float min, float max)
        {
            return Remap(value, byte.MinValue, byte.MaxValue, min, max);
        }

        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }
    }
}