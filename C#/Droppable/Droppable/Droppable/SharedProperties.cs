using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using Droppable.IO.Engines;
using Droppable.IO.Plugins;

namespace Droppable
{
    public static class SharedProperties
    {
        public static ManualResetEvent EngineCheckHold = new ManualResetEvent(false);

        public static Settings Settings = new Settings();
        public static PluginAgent PluginAgent = new PluginAgent();

        public static Engine[] Engines = new Engine[] { new EngineBeeMP3s(), new EngineMP3raid(), new EngineShareLagu(), new EngineNewMP3File(),
                                                        new EngineRLSmp3(), new Enginemp3musicx() };
    }
}
