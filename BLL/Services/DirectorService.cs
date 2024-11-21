using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    public class DirectorService : ServiceBase, IService<Director, DirectorModel>
    {
    
        public DirectorService(Db db) : base(db)
        {
        }
        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.OrderBy(s => s.Name).Select(s => new DirectorModel() { Record = s });
        }

        public ServiceBase Create(Director record)
        {
            if (_db.Directors.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim() &&
                p.IsRetired == record.IsRetired && p.Surname.ToLower() == record.Surname.ToLower().Trim()))
                return Error("Director with the same name and surname and  retired situation exists");
            record.Name = record.Name?.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges();
            return Success("Director created successfully.");
        }


        public ServiceBase Update(Director record)
        {
            if (_db.Directors.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim() &&
               p.IsRetired == record.IsRetired && p.Surname.ToLower() == record.Surname.ToLower().Trim()))
                return Error("Director with the same name and surname and retired situation exists!");
            record.Name = record.Name?.Trim();
            _db.Directors.Update(record);
            _db.SaveChanges();
            return Success("Director updated successfully.");
        }
        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(p => p.Movies).SingleOrDefault(p => p.Id == id);
            if (entity is not null && entity.Movies.Any())
                return Error("Director can't be deleted because it has relational movies!");
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Director deleted successfully.");
        }
    }
}
