CREATE TABLE [dbo].[Route]
(
	[RouteId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AirlineId] CHAR(2) NOT NULL, 
    [Origin] CHAR(3) NOT NULL, 
    [Destination] CHAR(3) NOT NULL,
	CONSTRAINT FK_AirlineId_TwoDigitCode FOREIGN KEY (AirlineId) REFERENCES Airline(TwoDigitCode),
	CONSTRAINT FK_Origin_IATA3 FOREIGN KEY (Origin) REFERENCES Airport(IATA3),
	CONSTRAINT FK_Destination_IATA3 FOREIGN KEY (Destination) REFERENCES Airport(IATA3)
	 
)

GO

CREATE NONCLUSTERED INDEX [IX_Route_Origin_Destination] ON [dbo].[Route] ([Origin], [Destination])
