using API.ViewModels;
using AutoMapper;
using Data.Dapper.Models;
using Data.Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public UserRoleController(IUserRoleRepository userRoleRepository, IMapper mapper) : base()
        {
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<UserRoleViewModel>> Get()
        {
            var result = await _userRoleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserRoleViewModel>>(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll/{page}/{pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _userRoleRepository.GetByDynamicAsync(new { }, page, pageSize, "CreatedDate");
            return Ok(_mapper.Map<IEnumerable<UserRoleViewModel>>(result));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetbyId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid Id)
        {
            UserRole def = new UserRole();
            if (Id != Guid.Empty)
            {
                var result = await _userRoleRepository.GetAsync(Id);
                return Ok(result);

            }
            return Ok(def);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Agregar(UserRole item)
        {
            item.Id = Guid.NewGuid();
            await _userRoleRepository.InsertAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); ;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Batch/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> BatchAdd(IEnumerable<UserRole> items)
        {
            await _userRoleRepository.InsertManyAsync(items);
            return Ok(items);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Actualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Edit(UserRole item)
        {
            await _userRoleRepository.UpdateAsync(item);
            return Ok(item);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Eliminar/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid Id)
        {
            UserRole def = new UserRole();
            if (Id != Guid.Empty)
            {
                await _userRoleRepository.DeleteRowsByDynamicValuesAsync(new { Id });
                return Ok(new { Mensaje = "El rol se borro correctamente" });
            }
            return Ok(new { Mensaje = "Error: El rol no existe" });

        }
    }
}
