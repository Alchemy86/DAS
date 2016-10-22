namespace DAS.DAL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdvSearch")]
    public partial class AdvSearch
    {
        [Key]
        [Column(Order = 0)]
        public Guid AdvSearchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string DomainName { get; set; }

        [StringLength(500)]
        public string DomainLink { get; set; }

        public int? Age { get; set; }

        public int? PageRank { get; set; }

        public int? MOZDA { get; set; }

        public int? MOZPA { get; set; }

        public decimal? MozRank { get; set; }

        public decimal? MozTrust { get; set; }

        public int? CF { get; set; }

        public int? TF { get; set; }

        public decimal? AlexARank { get; set; }

        public int? MozDom { get; set; }

        public int? MajDom { get; set; }

        [StringLength(200)]
        public string SimilarWeb { get; set; }

        [StringLength(50)]
        public string SemTarf { get; set; }

        public int? DomainPrice { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? EsitmateEnd { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid UserID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string Ref { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsAuction { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool IsGOdaddy { get; set; }
    }
}
