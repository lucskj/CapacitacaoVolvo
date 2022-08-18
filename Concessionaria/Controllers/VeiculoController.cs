using Microsoft.AspNetCore.Mvc;
using Concessionaria.Model;

namespace Concessionaria.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController:Controller{
        
        //Adiciona Veiculo No Banco de dados
        [HttpPost]
        public IActionResult add([FromBody]Veiculo veiculo){
            using(var context=new ConcessionariaContext()){
                try{
                    veiculo.checkChassi();
                    context.Veiculos.Add(veiculo);
                    context.SaveChanges();
                    return Ok(veiculo);
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
        
        //Pesquisa todos os veiculos do banco
        [HttpGet]
        public IActionResult getAll(){
            using(var context=new ConcessionariaContext()){ 
                try{
                    var veiculosListados = context.Veiculos.ToList();
                    return Ok(veiculosListados);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Pesquisa todos os veiculos do banco e ordena por quilometragem
        [HttpGet("Quilometragem")]
        public IActionResult getAllByQuilometragem(){
            using(var context=new ConcessionariaContext()){ 
                try{
                    var veiculosListados = context.Veiculos.ToList().OrderBy(v=> v.Quilometragem);
                    return Ok(veiculosListados);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Pesquisa todos os veiculos do banco e ordena pela versão do sistema
        [HttpGet("Versao")]
        public IActionResult getAllByVersao(){
            using(var context=new ConcessionariaContext()){                
               try{
                   var veiculosListados = context.Veiculos.ToList().OrderByDescending(v=> v.VersaoSistema);
                    return Ok(veiculosListados);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Pesquisa um veiculo com o id Especifico
        [HttpGet("{id}")]
        public IActionResult get(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var veiculo=context.Veiculos.Find(id);
                    if(veiculo==null){
                        return NotFound();
                    }
                    return Ok(veiculo);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        
        //Modifica um veiculo com um Id específico(Contido no Body)
        [HttpPut]
        public IActionResult set([FromBody] Veiculo newVeiculo){
            using(var context=new ConcessionariaContext()){
                try{
                    var veiculo=context.Veiculos.Find(newVeiculo.IdVeiculo);                
                    if(veiculo==null){
                        return NotFound();
                    }
                    context.Entry(veiculo).CurrentValues.SetValues(newVeiculo);
                    context.SaveChanges();
                    return Ok(veiculo);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Remove um veiculo com um ID especifico
        [HttpDelete("{id}")]
        public IActionResult delete(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var veiculo=context.Veiculos.Find(id);                
                    if(veiculo==null){
                        return NotFound();
                    }
                    var acessorios=context.AcessorioVeiculos.Where(av=>av.IdVeiculo==id).ToList();
                    foreach(var item in acessorios){
                        context.AcessorioVeiculos.Remove(item);
                    }
                    context.Veiculos.Remove(veiculo);
                    context.SaveChanges();
                    return Ok();
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Relativos aos acessórios
        //Adiciona um acessorio a um veiculo(usando o Id de ambos)
        [HttpPost("Acessorios/byId")]
        public IActionResult addAcessorio(int idAcessorio,int idVeiculo){
            using(var context=new ConcessionariaContext()){                
                try{
                    context.AcessorioVeiculos.Add(new AcessorioVeiculo(idAcessorio,idVeiculo));
                    context.SaveChanges();
                    return Ok();
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //Lista todos os acessorios de um veiculo um Id especifico
        [HttpGet("Acessorios/{id}")]
        public IActionResult getAcessorios(int id){
            using(var context=new ConcessionariaContext()){
                try{
                    var veiculo=context.Veiculos.Find(id);

                    if(veiculo==null){
                        return NotFound();
                    }
                    var acessorios=context.Acessorios.Join(context.AcessorioVeiculos,
                        acessorio=>acessorio.IdAcessorio,
                        acessorioVeiculo=>acessorioVeiculo.IdAcessorio,
                        (acessorio,acessorioVeiculo)=> new {
                            idAcessorio=acessorio.IdAcessorio,
                            nomeAcessorio=acessorio.Nome,
                            idVeiculo=acessorioVeiculo.IdVeiculo
                    }).Where(select=>select.idVeiculo==id).ToList();

                    return Ok(acessorios);
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }

        //remove um acessorio de um veiculo(Usando o Id de ambos)
        [HttpDelete("Acessorios/byId")]
        public IActionResult removeAcessorio(int idVeiculo,int idAcessorio){
            using(var context=new ConcessionariaContext()){                
                try{
                    var acessorioVeiculo=context.AcessorioVeiculos.FirstOrDefault(av=>av.IdVeiculo==idVeiculo && av.IdAcessorio==idAcessorio);
                    if(acessorioVeiculo==null){
                        return NotFound();
                    }
                    context.AcessorioVeiculos.Remove(acessorioVeiculo);
                    context.SaveChanges();
                    return Ok();
                }catch(Exception ex){
                    ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                }
            }
        }        
        
        //Busca os dados dos proprietarios de todos os veículo com determinadas caracteristicas
        [HttpGet("BuscaDetalhada")]
        public IActionResult buscaDetalhada(string? numChassi="",
            string? modelo="",
            string? cor="",
            string? versaoSistema="",
            string? cidadeProprietario="",
            int minQuilometragem=0,
            int maxQuilometragem=int.MaxValue,
            short minAno=0,
            short maxAno=short.MaxValue,
            int minValor=0,
            int maxValor=9999999
            ){
                using(var context=new ConcessionariaContext()){
                    try{                        
                        var lista=context.Veiculos.Where(v=>
                        v.NumChassi.Contains(numChassi)
                        && v.Modelo.Contains(modelo)                
                        && v.Cor.Contains(cor)
                        && v.VersaoSistema.Contains(versaoSistema)
                        && v.Quilometragem>=minQuilometragem 
                        && v.Quilometragem<=maxQuilometragem
                        && v.Ano>=minAno 
                        && v.Ano<=maxAno
                        && v.Valor>=minValor 
                        && v.Valor<=maxValor
                        ).Join(context.Proprietarios.Where(p=>p.Cidade.Contains(cidadeProprietario)),
                            v=>context.Venda.Where(vd=>vd.IdVenda==v.IdVenda).First().IdProprietario,
                            p=>p.IdProprietario,
                            (v,p)=>new{v.IdVeiculo,
                            v.Modelo,
                            v.NumChassi,
                            p.IdProprietario,
                            p.Nome,
                            p.Cidade,
                            p.Email}).ToList();
                        return Ok(lista);
                    }catch(Exception ex){
                        ExceptionLogController.logException(ex);
                    return new InternalServerError("Erro no sistema");
                    }
                }
        }
    }
}