using System.Reflection;

namespace hw5_mvc_.Models.Services
{
    public class FileService(IWebHostEnvironment environment)
    {
        public async Task<ImageFile> SaveAsync(string mainSubDir,IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + extension;

            var dir1 = fileName[0].ToString();
            var dir2 = fileName[1].ToString();

            Directory.CreateDirectory(Path.Combine(environment.WebRootPath, "uploads", mainSubDir, dir1, dir2));
            var fullFileName = Path.Combine(environment.WebRootPath, "uploads", mainSubDir, dir1, dir2, fileName);
            using var fs = new FileStream(fullFileName, FileMode.Create);
            await file.CopyToAsync(fs);

            return new ImageFile
            {
                FileName = string.Join("/", new List<string> { mainSubDir, dir1, dir2, fileName }),
                OriginalFileName = file.FileName
            };
        }
        public void Delete(ImageFile file)
        {
            var fullFilename = Path.Combine(environment.WebRootPath, "uploads", file.FileName);

            if (File.Exists(fullFilename))
            {
                File.Delete(fullFilename);
            }
        }
    }
}
