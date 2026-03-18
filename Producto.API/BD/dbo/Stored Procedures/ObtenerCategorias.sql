CREATE PROCEDURE ObtenerCategorias
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Nombre
    FROM Productos.dbo.Categorias;
END