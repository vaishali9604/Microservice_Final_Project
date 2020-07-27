using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class DBmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserCreated",
                table: "tblRatingandReviews",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "tblCustomerId",
                table: "tblRatingandReviews",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "tblRatingandReviews",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldDefaultValueSql: "('')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserCreated",
                table: "tblRatingandReviews",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "tblCustomerId",
                table: "tblRatingandReviews",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "tblRatingandReviews",
                maxLength: 10,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(int),
                oldDefaultValueSql: "((0))");
        }
    }
}
