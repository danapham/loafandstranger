using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Dapper;

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
            return _db.Tops;
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
