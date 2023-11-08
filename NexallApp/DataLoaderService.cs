namespace NexallApp
{
    public class DataLoaderService
    {
        private readonly ApplicationDbContext _context;

        public DataLoaderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void LoadDataFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var dataParts = line.Split('\t');
                if (dataParts.Length >= 3)
                {
                    var carData = new CarData
                    {
                        Date = DateTime.Parse(dataParts[0]),
                        Speed = int.Parse(dataParts[1]),
                        CarRegNo = dataParts[2]
                    };
                    _context.CarData.Add(carData);
                }
            }

            _context.SaveChanges();
        }
    }
}
