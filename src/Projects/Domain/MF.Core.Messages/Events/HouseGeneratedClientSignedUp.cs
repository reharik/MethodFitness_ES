﻿using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class HouseGeneratedClientSignedUp : IGESEvent
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public Guid TrainerId { get; private set; }
        public string Source { get; private set; }
        public string SourceNotes { get; private set; }
        public DateTime StartDate { get; private set; }

        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public HouseGeneratedClientSignedUp(Guid id,
            string firstName, 
            string lastName, 
            string emailAddress,
            string phone,
            Guid trainerId,
            string source,
            string sourceNotes,
            DateTime startDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Phone = phone;
            TrainerId = trainerId;
            Source = source;
            SourceNotes = sourceNotes;
            StartDate = startDate;
            EventType = "HouseGeneratedClientSignedUp";
        }

    }
}