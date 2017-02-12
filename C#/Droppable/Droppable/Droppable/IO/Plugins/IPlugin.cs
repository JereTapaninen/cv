using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Droppable.IO.Plugins
{
    public interface IPlugin
    {
        /// <summary>
        /// The identifier for the plugin: its name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Called upon plugin load
        /// </summary>
        void Load();

        /// <summary>
        /// This method will be called every 50 msec once the plugin has loaded
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// Called upon exit
        /// </summary>
        void Finish();
    }
}
