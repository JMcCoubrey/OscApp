using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Osc.Db;
using OscApp.Exceptions;
using OscApp.DAL;

namespace OscApp.DAL.Implementation
{
    public class TrainerRepository : ITrainerRepository
    {
        private ApplicationDbContext _dbContext;

        public TrainerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count(string searchTerm = "")
        {
            return _dbContext.Staff.Count(s => string.IsNullOrEmpty(searchTerm) || s.Id.Contains(searchTerm));
        }

        public Staff Create(Staff model)
        {
            var createdModel = _dbContext.Staff.Add(model);
            _dbContext.SaveChanges();

            return createdModel.Entity;
        }

        public void Delete(int id)
        {
            var modelToDelete = GetById(id);
            _dbContext.Staff.Remove(modelToDelete);
            _dbContext.SaveChanges();
        }

        public ICollection<Staff> GetAll(int skip = 0, int take = 50, string searchTerm = "")
        {
            return _dbContext.Staff
                .Where(m => string.IsNullOrEmpty(searchTerm) || m.Id.Contains(searchTerm))
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Staff GetById(int id)
        {
            var model = _dbContext.Staff.FirstOrDefault(rt => rt.OptimeIndex == id);

            if (model == null)
            {
                throw new NotFoundException("Could not find staff.");
            }

            return model;
        }

        public void Update(Staff model)
        {
            var modelToUpdate = GetById(model.OptimeIndex);
            modelToUpdate.Id = model.Id;
            modelToUpdate.Description = model.Description;
            modelToUpdate.forename = model.forename;
            modelToUpdate.surname = model.surname;
            modelToUpdate.LineManager = model.LineManager;


            _dbContext.SaveChanges();
        }

        public List<Staff> GetAllNotPaged()
        {
            return _dbContext.Staff
                .ToList();
        }
	}
}