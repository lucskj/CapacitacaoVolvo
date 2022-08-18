using System.Text.Json.Serialization;

namespace Concessionaria.Model
{
    public partial class Proprietario
    {
        public int IdProprietario { get;set; }
        public string Nome { get; set; }=null!;

        public string Documento {get;set;}=null!; 

        //Não vai aparecer em nenhum JSON, o sistema será o responsavel por atribuir o valor
        // e por mostrar o valor "traduzido" ao usuário
        [JsonIgnore]
        public bool TipoDocumento { get; set; }

        //"Traduz" o tipo do documento, de modo a exibir ao usuário
        public string? TipoDocumentoStr{
            get{
                if(TipoDocumento){
                    return "CNPJ";
                }else{
                    return "CPF";
                }
            }
        }
        public string? Cidade { get; set; }
        public string? Endereco { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public virtual ICollection<Venda>? Venda { get; }

        public Proprietario()
        {
            Venda = new HashSet<Venda>();
        }

        //Verifica se o documento é um CPF ou CNPJ, e sua validade(Caso não seja válido, lança uma Exception)
        public void checkDocumento( ){

                //Devem ser inseridos apenas os digitos do CPF/CNPJ
                foreach(char c in Documento){
                    if(!char.IsDigit(c)){
                        throw new CustomException("CPF/CNPJ inválido:"+Documento,"Insira apenas os dígitos do CPF/CNPJ");
                    }
                }      
                if(Documento.Length==11){
                    this.TipoDocumento=false;//CPF
                }else if(Documento.Length==14){
                    this.TipoDocumento=true;//CNPJ
                }else{
                    throw new CustomException("CPF/CNPJ inválido"+Documento,"Documento inválido");
                }
        }

    }
}
