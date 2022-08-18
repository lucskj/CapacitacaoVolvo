using Microsoft.AspNetCore.Mvc;
using Concessionaria.Model;

namespace Concessionaria.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController:Controller{

        //Adiciona um vendedor
        [HttpPost]
        public IActionResult add([FromBody]Vendedor vendedor){
            using(var context=new ConcessionariaContext()){
                try{
                    context.Vendedors.Add(vendedor);
                    context.SaveChanges();
                    return Ok(vendedor);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Busca todos os vendedores
        [HttpGet]
        public IActionResult getAll(){
            using(var context=new ConcessionariaContext()){                
                try{
                    return Ok(context.Vendedors.ToList());
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Busca um vendedor especifico pelo ID
        [HttpGet("{id}")]
        public IActionResult get(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var vendedor=context.Vendedors.Find(id);
                    if(vendedor==null){
                        return NotFound();
                    }
                    return Ok(vendedor);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }
        
        //Modifica um vendedor especifico pelo id(contido no body)
        [HttpPut]
        public IActionResult set([FromBody] Vendedor newVendedor){
            using(var context=new ConcessionariaContext()){
                try{
                    var vendedor=context.Vendedors.Find(newVendedor.IdVendedor);
                    if(vendedor==null){
                        return NotFound();
                    }
                    context.Entry(vendedor).CurrentValues.SetValues(newVendedor);
                    context.SaveChanges();
                    return Ok(vendedor);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }


        //Remove um vendedor pelo ID
        [HttpDelete("{id}")]
        public IActionResult delete(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var vendedor=context.Vendedors.Find(id);
                    if(vendedor==null){
                        return NotFound();
                    }
                    context.Vendedors.Remove(vendedor);
                    context.SaveChanges();
                    return Ok();
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Calcula o salário total de um vendedor(pelo ID), considerando as comissões do mes
        [HttpGet("Salario")]
        public IActionResult calcularSalario(int idVendedor,DateTime data){
            using(var context=new ConcessionariaContext()){
                try{
                    var vendedor=context.Vendedors.Find(idVendedor);
                    
                    if(vendedor==null){
                        return NotFound();
                    }

                    //Busca todas as vendas do vendedor naquele mes
                    var vendasMes=context.Venda.Where(
                        venda=>venda.IdVendedor==vendedor.IdVendedor 
                        && venda.DataVenda.Year==data.Year 
                        && venda.DataVenda.Month==data.Month).ToList();
                    
                    decimal comissao=0;
                    foreach(var venda in vendasMes){
                        //Adiciona a comissão de 1% de cada venda
                        comissao+=venda.ValordaVenda*0.01M;
                    }
                    
                    return Ok(vendedor.SalarioBase+comissao);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }
    }
}