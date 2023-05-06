using API.ViewModels;
using AutoMapper;
using Data.Dapper.Models;
using Data.Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly IMapper _mapper;
        public ActionsController(IActionsRepository actionsRepository, IMapper mapper) : base()
        {
            _mapper = mapper;
            _actionsRepository = actionsRepository;
        }




        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<ActionsViewModel>> Get()
        {
            var result = await _actionsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ActionsViewModel>>(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetAll/{page}/{pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _actionsRepository.GetByDynamicAsync(new { }, page, pageSize, "CreatedDate");
            return Ok(_mapper.Map<IEnumerable<ActionsViewModel>>(result));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/GetbyId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid Id)
        {
            Actions def = new Actions();
            if (Id != Guid.Empty)
            {
                var result = await _actionsRepository.GetAsync(Id);
                return Ok(result);

            }
            return Ok(def);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Agregar(Actions item)
        {
            item.Id = Guid.NewGuid();
            await _actionsRepository.InsertAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); ;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Batch/Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> BatchAdd(IEnumerable<Actions> items)
        {
            await _actionsRepository.InsertManyAsync(items);
            return Ok(items);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Actualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Edit(Actions item)
        {
            await _actionsRepository.UpdateAsync(item);
            return Ok(item);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]/Eliminar/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid Id)
        {
            Actions def = new Actions();
            if (Id != Guid.Empty)
            {
                await _actionsRepository.DeleteRowsByDynamicValuesAsync(new { Id });
                return Ok(new { Mensaje = "La accion se borro correctamente" });
            }
            return Ok(new { Mensaje = "Error: La accion no existe" });
        }
    }
}
