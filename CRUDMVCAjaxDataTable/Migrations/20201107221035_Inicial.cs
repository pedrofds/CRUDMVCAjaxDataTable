using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDMVCAjaxDataTable.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<string>(maxLength: 2, nullable: false),
                    CountryNameEnglish = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Office = table.Column<string>(nullable: false),
                    Salary = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionId = table.Column<string>(maxLength: 2, nullable: false),
                    RegionNameEnglish = table.Column<string>(nullable: false),
                    CountryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Region_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "CountryNameEnglish" },
                values: new object[] { "US", "United States of America" });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "CountryNameEnglish" },
                values: new object[] { "CA", "Canada" });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "RegionId", "CountryId", "RegionNameEnglish" },
                values: new object[,]
                {
                    { "CT", "US", "Connecticut" },
                    { "ME", "US", "Maine" },
                    { "MA", "US", "Massachusetts" },
                    { "NH", "US", "New Hampshire" },
                    { "RI", "US", "Rhode Island" },
                    { "VT", "US", "Vermont" },
                    { "NB", "CA", "New Brunswick" },
                    { "NF", "CA", "Newfoundland" },
                    { "NT", "CA", "Northwest Territories" },
                    { "NS", "CA", "Nova Scotia" },
                    { "NU", "CA", "Nunavut" },
                    { "ON", "CA", "Ontario" },
                    { "PE", "CA", "Prince Edward Island" },
                    { "QC", "CA", "Québec" },
                    { "SK", "CA", "Saskatchewan" },
                    { "YT", "CA", "Yukon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId",
                table: "Region",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
