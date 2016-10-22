namespace DAS.DAL2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AuctionHistory> AuctionHistory { get; set; }
        public virtual DbSet<Auctions> Auctions { get; set; }
        public virtual DbSet<AuctionSearch> AuctionSearch { get; set; }
        public virtual DbSet<BackOrders> BackOrders { get; set; }
        public virtual DbSet<GoDaddyAccount> GoDaddyAccount { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AdvSearch> AdvSearch { get; set; }
        public virtual DbSet<Alerts> Alerts { get; set; }
        public virtual DbSet<EventLog> EventLog { get; set; }
        public virtual DbSet<SearchConfig> SearchConfig { get; set; }
        public virtual DbSet<SearchLayout> SearchLayout { get; set; }
        public virtual DbSet<SearchTable> SearchTable { get; set; }
        public virtual DbSet<SystemConfig> SystemConfig { get; set; }
        public virtual DbSet<AuctionHistoryView> AuctionHistoryView { get; set; }
        public virtual DbSet<Chart_AuctionHistory> Chart_AuctionHistory { get; set; }
        public virtual DbSet<Chart_AuctionsEnding> Chart_AuctionsEnding { get; set; }
        public virtual DbSet<Chart_PopularDomainsThisMonth> Chart_PopularDomainsThisMonth { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoDaddyAccount>()
                .HasMany(e => e.BackOrders)
                .WithRequired(e => e.GoDaddyAccount1)
                .HasForeignKey(e => e.GoDaddyAccount);
        }
    }
}
