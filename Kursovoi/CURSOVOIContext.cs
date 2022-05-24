using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kursovoi
{
    public partial class CURSOVOIContext : DbContext
    {
        public CURSOVOIContext()
        {
        }

        public CURSOVOIContext(DbContextOptions<CURSOVOIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Bookmarks> Bookmarks { get; set; }
        public virtual DbSet<Description> Description { get; set; }
        public virtual DbSet<Photochepter> Photochepter { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<Translation> Translation { get; set; }
        public virtual DbSet<Translator> Translator { get; set; }
        public virtual DbSet<TypeOfComics> TypeOfComics { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-JINHBT2\\SQLEXPRESS;Database=CURSOVOI;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.CodeAuthor);

                entity.ToTable("AUTHOR");

                entity.Property(e => e.CodeAuthor)
                    .HasColumnName("Code_Author")
                    .ValueGeneratedNever();

                entity.Property(e => e.Author1).HasMaxLength(50);
            });

            modelBuilder.Entity<Bookmarks>(entity =>
            {
                entity.HasKey(e => e.CodeBookmarks);

                entity.ToTable("BOOKMARKS");

                entity.Property(e => e.CodeBookmarks)
                    .HasColumnName("Code_Bookmarks")
                    .ValueGeneratedNever();

                entity.Property(e => e.CodeTitle).HasColumnName("Code_Title");

                entity.Property(e => e.UnicCodeUsers).HasColumnName("Unic_Code_Users");

                entity.HasOne(d => d.CodeTitleNavigation)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.CodeTitle)
                    .HasConstraintName("FK_BOOKMARKS_TITLE");

                entity.HasOne(d => d.UnicCodeUsersNavigation)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.UnicCodeUsers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BOOKMARKS_USERS");
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasKey(e => e.CodeDescription);

                entity.ToTable("DESCRIPTION");

                entity.Property(e => e.CodeDescription)
                    .HasColumnName("Code_Description")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description1)
                    .HasColumnName("Description")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Photochepter>(entity =>
            {
                entity.HasKey(e => e.CodePhChepter);

                entity.ToTable("PHOTOCHEPTER");

                entity.HasIndex(e => e.CodeTitle);

                entity.Property(e => e.CodePhChepter)
                    .HasColumnName("Code_Ph_Chepter")
                    .ValueGeneratedNever();

                entity.Property(e => e.PathPhChepter)
                    .HasColumnName("Path_Ph_Chepter")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => e.CodeTitle);

                entity.ToTable("TITLE");

                entity.Property(e => e.CodeTitle)
                    .HasColumnName("Code_Title")
                    .ValueGeneratedNever();

                entity.Property(e => e.CodeAuthor).HasColumnName("Code_Author");

                entity.Property(e => e.CodeCodeTypeOfComics).HasColumnName("Code_Code_TypeOfComics");

                entity.Property(e => e.CodeDescription).HasColumnName("Code_Description");

                entity.Property(e => e.CodePhChepter).HasColumnName("Code_Ph_Chepter");

                entity.Property(e => e.CodeTranslation).HasColumnName("Code_Translation");

                entity.Property(e => e.CodeTranslator).HasColumnName("Code_Translator");

                entity.Property(e => e.Genre).IsRequired();

                entity.Property(e => e.Link).HasColumnType("text");

                entity.Property(e => e.NameOfTitle)
                    .HasColumnName("Name_Of_Title")
                    .HasMaxLength(80);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.HasOne(d => d.CodeAuthorNavigation)
                    .WithMany(p => p.Title)
                    .HasForeignKey(d => d.CodeAuthor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TITLE_AUTHOR");

                entity.HasOne(d => d.CodeCodeTypeOfComicsNavigation)
                    .WithMany(p => p.Title)
                    .HasForeignKey(d => d.CodeCodeTypeOfComics)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TITLE_TYPE_OF_COMICS");

                entity.HasOne(d => d.CodeDescriptionNavigation)
                    .WithMany(p => p.Title)
                    .HasForeignKey(d => d.CodeDescription)
                    .HasConstraintName("FK_TITLE_DESCRIPTION");

                entity.HasOne(d => d.CodePhChepterNavigation)
                    .WithMany(p => p.Title)
                    .HasForeignKey(d => d.CodePhChepter)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TITLE_PHOTOCHEPTER");

                entity.HasOne(d => d.CodeTranslationNavigation)
                    .WithMany(p => p.Title)
                    .HasForeignKey(d => d.CodeTranslation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TITLE_TRANSLATION");

                entity.HasOne(d => d.CodeTranslatorNavigation)
                    .WithMany(p => p.Title)
                    .HasForeignKey(d => d.CodeTranslator)
                    .HasConstraintName("FK_TITLE_TRANSLATOR");
            });

            modelBuilder.Entity<Translation>(entity =>
            {
                entity.HasKey(e => e.CodeTranslation);

                entity.ToTable("TRANSLATION");

                entity.Property(e => e.CodeTranslation)
                    .HasColumnName("Code_Translation")
                    .ValueGeneratedNever();

                entity.Property(e => e.Translation1)
                    .HasColumnName("Translation")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Translator>(entity =>
            {
                entity.HasKey(e => e.CodeTranslator);

                entity.ToTable("TRANSLATOR");

                entity.Property(e => e.CodeTranslator)
                    .HasColumnName("Code_Translator")
                    .ValueGeneratedNever();

                entity.Property(e => e.PhotoTranslator)
                    .HasColumnName("Photo_Translator")
                    .HasMaxLength(50);

                entity.Property(e => e.Translator1)
                    .HasColumnName("Translator")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TypeOfComics>(entity =>
            {
                entity.HasKey(e => e.CodeTypeOfComics);

                entity.ToTable("TYPE_OF_COMICS");

                entity.Property(e => e.CodeTypeOfComics)
                    .HasColumnName("Code_TypeOfComics")
                    .ValueGeneratedNever();

                entity.Property(e => e.TypeOfComics1)
                    .HasColumnName("TypeOfComics")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UnicCodeUsers);

                entity.ToTable("USERS");

                entity.Property(e => e.UnicCodeUsers)
                    .HasColumnName("Unic_Code_Users")
                    .ValueGeneratedNever();

                entity.Property(e => e.PhotoUsers)
                    .HasColumnName("Photo_Users")
                    .HasMaxLength(120);

                entity.Property(e => e.UsersLoqin).HasMaxLength(50);

                entity.Property(e => e.UsersPassword)
                    .IsRequired()
                    .HasColumnName("Users_Password")
                    .HasMaxLength(50);

                entity.Property(e => e.UsersRole).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
