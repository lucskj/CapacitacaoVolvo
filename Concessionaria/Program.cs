namespace Concessionaria{
        public class Program{        
        public static void Main(string[] args){
            createHostBuilder(args).Build().Run();
        }

        public static IHostBuilder createHostBuilder(string[] args)=>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder=>{
                webBuilder.UseStartup<Startup>();
            });
        
    }
}