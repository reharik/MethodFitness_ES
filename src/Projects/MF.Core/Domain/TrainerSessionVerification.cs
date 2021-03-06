﻿using System.Collections.Generic;
using CC.DataValidation.Attributes;

namespace MF.Core.Domain
{
    public class TrainerSessionVerification:DomainEntity
    {
        [DoNotValidate]
        public virtual User Trainer { get; set; }
        public virtual double Total { get; set; }
        private IList<Session> _trainerApprovedSessionItems = new List<Session>();
        public virtual IEnumerable<Session> TrainerApprovedSessionItems { get { return _trainerApprovedSessionItems; } }
        public virtual void RemoveTrainerApprovedSessionItem(Session session)
        {
            _trainerApprovedSessionItems.Remove(session);
        }
        public virtual void AddTrainerApprovedSessionItem(Session session)
        {
            if (_trainerApprovedSessionItems.Contains(session)) return;
            _trainerApprovedSessionItems.Add(session);
        }
    }
}