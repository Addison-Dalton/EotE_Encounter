using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EotE_Encounter.Migrations
{
    public partial class AddEncounterPropToCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncounterId",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Encounter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(maxLength: 2000, nullable: true),
                    Round = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounter", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_EncounterId",
                table: "Characters",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Encounter_EncounterId",
                table: "Characters",
                column: "EncounterId",
                principalTable: "Encounter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Encounter_EncounterId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Encounter");

            migrationBuilder.DropIndex(
                name: "IX_Characters_EncounterId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Characters");
        }
    }
}
