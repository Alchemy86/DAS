using System;
using DAS.Domain.Enum;
using DAS.Domain.GoDaddy.Alerts;

namespace DAS.DAL2
{
    public partial class Alerts
    {
        public Alert ToDomainObject()
        {
            return new Alert(AlertID, (AlertType) Enum.Parse(typeof(AlertType), AlertType), Auctions.ToDomainObject(), TriggerTime, Processed);
        }

        public void FromDomainObject(Alert alert)
        {
            AlertID = alert.AlertId;
            Processed = alert.Processed;
            TriggerTime = alert.TriggerTime;
            AlertType = alert.Type.ToName();
            Description = alert.Description;
        }
    }
}
