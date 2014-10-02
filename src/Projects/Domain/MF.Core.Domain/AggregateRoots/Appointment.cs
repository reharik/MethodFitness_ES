using System;
using MF.Core.Infrastructure.GES;
using MF.Core.Messages.Command;
using MF.Core.Messages.Events;

namespace MF.Core.Domain.AggregateRoots
{
    public class Appointment : AggregateBase
    {
        #region Handle
        public void Handle(BookHalfHourSession cmd)
        {
            RaiseEvent(new HalfHourSessionBooked(Id));
        }


        #endregion 
        #region Apply

        public void Apply(HalfHourSessionBooked vent)
        {
        }

     
        #endregion 
        #region Expect
        private void ExpectCorrectPassword(string password)
        {
           
        }

        #endregion

    }
}