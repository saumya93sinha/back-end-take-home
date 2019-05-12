CREATE TABLE [dbo].[Airline]
(
	[AirlineId] INT NOT NULL IDENTITY , 
    [Name] NVARCHAR(100) NOT NULL, 
    [TwoDigitCode] CHAR(2) NOT NULL, 
    [ThreeDigitCode] CHAR(3) NULL, 
    [Country] NVARCHAR(100) NULL, 
    PRIMARY KEY ([TwoDigitCode])
)
