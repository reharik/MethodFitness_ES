using System;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.ReadModel.Model
{
    public class CalendarAppointments : IReadModel
    {
        public string Id { get; set; }
        public Guid TrainerId { get; set; }
        public string ClientDisplay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Color { get; set; }
    }
}