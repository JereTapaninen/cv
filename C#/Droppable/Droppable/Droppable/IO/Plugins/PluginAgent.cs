using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Droppable.IO.Plugins
{
    public class PluginAgent
    {
        private bool _running;
        public bool Running
        {
            get { return _running; }
        }

        private List<IPlugin> _plugins;
        public List<IPlugin> Plugins
        {
            get { return _plugins; }
        }

        public PluginAgent()
        { }

        public void Set()
        {
            _running = true;

            _plugins = PluginLoader.LoadAll();

            foreach (var p in Plugins)
            {
                p.Load();
            }

            OnUpdate();
        }

        public void Unset()
        {
            _running = false;

            foreach (var p in Plugins)
            {
                p.Finish();
            }
        }

        private void OnUpdate()
        {
            new Thread(() =>
            {
                while (Running)
                {
                    foreach (var p in Plugins)
                    {
                        p.OnUpdate();
                    }

                    Thread.Sleep(50);
                }
            }).Start();
        }
    }
}
