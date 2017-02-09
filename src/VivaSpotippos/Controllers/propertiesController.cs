using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VivaSpotippos.Model.RestEntities;
using VivaSpotippos.Model.Validation;
using VivaSpotippos.Stores;

namespace VivaSpotippos.Controllers
{
    [Route("[controller]")]
    public class PropertiesController : Controller
    {
        private IPropertyStore propertyStore;

        public PropertiesController(IPropertyStore propertyStore)
        {
            this.propertyStore = propertyStore;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ObjectResult Post([FromBody]PropertyPostRequest data)
        {
            var response = new PropertyPostResponse();

            var validation = PropertyValidation.Validate(data);

            if (validation.IsValid)
            {
                response.Status = "0";
                response.Message = "Created";
                response.CreatedProperty = propertyStore.AddProperty(data);

                return Created(
                    string.Format("http://www.google.com/properties/{0}", response.CreatedProperty.id),
                    response);
            }
            else
            {
                response.Status = "1";
                response.Message = validation.ErrorMessage;

                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
