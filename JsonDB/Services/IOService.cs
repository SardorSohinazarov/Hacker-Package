using System.Text.Json;

namespace JsonDB.Services
{
    public partial class EntityService
    {
        private void WriteAllEntities<T>(List<T> entities, string fullPath)
        {
            var jsonEntities = JsonSerializer.Serialize(entities);

            using (var writer = new StreamWriter(fullPath))
            {
                writer.Write(jsonEntities);
            }
        }

        private List<T> ReadAllEntities<T>(string fullPath)
        {
            string jsonEntities;
            using (var reader = new StreamReader(fullPath))
            {
                jsonEntities = reader.ReadToEnd();
            }

            List<T> entities;

            try
            {
                entities = JsonSerializer.Deserialize<List<T>>(jsonEntities);
            }
            catch (Exception ex)
            {
                entities = new List<T>();
            }

            return entities;
        }

        private string FileManager<T>()
        {
            var fileName = typeof(T).Name + ".json";
            var fullPath = Path.Combine(rootPath, fileName);

            if (!File.Exists(fullPath))
            {
                using (FileStream stream = File.Create(fullPath))
                {

                }
            }

            return fullPath;
        }
    }
}
