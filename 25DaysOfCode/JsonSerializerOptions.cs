namespace _25DaysOfCode
{
    internal class JsonSerializerOptions
    {
        public JsonSerializerOptions()
        {
        }

        public bool IgnoreNullValues { get; set; }
        public bool PropertyNameCaseInsensitive { get; set; }
        public bool WriteIndented { get; set; }
    }
}