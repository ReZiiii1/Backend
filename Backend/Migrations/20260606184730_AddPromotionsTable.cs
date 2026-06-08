using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPromotionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promocje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opis = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cena = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    zdjęcie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsPremium = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promocje", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "promocje",
                columns: new[] { "Id", "Nazwa", "Opis", "Cena", "zdjęcie", "IsPremium" },
                values: new object[,]
                {
                    {
                        1,
                        "Zestaw dla dziecka",
                        "Nuggetsy z frytkami i colą 0,33l w super cenie. Idealny zestaw dla najmłodszych smakoszy.",
                        30.49m,
                        "https://heisenburger.pl/uploads/images/products/org/11.jpg",
                        false
                    },
                    {
                        2,
                        "2x Burger Wege",
                        "Zniżka 30% na drugiego burgera Wege.",
                        53.35m,
                        "https://www.frosta.pl/wp-content/uploads/sites/4/2020/11/shutterstock_794244805_Wege-burger-z-guacamole-scaled.jpg",
                        false
                    },
                    {
                        3,
                        "Chrupiący Box Przekąsek",
                        "Frytki, kurczaczki w sosie ostrym, krązki cebulowe i 2 autorskie sosy.",
                        34.99m,
                        "https://papupos.s3.amazonaws.com/media/company/143b6abe-d23b-406d-854d-1a2412edd69d/images/c41d545b-542a-4492-a7e4-dd5f01e5be11.png",
                        false
                    },
                    {
                        4,
                        "Łosoś Grillowany",
                        "Zniżka 9zł na najpyszniejszą rybę w Polsce. Oferta dla klubowiczów.",
                        43.00m,
                        "https://saproduwielbiaplmmedia.blob.core.windows.net/media/recipes/images/1699973472780.jpeg",
                        true
                    },
                    {
                        5,
                        "Frytki z batata",
                        "Zniżka 5zł na słodkie chrupiące frytki z batata. Oferta dla klubowiczów.",
                        9.00m,
                        "https://az.przepisy.pl/www-przepisy-pl/www.przepisy.pl/przepisy3ii/img/variants/800x0/frytki_z_marchewki_0994803.jpg",
                        true
                    },
                    {
                        6,
                        "Burger klasyczny XL",
                        "Powiększony o 80gr mięsa burger klasyczny. Oferta dla klubowiczów.",
                        32.99m,
                        "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?auto=format&fit=crop&w=500&q=80",
                        true
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "promocje");
        }
    }
}
