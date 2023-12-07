using MongoDB.Bson.IO;
using MongoDB.Driver;
using PizzasApi.Models;
using PizzasApi.Services;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using Azure.Identity;

namespace ContosoPizza.Service
{
	public class MemoryPizzaService
	{
		public string Id;
		public string PizzaName;
		public bool IsGlutenFree;

		public MemoryPizzaService(string id, string pizzaName, bool isGlutenFree)
		{
			Id = id;
			PizzaName = pizzaName;
			IsGlutenFree = isGlutenFree;
		}

		public void Backup()
		{			
			Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "backup"));
			var currentDirectory = Directory.GetCurrentDirectory();
			var backupDirectory = Path.Combine(currentDirectory, "backup");
			File.WriteAllText(Path.Combine(backupDirectory, "backup.json"), "" +
				$"{{\"ID\": \"{Id}\", \"PizzaName\": \"{PizzaName}\", \"IsGlutenFree\": \"{IsGlutenFree}\"}}");
			var pizzaJson = File.ReadAllText($"backup{Path.DirectorySeparatorChar}backup.json");
			var pizzaData = Newtonsoft.Json.JsonConvert.DeserializeObject<MemoryPizzaService>(pizzaJson);
		}
		public async void UploadBlob()
		{
			var blobServiceClient = new BlobServiceClient(
			new Uri("https://vukasinonboarding.blob.core.windows.net"),
			new DefaultAzureCredential());

			string containerName = "pizzabackup" + Guid.NewGuid().ToString();

			BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

			string localPath = "backup";
			string fileName = "backup.json";
			string localFilePath = Path.Combine(localPath, fileName);

			BlobClient blobClient = containerClient.GetBlobClient(fileName);

			Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

			await blobClient.UploadAsync(localFilePath, true);
		}
	}
}
