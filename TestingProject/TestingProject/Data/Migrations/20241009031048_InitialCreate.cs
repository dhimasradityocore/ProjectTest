using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingProject.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COM_CUSTOMER",
                columns: table => new
                {
                    COM_CUSTOMER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CUSTOMER_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COM_CUSTOMER", x => x.COM_CUSTOMER_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COM_CUSTOMER");
        }
    }
}
