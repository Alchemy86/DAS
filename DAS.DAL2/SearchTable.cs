namespace DAS.DAL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SearchTable")]
    public partial class SearchTable
    {
        [Key]
        [Column(Order = 0)]
        public Guid ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SearchValue { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string FieldName { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        [StringLength(50)]
        public string DataType { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemOrder { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid UserID { get; set; }
    }
}
