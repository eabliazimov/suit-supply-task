using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alteration.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlterationForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SuitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlterationForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlterationInstructions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlterationTypeId = table.Column<int>(type: "int", nullable: false),
                    AlterationLength = table.Column<double>(type: "float", nullable: false),
                    AlterationFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlterationInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlterationInstructions_AlterationForms_AlterationFormId",
                        column: x => x.AlterationFormId,
                        principalTable: "AlterationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlterationForms_SuitId",
                table: "AlterationForms",
                column: "SuitId");

            migrationBuilder.CreateIndex(
                name: "IX_AlterationInstructions_AlterationFormId",
                table: "AlterationInstructions",
                column: "AlterationFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlterationInstructions");

            migrationBuilder.DropTable(
                name: "AlterationForms");
        }
    }
}
