using Dapper;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger.DataAccess
{
    public class StrangersRepository
    {
        AppDbContext _db;
        public StrangersRepository(AppDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Stranger> GetAll()
        {
            var strangers = _db.Strangers
                .Include(s => s.Loaf)
                .Include(s => s.Top);

            return strangers;
        }
    }
}
