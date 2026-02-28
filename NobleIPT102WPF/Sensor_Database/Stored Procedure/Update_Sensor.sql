CREATE PROCEDURE [dbo].[Update_Sensor]
     @SensorID NVARCHAR(50) = Null, 
    @SensorName NVARCHAR(50) = NULL,         
    @SensorType NVARCHAR(30) = Null,         
    @Locations NVARCHAR(100) = Null,                   
    @SensorStatus NVARCHAR(20) = NuLL  
AS
BEGIN
	UPDATE [dbo].[Sensor]
	SET 
		[SensorName] = @SensorName,
		[SensorType] = @SensorType,
		[Locations] = @Locations,
		[SensorStatus] = @SensorStatus
	WHERE [SensorID] = @SensorID;
END