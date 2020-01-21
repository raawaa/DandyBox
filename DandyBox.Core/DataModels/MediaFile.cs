namespace DandyBox.Core.DataModels
{
    public class MediaFile
    {
        public int MediaFileId { get; set; }
        public string FilePath { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}