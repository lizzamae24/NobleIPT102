CREATE PROCEDURE [dbo].[Delete_Sensor]
	@SensorID NVARCHAR(50) = Null
AS

BEGIN
	DELETE FROM [dbo].[Sensor] WHERE SensorID = @SensorID;
END
