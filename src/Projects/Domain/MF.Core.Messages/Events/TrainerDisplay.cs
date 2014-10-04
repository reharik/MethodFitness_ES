using System;

namespace MF.Core.Messages.Events
{
    public class TrainerDisplay
    {
        public TrainerDisplay(Guid trainerId, string trainerName, string color = null)
        {
            TrainerId = trainerId;
            TrainerName = trainerName;
            Color = color;
        }

        public Guid TrainerId { get; private set; }
        public string TrainerName { get; private set; }
        public string Color { get; private set; }
    }
}