using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class UserHandler : HandlerBase, IHandler
    {
        private readonly IMongoRepository _mongoRepository;

        public UserHandler(IMongoRepository mongoRepository, IUIResponsePoster uiResponsePoster)
            : base(mongoRepository, uiResponsePoster)
        {
            _mongoRepository = mongoRepository;
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
                    Id = userLoggedIn.Id.ToString(),
                    Token = userLoggedIn.Token,
                    Date = userLoggedIn.Now
                };
            base._mongoRepository.Save(userLogins);
        }

        private void trainerHired(IGESEvent x)
        {
            var trainerHired = (TrainerHired)x;
            var user = new Users();
            user.Id = trainerHired.Id.ToString();
            user.UserName = trainerHired.Credentials.UserName;
            user.Contact= trainerHired.Contact;
            user.Address = trainerHired.Address;
            user.Dob = trainerHired.Dob;
            base._mongoRepository.Save(user);
        }

        private void userArchived(IGESEvent x)
        {
            var userArchived = (UserArchived)x;
            var user = _mongoRepository.Get<Users>(u => u.Id == userArchived.UserId.ToString());
            user.Archived = true;
            user.ArchivedDate = userArchived.ArchivedDate;
            base._mongoRepository.Save(user);
        }

        private void userUnArchived(IGESEvent x)
        {
            var userUnArchived = (UserUnArchived)x;
            var user = _mongoRepository.Get<Users>(u => u.Id == userUnArchived.UserId.ToString());
            user.Archived = false;
            user.ArchivedDate = userUnArchived.UnArchivedDate;
            base._mongoRepository.Save(user);
        }
    }
}