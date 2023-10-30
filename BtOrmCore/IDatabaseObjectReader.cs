
namespace BtOrmCore
{
    public interface IDatabaseObjectReader
    {
        /// <summary>
        /// Reads the values from the database for a given model.
        /// </summary>
        /// <param name="Model">Model with ID set already</param>
        /// <returns></returns>
        bool Read(object Model);
        /// <summary>
        /// Reads the values from the database for a given model with a direct ID
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Read(object Model, object id);

        /// <summary>
        /// Reads the values from the database for a given model type with a direct ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? Read<T>(object id) where T : class, new();
    }
}