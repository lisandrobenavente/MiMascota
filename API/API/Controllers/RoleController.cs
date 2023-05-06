using API.ViewModels;
using AutoMapper;
using Data.Dapper.Models;
using Data.Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleController(IRoleRepository roleRepository, IMapper mapper) : base()
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }




        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<RoleViewModel>> Get()
        {
            var result = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleViewModel>>(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll/{page}/{pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _roleRepository.GetByDynamicAsync(new { }, page, pageSize, "CreatedDate");
            return Ok(_mapper.Map<IEnumerable<RoleViewModel>>(result));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetbyId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid Id)
        {
            Role def = new Role();
            if (Id != Guid.Empty)
            {
                var result = await _roleRepository.GetAsync(Id);
                return Ok(result);

            }
            return Ok(def);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Agregar(Role item)
        {
            item.Id = Guid.NewGuid();
            await _roleRepository.InsertAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); ;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Batch/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> BatchAdd(IEnumerable<Role> items)
        {
            await _roleRepository.InsertManyAsync(items);
            return Ok(items);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Actualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Edit(Role item)
        {
            await _roleRepository.UpdateAsync(item);
            return Ok(item);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Eliminar/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid Id)
        {
            Role def = new Role();
            if (Id != Guid.Empty)
            {
                await _roleRepository.DeleteRowsByDynamicValuesAsync(new { Id });
                return Ok(new { Mensaje = "El Rol se borro correctamente" });
            }
            return Ok(new { Mensaje = "Error: El Rol no existe" });
        }
    }
}
