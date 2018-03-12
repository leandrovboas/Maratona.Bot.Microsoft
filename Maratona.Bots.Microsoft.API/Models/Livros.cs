using System;
using System.Collections.Generic;

namespace Maratona.Bots.Microsoft.API.Models
{
    public class Livros
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int QtdPaginas { get; set; }
        public string UrlImagem { get; set; }

        public static List<Livros> GetListaLivros() => new List<Livros> {
            new Livros{
                    Id = Guid.NewGuid(),
                    Nome = "Sociedade do Anel",
                    Autor = "J. R. R. Tolkien",
                    Editora = "Martins Fontes",
                    QtdPaginas = 468,
                    UrlImagem = "https://images.livrariasaraiva.com.br/imagemnet/imagem.aspx/?pro_id=452744&qld=90&l=430&a=-1"
                },
            new Livros{
                    Id = Guid.NewGuid(),
                    Nome = "As Duas Torres",
                    Autor = "J. R. R. Tolkien",
                    Editora = "Martins Fontes",
                    QtdPaginas = 388,
                    UrlImagem = "https://upload.wikimedia.org/wikipedia/pt/thumb/5/56/Asduastorres.jpg/230px-Asduastorres.jpg"
                },
            new Livros
                {
                    Id = Guid.NewGuid(),
                    Nome = "O Retorno do Rei",
                    Autor = "J. R. R. Tolkien",
                    Editora = "Martins Fontes",
                    QtdPaginas = 431,
                    UrlImagem = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ8mhfSkhDXbHlol-PWLjb1pE8Hz8fsPUKp1Ri46X6JPAWxSpI6Gg"
                },
            new Livros
                {
                    Id = Guid.NewGuid(),
                    Nome = "Daemon",
                    Autor = "Daniel Suárez",
                    Editora = "Planeta do Brasil",
                    QtdPaginas = 432,
                    UrlImagem = "https://pladlivrosbr1.cdnstatics.com/usuaris/libros/fotos/168/original/daemon_9788576655701.jpg"
                },
            new Livros
                {
                    Id = Guid.NewGuid(),
                    Nome = "Jogador número 1",
                    Autor = "Ernest Cline",
                    Editora = "Leya",
                    QtdPaginas = 464,
                    UrlImagem = "https://images.livrariasaraiva.com.br/imagemnet/imagem.aspx/?pro_id=3891319&qld=90&l=430&a=-1"
                },
            new Livros
                {
                    Id = Guid.NewGuid(),
                    Nome = "Jogos Mentais",
                    Autor = "Teri Terry",
                    Editora = "Farol Literário",
                    QtdPaginas = 480,
                    UrlImagem = "https://images.livrariasaraiva.com.br/imagemnet/imagem.aspx/?pro_id=9191102&qld=90&l=430&a=-1"
                }
        };
    }
}
