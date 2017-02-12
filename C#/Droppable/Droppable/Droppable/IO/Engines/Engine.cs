using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Droppable.IO.Utils;

namespace Droppable.IO.Engines
{
    public abstract class Engine
    {
        private string Name;
        private string BaseURL;
        private string SearchURL;
        private int BaseIndex;

        protected Engine(string name, string baseURL, string searchURL, int baseIndex)
        {
            this.Name = name;
            this.BaseURL = baseURL;
            this.SearchURL = searchURL;
            this.BaseIndex = baseIndex;
        }

        public abstract string Search(SongInfo info);
        public abstract string SearchDirty(string searchQuery);

        public string GetEngineName()
        {
            return this.Name;
        }

        public string GetBaseURL()
        {
            return this.BaseURL;
        }

        protected string GetSearchURL()
        {
            return this.SearchURL;
        }

        protected int GetBaseIndex()
        {
            return this.BaseIndex;
        }
    }
}
