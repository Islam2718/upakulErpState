using System.Threading.Tasks;
using CommonServices.Enums;
using CommonServices.Repository.Abastract;
using CommonServices.RequestModel;
using CommonServices.ResponseModel;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace CommonServices.Repository.Implementation
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteImage(string fileNameWithLocation)
        {
            //var wwPath = this._webHostEnvironment.WebRootPath;
            var path = Path.Combine(fileNameWithLocation);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
        #region Save
        public async Task<FileStorageResponse> SingleFileStorage(FileStorageRequest request)
        {
            if (request.SingleFile == null)
            {
                var fsr = new FileStorageResponse() { Message = "File not found" };
                return fsr;
            }
            else if ((request.MaxFileSize ?? (2 * 1024 * 1024)/* Default 2MB */) < request.SingleFile.Length)
            {
                var fsr = new FileStorageResponse() { Message = "File SIze is large" };
                return fsr;
            }
            else if (string.IsNullOrEmpty(request.Location))
            {
                var fsr = new FileStorageResponse() { Message = "File Location is required" };
                return fsr;
            }
            else
            {
                var fsr = new FileStorageResponse();
                var ext = Path.GetExtension(request.SingleFile.FileName).ToLower();

                string msg = (request.FileTypeAllow == FileTypeEnum.Image.ToString() && AllowedImageExtensions.Contains(ext)) ? "" // Image
                    : (request.FileTypeAllow == FileTypeEnum.Word.ToString() && AllowedWordExtensions.Contains(ext)) ? ""         // Word File
                    : (request.FileTypeAllow == FileTypeEnum.Excel.ToString() && AllowedExcelExtensions.Contains(ext)) ? ""      // Excel File
                    : (request.FileTypeAllow == FileTypeEnum.PDF.ToString() && AllowedPDFExtensions.Contains(ext)) ? ""         // PDF File
                    : (request.FileTypeAllow == FileTypeEnum.Any.ToString() && AllowedAnyExtensions.Contains(ext)) ? ""        // Any Type File
                    : "This file extension is not allow";
                if (msg != "")
                    fsr.Message = msg;
                else
                {
                    var contentPath = this._webHostEnvironment.ContentRootPath;
                    var dynamicLocation = "/"
                        + (string.IsNullOrEmpty(request.EmployeeId) ? "" : "E" + request.EmployeeId)
                        + (string.IsNullOrEmpty(request.MemberId) ? "" : "M" + request.MemberId);
                    var location = request.Location + dynamicLocation;
                    try
                    {
                        var path = Path.Combine(contentPath, location);

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var res = await FileSave(request.SingleFile, ext, path, dynamicLocation);
                        if (res.Success)
                        {
                            fsr.Success = true;
                            List<FileAfterStorageInfo> lst = new List<FileAfterStorageInfo>();
                            lst.Add(res.fileAfterStorageInfo);
                            fsr.fileAfterStorageInfos = lst;
                        }

                        else fsr.Message = res.Message;
                    }
                    catch (Exception ex)
                    {
                        fsr.Message = ex.Message;
                    }
                }
                return fsr;
            }
        }
        public async Task<FileStorageResponse> MultipleFileStorage(FileStorageRequest request)
        {
            if (request.MultipleFile == null)
            {
                var fsr = new FileStorageResponse() { Message = "File not found" };
                return fsr;
            }
            else if (string.IsNullOrEmpty(request.Location))
            {
                var fsr = new FileStorageResponse() { Message = "File Location is required" };
                return fsr;
            }

            else
            {
                var fsr = new FileStorageResponse();
                string msg = "";
                foreach (var item in request.MultipleFile)
                {
                    if ((request.MaxFileSize ?? (2 * 1024 * 1024)/* Default 2MB */) < item.Length)
                    {
                        msg = "File SIze is large";
                        break;
                    }
                    var ext = Path.GetExtension(item.FileName).ToLower();

                    msg = (request.FileTypeAllow == FileTypeEnum.Image.ToString() && AllowedImageExtensions.Contains(ext)) ? "" // Image
                        : (request.FileTypeAllow == FileTypeEnum.Word.ToString() && AllowedWordExtensions.Contains(ext)) ? ""         // Word File
                        : (request.FileTypeAllow == FileTypeEnum.Excel.ToString() && AllowedExcelExtensions.Contains(ext)) ? ""      // Excel File
                        : (request.FileTypeAllow == FileTypeEnum.PDF.ToString() && AllowedPDFExtensions.Contains(ext)) ? ""         // PDF File
                        : (request.FileTypeAllow == FileTypeEnum.Any.ToString() && AllowedAnyExtensions.Contains(ext)) ? ""        // Any Type File
                        : "This file extension is not allow";
                    if (msg != "")
                        break;

                }
                if (msg != "")
                    fsr.Message = msg;
                else
                {
                    var contentPath = this._webHostEnvironment.ContentRootPath;
                    var dynamicLocation = "\\"
                        + (string.IsNullOrEmpty(request.EmployeeId) ? "" : "E" + request.EmployeeId)
                        + (string.IsNullOrEmpty(request.MemberId) ? "" : "M" + request.MemberId);
                    var location = request.Location + dynamicLocation;
                    try
                    {
                        var path = Path.Combine(contentPath, location);

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var lst = new List<FileAfterStorageInfo>();
                        foreach (var file in request.MultipleFile)
                        {
                            var ext = Path.GetExtension(file.FileName).ToLower();
                            var res = await FileSave(file, ext, path, dynamicLocation);
                            if (res.Success)
                            {
                                fsr.Success = true;
                                lst.Add(res.fileAfterStorageInfo);
                            }
                            else
                            {
                                fsr = new FileStorageResponse();
                                fsr.Message = res.Message;
                                break;
                            }
                        }
                        if (fsr.Success)
                            fsr.fileAfterStorageInfos = lst;
                    }
                    catch (Exception ex)
                    {
                        fsr.Message = ex.Message;
                    }
                }

                return fsr;
            }
        }
        private async Task<ResponseStatus> FileSave(IFormFile formFile, string extension, string fullPath, string location)
        {
            var fsr = new ResponseStatus();
            try
            {
                var gud = Guid.NewGuid().ToString().Replace("-", "");
                var rnd = new Random().Next(1, gud.Length - 6);

                string uniqueString = DateTime.Now.ToString("yyyyMMddHHmmss") + gud.Substring(rnd).Substring(0, 6);

                var newFileName = "";

                if (AllowedImageExtensions.Contains(extension) && extension != ".webp")
                {
                    using var inputStream = new MemoryStream();
                    await formFile.CopyToAsync(inputStream);
                    inputStream.Position = 0;

                    // 2. Read the image with Magick.NET
                    using var image = new MagickImage(inputStream);

                    // 3. Set output format to WebP
                    image.Format = MagickFormat.WebP;

                    // Optional: adjust quality
                    image.Quality = 80;

                    // Define custom output path
                    var webpFileName = Path.ChangeExtension(uniqueString, ".webp");
                    newFileName= webpFileName;
                    var outputPath = Path.Combine(fullPath, webpFileName);

                    //using var outputStream = new MemoryStream();
                    await image.WriteAsync(outputPath);
                }
                else
                {
                     newFileName = uniqueString + extension;
                    var fileWithPath = Path.Combine(fullPath, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                    stream.Close();
                }
                fsr.Success = true;

                FileAfterStorageInfo fileAfterStorageInfo = new FileAfterStorageInfo()
                {
                    FileExtention = extension,
                    FileName = newFileName,
                    FileLocation = location,
                    FileOrginalLocation = fullPath
                };
                fsr.fileAfterStorageInfo = fileAfterStorageInfo;
            }
            catch (Exception ex)
            {
                fsr.Message = ex.Message;
            }
            return fsr;
        }

        //private void ConvertToWebP(string inputPath, string outputPath)
        //{
        //    using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(inputPath))
        //    {
        //        image.Save(outputPath, new WebpEncoder()
        //        {
        //            Quality = 75 // Adjust quality if needed
        //        });
        //    }
        //}

        private string[] AllowedImageExtensions = { ".jpg", ".png", ".jpeg", ".webp" };
        private string[] AllowedWordExtensions = { ".doc", ".docx" };
        private string[] AllowedExcelExtensions = { ".xls", ".xlsx", ".csv" };
        private string[] AllowedPDFExtensions = { ".pdf" };
        private string[] AllowedAnyExtensions = { ".jpg", ".png", ".jpeg", ".doc", ".docx", ".xls", ".xlsx", ".csv", ".pdf", ".pptx", ".ppt" };

        #endregion 
    }
}
