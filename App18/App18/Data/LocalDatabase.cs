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
        }

        public async Task<List<Student>> GetStudents()
        {
            return await db.Table<Student>().ToListAsync();
        }

        public async Task<List<T>> GetAll<T>() where T : class, new()
        {
            return await db.Table<T>().ToListAsync();
        }
    }
}
