using System;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;

namespace MF.Core.ReadModel.Model
{
    public class Clients : IReadModel
    {
        public Guid Id { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public string Source { get; set; }
        public string SourceNotes { get; set; }
        public bool Archived { get; set; }
        public DateTime ArchivedDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}