using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EotE_Encounter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Advantages = table.Column<byte>(nullable: false),
                    InitativeIndex = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Notes = table.Column<string>(maxLength: 1000, nullable: true),
                    Saved = table.Column<bool>(nullable: false),
                    Succeses = table.Column<byte>(nullable: false),
                    Triumphs = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
