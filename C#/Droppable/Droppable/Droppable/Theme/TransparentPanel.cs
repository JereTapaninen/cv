using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Droppable.Theme
{
    class TransparentPanel : Panel
    {
        public TransparentPanel()
            : base()
        {
            this.BackColor = Color.FromArgb(0, 0, 0, 0);
            this.DoubleBuffered = true;
        }
    }
}
