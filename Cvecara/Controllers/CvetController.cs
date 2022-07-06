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
    [Route("api/cvet")]
    [Produces("application/json", "applciation/xml")]
    public class CvetController : ControllerBase
    {
        private readonly ICvetRepository cvetRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IVrstaCvetaRepository vrstaCvetaRepository;

        public CvetController(ICvetRepository cvetRepository, IVrstaCvetaRepository vrstaCvetaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.cvetRepository = cvetRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.vrstaCvetaRepository = vrstaCvetaRepository;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni, Kupac")]
      // [Authorize(Roles = "Kupac")]
       // [Authorize]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Cvet>> GetCvetovi()
        {
            var cvetovi = cvetRepository.GetAllCvetovi();
            if (cvetovi == null || cvetovi.Count == 0)
            {
                return NoContent();
            }

            foreach (Cvet cvet in cvetovi)
            {
                VrstaCveta vrstaCveta = vrstaCvetaRepository.GetVrstaCvetaById(cvet.vrstaCvetaID);
                cvet.vrstaCveta = vrstaCveta;
            }

            return Ok(cvetovi);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{cvetId}")]
        public ActionResult<Cvet> GetCvet(int cvetId)
        {
            var cvet = cvetRepository.GetCvetById(cvetId);

            if (cvet == null)
            {
                return NotFound();
            }

            VrstaCveta vrstaCveta = vrstaCvetaRepository.GetVrstaCvetaById(cvet.vrstaCvetaID);
            cvet.vrstaCveta = vrstaCveta;

            return Ok(mapper.Map<Cvet>(cvet));
        }

       
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public ActionResult<Cvet> CreateCvet([FromBody] Cvet cvet)
        {
            try
            {

                Cvet cvetEntity = mapper.Map<Cvet>(cvet);
                Cvet confirmation = cvetRepository.CreateCvet(cvetEntity);
                cvetRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetCvet", "Cvet", new { cvetId = confirmation.cvetID});

                return StatusCode(StatusCodes.Status201Created, "Successfully created");
                
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<Cvet> UpdateCvet([FromBody] Cvet cvet)
        {
            try
            {
                var oldCvet = cvetRepository.GetCvetById(cvet.cvetID);

                if (oldCvet == null)
                {
                    return NotFound();
                }

                Cvet cvetEntity = mapper.Map<Cvet>(cvet);

                mapper.Map(cvetEntity, oldCvet);
                cvetRepository.SaveChanges();

                return Ok();
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
        [HttpDelete("{cvetId}")]
        public IActionResult DeleteCvet(int cvetId)
        {
            try
            {
                var cvet = cvetRepository.GetCvetById(cvetId);

                if (cvet == null)
                {
                    return NotFound();
                }

                cvetRepository.DeleteCvet(cvetId);
                cvetRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

    }
}

