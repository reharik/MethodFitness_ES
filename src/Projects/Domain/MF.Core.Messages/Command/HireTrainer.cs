using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class HireTrainer : IGESEvent
    {
        public HireTrainer(
            string userName, 
            string password, 
            string firstName, 
            string lastName, 
            string emailAddress,
            string address1,
            string address2,
            string city,
            string state,
            string zipCode,
            string phoneMobile,
            string phoneSecondary,
            DateTime dob)
        {
            UserName = userName;
            EmailAddress = emailAddress;
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            ZipCode = zipCode;
            PhoneMobile = phoneMobile;
            PhoneSecondary = phoneSecondary;
            Dob = dob;
            LastName = lastName;
            FirstName = firstName;
            Password = password;
            EventType = GetType().Name;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string PhoneMobile { get; private set; }
        public string PhoneSecondary { get; private set; }
        public DateTime Dob { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}