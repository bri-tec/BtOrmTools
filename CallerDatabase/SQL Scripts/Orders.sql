CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Date_Ordered] DATETIME NULL, 
    [Product_ID] INT NULL, 
    [Qty] FLOAT NULL, 
    [Caller_ID] INT NULL, 
    [Cancelled] BIT NULL, 
    [Complete] BIT NULL, 
    CONSTRAINT [FK_Orders_Callers] FOREIGN KEY ([Caller_ID]) REFERENCES [Callers]([Id]), 
    CONSTRAINT [FK_Orders_Products] FOREIGN KEY ([Product_ID]) REFERENCES [Products]([Id])
)
