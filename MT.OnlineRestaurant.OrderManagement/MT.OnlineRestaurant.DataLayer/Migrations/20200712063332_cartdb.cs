using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class cartdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCart",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tblCustomerId = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblRestaurantId = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblMenuId = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Quantity = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCart", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCart");
        }
    }
}
