using Dapper;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger.DataAccess
{
    public class StrangersRepository
    {
        readonly string ConnectionString;
        public StrangersRepository(IConfiguration config)
        {
            //ConnectionString = config.GetValue<string>("ConnectionStrings:LoafAndStranger");
            ConnectionString = config.GetConnectionString("LoafAndStranger");
        }
        public IEnumerable<Stranger> GetAll()
        {
            var sql = @"select *
                        from strangers s
	                        left join Tops t
		                        on s.TopId = t.Id
	                        left join Loaves l
		                        on s.LoafId = l.Id";

            using var db = new SqlConnection(ConnectionString);
            var strangers = db.Query<Stranger,Top,Loaf,Stranger>(sql,
                (stranger,top,loaf) =>
            {
                stranger.Loaf = loaf;
                stranger.Top = top;

                return stranger;
            },splitOn:"Id");

            return strangers;
        }
    }
}
