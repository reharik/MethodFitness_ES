namespace MF.Core.Messages.Events
{
    public class Contact
    {
        public Contact(string firstName, string lastName, string emailAddress, string phone= "", string phoneSecondary= "")
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Phone = phone;
            PhoneSecondary = phoneSecondary;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; set; }
        public string PhoneSecondary { get; set; }
    }
}