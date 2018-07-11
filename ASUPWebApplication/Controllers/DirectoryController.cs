using SeeAllClassLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASUPWebApplication.Controllers
{
    public class DirectoryController : Controller
    {
        IDirectoriesRepository directoriesRepository;

        public DirectoryController(IDirectoriesRepository repository)
        {
            directoriesRepository = repository;
        }

        // GET: Directory
        public ViewResult Index()
        {
            return View();
        }
    }
}