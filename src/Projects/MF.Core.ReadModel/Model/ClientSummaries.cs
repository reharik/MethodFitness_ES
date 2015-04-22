using System;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.ReadModel.Model
{
    public class ClientSummaries : IReadModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public bool Archived { get; set; }
        public DateTime ArchivedDate { get; set; }
    }
}