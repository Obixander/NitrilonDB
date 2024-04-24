CREATE PROCEDURE [dbo].Name
 @EventId INT
AS
BEGIN
    DECLARE @Count1 INT, @Count2 INT, @Count3 INT;

    SELECT @Count1 = COUNT(CASE WHEN RatingID = 1 THEN 1 ELSE NULL END),
           @Count2 = COUNT(CASE WHEN RatingID = 2 THEN 1 ELSE NULL END),
           @Count3 = COUNT(CASE WHEN RatingID = 3 THEN 1 ELSE NULL END)
    FROM EventRatings
    WHERE EventId = @EventId;

    SELECT @Count1 AS RatingId1Count,
           @Count2 AS RatingId2Count,
           @Count3 AS RatingId3Count;
END