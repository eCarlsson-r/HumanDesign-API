using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanDesign.Migrations
{
    /// <inheritdoc />
    public partial class NewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "awareness_arrow",
                table: "variables");

            migrationBuilder.DropColumn(
                name: "digestion_arrow",
                table: "variables");

            migrationBuilder.DropColumn(
                name: "environment_arrow",
                table: "variables");

            migrationBuilder.RenameColumn(
                name: "perspective_arrow",
                table: "variables",
                newName: "reasoning");

            migrationBuilder.AddColumn<int>(
                name: "AwarenessArrowId",
                table: "variables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DigestionArrowId",
                table: "variables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnvironmentArrowId",
                table: "variables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerspectiveArrowId",
                table: "variables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "variable_arrow",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    design_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsLeft = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Tone = table.Column<int>(type: "int", nullable: false),
                    Base = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variable_arrow", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_variables_AwarenessArrowId",
                table: "variables",
                column: "AwarenessArrowId");

            migrationBuilder.CreateIndex(
                name: "IX_variables_DigestionArrowId",
                table: "variables",
                column: "DigestionArrowId");

            migrationBuilder.CreateIndex(
                name: "IX_variables_EnvironmentArrowId",
                table: "variables",
                column: "EnvironmentArrowId");

            migrationBuilder.CreateIndex(
                name: "IX_variables_PerspectiveArrowId",
                table: "variables",
                column: "PerspectiveArrowId");

            migrationBuilder.AddForeignKey(
                name: "FK_variables_variable_arrow_AwarenessArrowId",
                table: "variables",
                column: "AwarenessArrowId",
                principalTable: "variable_arrow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_variables_variable_arrow_DigestionArrowId",
                table: "variables",
                column: "DigestionArrowId",
                principalTable: "variable_arrow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_variables_variable_arrow_EnvironmentArrowId",
                table: "variables",
                column: "EnvironmentArrowId",
                principalTable: "variable_arrow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_variables_variable_arrow_PerspectiveArrowId",
                table: "variables",
                column: "PerspectiveArrowId",
                principalTable: "variable_arrow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_variables_variable_arrow_AwarenessArrowId",
                table: "variables");

            migrationBuilder.DropForeignKey(
                name: "FK_variables_variable_arrow_DigestionArrowId",
                table: "variables");

            migrationBuilder.DropForeignKey(
                name: "FK_variables_variable_arrow_EnvironmentArrowId",
                table: "variables");

            migrationBuilder.DropForeignKey(
                name: "FK_variables_variable_arrow_PerspectiveArrowId",
                table: "variables");

            migrationBuilder.DropTable(
                name: "variable_arrow");

            migrationBuilder.DropIndex(
                name: "IX_variables_AwarenessArrowId",
                table: "variables");

            migrationBuilder.DropIndex(
                name: "IX_variables_DigestionArrowId",
                table: "variables");

            migrationBuilder.DropIndex(
                name: "IX_variables_EnvironmentArrowId",
                table: "variables");

            migrationBuilder.DropIndex(
                name: "IX_variables_PerspectiveArrowId",
                table: "variables");

            migrationBuilder.DropColumn(
                name: "AwarenessArrowId",
                table: "variables");

            migrationBuilder.DropColumn(
                name: "DigestionArrowId",
                table: "variables");

            migrationBuilder.DropColumn(
                name: "EnvironmentArrowId",
                table: "variables");

            migrationBuilder.DropColumn(
                name: "PerspectiveArrowId",
                table: "variables");

            migrationBuilder.RenameColumn(
                name: "reasoning",
                table: "variables",
                newName: "perspective_arrow");

            migrationBuilder.AddColumn<string>(
                name: "awareness_arrow",
                table: "variables",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "digestion_arrow",
                table: "variables",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "environment_arrow",
                table: "variables",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
