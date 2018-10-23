using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OscApp.DAL;
using Osc.Db;

namespace OscApp.Web.Controllers.Api
{
    public class GenericDataApiController<T, DTO, R> : BaseApiController where R : IRepository<T> where T : BaseEntity
    {
        private R _repo;
        private IMapper _mapper;

        public GenericDataApiController(R repo)
        {
            _repo = repo;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, DTO>();
                cfg.CreateMap<DTO, T>();

                // Add any more specific mappings / profiles here...
            }).CreateMapper();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<List<DTO>>(_repo.GetAllNotPaged()));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var model = _repo.GetById(id);
            return Ok(_mapper.Map<DTO>(model));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] DTO dto)
        {
            _repo.Update(_mapper.Map<T>(dto));
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DTO dto)
        {
            var model = _repo.Create(_mapper.Map<T>(dto));
            return Ok(_mapper.Map<DTO>(model));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok("{}");
        }
    }
}
