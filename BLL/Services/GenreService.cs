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
    public class GenreService : ServiceBase, IService<Genre, GenreModel>
    {
        public GenreService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim()))
                return Error("Genre with the same name and surname and  retired situation exists");
            record.Name = record.Name?.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges();
            return Success("Genre created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(p => p.MovieGenres).SingleOrDefault(p => p.Id == id);
            if (entity is not null && entity.MovieGenres.Any())
                return Error("Genre can't be deleted because it has relational entities!");
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully.");
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(o => o.Name).Select(o => new GenreModel() { Record = o });
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim()))
                return Error("Genre with the same name exists!");
            var entity = _db.Genres.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("Genre can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genre updated successfully.");
        }
    }
}
