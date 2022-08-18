using System.Text.Json.Serialization;

namespace Concessionaria.Model
{
    public partial class Vendedor
    {

        public int IdVendedor { get;set; }
        public string Nome { get; set; }=null!;
        public decimal SalarioBase {get; set;}

        public virtual ICollection<Venda> Venda { get;}

        public Vendedor()
        {
            Venda = new HashSet<Venda>();
            //Inicializa o salário como o mínimo, caso não seja definido
            this.SalarioBase = 1212;
        }
    }
}


