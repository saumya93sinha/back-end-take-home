CREATE TABLE [dbo].[Airport]
(
	[AirportId] INT NOT NULL IDENTITY , 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [City] NVARCHAR(MAX) NULL, 
    [Country] NVARCHAR(100) NULL, 
    [IATA3] CHAR(3) NOT NULL, 
    [Latitude] DECIMAL(10, 6) NULL, 
    [Longitude] DECIMAL(10, 6) NULL, 
    PRIMARY KEY ([IATA3])
)
