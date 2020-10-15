using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AnimalType = table.Column<string>(nullable: false),
                    Breed = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    AdoptionDate = table.Column<DateTime>(nullable: false),
                    DeathDate = table.Column<DateTime>(nullable: false),
                    HelpedTheirPrivateParts = table.Column<bool>(nullable: false),
                    CanBeWithKids = table.Column<string>(nullable: false),
                    Treatments = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: false),
                    Adoptable = table.Column<bool>(nullable: false),
                    AdoptedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Remark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    MadeBy = table.Column<string>(nullable: true),
                    AnimalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remark_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remark_AnimalId",
                table: "Remark",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remark");

            migrationBuilder.DropTable(
                name: "Animal");
        }
    }
}
