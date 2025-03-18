namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public double LengthInKm { get; set; }

        public String? WalkImageURL { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }


    }
}
