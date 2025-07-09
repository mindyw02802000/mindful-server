using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Dal.Do;

public partial class dbcontext : DbContext
{
    public dbcontext()
    {
    }

    public dbcontext(DbContextOptions<dbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetailingModel> DetailingModels { get; set; }

    public virtual DbSet<DetailingOrder> DetailingOrders { get; set; }

    public virtual DbSet<Modell> Modells { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
=> optionsBuilder.UseNpgsql("Host=dpg-d1eklbemcj7s73eficfg-a.frankfurt-postgres.render.com;Port=5432;Database=mindful_db_jjb0;Username=mindful_user;Password=vGE6JhJw5A1JZKuF2hXtvlDOrcIEbCH1;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetailingModel>(entity =>
        {
            entity.ToTable("detailingModels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Size).HasColumnName("size");

            entity.HasOne(d => d.IdModelNavigation).WithMany(p => p.DetailingModels)
                .HasForeignKey(d => d.IdModel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detailing__IdMod__3A4CA8FD");
        });

        modelBuilder.Entity<DetailingOrder>(entity =>
        {
            entity.ToTable("detailingOrders");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.DetailingOrders)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detailing__IdOrd__70DDC3D8");
        });

        modelBuilder.Entity<Modell>(entity =>
        {
            entity.HasKey(e => e.IdModel).HasName("PK__modell__C2F00099D25283D9");

            entity.ToTable("modell");

            entity.Property(e => e.IdModel).ValueGeneratedNever();
            entity.Property(e => e.Kategory).HasMaxLength(30);
            entity.Property(e => e.Picture).HasColumnType("text");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Order__C38F3009EE80D13A");

            entity.ToTable("Order");

            entity.Property(e => e.Contact)
                .HasMaxLength(30)
                .HasColumnName("Contact ");
            entity.Property(e => e.CostPrice).HasColumnName("CostPrice ");
            entity.Property(e => e.DateOfEvent).HasColumnType("date");
            entity.Property(e => e.DateOfOrdder).HasColumnType("date");
            entity.Property(e => e.PhoneContact)
                .HasMaxLength(10)
                .HasColumnName("PhoneContact ");
            entity.Property(e => e.ProvisionAddress).HasMaxLength(30);
            entity.Property(e => e.SchoolName).HasMaxLength(30);

            entity.HasOne(d => d.IdSchoolNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdSchool)
                .HasConstraintName("FK__Order__IdSchool__6FE99F9F");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payment__3214EC0725F1B4D8");

            entity.ToTable("payment");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.IdSchool).HasName("PK__school__B54450856C7F6819");

            entity.ToTable("school");

            entity.Property(e => e.AddressSchool)
                .HasMaxLength(30)
                .HasColumnName("addressSchool");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
