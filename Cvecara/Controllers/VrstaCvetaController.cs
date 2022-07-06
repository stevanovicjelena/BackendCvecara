using AutoMapper;
using Cvecara.Entities;
using Cvecara.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Controllers
{
    [ApiController]
    [Route("api/vrstaCveta")]
    [Produces("application/json", "applciation/xml")]
    public class VrstaCvetaController : ControllerBase
    {
        private readonly IVrstaCvetaRepository vrstaCvetaRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public VrstaCvetaController(IVrstaCvetaRepository vrstaCvetaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.vrstaCvetaRepository = vrstaCvetaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<VrstaCveta>> GetVrsteCvetova(string nazivVrste)
        {
            var vrsteCvetova = vrstaCvetaRepository.GetAllVrsteCvetova(nazivVrste);
            if (vrsteCvetova == null || vrsteCvetova.Count == 0)
            {
                return NoContent();
            }

            return Ok(vrsteCvetova);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{vrstaCvetaId}")]
        public ActionResult<VrstaCveta> GetVrstaCveta(int vrstaCvetaId)
        {
            var vrstaCveta = vrstaCvetaRepository.GetVrstaCvetaById(vrstaCvetaId);

            if (vrstaCveta == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VrstaCveta>(vrstaCveta));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public ActionResult<VrstaCveta> CreateVrstaCveta([FromBody] VrstaCveta vrstaCveta)
        {
            try
            {

                VrstaCveta vrstaCvetaEntity = mapper.Map<VrstaCveta>(vrstaCveta);
                VrstaCveta confirmation = vrstaCvetaRepository.CreateVrstaCveta(vrstaCvetaEntity);
                vrstaCvetaRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetvrstaCveta", "vrstaCveta", new { vrstaCvetaId = confirmation.vrstaCvetaID });


                return Created(location, mapper.Map<VrstaCveta>(confirmation));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPut]
        public ActionResult<VrstaCveta> UpdateVrstaCveta([FromBody] VrstaCveta vrstaCveta)
        {
            try
            {
                var oldVrstaCveta = vrstaCvetaRepository.GetVrstaCvetaById(vrstaCveta.vrstaCvetaID);

                if (oldVrstaCveta == null)
                {
                    return NotFound();
                }

                VrstaCveta vrstaCvetaEntity = mapper.Map<VrstaCveta>(vrstaCveta);
                mapper.Map(vrstaCvetaEntity, oldVrstaCveta);
                vrstaCvetaRepository.SaveChanges();

                return Ok(mapper.Map<VrstaCveta>(vrstaCveta));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{vrstaCvetaId}")]
        public IActionResult DeleteVrstaCveta(int vrstaCvetaId)
        {
            try
            {
                var vrstaCveta = vrstaCvetaRepository.GetVrstaCvetaById(vrstaCvetaId);

                if (vrstaCveta == null)
                {
                    return NotFound();
                }

                vrstaCvetaRepository.DeleteVrstaCveta(vrstaCvetaId);
                vrstaCvetaRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
