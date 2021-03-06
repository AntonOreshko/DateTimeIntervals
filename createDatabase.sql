CREATE DATABASE [DateTimeIntervalsClientLogs]
GO

USE  [DateTimeIntervalsClientLogs]
GO

CREATE TABLE Requests(
	Id INT IDENTITY(1,1) PRIMARY KEY,    
	UserId INT NULL,    
	ResponseCode INT NULL,    
	RequestMethod NVARCHAR(20) NOT NULL,
	RequestPath NVARCHAR(max) NOT NULL,
	RequestTime DATETIME NOT NULL,
	ResponseTime DATETIME NOT NULL,
	RequestProtocol NVARCHAR(20) NOT NULL,
	RequestBody NVARCHAR(max) NULL,
	ResponseBody NVARCHAR(max) NULL,
)

USE  [DateTimeIntervalsClientLogs]
GO

CREATE PROCEDURE spAddRequest     
(    
	@UserId INT,    
	@ResponseCode INT,
	@RequestMethod NVARCHAR(20),
	@RequestPath NVARCHAR(max),
	@RequestTime DATETIME,
	@ResponseTime DATETIME,
	@RequestProtocol NVARCHAR(20),
	@RequestBody NVARCHAR(max),
	@ResponseBody NVARCHAR(max))    
AS     
BEGIN     
    INSERT INTO Requests
	(
		UserId,
		ResponseCode,
		RequestMethod,
		RequestPath, 
		RequestTime,
		ResponseTime,
		RequestProtocol,
		RequestBody, 
		ResponseBody
	)     
    VALUES
	(
		@UserId,
		@ResponseCode,
		@RequestMethod,
		@RequestPath, 
		@RequestTime,
		@ResponseTime,
		@RequestProtocol,
		@RequestBody, 
		@ResponseBody
	)     
END