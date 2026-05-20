using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations.Restaurant
{
    /// <inheritdoc />
    public partial class Restauracja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "restauracje",
                columns: table => new
                {
                    Nr_restauracji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Miejscowosc = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ulica = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nr_budynku = table.Column<int>(type: "int", nullable: false),
                    Nr_lokalu = table.Column<int>(type: "int", nullable: true),
                    Dlugosc_geo = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Szerokosc_geo = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Zdjecie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restauracje", x => x.Nr_restauracji);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "restauracje");
        }
    }
}
