using Microsoft.AspNetCore.Mvc;

namespace Tutorial.Web.Controllers
{
    [Route("[controller]")]
    public class AboutController
    {
        public string Me()
        {
            return "Dave";
        }
        public string Company()
        {
            return "No Company";
        }
    }
}
