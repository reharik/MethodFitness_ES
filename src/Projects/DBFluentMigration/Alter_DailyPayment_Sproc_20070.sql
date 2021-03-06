/****** Object:  StoredProcedure [dbo].[DailyPayments]    Script Date: 1/27/2013 2:38:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[DailyPayments] 
	@StartDate	DateTime, 
	@EndDate	DateTime,
	@TrainerId	int,
	@ClientId	int
AS
BEGIN
	SET NOCOUNT ON;

SELECT p.CreatedDate,
		t.FirstName + ' ' + t.LastName AS Trainer,
		c.FirstName + ' ' + c.LastName AS Client, 
		p.PaymentTotal,
		p.EntityId,
		@StartDate as StartDate,
		@EndDate as EndDate
FROM Payment as p INNER JOIN [User] AS t ON p.CreatedById = t.EntityId 
				INNER JOIN Client as c ON p.ClientId = c.EntityId
WHERE @StartDate <= CAST(p.CreatedDate as DATE)
		AND @EndDate >= CAST(p.CreatedDate as DATE)
		AND (@TrainerId = 0 OR p.CreatedById = @TrainerId)
		AND (@ClientId = 0 OR p.ClientId = @ClientId)
END
