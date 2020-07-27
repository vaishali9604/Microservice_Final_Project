using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCustomer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblRestaurant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    ContactNo = table.Column<string>(maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    UserCreated = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    UserModified = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    Address = table.Column<string>(unicode: false, nullable: true),
                    Website = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    OpeningTime = table.Column<string>(maxLength: 5, nullable: false, defaultValueSql: "('')"),
                    CloseTime = table.Column<string>(maxLength: 5, nullable: false, defaultValueSql: "('')"),
                    TblRatingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRestaurant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblRating",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<string>(maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    Comments = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    tblRestaurantID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblCustomerId = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    UserCreated = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    UserModified = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRating", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRating_tblCustomerId",
                        column: x => x.tblCustomerId,
                        principalTable: "tblCustomer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblRating_tblRestaurantID",
                        column: x => x.tblRestaurantID,
                        principalTable: "tblRestaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblRating_tblCustomerId",
                table: "tblRating",
                column: "tblCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRating_tblRestaurantID",
                table: "tblRating",
                column: "tblRestaurantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRating");

            migrationBuilder.DropTable(
                name: "tblCustomer");

            migrationBuilder.DropTable(
                name: "tblRestaurant");
        }
    }
}
