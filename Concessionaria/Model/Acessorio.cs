using System.Text.Json.Serialization;

namespace Concessionaria.Model
{
    public partial class Acessorio
    {
        public int IdAcessorio { get;set;}
        public string Nome { get; set; }=null!;
        public virtual ICollection<AcessorioVeiculo>? AcessorioVeiculos { get; }

        public Acessorio()
        {
            AcessorioVeiculos = new HashSet<AcessorioVeiculo>();
        }
    }
}
