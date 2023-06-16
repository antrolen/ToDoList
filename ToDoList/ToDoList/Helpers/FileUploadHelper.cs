namespace ToDoList.Helpers
{
   static public class FileUploadHelper
    {
        static public async Task<string> UploadAsync(IFormFile ImageUrl)
        {
            if (ImageUrl != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageUrl.FileName)}";
                using var fs = new FileStream($@"wwwroot/uploads/{fileName}", FileMode.Create);
                await ImageUrl.CopyToAsync(fs);
                return $@"/uploads/{fileName}";
            }

            throw new Exception("File was not upload");
        }

    }
}
