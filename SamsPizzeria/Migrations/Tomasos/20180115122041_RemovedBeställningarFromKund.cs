using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SamsPizzeria.Migrations.Tomasos
{
    public partial class RemovedBeställningarFromKund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestallning_Kund",
                table: "Bestallning");

            //migrationBuilder.DropIndex(
            //    name: "IX_Bestallning_KundId",
            //    table: "Bestallning");

            migrationBuilder.DropColumn(
                name: "KundId",
                table: "Bestallning");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KundId",
                table: "Bestallning",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bestallning_KundId",
                table: "Bestallning",
                column: "KundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestallning_Kund_KundId",
                table: "Bestallning",
                column: "KundId",
                principalTable: "Kund",
                principalColumn: "KundID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
