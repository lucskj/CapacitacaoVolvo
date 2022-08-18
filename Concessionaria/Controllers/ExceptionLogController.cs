
namespace Concessionaria.Controllers
{

    //Controla o armazenamento dos Logs das Exceptions
    public class ExceptionLogController
    {
        //Diretório padrão onde serão armazenados os logs        
        static string logFolderLocation=@".\Logs";

        //Cria um log com uma string
        public static void logException(string? log){
            File.WriteAllText(getFileName(),log);            
        }

        //Cria um log com os dados de uma Exception
        public static void logException(Exception ex){
            File.WriteAllText(getFileName(),ex.Message+"\n"+ex.StackTrace+"\n"+ex.InnerException);            
        }


        //Cria o nome do arquivo usando a data do sistema
        private static string getFileName(){            
            string name="";
            
            foreach(char c in DateTime.UtcNow.ToString()){
                if(char.IsLetterOrDigit(c)){
                    name+=c;
                }else{
                    name+="_";
                }
            }

            return logFolderLocation+"\\"+name;
        }
    }
}