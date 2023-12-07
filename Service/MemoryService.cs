using Azure.Identity;
using Azure.Storage.Blobs;

namespace ContosoPizza.Service
{
    public interface IMemoryService
    {
        void Backup<T>(T documents);
        void UploadBlob<T>(T documents);
    }

    public class MemoryService : IMemoryService
    {
        public void Backup<T>(T document)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "backup"));
            var currentDirectory = Directory.GetCurrentDirectory();
            var backupDirectory = Path.Combine(currentDirectory, "backup");           
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(document);
            File.WriteAllText(Path.Combine(backupDirectory, $"backup-{typeof(T)}-{DateTime.Now.ToString("dd-MM-yyyy-HH")}.json"), json);
        }
        public async void UploadBlob<T>(T pizza)
        {
            var blobServiceClient = new BlobServiceClient(
            new Uri("https://vukasinonboarding.blob.core.windows.net"),
            new DefaultAzureCredential());

            string containerName = "pizzabackup" + Guid.NewGuid().ToString();

            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            string localPath = "backup";
            string fileName = $"backup-{typeof(T)}-{DateTime.Now.ToString("dd-MM-yyyy-HH")}.json";
            string localFilePath = Path.Combine(localPath, fileName);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            await blobClient.UploadAsync(localFilePath, true);
        }
    }
}
