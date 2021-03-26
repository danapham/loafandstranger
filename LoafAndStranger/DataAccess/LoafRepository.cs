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
    public class LoafRepository
    {
        AppDbContext _db;

        public LoafRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Loaf> GetAll() => _db.Loaves.ToList();


        public void Add(Loaf loaf)
        {
            _db.Loaves.Add(loaf);
            _db.SaveChanges();
        }

        public Loaf Get(int id)
        {
            return _db.Loaves.Find(id);
        }

        public void Update(Loaf loaf)
        {
            //option 1: how ef wants you to do updates
            //var existingLoaf = Get(loaf.Id); //get the thing to update
            //existingLoaf.Sliced = loaf.Sliced; //change the required/updated fields
            //// ...

            //_db.SaveChanges(); //then save it

            //this tells EF to take this loaf from the outside world and treat it like something it watched change.
            _db.Loaves.Attach(loaf).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Slice(int id)
        {
            var loaf = Get(id);
            loaf.Sliced = true;
            _db.SaveChanges();
        }

        public void Remove(int id)
        {
            _db.Loaves.Remove(Get(id));
        }
    }
}
