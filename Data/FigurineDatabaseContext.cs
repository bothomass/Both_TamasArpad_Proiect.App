using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Both_TamasArpad_Proiect.Models;

namespace Both_TamasArpad_Proiect.Data
{
    public class FigurineDatabaseContext
    {
        readonly SQLiteAsyncConnection _database;
        public FigurineDatabaseContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Figurine>().Wait();
        }
        public Task<List<Figurine>> GetFigurineAsync()
        {
            return _database.Table<Figurine>().ToListAsync();
        }
        public Task<Figurine> GetFigurineAsync(int id)
        {
            return _database.Table<Figurine>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveFigurineAsync(Figurine fig)
        {
            if (fig.ID != 0)
            {
                return _database.UpdateAsync(fig);
            }
            else
            {
                return _database.InsertAsync(fig);
            }
        }
        public Task<int> DeleteFigurineAsync(Figurine fig)
        {
            return _database.DeleteAsync(fig);
        }

        public Task<int> DeleteFigurineByIdAsync(int id)
        {
            return _database.Table<Figurine>().DeleteAsync(f => f.ID == id);
        }
    }
}