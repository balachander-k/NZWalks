using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Data for difficulties 
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("1bd62ef7-3c63-4c27-a70e-c7927de0cda5"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("11c186a9-83f3-4b5c-a150-21376265e8d8"),
                    Name="Medium"
                },
                new Difficulty()
                {
                    Id=Guid.Parse("b06ebf18-8123-454f-b293-adf7a8d3b795"),
                    Name="Hard"
                }
            };

            //seed Difficulties data to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);



            var regions = new List<Region>()
            {
                 new Region
                {
                    Id = Guid.Parse("dc8f246c-cd7a-4de5-a90b-97ce92a5c424"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageURL = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("a8f72932-8aac-49fb-9129-a588d3df4537"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageURL = null
                },
                new Region
                {
                    Id = Guid.Parse("0f9e1082-4fa7-4284-a02f-19ad1adada0e"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageURL = null
                },
                new Region
                {
                    Id = Guid.Parse("d58dc0d2-7b9d-4837-8158-fba49bf95200"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageURL = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f86fe480-719f-4e18-97f5-91499b9bf322"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageURL = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("788a5c15-3aa4-4f47-822d-ae4d8ccc9fc7"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageURL = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
