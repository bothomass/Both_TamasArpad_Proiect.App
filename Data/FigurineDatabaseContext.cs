using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
//using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using System.Threading;
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
            _database.CreateTableAsync<Creator>().Wait();
        }
        public Task<List<Figurine>> GetFigurineAsync()
        {
            return ReadOperations.GetAllWithChildrenAsync<Figurine>(_database, null, true, default);
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

        public Task<List<Creator>> GetCreatorsAsync()
        {
            return _database.Table<Creator>().ToListAsync();
        }

        public Task<int> SaveCreatorAsync(Creator creator)
        {
            if (creator.Id != 0)
            {
                return _database.UpdateAsync(creator);
            }
            else
            {
                return _database.InsertAsync(creator);
            }
        }
        public Task<int> DeleteCreatorAsync(Creator creator)
        {
            return _database.DeleteAsync(creator);
        }
    }
}