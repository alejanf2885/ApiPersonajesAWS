using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWS.Repositories
{
    public class RepositorieTelevision
    {
        private readonly TelevisionContext context;

        public RepositorieTelevision(TelevisionContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetAllPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }

        private async Task<int> GetMaxIdPersonajeAsync()
        {
            if (await context.Personajes.AnyAsync())
            {
                return await this.context.Personajes.MaxAsync(p => p.IdPersonaje);
            }
            else
            {
                return 0;
            }
        }

        public async Task InsertPersonajeAsync(string nombre, string imagen)
        {
            int maxId = await GetMaxIdPersonajeAsync();
            Personaje personaje = new Personaje
            {
                IdPersonaje = maxId + 1,
                Nombre = nombre,
                Imagen = imagen,
            };
            await this.context.Personajes.AddAsync(personaje);
            await this.context.SaveChangesAsync();
        }
    }
}
