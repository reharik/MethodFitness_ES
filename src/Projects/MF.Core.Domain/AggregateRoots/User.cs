using System;
using MF.Core.Infrastructure.GES;
using MF.Core.Messages.Command;
using MF.Core.Messages.Events;

namespace MF.Core.Domain.AggregateRoots
{
    public class User : AggregateBase
    {
        private string _password;
        private bool _loggedIn;
        private bool _isArchived;

        #region Handle
        public void Handle(HireTrainer cmd)
        {
            ExpectPasswordSecure(cmd.Credentials.Password);
            ExpectEmailAddressValid(cmd.Contact.EmailAddress);
            RaiseEvent(new TrainerHired(Id,
                                        cmd.Credentials, 
                                        cmd.Contact, 
                                        cmd.Address,
                                        cmd.Dob));
        }

        public void Handle(LoginUser cmd)
        {
            ExpectNotLoggedOn();
            ExpectCorrectPassword(cmd.Credentials.Password);
            var token = CreateToken();
            RaiseEvent(new UserLoggedIn(Id, cmd.Credentials.UserName, token, DateTime.Now));
        }

        public void Handle(ArchiveUser cmd)
        {
            ExpectNotArchived();
            RaiseEvent(new UserArchived(Id, DateTime.Now));
        }

        public void Handle(UnArchiveUser cmd)
        {
            ExpectArchived();
            RaiseEvent(new UserUnArchived(Id, DateTime.Now));
        }

        private Guid CreateToken()
        {
            return Guid.NewGuid();
        }

        #endregion 
        #region Apply
        public void Apply(TrainerHired vent)
        {
            _password = vent.Credentials.Password;
        }

        public void Apply(UserArchived vent)
        {
            _isArchived = true;
        }

        public void Apply(UserUnArchived vent)
        {
            _isArchived = false;
        }
        #endregion 
        #region Expect
        private void ExpectCorrectPassword(string password)
        {
            if (password != _password)
            {
                throw new Exception("Incorrect password dog");
            }
        }

        private void ExpectNotLoggedOn()
        {
            if (_loggedIn)
            {
                throw new Exception("User already logged in!");
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
        
        private void ExpectEmailAddressValid(string emailAddress)
        {
            if (emailAddress.Length <= 2)
            {
                throw new Exception("invalid email yo.");
            }
        }

        private void ExpectPasswordSecure(string password)
        {
            if (password.Length <= 2)
            {
                throw new Exception("Password needs to be longer than two characters");
            }
        }
        #endregion

    }
}