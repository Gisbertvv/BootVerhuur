namespace BootVerhuurWpf.Model
{
    public class Boat
    {
        public int BoatID { get; set; }
        public int NumberOfPeople { get; set; }
        public string Level { get; set; }
        public bool SteeringWheel { get; set; }
        public string Status { get; set; }

        public Boat(int boatID, int numberOfPeople, bool steeringWheel, string level, string status)
        {
            BoatID = boatID;
            NumberOfPeople = numberOfPeople;
            SteeringWheel = steeringWheel;
            Level = level;
            Status = status;
        }
    }
}
