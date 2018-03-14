using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EotE_Encounter.Migrations
{
    public partial class AddEncounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Encounter_EncounterId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Encounter",
                table: "Encounter");

            migrationBuilder.RenameTable(
                name: "Encounter",
                newName: "Encounters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Encounters",
                table: "Encounters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Encounters_EncounterId",
                table: "Characters",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Encounters_EncounterId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Encounters",
                table: "Encounters");

            migrationBuilder.RenameTable(
                name: "Encounters",
                newName: "Encounter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Encounter",
                table: "Encounter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Encounter_EncounterId",
                table: "Characters",
                column: "EncounterId",
                principalTable: "Encounter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
