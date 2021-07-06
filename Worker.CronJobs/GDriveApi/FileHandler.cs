using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Worker.CronJobs.GDriveApi
{
    public static class FileHandler
    {
        public static void FileUploadInFolder(string filepath)
        {
            //string file_path = @"C:\Backup\CloudApp_202106081805.bak";
            var driveService = Authentication.Credential();

            var FileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filepath),
                MimeType = MimeTypes.GetMimeType(Path.GetFileName(filepath)),
                //Parents = new List<string>
                //{
                //    folderId
                //}
            };
            
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(filepath, System.IO.FileMode.Open))
            {
                request = driveService.Files.Create(FileMetaData, stream,
                FileMetaData.MimeType);
                request.Fields = "id";
                request.Upload();
            }
            var file1 = request.ResponseBody;
        }
    }
}
