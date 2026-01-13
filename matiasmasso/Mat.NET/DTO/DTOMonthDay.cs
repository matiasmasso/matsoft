namespace DTO
{
    public class DTOMonthDay
    {
        public int Month { get; set; }
        public int Day { get; set; }

        public DTOMonthDay(int month, int day) : base()
        {
            Month = month;
            Day = day;
        }
    }
}
