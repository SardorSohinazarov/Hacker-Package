namespace JsonDB.Services
{
    public partial class EntityService
    {
        public static string rootPath;
        public EntityService()
        {
            rootPath = Path.Combine(Directory.GetCurrentDirectory(), "Database");
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
        }

        public T Add<T>(T entity)
        {
            var entityType = typeof(T);
            var fullPath = FileManager<T>();
            var entities = ReadAllEntities<T>(fullPath);
            entities.Add(entity);
            WriteAllEntities(entities, fullPath);

            return (T)entity;
        }

        public List<T> Get<T>()
        {
            var entityType = typeof(T);
            var fullPath = FileManager<T>();
            var entities = ReadAllEntities<T>(fullPath);

            return entities;
        }
    }
}
