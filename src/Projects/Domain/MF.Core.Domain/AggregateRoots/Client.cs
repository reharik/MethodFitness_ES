using System;
using MF.Core.Infrastructure.GES;
using MF.Core.Messages.Command;
using MF.Core.Messages.Events;

namespace MF.Core.Domain.AggregateRoots
{
    public class Client : AggregateBase
    {

        public Client() :  this(false)
        {
        }

        public User(bool isNew)
        {
            Register<UserLoggedIn>(e => { });
        
            if (isNew)
            {
                RaiseEvent(new UserCreated(Guid.NewGuid()));
            }
        }

        #region Handle
        public void Handle(HireTrainer cmd)
        {
            ExpectPasswordSecure(cmd.Password);
            ExpectEmailAddressValid(cmd.EmailAddress);
            RaiseEvent(new TrainerHired(Id,
                                        cmd.UserName,
                                        cmd.Password,
                                        cmd.FirstName,
                                        cmd.LastName,
                                        cmd.EmailAddress,
                                        cmd.Address1,
                                        cmd.Address2,
                                        cmd.City,
                                        cmd.State,
                                        cmd.ZipCode,
                                        cmd.PhoneMobile,
                                        cmd.PhoneSecondary,
                                        cmd.Dob));
        }

        public void Handle(LoginUser cmd)
        {
            ExpectNotLoggedOn();
            ExpectCorrectPassword(cmd.Password);
            var token = CreateToken();
            RaiseEvent(new UserLoggedIn(Id, cmd.UserName, token, DateTime.Now));
        }

        private Guid CreateToken()
        {
            return Guid.NewGuid();
        }

        #endregion 
        #region Apply
        public void Apply(UserCreated vent)
        {
            Id = vent.Id;
        }

        public void Apply(TrainerHired vent)
        {
            _password = vent.Password;
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