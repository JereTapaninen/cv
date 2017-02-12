using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Droppable.IO.Plugins
{
    public static class PluginLoader
    {
        private static readonly string PLUGIN_DIR = Application.StartupPath + @"\Plugins";

        public static List<IPlugin> LoadAll()
        {
            if (Directory.Exists(PLUGIN_DIR))
            {
                var subdirs = Directory.GetDirectories(PLUGIN_DIR);
                var plugins = new List<IPlugin>();

                foreach (var subdir in subdirs)
                {
                    string[] dllFileNames = Directory.GetFiles(subdir, "*.dll");

                    var assemblies = new List<Assembly>(dllFileNames.Length);

                    foreach (var dllFile in dllFileNames)
                    {
                        var an = AssemblyName.GetAssemblyName(dllFile);
                        var assembly = Assembly.Load(an);
                        assemblies.Add(assembly);
                    }

                    var pluginType = typeof(IPlugin);
                    var pluginTypes = new List<Type>();

                    foreach (var assembly in assemblies)
                    {
                        if (assembly != null)
                        {
                            var types = assembly.GetTypes();

                            foreach (var type in types)
                            {
                                if (type.IsInterface || type.IsAbstract)
                                    continue;

                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }

                    foreach (var type in pluginTypes)
                    {
                        var plugin = Activator.CreateInstance(type) as IPlugin;
                        plugins.Add(plugin);
                    }
                }

                Console.WriteLine("[PluginLoader] {0} plugin(s) successfully loaded!", plugins.Count);

                return plugins;
            }
            else
            {
                Directory.CreateDirectory(PLUGIN_DIR);

                return LoadAll();
            }
        }
    }
}
