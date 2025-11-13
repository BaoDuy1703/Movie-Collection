using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN232_PE_FA25_LeVoBaoDuy.Migrations
{
    /// <inheritdoc />
    public partial class InitMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: true),
                    PosterImage = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedAt", "Genre", "PosterImage", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 13, 16, 14, 11, 0, DateTimeKind.Utc), "Sci-Fi", "https://picsum.photos/seed/matrix/400/600", 5, "The Matrix" },
                    { 2, new DateTime(2025, 11, 13, 16, 14, 11, 0, DateTimeKind.Utc), "Sci-Fi", "https://picsum.photos/seed/inception/400/600", 5, "Inception" },
                    { 3, new DateTime(2025, 11, 13, 16, 14, 11, 0, DateTimeKind.Utc), "Action", "https://picsum.photos/seed/darkknight/400/600", 5, "The Dark Knight" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

