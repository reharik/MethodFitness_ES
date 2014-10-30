using System;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;

namespace MF.Core.ReadModel.Model
{
    public class Users : IReadModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public bool Archived { get; set; }
        public DateTime ArchivedDate { get; set; }
        public DateTime Dob { get; set; }
    }
}