using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using OscApp.DAL;
using OscApp.Model.DataViewModels;

namespace OscApp.Web.Controllers.Data
{
    public class GenericDataController<T, R> : Controller where R : IRepository<T>
    {
        private R _repo;

        public GenericDataController(R repo)
        {
            _repo = repo;
        }

        public IActionResult Index(int skip, int take, string searchTerm)
        {
            if (skip == 0 && take == 0)
            {
                take = 10;
            }

            var models = _repo.GetAll(skip, take, searchTerm);

            var totalShifts = _repo.Count();
            var numPages = Math.Ceiling((double)totalShifts / take);
            var currentPage = (skip / take) + 1;

            return View("~/Views/Data/" + typeof(T).Name + "/Index.cshtml", new GenericDataViewModel<T>() { Models = models, SearchTerm = searchTerm, TotalNumResults = totalShifts, NumPages = (int)numPages, CurrentPageNumber = currentPage, Skip = skip, Take = take });
        }
    }
}
