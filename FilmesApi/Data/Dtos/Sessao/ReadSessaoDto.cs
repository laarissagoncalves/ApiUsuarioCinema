﻿using FilmesAPI.Models;
using System;

namespace FilmesApi.Data.Dtos.Sessao
{
    public class ReadSessaoDto
    {
        public int Id { get; set; }
        public Cinema Cinema { get; set; }
        public Filme Filme { get; set; }
        public DateTime EncerramentoSessao { get; set; }
        public DateTime InicioSessao { get; set; }
    }
}
