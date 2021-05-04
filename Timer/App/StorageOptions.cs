namespace Timer.App
{
    public class StorageOptions
    {
        public StorageOptions(string storageFolder)
        {
            StorageFolder = storageFolder;
        }

        public string StorageFolder { get; }
    }
}