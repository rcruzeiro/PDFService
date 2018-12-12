using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDFService.Repository.MySQL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    clientID = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "now()"),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "now()"),
                    title = table.Column<string>(nullable: false),
                    desc = table.Column<string>(nullable: true),
                    page = table.Column<string>(nullable: true),
                    content = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_templates_clientID",
                table: "templates",
                column: "clientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "templates");
        }
    }
}
