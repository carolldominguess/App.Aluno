using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.App.Aluno.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    usuario = table.Column<string>(type: "varchar(45)", nullable: false),
                    senha = table.Column<string>(type: "char(60)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataModificacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "turma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(45)", nullable: false),
                    ano = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataModificacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aluno_turma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataModificacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno_turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aluno_turma_aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aluno_turma_turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluno_turma_AlunoId",
                table: "aluno_turma",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_turma_TurmaId",
                table: "aluno_turma",
                column: "TurmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aluno_turma");

            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "turma");
        }
    }
}
