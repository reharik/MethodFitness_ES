using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class UserHandler : HandlerBase, IHandler
    {
        private readonly IMongoRepository _repository;

        public UserHandler(IMongoRepository repository) : base(repository)
        {
            _repository = repository;
            register(typeof(TrainerHired), trainerHired);
            register(typeof(UserLoggedIn), userLoggedIn);
            register(typeof(UserArchived), userArchived);
            register(typeof(UserUnArchived), userUnArchived);
        }

        private void userLoggedIn(IGESEvent x)
        {
            var userLoggedIn = (UserLoggedIn) x;
            var userLogins = new UserLogins
                {
                    UserName = userLoggedIn.UserName,
                    Id = userLoggedIn.Id,
                    Token = userLoggedIn.Token,
                    Date = userLoggedIn.Now
                };
            _mongoRepository.Save(userLogins);
        }

        private void trainerHired(IGESEvent x)
        {
            var trainerHired = (TrainerHired)x;
            var user = new User();
            user.Id = trainerHired.Id;
            user.UserName = trainerHired.Credentials.UserName;
            user.FirstName = trainerHired.Contact.FirstName;
            user.LastName = trainerHired.Contact.LastName;
            user.EmailAddress = trainerHired.Contact.EmailAddress;
            user.Address1 = trainerHired.Address.Address1;
            user.Address2 = trainerHired.Address.Address2;
            user.City = trainerHired.Address.City;
            user.State = trainerHired.Address.State;
            user.ZipCode = trainerHired.Address.ZipCode;
            user.PhoneMobile = trainerHired.Contact.PhoneMobile;
            user.PhoneSecondary = trainerHired.Contact.PhoneSecondary;
            user.Dob = trainerHired.Dob;
            _mongoRepository.Save(user);
        }

        private void userArchived(IGESEvent x)
        {
            var userArchived = (UserArchived)x;
            var user = _repository.Get<User>(u => u.Id == userArchived.UserId);
            user.Archived = true;
            user.ArchivedDate = userArchived.ArchivedDate;
            _mongoRepository.Save(user);
        }

        private void userUnArchived(IGESEvent x)
        {
            var userUnArchived = (UserUnArchived)x;
            var user = _repository.Get<User>(u => u.Id == userUnArchived.UserId);
            user.Archived = false;
            user.ArchivedDate = userUnArchived.UnArchivedDate;
            _mongoRepository.Save(user);
        }
    }
}