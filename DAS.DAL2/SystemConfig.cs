namespace DAS.DAL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemConfig")]
    public partial class SystemConfig
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PropertyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Value { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
