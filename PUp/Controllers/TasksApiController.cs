using Newtonsoft.Json;
using PUp.Models.Repository;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace PUp.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TasksApiController : ApiController
    {
        // GET api/<controller>
        public  NegotiatedContentResult<string> Get()
        {
            TaskRepository tf = new TaskRepository();
            var tasks = tf.GetAll();
            var list =JsonConvert.SerializeObject(tasks,
               Formatting.None,
               new JsonSerializerSettings()
               {
                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                   MaxDepth= 1,
               });
            return Content(HttpStatusCode.OK,list);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}