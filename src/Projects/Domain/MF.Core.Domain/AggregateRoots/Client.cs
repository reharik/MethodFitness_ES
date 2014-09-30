using System;
using MF.Core.Infrastructure.GES;
using MF.Core.Messages.Command;
using MF.Core.Messages.Events;

namespace MF.Core.Domain.AggregateRoots
{
    public class Client : AggregateBase
    {
        private DateTime _startDate;

        public Client() :  this(false)
        {
        }

        public Client(bool isNew)
        {
            if (isNew)
            {
                RaiseEvent(new ClientCreated(Guid.NewGuid()));
            }
        }

        #region Handle
        public void Handle(SignUpTrainerGeneratedClient cmd)
        {
            ExpectEmailAddressValid(cmd.EmailAddress);
            RaiseEvent(new TrainerGeneratedClientSignedUp(Id,
                                        cmd.FirstName,
                                        cmd.LastName,
                                        cmd.EmailAddress,
                                        cmd.Phone,
                                        cmd.TrainerId,
                                        cmd.SourceNotes,
                                        cmd.StartDate));
        }

        public void Handle(SignUpHouseGeneratedClient cmd)
        {
            ExpectEmailAddressValid(cmd.EmailAddress);
            RaiseEvent(new HouseGeneratedClientSignedUp(Id,
                                        cmd.FirstName,
                                        cmd.LastName,
                                        cmd.EmailAddress,
                                        cmd.Phone,
                                        cmd.TrainerId,
                                        cmd.Source,
                                        cmd.SourceNotes,
                                        cmd.StartDate));
        }

        #endregion 
        #region Apply
        public void Apply(ClientCreated vent)
        {
            Id = vent.Id;
        }

        public void Apply(TrainerGeneratedClientSignedUp vent)
        {
            _startDate = vent.StartDate;
        }

        public void Apply(HouseGeneratedClientSignedUp vent)
        {
            _startDate = vent.StartDate;
        }
        #endregion 
        #region Expect
     
        private void ExpectEmailAddressValid(string emailAddress)
        {
            if (emailAddress.Length <= 2)
            {
                throw new Exception("invalid email yo.");
            }
        }

        #endregion

    }
}