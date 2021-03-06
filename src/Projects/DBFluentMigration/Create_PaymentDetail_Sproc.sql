/****** Object:  StoredProcedure [dbo].[DailyPayments]    Script Date: 2/14/2013 2:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [PaymentDetail] 
	@EntityId int
AS
BEGIN
	SET NOCOUNT ON;
SELECT FullHour,
FullHourPrice,
FullHourTenPack,
FullHourTenPackPrice,
HalfHour,
HalfHourPrice,
HalfHourTenPack,
HalfHourTenPackPrice,
Pair,
PairPrice,
PairTenPack,
PairTenPackPrice
FROM            Payment 
WHERE Payment.EntityId = @EntityId
END
