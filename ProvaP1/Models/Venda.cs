﻿namespace ProvaP1.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }
        public int Quantidade { get; set; }
    }
}
