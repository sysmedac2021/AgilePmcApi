using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AgilePMC.Models
{
    public partial class agileadminContext : DbContext
    {
        public agileadminContext()
        {
        }

        public agileadminContext(DbContextOptions<agileadminContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLogin> AdminLogins { get; set; } = null!;
        public virtual DbSet<ContactU> ContactUs { get; set; } = null!;
        public virtual DbSet<NewsLetter> NewsLetters { get; set; } = null!;
        public virtual DbSet<Slider> Sliders { get; set; } = null!;
        public virtual DbSet<SmtpDetail> SmtpDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=166.62.6.38;port=3306;database=agileadmin;user id=agile;password=Agilepmc@2023;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.6.51-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<AdminLogin>(entity =>
            {
                entity.ToTable("AdminLogin");

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(250);
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.HasKey(e => e.ContactUsId)
                    .HasName("PRIMARY");

                entity.Property(e => e.ContactUsId).HasColumnType("bigint(20)");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ApplyingFor).HasMaxLength(250);

                entity.Property(e => e.AssociateFor).HasMaxLength(250);

                entity.Property(e => e.BusinessProfille).HasMaxLength(250);

                entity.Property(e => e.ContactName).HasMaxLength(250);

                entity.Property(e => e.ContactUsType).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Experiance).HasColumnType("bigint(20)");

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.OrganizationName).HasMaxLength(250);

                entity.Property(e => e.OrganizationType).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Qualification).HasMaxLength(500);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.SurName).HasMaxLength(250);

                entity.Property(e => e.Website).HasMaxLength(500);
            });

            modelBuilder.Entity<NewsLetter>(entity =>
            {
                entity.ToTable("NewsLetter");

                entity.Property(e => e.NewsLetterId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.TemplateImageUrl).HasMaxLength(1000);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.ToTable("Slider");

                entity.Property(e => e.SliderId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.SliderImageUrl).HasMaxLength(1000);

                entity.Property(e => e.SliderName).HasMaxLength(250);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<SmtpDetail>(entity =>
            {
                entity.ToTable("SMTP_Details");

                entity.Property(e => e.SmtpDetailId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("SMTP_Detail_Id");

                entity.Property(e => e.SmtpDomainName)
                    .HasMaxLength(250)
                    .HasColumnName("SMTP_Domain_Name");

                entity.Property(e => e.SmtpPassword)
                    .HasMaxLength(250)
                    .HasColumnName("SMTP_Password");

                entity.Property(e => e.SmtpPort)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("SMTP_Port");

                entity.Property(e => e.SmtpUserEmail)
                    .HasMaxLength(250)
                    .HasColumnName("SMTP_User_Email");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
