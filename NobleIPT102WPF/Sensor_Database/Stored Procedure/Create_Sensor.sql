CREATE PROCEDURE [dbo].[Create_Sensor]
	@SensorID NVARCHAR(50) = Null, 
    @SensorName NVARCHAR(50) = NULL,         
    @SensorType NVARCHAR(30) = Null,         
    @Locations NVARCHAR(100) = Null,                   
    @SensorStatus NVARCHAR(20) = NuLL  
AS
BEGIN
	INSERT INTO [dbo].[Sensor] ([SensorID], [SensorName], [SensorType], [Locations],[SensorStatus])
	VALUES (@SensorID,@SensorName,@SensorType,@Locations,@SensorStatus);
END