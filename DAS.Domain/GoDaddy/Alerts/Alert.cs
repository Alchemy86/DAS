using System;
using System.ComponentModel;
using DAS.Domain.Enum;

namespace DAS.Domain.GoDaddy.Alerts
{
    public class Alert
    {
        public Guid AlertId { get; private set; }

        public Auction Auction { get; private set; }

        public string Description => Type.ToName();

        public DateTime TriggerTime { get; private set; }

        public bool Processed { get; private set; }

        public AlertType Type { get; private set; }

        public Alert(Guid alertId, AlertType type, Auction auction, DateTime triggerTime, bool processed)
        {
            AlertId = alertId;
            Type = type;
            Auction = auction;
            TriggerTime = triggerTime;
            Processed = processed;
        }
    }


    public enum AlertType
    {
        [Description("Win Alert")]
        Win,
        [Description("1 Hour Alert")]
        Reminder1Hour,
        [Description("12 Hour Alert")]
        Reminder12Hours
    }
}
