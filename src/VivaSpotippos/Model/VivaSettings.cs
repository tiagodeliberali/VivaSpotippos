namespace VivaSpotippos.Model
{
    // General settings. This class can be changed to retrieve information from other sources
    public class VivaSettings
    {
        public static int MinMapX { get { return 0; } }
        public static int MaxMapX { get { return 1400; } }

        public static int MinMapY { get { return 0; } }
        public static int MaxMapY { get { return 1000; } }

        public static int MinBathsNumber { get { return 1; } }
        public static int MaxBathsNumber { get { return 4; } }

        public static int MinBedsNumber { get { return 1; } }
        public static int MaxBedsNumber { get { return 5; } }

        public static int MinPropertySize { get { return 20; } }
        public static int MaxPropertySize { get { return 240; } }
    }
}
