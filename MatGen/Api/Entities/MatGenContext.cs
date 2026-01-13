using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities;

public partial class MatGenContext : DbContext
{
    public MatGenContext()
    {
    }

    public MatGenContext(DbContextOptions<MatGenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumItem> AlbumItems { get; set; }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<Cognom> Cognoms { get; set; }

    public virtual DbSet<Doc> Docs { get; set; }

    public virtual DbSet<DocCod> DocCods { get; set; }

    public virtual DbSet<DocFile> DocFiles { get; set; }

    public virtual DbSet<DocRel> DocRels { get; set; }

    public virtual DbSet<DocSrc> DocSrcs { get; set; }

    public virtual DbSet<DocTarget> DocTargets { get; set; }

    public virtual DbSet<Enlace> Enlaces { get; set; }

    public virtual DbSet<Firstnom> Firstnoms { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Pub> Pubs { get; set; }

    public virtual DbSet<Repo> Repos { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<VwArgenter> VwArgenters { get; set; }

    public virtual DbSet<VwDoc> VwDocs { get; set; }

    public virtual DbSet<VwEnlace> VwEnlaces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.74.52.10;TrustServerCertificate=true;Initial Catalog=MatGen;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("Album");

            entity.HasIndex(e => new { e.Year, e.Month, e.Day, e.Name }, "Album_Default").IsUnique();

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.FchCreated).HasDefaultValueSql("(getdate())", "DF_Album_FchCreated");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsrCreated).HasDefaultValue(new Guid("3555f3aa-3074-4c77-8742-25f8c9a4264a"), "DF_Album_UsrCreated");

            entity.HasOne(d => d.UsrCreatedNavigation).WithMany(p => p.Albums)
                .HasForeignKey(d => d.UsrCreated)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Album_UserAccount");
        });

        modelBuilder.Entity<AlbumItem>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("AlbumItem");

            entity.HasIndex(e => new { e.Album, e.Hash }, "AlbumItem_Default").IsUnique();

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.ContentType)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.FchCreated).HasDefaultValueSql("(getdate())", "DF_AlbumItem_FchCreated");
            entity.Property(e => e.Hash)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.UsrCreated).HasDefaultValue(new Guid("3555f3aa-3074-4c77-8742-25f8c9a4264a"), "DF_AlbumItem_UsrCreated");

            entity.HasOne(d => d.AlbumNavigation).WithMany(p => p.AlbumItems)
                .HasForeignKey(d => d.Album)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumItem_Album");

            entity.HasOne(d => d.UsrCreatedNavigation).WithMany(p => p.AlbumItems)
                .HasForeignKey(d => d.UsrCreated)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumItem_UserAccount");
        });

        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Container).IsUnicode(false);
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_Cita_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Pags)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Text).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cognom>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_COGNOM");

            entity.ToTable("Cognom");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_Cognom_Guid");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doc>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_DOC");

            entity.ToTable("Doc");

            entity.HasIndex(e => new { e.Fch, e.Src, e.Tit }, "IX_Doc_Src_Fch");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_DOC_Guid");
            entity.Property(e => e.Asin)
                .HasMaxLength(24)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExternalUrl).HasColumnType("text");
            entity.Property(e => e.Fch)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_DOC_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.FchLastEdited)
                .HasDefaultValueSql("(getdate())", "DF_Doc_FchLastEdited")
                .HasColumnType("datetime");
            entity.Property(e => e.Hash)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.OldCod).HasColumnName("Old_Cod");
            entity.Property(e => e.SrcDetail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_DOC_SrcDetail");
            entity.Property(e => e.Stream).HasColumnType("image");
            entity.Property(e => e.Thumbnail).HasColumnType("image");
            entity.Property(e => e.Tit)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Transcripcio).HasColumnType("text");

            entity.HasOne(d => d.HashNavigation).WithMany(p => p.Docs)
                .HasForeignKey(d => d.Hash)
                .HasConstraintName("FK_Doc_DocFile");
        });

        modelBuilder.Entity<DocCod>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_DOCCOD_1");

            entity.ToTable("DocCod");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_DOCCOD_Guid");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_DOCCOD_Esp");
            entity.Property(e => e.Obs).HasColumnType("text");
            entity.Property(e => e.Ord)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_DOCCOD_Ord");
        });

        modelBuilder.Entity<DocFile>(entity =>
        {
            entity.HasKey(e => e.Hash);

            entity.ToTable("DocFile");

            entity.HasIndex(e => e.FchCreated, "IX_Docfile_FchCreated").IsDescending();

            entity.Property(e => e.Hash)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_DocFile_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Pags).HasDefaultValue(0, "DF_DocFile_Pags");
            entity.Property(e => e.Sha256)
                .HasMaxLength(44)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Size).HasDefaultValue(0, "DF_DocFile_Length");
            entity.Property(e => e.StreamMime).HasDefaultValue(4, "DF_DocFile_MimeStream");
            entity.Property(e => e.Thumbnail).HasColumnType("image");
            entity.Property(e => e.ThumbnailMime).HasDefaultValue(1, "DF_DocFile_MimeThumbnail");
        });

        modelBuilder.Entity<DocRel>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_DOCREL");

            entity.ToTable("DocRel");

            entity.HasIndex(e => new { e.SubjectePassiu, e.Ord }, "IX_DocRel_SubjectePassiuOrd");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_DOCREL_Guid");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ord)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_DOCREL_ORD");
        });

        modelBuilder.Entity<DocSrc>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_DOCSRC");

            entity.ToTable("DocSrc");

            entity.HasIndex(e => e.NomLlarg, "Xl_DocSrc_NomLlarg");

            entity.HasIndex(e => new { e.Parent, e.Ord, e.Nom }, "Xl_DocSrc_ParentOrdNom");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_DOCSRC_GUID");
            entity.Property(e => e.Asin)
                .HasMaxLength(24)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_DOCSRC_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Hash)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_DOCSRC_TXT");
            entity.Property(e => e.NomLlarg)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Obs).HasColumnType("text");
            entity.Property(e => e.Ord).HasDefaultValue(999, "DF_DOCSRC_ORD_1");
            entity.Property(e => e.Url).HasColumnType("text");

            entity.HasOne(d => d.HashNavigation).WithMany(p => p.DocSrcs)
                .HasForeignKey(d => d.Hash)
                .HasConstraintName("FK_DocSrc_DocFile");

            entity.HasOne(d => d.RepoNavigation).WithMany(p => p.DocSrcs)
                .HasForeignKey(d => d.Repo)
                .HasConstraintName("FK_DocSrc_Repo");
        });

        modelBuilder.Entity<DocTarget>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_DOCCONTACT");

            entity.ToTable("DocTarget");

            entity.HasIndex(e => new { e.Doc, e.Target, e.Rel }, "IX_DocContact_Doc_Contact_Rel").IsUnique();

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_DOCCONTACT_Guid");
            entity.Property(e => e.Cit).HasColumnName("CIT");
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_DocTarget_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Obs)
                .HasColumnType("text")
                .HasColumnName("OBS");
            entity.Property(e => e.TargetCod).HasDefaultValue(1, "DF_DocTarget_TargetCod");

            entity.HasOne(d => d.DocNavigation).WithMany(p => p.DocTargets)
                .HasForeignKey(d => d.Doc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCCONTACT_Doc");

            entity.HasOne(d => d.RelNavigation).WithMany(p => p.DocTargets)
                .HasForeignKey(d => d.Rel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCCONTACT_DOCREL");
        });

        modelBuilder.Entity<Enlace>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_ENLACE");

            entity.ToTable("Enlace");

            entity.HasIndex(e => new { e.Marit, e.NupciesMarit }, "IX_Enlace_Marit").IsUnique();

            entity.HasIndex(e => new { e.Marit, e.Muller }, "IX_Enlace_MaritMuller").IsUnique();

            entity.HasIndex(e => new { e.Muller, e.NupciesMuller }, "IX_Enlace_Muller").IsUnique();

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_ENLACE_Guid");
            entity.Property(e => e.Fch)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_ENLACE_Fch");
            entity.Property(e => e.Fch2)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_Enlace_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.FchQualifier)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Obs).HasColumnType("text");

            entity.HasOne(d => d.MaritNavigation).WithMany(p => p.EnlaceMaritNavigations)
                .HasForeignKey(d => d.Marit)
                .HasConstraintName("FK_ENLACE_MARIT");

            entity.HasOne(d => d.MullerNavigation).WithMany(p => p.EnlaceMullerNavigations)
                .HasForeignKey(d => d.Muller)
                .HasConstraintName("FK_ENLACE_MULLER");
        });

        modelBuilder.Entity<Firstnom>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_NOM");

            entity.ToTable("Firstnom");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_NOM_GUID");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Obs).HasColumnType("text");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("Location");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_Location_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NomLlarg)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_PERSON");

            entity.ToTable("Person");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_Person_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.FchFrom)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchFrom2)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchFromQualifier)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.FchLastEdited)
                .HasDefaultValueSql("(getdate())", "DF_Person_FchLastEdited")
                .HasColumnType("datetime");
            entity.Property(e => e.FchSepultura)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchSepultura2)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchSepulturaQualifier)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.FchTo)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchTo2)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchToQualifier)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Obs).HasColumnType("text");

            entity.HasOne(d => d.CognomNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.Cognom)
                .HasConstraintName("FK_PERSON_COGNOM");

            entity.HasOne(d => d.FirstnomNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.Firstnom)
                .HasConstraintName("FK_PERSON_FIRSTNOM");

            entity.HasOne(d => d.MareNavigation).WithMany(p => p.InverseMareNavigation)
                .HasForeignKey(d => d.Mare)
                .HasConstraintName("FK_PERSON_MARE");

            entity.HasOne(d => d.PareNavigation).WithMany(p => p.InversePareNavigation)
                .HasForeignKey(d => d.Pare)
                .HasConstraintName("FK_PERSON_PARE");

            entity.HasOne(d => d.ProfessionNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.Profession)
                .HasConstraintName("FK_PERSON_PROFESSIO");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_PROFESSIO_1");

            entity.ToTable("Profession");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_PROFESSIO_Guid");
            entity.Property(e => e.Llati)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_PROFESSIO_Llati");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_PROFESSIO_Nom");
            entity.Property(e => e.Obs).HasColumnType("text");
        });

        modelBuilder.Entity<Pub>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("Pub");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_Pub_Guid");
            entity.Property(e => e.Asin)
                .HasMaxLength(24)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FchCreated)
                .HasDefaultValueSql("(getdate())", "DF_Pub_FchCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Hash)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nom).HasMaxLength(60);

            entity.HasOne(d => d.HashNavigation).WithMany(p => p.Pubs)
                .HasForeignKey(d => d.Hash)
                .HasConstraintName("FK_Pub_DocFile");
        });

        modelBuilder.Entity<Repo>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("Repo");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.Abr)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Adr)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("ES", "DF_Repo_Country");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.EmailAddress, "NonClusteredIndex-20230102-173526");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Hash)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwArgenter>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwArgenters");

            entity.Property(e => e.FchFrom)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.FchTo)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Passantia)
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwDoc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwDocs");

            entity.Property(e => e.ExternalUrl).HasColumnType("text");
            entity.Property(e => e.Fch)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchCreated).HasColumnType("datetime");
            entity.Property(e => e.Hash)
                .HasMaxLength(24)
                .IsUnicode(false);
            entity.Property(e => e.SrcDetail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tit)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwEnlace>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwEnlaces");

            entity.Property(e => e.Fch)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Fch2)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FchQualifier)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
