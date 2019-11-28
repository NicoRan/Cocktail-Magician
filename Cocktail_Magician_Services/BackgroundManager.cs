//using Cocktail_Magician_DB;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Cocktail_Magician_Services
//{
//    public class BackgroundManager : IHostedService, IDisposable
//    {
//        private Timer _timer;
//        private IServiceProvider services;

//        public BackgroundManager(IServiceProvider serviceProvider)
//        {
//            services = serviceProvider;
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            var timer = new Timer(DeleteUnusedIngredients,null,TimeSpan.FromMinutes(300), TimeSpan.FromMinutes(300));

//            return Task.CompletedTask;
//        }

//        private async void DeleteUnusedIngredients(object stateInfo)
//        {
//            using (var scope = services.CreateScope())
//            {
//                var context =
//                scope.ServiceProvider
//                    .GetRequiredService<CMContext>();


//                var allIngredients = await context.Ingredients.Include(i=>i.CocktailIngredient).Where(c=> c.CocktailIngredient.FirstOrDefault(cc=>cc.IngredientId == c.IngredientId)==null).ToListAsync();

//                context.Ingredients.RemoveRange(allIngredients);
//                await context.SaveChangesAsync();

//            }
//        }
//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            _timer?.Change(Timeout.Infinite, 0);

//            return Task.CompletedTask;
//        }
//        public void Dispose()
//        {
//            _timer?.Dispose();
//        }
//    }
//}
