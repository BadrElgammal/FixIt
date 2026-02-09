using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixIt.Domain.Entities;


namespace FixIt.Infrastructure.Context
{
    public class FIXITDbContext:DbContext
    {
        public FIXITDbContext()
        { }
        public FIXITDbContext(DbContextOptions<FIXITDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkerProfile> WorkerProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<WithdrawRequest> WithdrawRequests { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================= Transactions =================
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromWallet)
                .WithMany(w => w.FromTransactions)
                .HasForeignKey(t => t.FromWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToWallet)
                .WithMany(w => w.ToTransactions)
                .HasForeignKey(t => t.ToWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= Reviews =================
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Request)
                .WithOne(sr => sr.Review)
                .HasForeignKey<Review>(r => r.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ReviewedWorker)
                .WithMany(w => w.Reviews)
                .HasForeignKey(r => r.ReviewedWorkerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= ChatRooms =================
            modelBuilder.Entity<ChatRoom>()
                .HasOne(c => c.Client)
                .WithMany(u => u.ClientChatRooms)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatRoom>()
                .HasOne(c => c.Worker)
                .WithMany(u => u.WorkerChatRooms)
                .HasForeignKey(c => c.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= Favorites =================
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Client)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Worker)
                .WithMany(w => w.Favorites)
                .HasForeignKey(f => f.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= ServiceRequests =================
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Worker)
                .WithMany(w => w.ReceivedRequests)
                .HasForeignKey(sr => sr.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= User Indexes =================
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phone)
                .IsUnique();




            modelBuilder.Entity<User>()
    .HasOne(u => u.Wallet)
    .WithOne(w => w.User)
    .HasForeignKey<User>(u => u.WalletId)
    .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
