using System.Text.Json.Serialization;

namespace Concessionaria.Model
{
    public partial class Venda
    {

        public int IdVenda { get; set;}
        public int IdVendedor { get; set; }
        public int IdProprietario { get; set; }
        public DateTime DataVenda { get; set; }


        public decimal ValordaVenda { get; set; }

        [JsonIgnore]
        public virtual Proprietario? IdProprietarioNavigation { get;}

        [JsonIgnore]
        public virtual Vendedor? IdVendedorNavigation { get; }
        public virtual ICollection<Veiculo> Veiculos { get;}

        public Venda()
        {
            Veiculos = new HashSet<Veiculo>();
        }
    }
}
