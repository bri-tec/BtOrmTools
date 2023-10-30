CREATE TABLE [dbo].[Callers]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Firstname] NVARCHAR(MAX) NULL, 
    [Lastname] NVARCHAR(MAX) NULL, 
    [Title] NVARCHAR(50) NULL, 
    [Telephone_No] NVARCHAR(20) NULL, 
    [Email] NVARCHAR(MAX) NULL
)
