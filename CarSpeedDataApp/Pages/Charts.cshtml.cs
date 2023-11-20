using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp.Pages
{
    public class ChartsModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public ChartsModel(ApplicationDbContext context)
		{
			_context = context;
        }
		public List<double> AverageSpeedByHour { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime SelectedDate { get; set; }

        public void OnGet()
        {
            IQueryable<CarData> query = _context.CarData;

            if (SelectedDate != default)
            {
                query = query.Where(c => c.Date.Date == SelectedDate.Date);
            }
            
            var data = query.ToList();
            _context.Database.SetCommandTimeout(360);

            AverageSpeedByHour = CalculateAverageSpeedByHour(data);
		}

		private List<double> CalculateAverageSpeedByHour(List<CarData> data)
		{
			var averageSpeedByHour = new List<double>();

			for (int hour = 0; hour < 24; hour++)
			{
				var carDataForHour = data.Where(item => item.Date.Hour == hour);

				if (carDataForHour.Any())
				{
					var averageSpeed = carDataForHour.Average(item => item.Speed);
					averageSpeedByHour.Add(averageSpeed);
				}
				else
				{
					averageSpeedByHour.Add(0);
				}
			}

			return averageSpeedByHour;
		}	
	}
}
