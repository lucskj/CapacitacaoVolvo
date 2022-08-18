using Microsoft.AspNetCore.Mvc;
using Concessionaria.Model;

namespace Concessionaria.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class ProprietarioController:Controller{

        //Cria um Proprietario
        [HttpPost]
        public IActionResult add([FromBody]Proprietario proprietario){
            using(var context=new ConcessionariaContext()){
                try{
                    proprietario.checkDocumento();
                    context.Proprietarios.Add(proprietario);                    
                    context.SaveChanges();
                    return Ok(proprietario);
                }catch(CustomException cEx){
                    ExceptionLogController.logException(cEx.ErrorLog);
                    return new InternalServerError(cEx.UserMessage);
                }
                catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Busca todos os proprietários
        [HttpGet]
        public IActionResult getAll(){
            using(var context=new ConcessionariaContext()){                
                try{
                    return Ok(context.Proprietarios.ToList());
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }


        //Busca um proprietário especifico pelo ID
        [HttpGet("{id}")]
        public IActionResult get(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var proprietario=context.Proprietarios.Find(id);
                    if(proprietario==null){
                        return NotFound();
                    }
                    return Ok(proprietario);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }
        
        //Modifica um proprietário especifico pelo ID(Contido no body)
        [HttpPut]
        public IActionResult set([FromBody] Proprietario newProprietario){
            using(var context=new ConcessionariaContext()){
                try{
                    var proprietario=context.Proprietarios.Find(newProprietario.IdProprietario);
                    if(proprietario==null){
                        return NotFound();
                    }
                    //Documento não deve ser modificado, o código garante isso
                    newProprietario.Documento = proprietario.Documento;
                    context.Entry(proprietario).CurrentValues.SetValues(newProprietario);
                    context.SaveChanges();
                    return Ok(proprietario);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Deleta um proprietário pelo ID
        [HttpDelete("{id}")]
        public IActionResult delete(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var proprietario=context.Proprietarios.Find(id);
                    if(proprietario==null){
                        return NotFound();
                    }
                    context.Proprietarios.Remove(proprietario);
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