using System.Text.Json.Serialization;

namespace Concessionaria.Model
{
    public partial class AcessorioVeiculo
    {
        public int IdAcessorioVeiculo { get; set;}
        public int IdVeiculo { get; set; }
        public int IdAcessorio { get; set; }
        public virtual Acessorio IdAcessorioNavigation { get;}=null!;
        public virtual Veiculo IdVeiculoNavigation { get; }=null!;

        public AcessorioVeiculo(){
        }

        //Construtor com ids necessários
        public AcessorioVeiculo(int acessorio,int veiculo){
            this.IdAcessorio=acessorio;
            this.IdVeiculo=veiculo;
        }
    }
}
