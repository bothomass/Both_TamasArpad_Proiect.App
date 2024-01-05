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
        public Task<int> SaveFigurineAsync(Figurine figlist)
        {
            if (figlist.ID != 0)
            {
                return _database.UpdateAsync(figlist);
            }
            else
            {
                return _database.InsertAsync(figlist);
            }
        }
        public Task<int> DeleteFigurineAsync(Figurine figlist)
        {
            return _database.DeleteAsync(figlist);
        }
    }
}