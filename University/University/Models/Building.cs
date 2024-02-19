namespace University.Models
{
    public class Building
    {
        public int Id { get; set; }
        public int BuildingNumber { get; set; }
        public int CapacityRooms { get; set; }
        public string Address { get; set; } = null!;
    }
}
