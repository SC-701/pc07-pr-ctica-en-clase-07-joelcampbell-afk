CREATE PROCEDURE ObtenerSubCategorias
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        IdCategoria,
        Nombre
    FROM Productos.dbo.SubCategorias
    WHERE IdCategoria = @IdCategoria;
END