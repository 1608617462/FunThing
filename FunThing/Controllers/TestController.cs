using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FunThing.Controllers
{
    /// <summary>
    /// 测试Swagger运行是否正常
    /// </summary>
    public class TestController : ApiController
    {
        // 这是一个Get方法
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // 这也是一个Get方法
        public string Get(int id)
        {
            return "value";
        }

        // 这是一个POST方法
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