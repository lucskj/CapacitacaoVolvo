using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Concessionaria{

    [DefaultStatusCode(500)]
    public class InternalServerError:ObjectResult{


        //Classe de retorno HTTP com o codigo 500
        public InternalServerError([ActionResultObjectValue]object? value):base(value){
            StatusCode=500;
        }
    }
}