using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiManicure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_HorariosDisponiveis_HorarioDisponivelId",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_HorarioDisponivelId",
                table: "Agendamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_HorarioDisponivelId",
                table: "Agendamentos",
                column: "HorarioDisponivelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_HorariosDisponiveis_HorarioDisponivelId",
                table: "Agendamentos",
                column: "HorarioDisponivelId",
                principalTable: "HorariosDisponiveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
