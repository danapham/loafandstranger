using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace LoafAndStranger.DataAccess
{
    public class TopsRepository
    {
        const string ConnectionString = "Server=localhost;Database=LoafAndStranger;Trusted_Connection=True;";
        AppDbContext _db;

        public TopsRepository(AppDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Top> GetAll()
        {
            _db.Database.GetDbConnection().Query<Top>("select * from tops");
            return _db.Tops
                .Include(t => t.Strangers)
                .ThenInclude(s => s.Loaf)
                //.Where(t => t.Strangers.Any(s => s.Loaf.Type == "Monkey Bread"))
                .AsNoTracking();
        }

        public Top Add(int numberOfSeats)
        {
            var top = new Top { NumberOfSeats = numberOfSeats };

            _db.Tops.Add(top);
            _db.SaveChanges();

            return top;
        }
    }
}
