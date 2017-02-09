using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VivaSpotippos.Model;
using VivaSpotippos.Model.RestEntities;
using VivaSpotippos.Model.Validation;

namespace VivaSpotippos.Controllers
{
    [Route("[controller]")]
    public class PropertiesController : Controller
    {
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
        public PostResponse Post([FromBody]IPropertyData property)
        {
            var response = new PostResponse();

            var validation = PropertyValidation.Validate(property);

            if (validation.IsValid)
            {
                response.Message = "Created";
            }
            else
            {
                response.Status = "1";
                response.Message = validation.ErrorMessage;
            }

            return response;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
