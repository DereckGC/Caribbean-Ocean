namespace CaribbeanOcean.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int RoomTypeId { get; set; }
        public bool Active { get; set; }
    }
}