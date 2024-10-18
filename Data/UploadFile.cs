namespace SnapPrintWeb.Data
{
    public class UploadedFile
    {
        public int FileId { get; set; }  // Primary Key
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public byte[] FileData { get; set; }

    }
}

