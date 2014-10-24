using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class UserSummaryHandler : HandlerBase, IHandler
    {
        private readonly IMongoRepository _repository;

        public UserSummaryHandler(IMongoRepository repository) : base(repository)
        {
            _repository = repository;
            register(typeof(TrainerHired), trainerHired);
            register(typeof(UserArchived), userArchived);
            register(typeof(UserUnArchived), userUnArchived);
        }

        private void trainerHired(IGESEvent x)
        {
            var trainerHired = (TrainerHired)x;
            var user = new UserSummaries();
            user.FirstName = trainerHired.Contact.FirstName;
            user.LastName = trainerHired.Contact.LastName;
            user.EmailAddress = trainerHired.Contact.EmailAddress;
            user.PhoneMobile = trainerHired.Contact.Phone;
            _mongoRepository.Save(user);
        }

        private void userArchived(IGESEvent x)
        {
            var userArchived = (UserArchived)x;
            var user = _repository.Get<UserSummaries>(u => u.Id == userArchived.UserId);
            user.Archived = true;
            user.ArchivedDate = userArchived.ArchivedDate;
            _mongoRepository.Save(user);
        }

        private void userUnArchived(IGESEvent x)
        {
            var userUnArchived = (UserUnArchived)x;
            var user = _repository.Get<UserSummaries>(u => u.Id == userUnArchived.UserId);
            user.Archived = false;
            user.ArchivedDate = userUnArchived.UnArchivedDate;
            _mongoRepository.Save(user);
        }
    }
}