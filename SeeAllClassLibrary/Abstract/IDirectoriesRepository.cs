using SeeAllClassLibrary.Directories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Abstract
{
    public interface IDirectoriesRepository
    {
        IEnumerable<Plant> Plants { get; }
        IEnumerable<Workshop> Workshops { get; }
        IEnumerable<Department> Departments { get; }
        IEnumerable<Point> Points { get; }
    }
}
