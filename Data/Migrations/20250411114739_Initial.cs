using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryOfIndividuals.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Sex = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonalNumber = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    ImageAdress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectedIndividuals",
                columns: table => new
                {
                    ConnectionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConnectionKind = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonAInd = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonBInd = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectedIndividuals", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_ConnectedIndividuals_Individuals_PersonAInd",
                        column: x => x.PersonAInd,
                        principalTable: "Individuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConnectedIndividuals_Individuals_PersonBInd",
                        column: x => x.PersonBInd,
                        principalTable: "Individuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    PhoneNumberId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberType = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.PhoneNumberId);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Individuals_NumberOwnerId",
                        column: x => x.NumberOwnerId,
                        principalTable: "Individuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedIndividuals_PersonAInd",
                table: "ConnectedIndividuals",
                column: "PersonAInd");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedIndividuals_PersonBInd",
                table: "ConnectedIndividuals",
                column: "PersonBInd");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_NumberOwnerId",
                table: "PhoneNumbers",
                column: "NumberOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "ConnectedIndividuals");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Individuals");
        }
    }
}
