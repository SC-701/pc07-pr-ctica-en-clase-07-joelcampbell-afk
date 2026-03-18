CREATE PROCEDURE AgregarProducto
    @Id AS uniqueidentifier,
    @IdSubCategoria AS UNIQUEIDENTIFIER,
    @Nombre         AS VARCHAR(MAX),
    @Descripcion    AS VARCHAR(MAX),
    @Precio         AS DECIMAL(18,0),
    @Stock          AS INT,
    @CodigoBarras   AS VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Implementación pendiente
BEGIN TRANSACTION
  INSERT INTO [dbo]. [Producto] 
    (Id,
    IdSubCategoria, 
    Nombre, 
    Descripcion, 
    Precio, 
    Stock, 
    CodigoBarras)
VALUES 
    (@Id,
    @IdSubCategoria, 
    @Nombre, 
    @Descripcion, 
    @Precio, 
    @Stock, 
    @CodigoBarras)
SELECT @Id 
COMMIT TRANSACTION
END