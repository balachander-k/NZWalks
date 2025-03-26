using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11c186a9-83f3-4b5c-a150-21376265e8d8"), "Medium" },
                    { new Guid("1bd62ef7-3c63-4c27-a70e-c7927de0cda5"), "Easy" },
                    { new Guid("b06ebf18-8123-454f-b293-adf7a8d3b795"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("0f9e1082-4fa7-4284-a02f-19ad1adada0e"), "BOP", "Bay Of Plenty", null },
                    { new Guid("788a5c15-3aa4-4f47-822d-ae4d8ccc9fc7"), "STL", "Southland", null },
                    { new Guid("a8f72932-8aac-49fb-9129-a588d3df4537"), "NTL", "Northland", null },
                    { new Guid("d58dc0d2-7b9d-4837-8158-fba49bf95200"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("dc8f246c-cd7a-4de5-a90b-97ce92a5c424"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("f86fe480-719f-4e18-97f5-91499b9bf322"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("11c186a9-83f3-4b5c-a150-21376265e8d8"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1bd62ef7-3c63-4c27-a70e-c7927de0cda5"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b06ebf18-8123-454f-b293-adf7a8d3b795"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0f9e1082-4fa7-4284-a02f-19ad1adada0e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("788a5c15-3aa4-4f47-822d-ae4d8ccc9fc7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a8f72932-8aac-49fb-9129-a588d3df4537"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d58dc0d2-7b9d-4837-8158-fba49bf95200"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("dc8f246c-cd7a-4de5-a90b-97ce92a5c424"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f86fe480-719f-4e18-97f5-91499b9bf322"));
        }
    }
}
