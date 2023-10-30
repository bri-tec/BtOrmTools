CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY , 
    [ProductName] NVARCHAR(250) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] DECIMAL NULL, 
    [Qty_In_Stock] FLOAT NULL
)
