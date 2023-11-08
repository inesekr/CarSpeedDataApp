using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NexallApp.Pages
{
    public class ChartsModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public ChartsModel(ApplicationDbContext context)
		{
			_context = context;
            //_context.Database.SetCommandTimeout(120);
        }
		public List<double> AverageSpeedByHour { get; set; }

		public void OnGet(DateTime selectedDate)
		{
            IQueryable<CarData> query = _context.CarData;
			query = query.Where(c => c.Date.Date ==  selectedDate.Date);
            //_context.Database.SetCommandTimeout(300);
            var data = query.ToList();
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
