CREATE PROCEDURE dbo.EditarProducto
    @Id             AS UNIQUEIDENTIFIER,
    @IdSubCategoria AS UNIQUEIDENTIFIER,
    @Nombre         AS VARCHAR(MAX),
    @Descripcion    AS VARCHAR(MAX),
    @Precio         AS DECIMAL(18,2),
    @Stock          AS INT,
    @CodigoBarras   AS VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Implementación pendiente
    BEGIN TRANSACTION
    UPDATE [dbo].[Producto]
    SET [IdSubCategoria] = @IdSubCategoria,
        [Nombre] = @Nombre,
        [Descripcion] = @Descripcion,
        [Precio] = @Precio,
        [Stock] = @Stock,
        [CodigoBarras] = @CodigoBarras
    WHERE [Id] = @Id
    SELECT @Id
    COMMIT TRANSACTION
END