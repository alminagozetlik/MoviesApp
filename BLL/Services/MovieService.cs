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
    public interface IMovieService
    {
        public IQueryable<MovieModel> Query();
        public ServiceBase Create(Movie record);
        public ServiceBase Update(Movie record);
        public ServiceBase Delete(int id);
    }
    public class MovieService : ServiceBase, IMovieService
    {
        public MovieService(Db db) : base(db)
        {
        }


        IQueryable<MovieModel> IMovieService.Query()
        {
            return _db.Movie.Include(p=>p.Director).OrderByDescending(p => p.ReleaseDate).ThenByDescending(p => p.Name).
                 Select(p => new MovieModel() { Record = p });
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
       
            var entity = _db.Movie.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("Movies can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Movie.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Movies updated successfully.");
        }
        public ServiceBase Delete(int id)
        {
            var entity = _db.Movie.Include(s => s.MovieGenre).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Movies can't be found!");
            if (entity.MovieGenre.Any()) // Count > 0
                return Error("Movies has relational pets!");
            _db.Movie.Remove(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Movies deleted successfully.");
        }
    }
}
