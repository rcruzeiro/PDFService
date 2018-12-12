using System;
using System.Threading.Tasks;
using Core.Framework.API.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PDFService.Adapter;
using PDFService.API.Messages.Templates;
using PDFService.DI;
using PDFService.DTO;

namespace PDFService.API.Controllers
{
    [Route("clients/{clientID}/[controller]")]
    public class TemplateController : BaseController
    {
        public TemplateController(IConfiguration configuration)
            : base(configuration)
        { }

        /// <summary>
        /// Gets all stored templates of the specified client.
        /// </summary>
        /// <returns>A list with templates.</returns>
        /// <param name="clientID">Client identifier.</param>
        /// <response code="200">Get was successfull.</response>
        /// <response code="500">Internal error. See response message for details.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetTemplatesResponse), 200)]
        [ProducesResponseType(typeof(GetTemplatesResponse), 500)]
        [HttpGet]
        public ActionResult<GetTemplatesResponse> Get([FromRoute]string clientID)
        {
            GetTemplatesResponse response;
            string responseMessage = $"GET_{clientID}_TEMPLATES";
            string cacheKey = $"{clientID}_TEMPLATES";

            try
            {
                if (ExistsInCache(cacheKey))
                    response = GetFromCache<GetTemplatesResponse>(cacheKey);
                else
                {
                    var factory = Factory.Instance.GetTemplate(_configuration);
                    var templates = factory.GetTemplates(clientID);
                    response = new GetTemplatesResponse
                    {
                        StatusCode = "200"
                    };
                    templates.ForEach(template =>
                        response.Data.Add(template.Adapt()));
                    AddToCache(cacheKey, response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new GetTemplatesResponse
                {
                    StatusCode = "500"
                };
                response.Messages.Add(ResponseMessage.Create(ex, responseMessage));
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Get the specified stored template.
        /// </summary>
        /// <returns>The template DTO.</returns>
        /// <param name="clientID">Client identifier.</param>
        /// <param name="id">Template identifier.</param>
        /// <response code="200">Get was successfull.</response>
        /// <response code="500">Internal error. See response message for details.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetTemplateResponse), 200)]
        [ProducesResponseType(typeof(GetTemplateResponse), 500)]
        [HttpGet("{id}")]
        public ActionResult<GetTemplateResponse> Get([FromRoute]string clientID, [FromRoute]int id)
        {
            GetTemplateResponse response;
            string responseMessage = $"GET_{clientID}_TEMPLATE_{id}";
            string cacheKey = $"{clientID}_TEMPLATE_{id}";

            try
            {
                if (ExistsInCache(cacheKey))
                    response = GetFromCache<GetTemplateResponse>(cacheKey);
                else
                {
                    var factory = Factory.Instance.GetTemplate(_configuration);
                    var template = factory.GetTemplate(clientID, id);
                    response = new GetTemplateResponse
                    {
                        StatusCode = "200",
                        Data = template.Adapt()
                    };
                    AddToCache(cacheKey, response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new GetTemplateResponse
                {
                    StatusCode = "500"
                };
                response.Messages.Add(ResponseMessage.Create(ex, responseMessage));
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Create a new template for the specified client.
        /// </summary>
        /// <param name="clientID">Client identifier.</param>
        /// <param name="request">Request with new template data.</param>
        /// <response code="200">Create was successfull.</response>
        /// <response code="500">Internal error. See response message for details.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateTemplateResponse), 200)]
        [ProducesResponseType(typeof(CreateTemplateResponse), 500)]
        [HttpPost]
        public async Task<ActionResult<CreateTemplateResponse>> Post([FromRoute]string clientID, [FromBody]CreateTemplateRequest request)
        {
            CreateTemplateResponse response = new CreateTemplateResponse();
            string responseMessage = $"CREATE_{clientID}_TEMPLATE_{request.Title}";

            try
            {
                var dto = new TemplateDTO
                {
                    ClientID = clientID,
                    Title = request.Title,
                    Description = request.Description,
                    Page = request.Page,
                    Content = request.Content
                };
                var factory = Factory.Instance.GetTemplate(_configuration);
                await factory.Save(dto.Adapt());
                response.StatusCode = "200";
                response.Data = $"Template {request.Title} created with success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = "500";
                response.Messages.Add(ResponseMessage.Create(ex, responseMessage));
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Update the specified template.
        /// </summary>
        /// <param name="clientID">Client identifier.</param>
        /// <param name="id">Template identifier.</param>
        /// <param name="request">Request with update data.</param>
        /// <response code="200">Update was successfull.</response>
        /// <response code="500">Internal error. See response message for details.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(UpdateTemplateResponse), 200)]
        [ProducesResponseType(typeof(UpdateTemplateResponse), 500)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateTemplateResponse>> Put([FromRoute]string clientID, [FromRoute]int id, [FromBody]UpdateTemplateRequest request)
        {
            UpdateTemplateResponse response = new UpdateTemplateResponse();
            string responseMessage = $"UPDATE_{clientID}_TEMPLATE_{id}";
            string cacheKey = $"{clientID}_TEMPLATE_{id}";

            try
            {
                var factory = Factory.Instance.GetTemplate(_configuration);
                var template = factory.GetTemplate(clientID, id);
                #region update template
                template.Title = request.Title;
                template.Description = request.Description;
                template.Page = request.Page;
                template.Content = request.Content;
                #endregion
                await factory.Save(template);
                RemoveFromCache(cacheKey);
                response.StatusCode = "200";
                response.Data = $"Template {id} updated with success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = "500";
                response.Messages.Add(ResponseMessage.Create(ex, responseMessage));
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Delete the specified template.
        /// </summary>
        /// <param name="clientID">Client identifier.</param>
        /// <param name="id">Template identifier.</param>
        /// <response code="200">Delete was successfull.</response>
        /// <response code="500">Internal error. See response message for details.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(DeleteTemplateResponse), 200)]
        [ProducesResponseType(typeof(DeleteTemplateResponse), 500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteTemplateResponse>> Delete([FromRoute]string clientID, [FromRoute]int id)
        {
            DeleteTemplateResponse response = new DeleteTemplateResponse();
            string responseMessage = $"DELETE_{clientID}_TEMPLATE_{id}";

            try
            {
                var factory = Factory.Instance.GetTemplate(_configuration);
                await factory.Delete(clientID, id);
                response.StatusCode = "200";
                response.Data = $"Template {id} deleted with success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = "500";
                response.Messages.Add(ResponseMessage.Create(ex, responseMessage));
                return StatusCode(500, response);
            }
        }
    }
}
