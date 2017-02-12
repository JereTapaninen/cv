using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Droppable
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            SharedProperties.Settings.Load();

            cbFilterCovers.Checked = SharedProperties.Settings.FilterCovers;
            cbFilterRemixes.Checked = SharedProperties.Settings.FilterRemixes;
            cbFilterNightcore.Checked = SharedProperties.Settings.FilterNightcore;
            rbExactMatch.Checked = SharedProperties.Settings.ExactMatch;
            rbPartialMatch.Checked = SharedProperties.Settings.PartialMatch;
            cbFastSearch.Checked = SharedProperties.Settings.FastSearch;
            nudMinimumDownloadSize.Value = SharedProperties.Settings.MinimumSizeKB;
            nudEngineTimeout.Value = SharedProperties.Settings.TimeoutSeconds;
            cbSkipEngineCheck.Checked = SharedProperties.Settings.SkipEngineCheck;

            if (!rbExactMatch.Checked && !rbPartialMatch.Checked)
                rbNoMatch.Checked = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            new Settings(cbFilterCovers.Checked, cbFilterRemixes.Checked, cbFilterNightcore.Checked,
                rbExactMatch.Checked, rbPartialMatch.Checked, cbFastSearch.Checked, (int)nudMinimumDownloadSize.Value,
                (int)nudEngineTimeout.Value, cbSkipEngineCheck.Checked).Save();

            SharedProperties.Settings.Load();

            Application.Restart();
        }
    }
}
