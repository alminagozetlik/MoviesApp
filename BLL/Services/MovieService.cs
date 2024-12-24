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
    public class MovieService : ServiceBase, IService<Movie, MovieModel>
    {
        public MovieService(Db db) : base(db)
        {
        }


        public IQueryable<MovieModel>Query()
        {
            return _db.Movie.Include(m => m.Director).Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
                .OrderByDescending(m => m.ReleaseDate).ThenByDescending(m => m.Name).Select(m => new MovieModel() { Record = m });
        }

        public ServiceBase Create(Movie record)
        {
            if (_db.Movie.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movies with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Movie.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("Movies created successfully.");
        }

        public ServiceBase Update(Movie record)
        {
            if (_db.Movie.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movies with the same name exists!");
       
            var entity = _db.Movie.Include(m => m.MovieGenres).SingleOrDefault(m => m.Id == record.Id);
            if (entity is null)
                return Error("Movie can't be found!");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.Name = record.Name?.Trim();
            entity.ReleaseDate = record.ReleaseDate;
            entity.TotalRevenue = record.TotalRevenue;
            entity.Director = record.Director;
            entity.MovieGenres = record.MovieGenres;
            _db.Movie.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Movies updated successfully.");
        }
        public ServiceBase Delete(int id)
        {
            var entity = _db.Movie.Include(s => s.MovieGenres).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Movies can't be found!");
            if (entity.MovieGenres.Any()) // Count > 0
                return Error("Movies has relational pets!");
            _db.Movie.Remove(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Movies deleted successfully.");
        }
    }
}
