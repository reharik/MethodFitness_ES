/****** Object:  StoredProcedure [dbo].[TrainerMetric]    Script Date: 1/31/2013 7:18:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[TrainerMetric] 
	@StartDate DateTime ,
	@EndDate DateTime ,
	@TrainerId int
AS
BEGIN
	SET NOCOUNT ON;

select 
	t.firstname + ' ' + t.lastName as Trainer,
	c.firstname + ' ' + c.lastName as Client,
	@StartDate as StartDate,
	@EndDate as EndDate,

    SUM(case when s.appointmentType = 'Hour' then 1 else 0 end) Hour,
    SUM(case when s.appointmentType = 'Half Hour' then 1 else 0 end) HalfHour,
    SUM(case when s.appointmentType = 'Pair' then 1 else 0 end) Pair,

	Cast(
		SUM(case when s.appointmentType = 'Hour' then 1 else 0 end) 
		+ CAST(SUM(case when s.appointmentType = 'Half Hour' then 1 else 0 end) as decimal(10,4))/2
		+ SUM(case when s.appointmentType = 'Pair' then 1 else 0 end) as decimal(10,1)) TotalHours,

	CAST(CAST(DATEDIFF(DAY, @StartDate, @EndDate) +1 as decimal(10,4))/7 as decimal(10,2)) NumberOfWeeks,
	CASE WHEN CAST(DATEDIFF(DAY, @StartDate, @EndDate) +1 as decimal(10,4))/7 >0
		 THEN 
			Cast(
				(SUM(case when s.appointmentType = 'Hour' then 1 else 0 end) 
					+ CAST(SUM(case when s.appointmentType = 'Half Hour' then 1 else 0 end) as decimal(10,4))/2
					+ SUM(case when s.appointmentType = 'Pair' then 1 else 0 end))
				/
				(CAST(DATEDIFF(DAY, @StartDate, @EndDate) +1 as decimal(10,4))/7)
			as decimal(10,2))
		ELSE 0
	END as HoursPerWeek

From Client as c 
left outer join [session] as s on c.Entityid = s.ClientId
left join [user] as t on s.trainerid = t.entityid
left join appointment as a on s.appointmentid = a.entityId
where t.entityid = @TrainerId 
	AND @StartDate <= CAST(a.[date] as DATE) and @EndDate >= CAST(a.[date] as DATE)
group by c.firstname, c.lastname, t.firstname, t.lastname
having (SUM(case when s.appointmentType = 'Hour' then 1 else 0 end) 
		+ CAST(SUM(case when s.appointmentType = 'Half Hour' then 1 else 0 end) as decimal(10,4))/2
		+ SUM(case when s.appointmentType = 'Pair' then 1 else 0 end)) > 0

order by c.LastName

END