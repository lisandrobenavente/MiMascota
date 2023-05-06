using API.ViewModels;
using AutoMapper;
using Data.Dapper.Models;
using Data.Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IMapper _mapper;
        public ServicesController(IServicesRepository servicesRepository, IMapper mapper) : base()
        {
            _mapper = mapper;
            _servicesRepository = servicesRepository;
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<ServicesViewModel>> Get()
        {
            var result = await _servicesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServicesViewModel>>(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll/{page}/{pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _servicesRepository.GetByDynamicAsync(new { }, page, pageSize, "CreatedDate");
            return Ok(_mapper.Map<IEnumerable<ServicesViewModel>>(result));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetbyId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid Id)
        {
            Services  def = new Services();
            if (Id != Guid.Empty)
            {
                var result = await _servicesRepository.GetAsync(Id);
                return Ok(result);

            }
            return Ok(def);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Agregar(Services item)
        {
            item.Id = Guid.NewGuid();
            await _servicesRepository.InsertAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); ;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Batch/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> BatchAdd(IEnumerable<Services> items)
        {
            await _servicesRepository.InsertManyAsync(items);
            return Ok(items);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Actualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Edit(Services item)
        {
            await _servicesRepository.UpdateAsync(item);
            return Ok(item);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Eliminar/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid Id)
        {
            Services def = new Services();
            if (Id != Guid.Empty)
            {
                await _servicesRepository.DeleteRowsByDynamicValuesAsync(new { Id });
                return Ok(new { Mensaje = "El servicio se borro correctamente" });
            }
            return Ok(new { Mensaje = "Error: El servicio no existe" });
        }
    }
}
