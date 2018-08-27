using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TACSProject.Controllers
{
    public class TestController : Controller
    {
        [Route("test/test/xyz/{var2}")]
        public ActionResult Test(string var2)
        {

            return Content(var2);
        }
    }
}