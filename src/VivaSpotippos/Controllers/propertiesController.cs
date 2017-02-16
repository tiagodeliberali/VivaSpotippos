using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Entities;
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
        public PropertyGetListResponse Get(int ax, int ay, int bx, int by)
        {
            var startPosition = new Position(ax, ay);
            var endPosition = new Position(bx, by);

            var properties = propertyStore.Get(startPosition, endPosition);

            return new PropertyGetListResponse()
            {
                foundProperties = properties.Count,
                properties = properties
            };
        }

        [HttpGet("{id}")]
        public Property Get(int id)
        {
            return propertyStore.Get(id);
        }

        [HttpPost]
        public ObjectResult Post([FromBody]PropertyPostRequest data)
        {
            var response = new PropertyPostResponse();

            var validation = PropertyValidation.Validate(data);

            try
            {
                if (validation.IsValid)
                {
                    response.Status = ResponseStatus.Success;
                    response.Message = SystemMessages.Created;
                    response.CreatedProperty = propertyStore.AddProperty(data);

                    return Created(
                        string.Format("http://www.google.com/properties/{0}", response.CreatedProperty.Id),
                        response);
                }
                else
                {
                    response.Status = ResponseStatus.InvalidProperty;
                    response.Message = validation.ErrorMessage;

                    return BadRequest(response);
                }
            }
            catch(PropertyStoreAddException storeEx)
            {
                response.Status = ResponseStatus.PropertyAddException;
                response.Message = storeEx.Message;

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.GeneralException;
                response.Message = ex.Message;

                return InternalError(response);
            }
        }

        private ObjectResult InternalError(object response)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
    }
}
