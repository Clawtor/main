using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Blog.Controllers
{
	public class PersonController : ApiController
	{
		public IHttpActionResult Get()
		{
			return Ok("Hello, web api");
		}
	}
}