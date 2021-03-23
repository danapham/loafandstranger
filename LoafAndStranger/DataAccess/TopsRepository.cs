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
        public IEnumerable<Top> GetAll()
        {
            using var db = new SqlConnection(ConnectionString);
            //var topsSql = @"Select *
            //            From Tops";
            //var strangersSql = @"select *
            //                   from strangers
            //                   where topId =@id";

            //var tops = db.Query<Top>(topsSql);

            //foreach (var top in tops)
            //{
            //    var relatedStrangers = db.Query<Stranger>(strangersSql, top);
            //    top.Strangers = relatedStrangers.ToList();
            //}

            var topsSql = @"Select *
                        From Tops";
            var strangersSql = @"select *
                               from strangers
                               where topId is not null";

            var tops = db.Query<Top>(topsSql);
            var strangers = db.Query<Stranger>(strangersSql);

            foreach (var top in tops)
            {
                top.Strangers = strangers.Where(s => s.TopId == top.Id).ToList();
            }

            //var groupedStrangers = strangers.GroupBy(s => s.TopId);

            //foreach (var groupedStranger in groupedStrangers)
            //{
            //    tops.First(t => t.Id == groupedStranger.Key).Strangers = groupedStranger.ToList();
            //}
            return tops;
        }

        public Top Add(int numberOfSeats)
        {
            using var db = new SqlConnection(ConnectionString);

            var sql = @"INSERT INTO [dbo].[Tops] ([NumberOfSeats])
                        OUTPUT inserted.*
                        VALUES (@numberOfSeats)";

            return db.QuerySingle<Top>(sql, new { numberOfSeats });
        }
    }
}
