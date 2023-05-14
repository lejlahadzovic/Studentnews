using Microsoft.EntityFrameworkCore;
using StudentOglasi.Autentifikacija.Models;
using StudentOglasi.Models;

namespace StudentOglasi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<Univerzitet> Univerzitet { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Objava> Objava { get; set; }
        public DbSet<Fakultet> Fakultet { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<IzdavacSmjestaja> IzdavacSmjestaja { get; set; }
        public DbSet<Stipenditor> Stipenditor { get; set; }
        public DbSet<Firma> Firma { get; set; }
        public DbSet<UposlenikStipenditora> UposlenikStipenditora { get; set; }
        public DbSet<UposlenikFirme> UposlenikFirme { get; set; }
        public DbSet<Komentar> Komentar { get; set; }
        public DbSet<ReferentFakulteta> ReferentFakulteta { get; set; }
        public DbSet<ReferentUniverziteta> ReferentUniverziteta { get; set; }
        public DbSet<Oglas> Oglas { get; set; }
        public DbSet<Stipendija> Stipendija { get; set; }
        public DbSet<Praksa> Praksa { get; set; }
        public DbSet<Smjestaj> Smjestaj { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Ocjena> Ocjena { get; set; }
        public DbSet<PrijavaPraksa> PrijavaPraksa { get; set; }
        public DbSet<PrijavaStipendija> PrijavaStipendija { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<Lokacija> Lokacija { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rezervacija>()
                .HasKey(x => new { x.StudentId, x.SmjestajId });

            modelBuilder.Entity<Rezervacija>()
            .HasOne(r => r.Smjestaj)
            .WithMany(p => p.Rezervacije)
            .HasForeignKey(r => r.SmjestajId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.Student)
                .WithMany(t => t.Rezervacije)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<PrijavaPraksa>()
               .HasKey(x => new { x.StudentId, x.PraksaId });

            modelBuilder.Entity<PrijavaPraksa>()
            .HasOne(pp => pp.Praksa)
            .WithMany(p => p.Prijave)
            .HasForeignKey(pp => pp.PraksaId)
            .OnDelete(DeleteBehavior.ClientSetNull); 

            modelBuilder.Entity<PrijavaPraksa>()
                .HasOne(pp => pp.Student)
                .WithMany(t => t.PrijavePrakse)
                .HasForeignKey(pp => pp.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<PrijavaStipendija>()
             .HasKey(x => new { x.StudentId, x.StipendijaID });

            modelBuilder.Entity<PrijavaStipendija>()
            .HasOne(ps => ps.Stipendija)
            .WithMany(p => p.Prijave)
            .HasForeignKey(ps => ps.StipendijaID)
            .OnDelete(DeleteBehavior.ClientSetNull); ;

            modelBuilder.Entity<PrijavaStipendija>()
                .HasOne(ps => ps.Student)
                .WithMany(t => t.PrijaveStipendije)
                .HasForeignKey(ps => ps.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
