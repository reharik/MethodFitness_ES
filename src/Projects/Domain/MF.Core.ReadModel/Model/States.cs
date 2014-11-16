using System;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.ReadModel.Model
{
    public class States : IReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}