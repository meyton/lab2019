using App18.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App18.Data
{
    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection db;

        public LocalDatabase(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Student>().Wait();
            db.CreateTableAsync<Teacher>().Wait();
        }

        public async Task<List<Student>> GetStudents()
        {
            return await db.Table<Student>().ToListAsync();
        }

        public async Task<List<T>> GetAll<T>() where T : class, new()
        {
            return await db.Table<T>().ToListAsync();
        }

        public async Task<T> GetByID<T>(int id) where T : class, ISqliteModel, new()
        {
            return await db.Table<T>().Where(t => t.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItem<T>(T item) where T : class, ISqliteModel, new()
        {
            var result = await db.UpdateAsync(item);

            if (result == 0)
            {
                result = await db.InsertAsync(item);
            }

            return result;
        }

        public async Task<int> DeleteItem<T>(T item) where T : class, ISqliteModel, new()
        {
            return await db.DeleteAsync(item);
        }
    }
}
