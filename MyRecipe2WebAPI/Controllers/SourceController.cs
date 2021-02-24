using MyRecipe2.Services;
using MyRecipe2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyRecipe2WebAPI.Controllers
{
    public class SourceController : ApiController
    {
        public IHttpActionResult Get()
        {
            SourceService sourceService = CreateSourceService();
            var notes = sourceService.GetAllSources();
            return Ok(notes);
        }
        public IHttpActionResult Get(int id)
        {
            SourceService sourceService = CreateSourceService();
            var source = sourceService.GetSourceById(id);
            return Ok(source);
        }
        public IHttpActionResult Post(SourceCreate source)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSourceService();

            if (!service.CreateSource(source))
                return InternalServerError();

            return Ok();
        }
        private SourceService CreateSourceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var sourceService = new SourceService(userId);
            return sourceService;
        }

    }
}
