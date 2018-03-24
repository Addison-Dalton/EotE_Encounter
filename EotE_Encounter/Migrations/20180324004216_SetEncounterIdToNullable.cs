using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EotE_Encounter.Migrations
{
    public partial class SetEncounterIdToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Encounters_EncounterId",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "EncounterId",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(int));

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

            migrationBuilder.AlterColumn<int>(
                name: "EncounterId",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Encounters_EncounterId",
                table: "Characters",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
