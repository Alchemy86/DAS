namespace DAS.DAL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventLog")]
    public partial class EventLog
    {
        [Key]
        [Column(Order = 0)]
        public Guid ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Event { get; set; }

        public string Message { get; set; }
    }
}
