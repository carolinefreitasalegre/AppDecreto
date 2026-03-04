using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNumeroDecretoDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:secretaria_enum.secretaria", "procuradoria_geral,casa_civil,governo_e_relacoes_institucionais,controladoria_geral,seguranca_e_defesa_social,fazenda,administracao_e_inovacao,articulacao_regional,planejamento_e_parcerias_estrategicas,licitacoes_e_contratos,infraestrutura_e_servicos_publicos,meio_ambiente_e_desenvolvimento,educacao,saude,assistencia_social_e_cidadania,executiva_da_mulher,executiva_da_juventude,instituto_previdencia,agencia_habitacao,agencia_meio_ambiente,agencia_regulacao,agencia_transporte_transito,agencia_tecnologia_informacao,fundacao_cultura,fundacao_esportes")
                .Annotation("Npgsql:Enum:usuario_role_enum.usuario_role", "gestor,usuario")
                .Annotation("Npgsql:Enum:usuario_status_enum.usuario_status", "ativo,inativo");

            migrationBuilder.CreateSequence<int>(
                name: "decreto_numero_seq",
                startValue: 5966L);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Matricula = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decretos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroDecreto = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('decreto_numero_seq')"),
                    Solicitante = table.Column<string>(type: "text", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataParaUso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Secretaria = table.Column<int>(type: "integer", nullable: false),
                    Justificativa = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decretos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decretos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Decretos_UsuarioId",
                table: "Decretos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Decretos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropSequence(
                name: "decreto_numero_seq");
        }
    }
}
