CREATE   PROCEDURE dbo.Obtener
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
    INNER JOIN dbo.SubCategorias sc ON sc.Id = p.IdSubCategoria
    INNER JOIN dbo.Categorias c ON c.Id = sc.IdCategoria;
END