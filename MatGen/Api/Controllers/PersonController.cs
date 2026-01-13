using Api.Extensions;
using Api.Services;
using Azure.Core;
using DocumentFormat.OpenXml.Office2016.Excel;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_personService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpPost()]
        public IActionResult Update([FromBody] PersonModel model)
        {
            try
            {
                var value = _personService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _personService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


    }



    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private PersonsService _personsService;

        public PersonsController(PersonsService personsService)
        {
            _personsService = personsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_personsService.All()); }
            catch (Exception ex) { 
                return BadRequest(ex.ProblemDetails()); }
        }


        [HttpPost("lineage/{personIdFrom}/{personIdTo}")]
        public IActionResult GenerateLineageBranchesPdf(Guid personIdFrom, Guid personIdTo)
        {
            var persons = _personsService.All();
            var personFrom = persons.FirstOrDefault(x => x.Guid == personIdFrom);
            var personTo  = persons.FirstOrDefault(x => x.Guid == personIdTo);
            var ancestorsFrom = personFrom?.Ancestors(persons) ?? new();
            var ancestorsTo = personTo?.Ancestors(persons) ?? new();
            var commonAncestors = ancestorsFrom.Where(x => ancestorsTo.Any(y => x.Person.Guid == y.Person.Guid)).ToList();
            var commonAncestor1 = commonAncestors.MinBy(x => x.Id);
            var commonAncestor2 = ancestorsTo.FirstOrDefault(x => x.Person.Guid == commonAncestor1?.Person.Guid);
            
           
            var line1 = GetLine(ancestorsFrom, commonAncestor1?.Id ?? 0);
            //line1.Reverse();
            var line2 = GetLine(ancestorsTo, commonAncestor2?.Id ?? 0);

            var pdfBytes = PdfService.GenerateLineageBranchesPdf(line1, line2);
            return File(pdfBytes, "application/pdf", "arbre-genealogic.pdf");
        }

        List<AncestorModel> GetLine(List<AncestorModel> ancestors, int rootId)
        {
            List<AncestorModel> retval = new();
            var ancestor = ancestors.First(x => x.Id == rootId);
            while (ancestor != null)
            {
                retval.Add(ancestor);
                if (ancestor.Id == 1) break; // root ancestor
                var nextId = ancestor.Id % 2 == 0 ? ancestor.Id / 2 : (ancestor.Id - 1) / 2;
                ancestor = ancestors.FirstOrDefault(x => x.Id == nextId);
            }
            return retval;
        }



    }
}
