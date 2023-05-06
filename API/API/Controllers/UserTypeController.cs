using API.ViewModels;
using AutoMapper;
using Data.Dapper.Models;
using Data.Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IMapper _mapper;
        public UserTypeController(IUserTypeRepository userTypeRepository, IMapper mapper) : base()
        {
            _mapper = mapper;
            _userTypeRepository = userTypeRepository;
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<UserTypeViewModel>> Get()
        {
            var result = await _userTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserTypeViewModel>>(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll/{page}/{pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _userTypeRepository.GetByDynamicAsync(new { }, page, pageSize, "CreatedDate");
            return Ok(_mapper.Map<IEnumerable<UserTypeViewModel>>(result));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetbyId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid Id)
        {
            UserType def = new UserType();
            if (Id != Guid.Empty)
            {
                var result = await _userTypeRepository.GetAsync(Id);
                return Ok(result);

            }
            return Ok(def);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Agregar(UserType item)
        {
            item.Id = Guid.NewGuid();
            await _userTypeRepository.InsertAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); ;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Batch/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> BatchAdd(IEnumerable<UserType> items)
        {
            await _userTypeRepository.InsertManyAsync(items);
            return Ok(items);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Actualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Edit(UserType item)
        {
            await _userTypeRepository.UpdateAsync(item);
            return Ok(item);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Eliminar/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid Id)
        {
            UserType def = new UserType();
            if (Id != Guid.Empty)
            {
                await _userTypeRepository.DeleteRowsByDynamicValuesAsync(new { Id });
                return Ok(new { Mensaje = "El tipo de usuario se borro correctamente" });
            }
            return Ok(new { Mensaje = "Error: El tipo de usuario no existe" });

        }
    }
}
