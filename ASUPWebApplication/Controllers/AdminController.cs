using SeeAllClassLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASUPWebApplication.Controllers
{
    public class AdminController : Controller
    {
        ISeeAllRepository seeAllRepository;

        public AdminController(ISeeAllRepository repository)
        {
            seeAllRepository = repository;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(seeAllRepository);
        }
    }
}