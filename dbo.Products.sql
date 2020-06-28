CREATE TABLE [dbo].[Products] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NULL,
    [Volume] NVARCHAR (50) NULL,
    [State] NVARCHAR(50) NULL, 
    [Closed_volume] NVARCHAR(50) NULL, 
    [Date_in] DATE NULL, 
    [Dane_out] DATE NULL, 
    [Brand] TEXT NULL, 
    [Diameter] TEXT NULL, 
    [Wall] TEXT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

