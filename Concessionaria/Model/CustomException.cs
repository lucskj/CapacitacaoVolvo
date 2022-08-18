using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Concessionaria.Model{
    public class CustomException:ApplicationException{
        
        //Classe de Exceptions customizada. Permite armazenar uma mensagem de erro, que vai para o log
        // e uma mensagem de erro que vai para o usuário
        
        
        public string? ErrorLog{get;set;}
        public string? UserMessage{get;set;}
        
        public CustomException():base(){
        }

        public CustomException(string log,string userMessage):this(){            
            this.ErrorLog=log;
            this.UserMessage=userMessage;
        }
    }
}