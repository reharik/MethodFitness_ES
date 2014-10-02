namespace MF.Core.Messages.Events
{
    public class Contact
    {
        public Contact(string firstName, string lastName, string emailAddress, string phoneMobile= "", string phoneSecondary= "")
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneMobile = phoneMobile;
            PhoneSecondary = phoneSecondary;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string PhoneMobile { get; set; }
        public string PhoneSecondary { get; set; }
    }
}