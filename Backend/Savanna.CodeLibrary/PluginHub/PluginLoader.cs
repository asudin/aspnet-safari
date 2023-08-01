using Animals.CodeLibrary.Abstractions;
using System.Reflection;

namespace Savanna.CodeLibrary.PluginHub
{
    public sealed class PluginLoader
    {
        private static readonly string _fileName = "Plugins";
        private static readonly string? _directory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName!;
        private static readonly string _filePath = @"D:\Accenture\Safari_WebAPI\Plugins";

        public PluginLoader()
        {
            if (!Directory.Exists(_filePath) && _filePath != null)
            {
                Directory.CreateDirectory(_filePath);
            }
        }

        private static Assembly LoadPlugin(string relativePath)
        {
            var pluginLocation = relativePath;
            var loadContext = new PluginLoadContext(pluginLocation);

            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        private static IEnumerable<IAnimalPlugin> CreateAnimals(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IAnimalPlugin).IsAssignableFrom(type) &&
                    Activator.CreateInstance(type) is IAnimalPlugin result)
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<IAnimalPlugin> LoadAnimals()
        {
            string[] dllFilesWithAnimals = Directory.GetFiles(_filePath, "*.dll");

            IEnumerable<IAnimalPlugin> animals = dllFilesWithAnimals.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);

                return CreateAnimals(pluginAssembly);
            }).ToList();

            if (!animals.Any())
            {
                return null!;
            }

            return animals;
        }
    }
}
