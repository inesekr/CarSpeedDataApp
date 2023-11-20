using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarSpeedDataApp.Pages
{
    public class FileUploadModel : PageModel
    {
        [BindProperty]
        public string? FilePath { get; set; }

        private readonly DataLoaderService _dataLoaderService;

        public FileUploadModel(DataLoaderService dataLoaderService)
        {
            _dataLoaderService = dataLoaderService;
        }

        public void OnPost()
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                _dataLoaderService.LoadDataFromFile(FilePath);

                TempData["SuccessMessage"] = "Data has been uploaded successfully!";
            }
            RedirectToPage("/Index");
          
            // add error message if empty, or file not found
        }
    }
}
