namespace DAS.DAL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alerts
    {
        [Key]
        [Column(Order = 0)]
        public Guid AlertID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(800)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime TriggerTime { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool Processed { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid AuctionID { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool Custom { get; set; }

        [StringLength(50)]
        public string AlertType { get; set; }

        public virtual Auctions Auctions { get; set; }
    }
}
