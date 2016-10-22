namespace DAS.DAL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SearchConfig")]
    public partial class SearchConfig
    {
        [Key]
        [Column(Order = 0)]
        public Guid ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool Active { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool RequirePR { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRMin { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRMax { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticBacklinksMin { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticBacklinksMAX { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticTrustFlowMin { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticTrustFlowMax { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticCitationFlowMax { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticCitationFlowMin { get; set; }

        [Key]
        [Column(Order = 13)]
        public bool MOZPA { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MOZPAMin { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MOZPAMax { get; set; }

        [Key]
        [Column(Order = 16)]
        public bool MOZDA { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MOZDAMin { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MOZDAMax { get; set; }

        [Key]
        [Column(Order = 19)]
        public bool MajesticIPS { get; set; }

        [Key]
        [Column(Order = 20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticIPSMin { get; set; }

        [Key]
        [Column(Order = 21)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajesticIPSMax { get; set; }

        [Key]
        [Column(Order = 22)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DomainAgeMin { get; set; }

        [Key]
        [Column(Order = 23)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DomainAgeMax { get; set; }

        [Key]
        [Column(Order = 24)]
        public bool DomainPrice { get; set; }

        [Key]
        [Column(Order = 25)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DomainPriceMin { get; set; }

        [Key]
        [Column(Order = 26)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DomainPriceMax { get; set; }

        [Key]
        [Column(Order = 27)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacebookLikesMin { get; set; }

        [Key]
        [Column(Order = 28)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacebookLikesMax { get; set; }

        [Key]
        [Column(Order = 29)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacebookSharesMin { get; set; }

        [Key]
        [Column(Order = 30)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacebookSharesMax { get; set; }

        [Key]
        [Column(Order = 31)]
        public bool TwitterShares { get; set; }

        [Key]
        [Column(Order = 32)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TwitterSharesMin { get; set; }

        [Key]
        [Column(Order = 33)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TwitterSharesMax { get; set; }

        [Key]
        [Column(Order = 34)]
        public bool RedditShares { get; set; }

        [Key]
        [Column(Order = 35)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RedditSharesMin { get; set; }

        [Key]
        [Column(Order = 36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RedditSharesMax { get; set; }

        [Key]
        [Column(Order = 37)]
        public bool PintrestShares { get; set; }

        [Key]
        [Column(Order = 38)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PintrestSharesMin { get; set; }

        [Key]
        [Column(Order = 39)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PintrestSharesMax { get; set; }

        [Key]
        [Column(Order = 40)]
        public bool GPlusShares { get; set; }

        [Key]
        [Column(Order = 41)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GPlusSharesMin { get; set; }

        [Key]
        [Column(Order = 42)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GPlusSharesMax { get; set; }

        [Key]
        [Column(Order = 43)]
        public bool Auction { get; set; }

        [Key]
        [Column(Order = 44)]
        public bool BuyNow { get; set; }

        [Key]
        [Column(Order = 45)]
        public bool BarginBin { get; set; }

        [Key]
        [Column(Order = 46)]
        public bool CloseOut { get; set; }

        [Key]
        [Column(Order = 47)]
        public bool PendingDelete { get; set; }

        [Key]
        [Column(Order = 48)]
        public bool Expired { get; set; }

        [Key]
        [Column(Order = 49)]
        public bool GoDaddy { get; set; }

        [Key]
        [Column(Order = 50)]
        public bool NameJet { get; set; }

        [Key]
        [Column(Order = 51)]
        public bool Dynadot { get; set; }

        [Key]
        [Column(Order = 52)]
        public bool SnapName { get; set; }

        [Key]
        [Column(Order = 53)]
        public bool Sedo { get; set; }

        [Key]
        [Column(Order = 54)]
        public bool FakePR { get; set; }

        [Key]
        [Column(Order = 55)]
        public bool FakeAlexa { get; set; }

        [Key]
        [Column(Order = 56)]
        public bool RequireSemrushKey { get; set; }

        [Key]
        [Column(Order = 57)]
        public bool MajMillion { get; set; }

        [Key]
        [Column(Order = 58)]
        public bool QuantMillion { get; set; }

        [Key]
        [Column(Order = 59)]
        public bool AlexARankRequired { get; set; }

        [Key]
        [Column(Order = 60)]
        public bool SemrushRankReq { get; set; }

        [Key]
        [Column(Order = 61)]
        public bool SimilarWebRankReq { get; set; }

        [Key]
        [Column(Order = 62)]
        public bool RequireAv { get; set; }

        [Key]
        [Column(Order = 63)]
        public bool HideAdult { get; set; }

        [Key]
        [Column(Order = 64)]
        public bool HideSpammy { get; set; }

        [Key]
        [Column(Order = 65)]
        public bool HideBrand { get; set; }

        [Key]
        [Column(Order = 66)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndInLower { get; set; }

        [Key]
        [Column(Order = 67)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndInUpper { get; set; }

        [Key]
        [Column(Order = 68)]
        [StringLength(50)]
        public string ParentCat { get; set; }

        [Key]
        [Column(Order = 69)]
        [StringLength(50)]
        public string ChildCat { get; set; }

        [Key]
        [Column(Order = 70)]
        [StringLength(50)]
        public string ParentTopCat { get; set; }

        [Key]
        [Column(Order = 71)]
        [StringLength(50)]
        public string ChildTopCat { get; set; }

        [Key]
        [Column(Order = 72)]
        public bool GoogleIndex { get; set; }

        [Key]
        [Column(Order = 73)]
        public bool dmoz { get; set; }

        [Key]
        [Column(Order = 74)]
        public bool AllowDash { get; set; }

        [Key]
        [Column(Order = 75)]
        public bool AllowDigits { get; set; }

        [Key]
        [Column(Order = 76)]
        public bool OnlyDigits { get; set; }

        [Key]
        [Column(Order = 77)]
        public bool SalesTypeOffer { get; set; }

        [Key]
        [Column(Order = 78)]
        [StringLength(50)]
        public string SearchType { get; set; }

        [Key]
        [Column(Order = 79)]
        [StringLength(100)]
        public string Keyword { get; set; }

        [Key]
        [Column(Order = 80)]
        [StringLength(50)]
        public string KeywordSearchType { get; set; }

        [Key]
        [Column(Order = 81)]
        [StringLength(50)]
        public string Pattern { get; set; }

        [Key]
        [Column(Order = 82)]
        [StringLength(50)]
        public string PatternType { get; set; }

        [Key]
        [Column(Order = 83)]
        public bool Droplists { get; set; }

        [Key]
        [Column(Order = 84)]
        public bool RequireSemrushTraffic { get; set; }

        [Key]
        [Column(Order = 85)]
        public bool end_asia { get; set; }

        [Key]
        [Column(Order = 86)]
        public bool end_at { get; set; }

        [Key]
        [Column(Order = 87)]
        public bool end_au { get; set; }

        [Key]
        [Column(Order = 88)]
        public bool end_be { get; set; }

        [Key]
        [Column(Order = 89)]
        public bool end_biz { get; set; }

        [Key]
        [Column(Order = 90)]
        public bool end_ca { get; set; }

        [Key]
        [Column(Order = 91)]
        public bool end_cc { get; set; }

        [Key]
        [Column(Order = 92)]
        public bool end_ch { get; set; }

        [Key]
        [Column(Order = 93)]
        public bool end_co { get; set; }

        [Key]
        [Column(Order = 94)]
        public bool end_com { get; set; }

        [Key]
        [Column(Order = 95)]
        public bool end_de { get; set; }

        [Key]
        [Column(Order = 96)]
        public bool end_eu { get; set; }

        [Key]
        [Column(Order = 97)]
        public bool end_fr { get; set; }

        [Key]
        [Column(Order = 98)]
        public bool end_ie { get; set; }

        [Key]
        [Column(Order = 99)]
        public bool end_in { get; set; }

        [Key]
        [Column(Order = 100)]
        public bool end_info { get; set; }

        [Key]
        [Column(Order = 101)]
        public bool end_it { get; set; }

        [Key]
        [Column(Order = 102)]
        public bool end_me { get; set; }

        [Key]
        [Column(Order = 103)]
        public bool end_mobi { get; set; }

        [Key]
        [Column(Order = 104)]
        public bool end_mx { get; set; }

        [Key]
        [Column(Order = 105)]
        public bool end_net { get; set; }

        [Key]
        [Column(Order = 106)]
        public bool end_nl { get; set; }

        [Key]
        [Column(Order = 107)]
        public bool end_nu { get; set; }

        [Key]
        [Column(Order = 108)]
        public bool end_org { get; set; }

        [Key]
        [Column(Order = 109)]
        public bool end_pl { get; set; }

        [Key]
        [Column(Order = 110)]
        public bool end_ru { get; set; }

        [Key]
        [Column(Order = 111)]
        public bool end_se { get; set; }

        [Key]
        [Column(Order = 112)]
        public bool end_su { get; set; }

        [Key]
        [Column(Order = 113)]
        public bool end_tv { get; set; }

        [Key]
        [Column(Order = 114)]
        public bool end_uk { get; set; }

        [Key]
        [Column(Order = 115)]
        public bool end_us { get; set; }

        [Key]
        [Column(Order = 116)]
        public bool end_misc { get; set; }
    }
}
