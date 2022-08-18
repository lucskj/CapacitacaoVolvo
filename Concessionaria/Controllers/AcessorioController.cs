using Microsoft.AspNetCore.Mvc;
using Concessionaria.Model;
using System;

namespace Concessionaria.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessorioController:Controller{

        //Adiciona um acessorio
        [HttpPost]
        public IActionResult add([FromBody]Acessorio acessorio){
            using(var context=new ConcessionariaContext()){
                try{
                    context.Acessorios.Add(acessorio);
                    int result=context.SaveChanges();
                    return Ok(acessorio);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Busca todos os acessorios
        [HttpGet]
        public IActionResult getAll(){
            using(var context=new ConcessionariaContext()){                   
                try{
                    var lista=context.Acessorios.ToList(); 
                    return Ok(lista);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Busca um acessorio especifico pelo ID
        [HttpGet("{id}")]
        public IActionResult get(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var acessorio=context.Acessorios.Find(id);
                    if(acessorio==null){
                        return NotFound();
                    }
                    return Ok(acessorio);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }
        
        //Modifica um acessorio especifico pelo ID(Contido no Body)
        [HttpPut]
        public IActionResult set([FromBody] Acessorio newAcessorio){
            using(var context=new ConcessionariaContext()){
                try{   
                    var acessorio=context.Acessorios.Find(newAcessorio.IdAcessorio);
                    if(acessorio==null){
                        return NotFound();
                    }
                    context.Entry(acessorio).CurrentValues.SetValues(newAcessorio);
                    context.SaveChanges();
                    return Ok(acessorio);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }


        //Deleta um acessorio pelo ID
        [HttpDelete("{id}")]
        public IActionResult delete(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var acessorio=context.Acessorios.Find(id);
                    if(acessorio==null){
                        return NotFound();
                    }
                    context.Acessorios.Remove(acessorio);
                    context.SaveChanges();
                    return Ok();
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

    }
}