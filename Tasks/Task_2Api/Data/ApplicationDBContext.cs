using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Task_2Api.Models;

namespace Task_2Api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<ApprovalModel> Approvals { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<DiagonsisModel> Diagnoses { get; set; }
        public DbSet<AttachmentModel> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceModel>()
                .HasOne(a => a.Approval)
                .WithMany(a => a.Services)
                .HasForeignKey(a => a.ApprovalModelId);

            modelBuilder.Entity<DiagonsisModel>()
                .HasOne(a => a.Approval)
                .WithMany(a => a.Diagnoses)
                .HasForeignKey(a => a.ApprovalModelId);

            modelBuilder.Entity<AttachmentModel>()
                .HasOne(a => a.Approval)
                .WithMany(a => a.Attachments)
                .HasForeignKey(a => a.ApprovalModelId);

            modelBuilder.Entity<ServiceModel>()
    .Ignore(s => s.Approval);

            modelBuilder.Entity<DiagonsisModel
                >()
                .Ignore(d => d.Approval);
        }
    }
}

