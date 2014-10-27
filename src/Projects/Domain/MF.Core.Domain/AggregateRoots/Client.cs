using System;
using MF.Core.Infrastructure.GES;
using MF.Core.Messages.Command;
using MF.Core.Messages.Events;

namespace MF.Core.Domain.AggregateRoots
{
    public class Client : AggregateBase
    {
        private DateTime _startDate;
        private bool _isArchived;

        #region Handle
        public void Handle(SignUpTrainerGeneratedClient cmd)
        {
            ExpectEmailAddressValid(cmd.Contact.EmailAddress);
            RaiseEvent(new TrainerGeneratedClientSignedUp(Guid.NewGuid(),
                                        cmd.Contact,
                                        cmd.Address,
                                        cmd.TrainerId,
                                        cmd.SourceNotes,
                                        cmd.StartDate,
                                        cmd.Dob));
        }

        public void Handle(SignUpHouseGeneratedClient cmd)
        {
            ExpectEmailAddressValid(cmd.Contact.EmailAddress);
            RaiseEvent(new HouseGeneratedClientSignedUp(Guid.NewGuid(),
                                        cmd.Contact,
                                        cmd.Address,
                                        cmd.TrainerId,
                                        cmd.Source,
                                        cmd.SourceNotes,
                                        cmd.StartDate,
                                        cmd.Dob));
        }

        public void Handle(UnArchiveClient unArchiveClient)
        {
            ExpectArchived();
            RaiseEvent(new ClientUnArchived(Id, DateTime.Now));
        }

        public void Handle(ArchiveClient archiveClient)
        {
            ExpectNotArchived();
            RaiseEvent(new ClientArchived(Id, DateTime.Now));
        }

        #endregion 
        #region Apply
   
        public void Apply(TrainerGeneratedClientSignedUp vent)
        {
            Id = vent.Id;
        }

        public void Apply(HouseGeneratedClientSignedUp vent)
        {
            Id = vent.Id;
        }

        public void Apply(ClientArchived vent)
        {
            _isArchived = true;
        }

        public void Apply(ClientUnArchived vent)
        {
            _isArchived = false;
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

        private void ExpectNotArchived()
        {
            if (_isArchived)
            {
                throw new Exception("User already archived!");
            }
        }

        private void ExpectArchived()
        {
            if (!_isArchived)
            {
                throw new Exception("User is not archived!");
            }
        }
        
        #endregion

        
    }
}