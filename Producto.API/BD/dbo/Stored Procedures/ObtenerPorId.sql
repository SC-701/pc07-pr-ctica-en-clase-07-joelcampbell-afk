CREATE   PROCEDURE dbo.ObtenerPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.Id           AS Id,
        sc.Nombre      AS SubCategoria,
        c.Nombre       AS Categoria,
        p.Nombre       AS Nombre,
        p.Descripcion  AS Descripcion,
        p.Precio       AS Precio,
        p.Stock        AS Stock,
        p.CodigoBarras AS CodigoBarras
    FROM dbo.Producto p
    LEFT JOIN dbo.SubCategorias sc ON sc.Id = p.IdSubCategoria
    LEFT JOIN dbo.Categorias c ON c.Id = sc.IdCategoria
    WHERE p.Id = @Id;
END