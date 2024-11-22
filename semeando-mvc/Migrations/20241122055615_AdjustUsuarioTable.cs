using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace semeando_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AdjustUsuarioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RM553326");

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                schema: "RM553326",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_USUARIO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    RANKING = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false),
                    STREAK = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BIO = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.ID_USUARIO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USUARIO",
                schema: "RM553326");
        }
    }
}
