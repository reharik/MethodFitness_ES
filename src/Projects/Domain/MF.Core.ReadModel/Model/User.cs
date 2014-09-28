﻿using System;
using EventSpike.Infrastructure.SharedModels;

namespace EventSpike.ReadModel.Model
{
    public class User : IReadModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}