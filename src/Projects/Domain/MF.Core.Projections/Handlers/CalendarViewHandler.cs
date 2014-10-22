using System;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class CalendarViewHandler : HandlerBase, IHandler
    {
        public CalendarViewHandler(IMongoRepository mongoRepository) : base(mongoRepository)
        {
            register(typeof(HalfHourSessionBooked), singleSessionBooked);
            register(typeof(FullHourSessionBooked), singleSessionBooked);
        }

        private void singleSessionBooked(IGESEvent x)
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
            _mongoRepository.Save(item);
        }
    }
}