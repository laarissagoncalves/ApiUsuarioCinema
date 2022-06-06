using FilmesApi.Models;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //nossa entidade do tipo Endereco
            builder.Entity<Endereco>()
                //tem um cinema
                .HasOne(endereco => endereco.Cinema)
                //logo o cinema possui um endereco
                .WithOne(cinema => cinema.Endereco)
                //chave que une as tabelas, está alojada em cinema e é nosso EnderecoId
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);

            builder.Entity<Cinema>()
                //um cinema possui um gerente
                .HasOne(cinema => cinema.Gerente)
                //que possui um ou mais cinemas
                .WithMany(gerente => gerente.Cinemas)
                //(cinema => cinema.GerenteId).IsRequired(false) assim o id pode ser null, assim o cinema pode existir sem a necessidade de uma chave de um gerente
                .HasForeignKey(cinema => cinema.GerenteId);
            //ao deletar deixa de ser Cascata (default migration) e passa a ser Restrito 
            //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Filme)
                .WithMany(filme => filme.Sessoes)
                .HasForeignKey(sessao => sessao.FilmeId);

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessoes)
                .HasForeignKey(sessao => sessao.CinemaId);
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
    }
}