namespace ProvaP1.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public double Salario { get; set; }
    }
}
