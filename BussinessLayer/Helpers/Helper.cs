using BussinessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BussinessLayer.Helpers 
{
    public  class Helper
    {

        public string PathImage { get; set; } 
        public string LivePathImages { get; set; }

        public void LogError(Exception ex)
        {
            //Log Error Code Here
        }

        #region Delete File From Directory
        /// <summary>
        /// Delete File from specified Directory 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns>
        /// true if file is deleted successfully or false if failed
        /// </returns>
        public bool DeleteFiles(string FileName)
        {
            try
            {
                #region Check if FileName 
                #endregion
                if (string.IsNullOrEmpty(FileName))
                    return false;
                string PathToSave = PathImage + FileName;
                System.IO.File.Delete(PathToSave);
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
        #endregion

        #region Function take image and return image name that store in db 
        /// <summary>
        /// generate unique name of image and save image in specified path 
        /// </summary>
        /// <param name="Image"></param>
        /// <returns>
        /// unique name of iamge concatenating with extension of image 
        /// </returns>
        public string UploadImage(IFormFile Image,string path)
        {
            try
            {
                var pathToSave = path;
                if (Image.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var fullPath = Path.Combine(pathToSave, fileName);


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                        Image.CopyTo(stream);
                    return fileName;
                }
                else
                    return "error";
            }
            catch (Exception ex)
            {
                LogError(ex);
                return "error in Upload Iamge ";
            }
        }

        #endregion
    }
}
