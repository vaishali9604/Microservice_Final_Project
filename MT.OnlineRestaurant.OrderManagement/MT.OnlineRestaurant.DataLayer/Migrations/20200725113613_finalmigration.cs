using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class finalmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "tblCart",
                nullable: false,
                defaultValueSql: "('')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "tblCart");
        }
    }
}
