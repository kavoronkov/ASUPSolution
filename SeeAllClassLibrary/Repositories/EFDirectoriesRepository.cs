using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Repositories
{
    public class EFDirectoriesRepository : IDirectoriesRepository
    {
        EFDirectoriesContext context = new EFDirectoriesContext();

        public IEnumerable<Plant> Plants
        {
            get { return context.Plants; }
        }

        public IEnumerable<Workshop> Workshops
        {
            get { return context.Workshops; }
        }

        public IEnumerable<Department> Departments
        {
            get { return context.Departments; }
        }

        public IEnumerable<Point> Points
        {
            get { return context.Points; }
        }
    }
}
