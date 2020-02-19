namespace aiala.Backend.Data.Activities
{
    /// <summary>
    /// Activity type. The values are divided into the ranges defined by <see cref="ActivityTypeRanges"/>.
    /// </summary>
    public enum ActivityType
    {
        Unknown = 0,
        //
        GeneralAppPageNavigation = 101,
        GeneralPlaceNavigation = 102,
        GeneralEmergencyHelpTextLink = 103,
        //
        EmergencyStart = 201,
        EmergencyMood = 202,
        EmergencyEnd = 203,
        //
        StepState = 301,
        //
        TaskDelayed = 401,
        TaskFeedback = 402,
        //
        PictureAdded = 501
    }

    /// <summary>
    /// Definition of the ranges of values used by <see cref="ActivityType"/>.
    /// </summary>
    public static class ActivityTypeRanges
    {
        public static Range Technical { get; } = new Range(0);
        public static Range General { get; } = new Range(100);
        public static Range Emergency { get; } = new Range(200);
        public static Range Step { get; } = new Range(300);
        public static Range Task { get; } = new Range(400);
        public static Range Picture { get; } = new Range(500);

        public static Range AllReports { get; } = new Range(200, 599);

        public class Range
        {
            internal Range(int min, int maxOffset = 99)
            {
                Min = min;
                Max = min + maxOffset;
            }

            public int Max { get; }

            public int Min { get; }

            public bool Contains(ActivityType type)
            {
                var value = (int) type;
                return value <= Max && value >= Min;
            }
        }
    }
}
