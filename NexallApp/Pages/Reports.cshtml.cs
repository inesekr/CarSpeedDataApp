using Microsoft.AspNetCore.Mvc;
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
        public int MinSpeed { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime FromDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime ToDate { get; set; }

        public int TotalItems { get; set; }

        public void OnGet(int minSpeed, int pageNumber = 1)
        {
            int pageSize = 20;
            IQueryable<CarData> query = _context.CarData;

            if (minSpeed > 0)
            {
                query = query.Where(c => c.Speed >= minSpeed);
                _context.Database.SetCommandTimeout(300);
            }

            if (FromDate != default(DateTime))
            {
                query = query.Where(c => c.Date.Date >= FromDate.Date);
                _context.Database.SetCommandTimeout(300);
            }

            if (ToDate != default(DateTime))
            {
                ToDate = ToDate.Date.AddDays(1).AddMilliseconds(-1);
                query = query.Where(c => c.Date.Date <= ToDate.Date);
                _context.Database.SetCommandTimeout(300);
            }

            _context.Database.SetCommandTimeout(300);
            TotalItems = query.Count();
            _context.Database.SetCommandTimeout(300);

            TotalPages = (int)Math.Ceiling(TotalItems / (double)pageSize);

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

            int skip = (Page - 1) * pageSize;
     
            FilteredCarData = query.Skip(skip).Take(pageSize).ToList();

            MinSpeed = minSpeed;
            FromDate = FromDate.Date;
            ToDate = ToDate.Date;

            Page = pageNumber;
        }
    }
}
