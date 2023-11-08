using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NexallApp.Pages
{
    public class ReportsModel : PageModel
    {
        private readonly ApplicationDbContext _context; 

        public ReportsModel(ApplicationDbContext context)
        {
            _context = context; 
        }
        public List<CarData> FilteredCarData { get; set; }

        public int TotalPages { get; set; }
        public int Page { get; set; }



        public void OnGet(int minSpeed, DateTime fromDate, DateTime toDate, int pageNumber = 1)
        {
            int pageSize = 20;
            IQueryable<CarData> query = _context.CarData;

            if (minSpeed > 0)
            {
                query = query.Where(c => c.Speed >= minSpeed);
                //_context.Database.SetCommandTimeout(120);
            }

            if (fromDate != default(DateTime))
            {
                query = query.Where(c => c.Date >= fromDate);
                //_context.Database.SetCommandTimeout(120);
            }

            if (toDate != default(DateTime))
            {
                query = query.Where(c => c.Date <= toDate);
                //_context.Database.SetCommandTimeout(120);
            }

            int totalItems = query.Count();
            //_context.Database.SetCommandTimeout(120);

            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (pageNumber < 1)
            {
                Page = 1;
            }
            else if (pageNumber > TotalPages)
            {
                Page = TotalPages;
            }
            else
            {
                Page = pageNumber;
            }

            int skip = (pageNumber - 1) * pageSize;

            FilteredCarData = query.Skip(skip).Take(pageSize).ToList();
        }
    }
}
