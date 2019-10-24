namespace UtiliZek
{
    public static class Utilizek
    {
        private const float CLOSE_ENOUGH = 0.001f;

        public static bool CloseEnough(float a, float b) { return a + CLOSE_ENOUGH > b && a - CLOSE_ENOUGH < b; }
    }
}