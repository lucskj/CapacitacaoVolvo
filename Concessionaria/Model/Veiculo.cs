using System.Text.Json.Serialization;

namespace Concessionaria.Model
{
    public partial class Veiculo
    {

        public int IdVeiculo { get; set;}


        public int? IdVenda { get; set; }
        public string NumChassi { get; set; }=null!;
        public string Modelo { get; set; }=null!;
        public short Ano { get; set; }
        public string Cor { get; set; }=null!;
        public decimal Valor { get; set; }
        public int Quilometragem { get; set; }
        public string VersaoSistema { get; set; }=null!;

        [JsonIgnore]
        public virtual Venda? IdVendaNavigation { get; }
        public virtual ICollection<AcessorioVeiculo>? AcessorioVeiculos { get; }

        public Veiculo()
        {
            AcessorioVeiculos = new HashSet<AcessorioVeiculo>();
        }


        //Verifica a validade do numero do chassi, lança uma Exception caso não seja válido
        public void checkChassi(){
            if(NumChassi.Length!=17){
                throw new CustomException("Chassi inválido(Numero de caracteres):"+NumChassi,"Chassi inválido, O chassi possui 17 digitos");
            }
            foreach(char c in NumChassi){
                if(!char.IsLetterOrDigit(c)){
                    throw new CustomException("Chassi Inválido(Digitos):"+NumChassi,"Chassi inválido, insira apenas digitos e numeros");
                }
            }
        }
    }
}
