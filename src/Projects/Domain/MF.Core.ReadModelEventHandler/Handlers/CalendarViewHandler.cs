using System;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.ReadModelEventHandler.Handlers
{
    public class CalendarViewHandler : HandlerBase, IHandler
    {
        public CalendarViewHandler(IMongoRepository mongoRepository) : base(mongoRepository)
        {
            register(typeof(HalfHourSessionBooked), singleSessionBooked);
            register(typeof(FullHourSessionBooked), singleSessionBooked);
        }

        private IReadModel singleSessionBooked(IGESEvent x)
        {
            var vent = (SingleSessionBooked)x;
            var item = new CalendarAppointment
                {
                    Id = Guid.NewGuid(),
                    TrainerId = vent.TrainerDisplay.TrainerId,
                    ClientDisplay = vent.ClientDisplay.ClientName,
                    StartTime = vent.StartTime,
                    EndTime = vent.EndTime,
                    Date = vent.AppointmentDate,
                    Location = vent.Location,
                    Color = vent.TrainerDisplay.Color
                };
            return item;
        }
    }

    public class CalendarAppointment : IReadModel
    {
        public Guid Id { get; set; }
        public Guid TrainerId { get; set; }
        public string ClientDisplay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Color { get; set; }
    }
}