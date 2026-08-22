using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysNova.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleSubjectToCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleSubject",
                table: "Cliente",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_GoogleSubject",
                table: "Cliente",
                column: "GoogleSubject",
                unique: true,
                filter: "[GoogleSubject] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_GoogleSubject",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "GoogleSubject",
                table: "Cliente");
        }
    }
}
