using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateanswercolumnwithdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PersonId",
                table: "Answers",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_People_PersonId",
                table: "Answers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_People_PersonId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_PersonId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Answers");
        }
    }
}
