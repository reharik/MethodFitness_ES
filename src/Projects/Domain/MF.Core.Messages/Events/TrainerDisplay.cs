using System;

namespace MF.Core.Messages.Events
{
    public class TrainerDisplay
    {
        public TrainerDisplay(Guid trainerId, string trainerName)
        {
            TrainerId = trainerId;
            TrainerName = trainerName;
        }

        public Guid TrainerId { get; private set; }
        public string TrainerName { get; private set; }
    }
}