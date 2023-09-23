using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGalaxy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE uspListarInstructores
                                (
	                                @Nombres NVARCHAR(100) = NULL,
	                                @NroDocumento NVARCHAR(100) = NULL,
	                                @CategoriaId INT = NULL
                                )
                                AS
                                BEGIN

	                                SELECT 
		                                I.Id,
		                                I.Nombres,
		                                I.NroDocumento,
		                                C.Nombre AS Categoria,
		                                I.CategoriaId
	                                FROM Instructor I (NOLOCK)
	                                INNER JOIN Categoria C (NOLOCK) ON I.CategoriaId = C.ID
	                                WHERE I.Estado = 1
	                                AND (@NroDocumento IS NULL OR (I.NroDocumento = @NroDocumento))
	                                AND (@Nombres IS NULL OR (I.Nombres LIKE '%' + @Nombres + '%'))
	                                AND (@CategoriaId IS NULL OR (I.CategoriaId = @CategoriaId))
	                                ORDER BY I.Nombres

                                END
                            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC uspListarInstructores");
        }
    }
}
