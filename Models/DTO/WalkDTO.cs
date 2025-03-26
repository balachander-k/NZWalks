namespace NZWalks.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public double LengthInKm { get; set; }

        public String? WalkImageURL { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

        public RegionDTO region { get; set; }

        public DifficultyDTO difficulty { get; set; }
    }
}
