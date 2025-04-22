using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.App.Aluno.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Ajustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turma_aluno_AlunoId",
                table: "aluno_turma");

            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turma_turma_TurmaId",
                table: "aluno_turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_turma",
                table: "turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aluno",
                table: "aluno");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "turma");

            migrationBuilder.RenameTable(
                name: "turma",
                newName: "turmas");

            migrationBuilder.RenameTable(
                name: "aluno",
                newName: "alunos");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "aluno_turma",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "turmas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "senha",
                table: "alunos",
                type: "varchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(60)");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "alunos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_turmas",
                table: "turmas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alunos",
                table: "alunos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turma_alunos_AlunoId",
                table: "aluno_turma",
                column: "AlunoId",
                principalTable: "alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turma_turmas_TurmaId",
                table: "aluno_turma",
                column: "TurmaId",
                principalTable: "turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turma_alunos_AlunoId",
                table: "aluno_turma");

            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turma_turmas_TurmaId",
                table: "aluno_turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_turmas",
                table: "turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_alunos",
                table: "alunos");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "aluno_turma");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "turmas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "alunos");

            migrationBuilder.RenameTable(
                name: "turmas",
                newName: "turma");

            migrationBuilder.RenameTable(
                name: "alunos",
                newName: "aluno");

            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "turma",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "senha",
                table: "aluno",
                type: "char(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_turma",
                table: "turma",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aluno",
                table: "aluno",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turma_aluno_AlunoId",
                table: "aluno_turma",
                column: "AlunoId",
                principalTable: "aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turma_turma_TurmaId",
                table: "aluno_turma",
                column: "TurmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}