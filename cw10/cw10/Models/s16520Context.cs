using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cw10.Models
{
    public partial class s16520Context : DbContext
    {
        public s16520Context()
        {
        }

        public s16520Context(DbContextOptions<s16520Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Budzet> Budzet { get; set; }
        public virtual DbSet<Dept> Dept { get; set; }
        public virtual DbSet<Emp> Emp { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Gosc> Gosc { get; set; }
        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Pokoj> Pokoj { get; set; }
        public virtual DbSet<Rezerwacja> Rezerwacja { get; set; }
        public virtual DbSet<Salgrade> Salgrade { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16520;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budzet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("budzet");

                entity.Property(e => e.Wartosc).HasColumnName("wartosc");
            });

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DEPT");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Dname)
                    .HasColumnName("DNAME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Loc)
                    .HasColumnName("LOC")
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EMP");

                entity.Property(e => e.Comm).HasColumnName("COMM");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Empno).HasColumnName("EMPNO");

                entity.Property(e => e.Ename)
                    .HasColumnName("ENAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hiredate)
                    .HasColumnName("HIREDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Job)
                    .HasColumnName("JOB")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Mgr).HasColumnName("MGR");

                entity.Property(e => e.Sal).HasColumnName("SAL");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment)
                    .HasName("Enrollment_pk");

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<Gosc>(entity =>
            {
                entity.HasKey(e => e.IdGosc)
                    .HasName("PK__Gosc__8126AB6DDE09CD7D");

                entity.Property(e => e.IdGosc).ValueGeneratedNever();

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProcentRabatu).HasColumnName("Procent_rabatu");
            });

            modelBuilder.Entity<Kategoria>(entity =>
            {
                entity.HasKey(e => e.IdKategoria)
                    .HasName("PK__Kategori__31412B2622FF6F6A");

                entity.Property(e => e.IdKategoria).ValueGeneratedNever();

                entity.Property(e => e.Cena).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pokoj>(entity =>
            {
                entity.HasKey(e => e.NrPokoju)
                    .HasName("PK__Pokoj__18804ABE8D408A6D");

                entity.Property(e => e.NrPokoju).ValueGeneratedNever();

                entity.Property(e => e.LiczbaMiejsc).HasColumnName("Liczba_miejsc");

                entity.HasOne(d => d.IdKategoriaNavigation)
                    .WithMany(p => p.Pokoj)
                    .HasForeignKey(d => d.IdKategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pokoj__IdKategor__49C3F6B7");
            });

            modelBuilder.Entity<Rezerwacja>(entity =>
            {
                entity.HasKey(e => e.IdRezerwacja)
                    .HasName("PK__Rezerwac__68F5E1865F597E3B");

                entity.Property(e => e.IdRezerwacja).ValueGeneratedNever();

                entity.Property(e => e.DataDo).HasColumnType("datetime");

                entity.Property(e => e.DataOd).HasColumnType("datetime");

                entity.HasOne(d => d.IdGoscNavigation)
                    .WithMany(p => p.Rezerwacja)
                    .HasForeignKey(d => d.IdGosc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rezerwacj__IdGos__4CA06362");

                entity.HasOne(d => d.NrPokojuNavigation)
                    .WithMany(p => p.Rezerwacja)
                    .HasForeignKey(d => d.NrPokoju)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rezerwacj__NrPok__4D94879B");
            });

            modelBuilder.Entity<Salgrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SALGRADE");

                entity.Property(e => e.Grade).HasColumnName("GRADE");

                entity.Property(e => e.Hisal).HasColumnName("HISAL");

                entity.Property(e => e.Losal).HasColumnName("LOSAL");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber)
                    .HasName("Student_pk");

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy)
                    .HasName("Studies_pk");

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
