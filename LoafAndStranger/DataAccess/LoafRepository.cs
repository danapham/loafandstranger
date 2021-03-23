using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LoafAndStranger.DataAccess
{
    public class LoafRepository
    {
        const string ConnectionString = "Server=localhost;Database=LoafAndStranger;Trusted_Connection=True;";

        public List<Loaf> GetAll()
        {
            //create a connection
            using var db = new SqlConnection(ConnectionString);

            //telling the command what you want to do
            var sql = @"Select * 
                        From Loaves";

            return db.Query<Loaf>(sql).ToList();
        }

        public void Add(Loaf loaf)
        {
            var sql = @"INSERT INTO [Loaves] ([Size],[Type],[WeightInOunces],[Price],[Sliced])
                        OUTPUT inserted.Id
                        VALUES(@Size, @type, @weightInOunces, @Price, @Sliced)";

            using var db = new SqlConnection(ConnectionString);

            var id = db.ExecuteScalar<int>(sql, loaf);

            loaf.Id = id;
        }

        public Loaf Get(int id)
        {
            var sql = @"Select *
                        From Loaves
                        where Id = @id";

            //create a connection
            using var db = new SqlConnection(ConnectionString);

            var loaf = db.QueryFirstOrDefault<Loaf>(sql, new { id = id });

            return loaf;
        }

        public void Update(Loaf loaf)
        {
            var sql = @"UPDATE Loaves
                        SET Price = @price,
                            Size = @size,
                            WeightInOunces = @weightInOunces,
                            Sliced = @sliced,
                            Type = @type
                        WHERE Id = @id";

            using var db = new SqlConnection(ConnectionString);

            db.Execute(sql, loaf);
        }

        public void Slice(int id)
        {
            var sql = @"UPDATE Loaves
                        SET Sliced = 1
                        WHERE Id = @id";

            using var db = new SqlConnection(ConnectionString);

            //anonymous type with implicit property naming
            db.Execute(sql, new { id });
        }

        public void Remove(int id)
        {
            var sql = @"Delete 
                        from Loaves 
                        where Id = @id";

            using var db = new SqlConnection(ConnectionString);

            db.Execute(sql, new { id });
        }
    }
}
