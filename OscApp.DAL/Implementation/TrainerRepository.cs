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
        private IApplicationDbContext _dbContext;
        protected Tenancy _currentTenancy { get; set; }

        public TrainerRepository(IApplicationDbContext dbContext, Tenancy currentTenancy)
        {
            _dbContext = dbContext;
            _currentTenancy = currentTenancy;
        }

        public int Count(string searchTerm = "")
        {
            return _dbContext.Staffs.Count(s => string.IsNullOrEmpty(searchTerm) || s.Id.Contains(searchTerm));
        }

        public Staff Create(Staff model)
        {
            var createdModel = _dbContext.Staffs.Add(model);
            _dbContext.SaveChanges();

            return createdModel.Entity;
        }

        public void Delete(int id)
        {
            var modelToDelete = GetById(id);
            _dbContext.Staffs.Remove(modelToDelete);
            _dbContext.SaveChanges();
        }

        public ICollection<Staff> GetAll(int skip = 0, int take = 50, string searchTerm = "")
        {
            return _dbContext.Staffs
                .Include("StaffAllocs")
                .Where(m => string.IsNullOrEmpty(searchTerm) || m.Id.Contains(searchTerm))
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Staff GetById(int id)
        {
            var model = _dbContext.Staffs
                                .Include("StaffAllocs.Event.RoomAllocs.Room")
                                .Include("StaffAllocs.Event.EventModules.Module.CourseType1")
								.Include("StaffAllocs.Event.Shift")
								.Include("StaffAllocs.Event.StaffAllocs.Staff")
								.FirstOrDefault(rt => rt.OptimeIndex == id);

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
            modelToUpdate.SiteId = model.SiteId;
            modelToUpdate.StaffTypeId = model.StaffTypeId;

            _dbContext.SaveChanges();
        }

        public List<Staff> GetAllNotPaged()
        {
            return _dbContext.Staffs
                .Include("StaffAllocs")
                .ToList();
        }

        public int GetNumTrainersInUseThisWeek(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Staffs.Count(x => x.StaffAllocs.Any(r => r.StartTime >= startDate && r.StartTime <= endDate));
        }

        public int GetNumTrainersInUseToday()
        {
            var today = DateTime.Now.Date;

            return _dbContext.Staffs.Count(x => x.StaffAllocs.Any(r => r.StartTime.Value.Date == today));
        }

        public int GetNumTrainersNotInUseThisWeek(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Staffs.Count() - _dbContext.Staffs.Count(x => x.StaffAllocs.Any(r => r.StartTime >= startDate && r.StartTime <= endDate));
        }

        public int GetNumTrainersPartiallyAvailableThisWeek(DateTime startDate, DateTime endDate)
        {
            //Hacky but any staff that do not have a start time before 12 and after 12 
            var allStaff = GetAllNotPaged().ToList();

            var dates = Enumerable.Range(0, 4).Select(x => startDate.AddDays(x).Date);
            List<int> staffs = new List<int>();

            foreach (var staff in allStaff)
            {
                foreach (var date in dates)
                {
                    var usedAM = staff.StaffAllocs != null ? staff.StaffAllocs.Any(x => x.StartTime?.Hour < 12 && x.StartTime?.Date == date && x.RoomId == staff.OptimeIndex) : false;
                    var usedPM = staff.StaffAllocs != null ? staff.StaffAllocs.Any(x => x.StartTime?.Hour > 12 && x.StartTime?.Date == date && x.RoomId == staff.OptimeIndex) : false;

                    if (!usedAM || !usedPM)
                    {
                        staffs.Add(staff.OptimeIndex);
                        break;
                    }
                }
            }

            staffs = staffs.Distinct().ToList();

            return staffs.Count();
        }

        public List<Staff> GetStaffNotInUse(List<int> staffInUse, string queryText)
        {
            return _dbContext.Staffs.AsNoTracking()
                .Where(x => (queryText == "" || x.Id.ToLower().Contains(queryText)) && !staffInUse.Contains(x.OptimeIndex))
                .Include("StaffAllocs")
                .ToList();
        }
	}
}