using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SamsPizzeria.Migrations.Tomasos
{
    public partial class RemovedFKKundFromBeställning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Bestallning_Kund",
            //    table: "Bestallning");

            //migrationBuilder.RenameColumn(
            //    name: "KundID",
            //    table: "Bestallning",
            //    newName: "KundId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Bestallning_KundID",
            //    table: "Bestallning",
            //    newName: "IX_Bestallning_KundId");

            //migrationBuilder.AlterColumn<int>(
            //    name: "KundId",
            //    table: "Bestallning",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Bestallning_Kund_KundId",
            //    table: "Bestallning",
            //    column: "KundId",
            //    principalTable: "Kund",
            //    principalColumn: "KundID",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestallning_Kund_KundId",
                table: "Bestallning");

            migrationBuilder.RenameColumn(
                name: "KundId",
                table: "Bestallning",
                newName: "KundID");

            migrationBuilder.RenameIndex(
                name: "IX_Bestallning_KundId",
                table: "Bestallning",
                newName: "IX_Bestallning_KundID");

            migrationBuilder.AlterColumn<int>(
                name: "KundID",
                table: "Bestallning",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestallning_Kund",
                table: "Bestallning",
                column: "KundID",
                principalTable: "Kund",
                principalColumn: "KundID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
