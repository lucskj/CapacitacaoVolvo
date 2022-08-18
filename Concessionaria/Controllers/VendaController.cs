using Microsoft.AspNetCore.Mvc;
using Concessionaria.Model;

namespace Concessionaria.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController:Controller{


        //Cria uma venda
        [HttpPost]
        public IActionResult add([FromBody]Venda venda){
            using(var context=new ConcessionariaContext()){
                try{
                    context.Venda.Add(venda);
                    context.SaveChanges();
                    return Ok(venda);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Busca todas as vendas
        [HttpGet]
        public IActionResult getAll(){
            using(var context=new ConcessionariaContext()){
                
                try{
                    return Ok(context.Venda.ToList());
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Busca uma venda especifica pelo ID
        [HttpGet("{id}")]
        public IActionResult get(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var venda=context.Venda.Find(id);
                    if(venda==null){
                        return NotFound();
                    }
                    return Ok(venda);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }


        //Modifica uma venda pelo ID(Contido no body)
        [HttpPut]
        public IActionResult set([FromBody] Venda newVenda){
            using(var context=new ConcessionariaContext()){
                try{
                    var venda=context.Venda.Find(newVenda.IdVenda);
                    if(venda==null){
                        return NotFound();
                    }
                    context.Entry(venda).CurrentValues.SetValues(newVenda);
                    context.SaveChanges();
                    return Ok(venda);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Deleta uma venda pelo ID
        [HttpDelete("{id}")]
        public IActionResult delete(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var venda=context.Venda.Find(id);
                    if(venda==null){
                        return NotFound();
                    }
                    context.Venda.Remove(venda);
                    context.SaveChanges();
                    return Ok();
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Veiculos da venda
        
        //Adiciona um veiculo à venda, altera o IdVenda no veiculo e altera o valor total da venda
        [HttpPut("Veiculo/ById")]
        public IActionResult adicionarVeiculo(int idVenda, int idVeiculo){
            using(var context=new ConcessionariaContext()){                
                try{
                    var venda=context.Venda.Find(idVenda);
                    var veiculo=context.Veiculos.Find(idVeiculo);
                    
                    if(venda==null || veiculo==null){
                        return NotFound();
                    }

                    veiculo.IdVenda=idVenda;
                    venda.ValordaVenda+=veiculo.Valor;
                    
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