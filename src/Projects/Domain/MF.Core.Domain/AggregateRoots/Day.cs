using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MF.Core.Infrastructure.GES;
using MF.Core.Messages.Command;
using MF.Core.Messages.Events;

namespace MF.Core.Domain.AggregateRoots
{
    public class Day : AggregateBase
    {
        private List<Appointment> _appointments;
        #region Handle
        public void Handle(BookHalfHourSession cmd)
        {
            var startEndTime = new StartEndTime(cmd.StartTime, 30);
            ExpectNoConflictingAppointmentsForTrainer(cmd.TrainerDisplay.TrainerId, startEndTime);

            RaiseEvent(new HalfHourSessionBooked(
                           Guid.NewGuid(),
                           cmd.Location,
                           cmd.TrainerDisplay,
                           cmd.ClientDisplay,
                           cmd.AppointmentDate,
                           cmd.StartTime,
                           startEndTime.EndTime,
                           cmd.Notes));
        }

        public void Handle(BookFullHourSession cmd)
        {
            var startEndTime = new StartEndTime(cmd.StartTime, 60);
            ExpectNoConflictingAppointmentsForTrainer(cmd.TrainerDisplay.TrainerId, startEndTime);

            RaiseEvent(new HalfHourSessionBooked(
                           Guid.NewGuid(),
                           cmd.Location,
                           cmd.TrainerDisplay,
                           cmd.ClientDisplay,
                           cmd.AppointmentDate,
                           cmd.StartTime,
                           startEndTime.EndTime,
                           cmd.Notes));
        }


        #endregion 
        #region Apply

        public void Apply(HalfHourSessionBooked vent)
        {
            _appointments.Add(new Appointment(vent.Id,
                                new StartEndTime(vent.StartTime, 30),
                                vent.Location,
                                vent.TrainerDisplay.TrainerId, 
                                vent.AppointmentDate));
        }

        public void Apply(FullHourSessionBooked vent)
        {
            _appointments.Add(new Appointment(vent.Id,
                                new StartEndTime(vent.StartTime, 30),
                                vent.Location,
                                vent.TrainerDisplay.TrainerId,
                                vent.AppointmentDate));
        }

        #endregion 
        #region Expect
        private void ExpectNoConflictingAppointmentsForTrainer(Guid trainerId, StartEndTime set)
        {
            _appointments.Where(x => x.TrainerId == trainerId).ToList().ForEach(set.ExpectAppointmentNotConcurrent);
        }
        #endregion
    }

    public class StartEndTime
    {
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }
        private DateTime StartDateTime;
        public StartEndTime(string startTime, int duration)
        {
            expectStartTimeInValidFormat(startTime);
            StartTime = startTime;
            convertStringToDateTime(startTime);
            addDuration(duration);
        }

        private void addDuration(int duration)
        {
            var endTime = StartDateTime.AddMinutes(duration);
            EndTime = endTime.ToShortTimeString();
        }

        private void convertStringToDateTime(string startTime)
        {
            StartDateTime = DateTime.Parse(startTime); 
        }

        private void expectStartTimeInValidFormat(string startTime)
        {
            if (!Regex.IsMatch(startTime,
                @"^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$"))
            {
                throw new Exception(string.Format("Start time not in proper format; {0}", startTime));
            }
        }

        public void ExpectAppointmentNotConcurrent(Appointment appt)
        {
            var dts = getSrcTargetDTs(appt);
            if (DateTime.Compare(dts.TargetET, dts.SrcST) > 0 && DateTime.Compare(dts.TargetST, dts.SrcET) <=0) { throw new Exception(); }
            if (DateTime.Compare(dts.TargetST, dts.SrcET) < 0 && DateTime.Compare(dts.TargetET, dts.SrcST) >0) { throw new Exception(); }
        }

        private SrcTargetDTs getSrcTargetDTs(Appointment appt)
        {
            return new SrcTargetDTs
                {
                    TargetST = StartDateTime,
                    TargetET = DateTime.Parse(EndTime),
                    SrcST = DateTime.Parse(appt.StartEndTime.StartTime),
                    SrcET = DateTime.Parse(appt.StartEndTime.EndTime)
                };
        }
    }

    public class SrcTargetDTs
    {
        public DateTime TargetST { get; set; }
        public DateTime TargetET { get; set; }
        public DateTime SrcST { get; set; }
        public DateTime SrcET { get; set; }
    }

    public class Appointment
    {
        public Appointment(Guid id, StartEndTime startEndTime, string location, Guid trainerId, DateTime appointmentDate)
        {
            Id = id;
            StartEndTime = startEndTime;
            Location = location;
            TrainerId = trainerId;
            AppointmentDate = appointmentDate;
        }

        public Guid Id { get; private set; }
        public StartEndTime StartEndTime { get; private set; }
        public string Location { get; private set; }
        public Guid TrainerId { get; private set; }
        public DateTime AppointmentDate { get; private set; }
    }
}
