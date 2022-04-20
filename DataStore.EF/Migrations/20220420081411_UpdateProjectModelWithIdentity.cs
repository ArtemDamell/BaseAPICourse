using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataStore.EF.Migrations
{
    public partial class UpdateProjectModelWithIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventAdministrators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventAdministrators",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventAdministrators",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Projects",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Project 1" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Project 2" });

            migrationBuilder.InsertData(
                table: "EventAdministrators",
                columns: new[] { "Id", "Address", "Age", "FirstName", "LastName", "Phone", "ProjectId" },
                values: new object[,]
                {
                    { 1, "Somestreet 1", 34, "Admin 1", "Adminov 1", "0409612987", 1 },
                    { 2, "Somestreet 2", 23, "Admin 2", "Adminov 2", "0419397987", 1 },
                    { 3, "Somestreet 3", 40, "Admin 3", "Adminov 3", "0459697145", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "EnteredDate", "EventDate", "Owner", "ProjectId", "Title" },
                values: new object[,]
                {
                    { 1, "Ticket for Project 1", null, null, null, 1, "Ticket 1" },
                    { 2, "Ticket for Project 1", null, null, null, 1, "Ticket 2" },
                    { 3, "Ticket for Project 2", null, null, null, 2, "Ticket 3" }
                });
        }
    }
}
