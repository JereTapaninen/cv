using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Droppable.Theme
{
    class WhiteScaleTheme : ThemeContainer154
    {
        private bool showFooter;

        public bool ShowFooter
        {
            get { return this.showFooter; }
            set { this.showFooter = value; }
        }

        private Pen Border;
        private Color ClearColor;

        private Brush BackColorBrush;
        private Brush TextBrush;

        public WhiteScaleTheme()
        {
            base.TransparencyKey = Color.Fuchsia;
            base.BackColor = Color.FromArgb(240, 240, 240);
            base.Font = new Font("Tahoma", 9);

            base.SetColor("Border", Color.FromArgb(200, 200, 200));
            base.SetColor("ClearColor", Color.White);
            base.SetColor("Text", Color.Black);

            this.BackColorBrush = new SolidBrush(base.BackColor);
        }

        protected override void ColorHook()
        {
            this.Border = base.GetPen("Border");
            this.ClearColor = base.GetColor("ClearColor");
            this.TextBrush = base.GetBrush("Text");
        }

        protected override void PaintHook()
        {
            G.Clear(this.ClearColor);

            var hb = new HatchBrush(HatchStyle.Sphere, Color.FromArgb(3, Color.Black), Color.Transparent);
            /*var br = new LinearGradientBrush(new Rectangle(6, 26, this.Width - 13, this.Height - 43), Color.Black, Color.Black, 90F, false);
            var cb = new ColorBlend();
            cb.Positions = new[] { 0, 4/10f, 5/10f, 6/10f, 1 };
            cb.Colors = new[] { Color.FromArgb(60, Color.Black), Color.FromArgb(200, Color.White), Color.FromArgb(200, Color.White), Color.FromArgb(200, Color.White), Color.FromArgb(60, Color.Black) };
            br.InterpolationColors = cb;*/

            if (showFooter)
            {
                G.FillRectangle(this.BackColorBrush, new Rectangle(6, 26, this.Width - 13, this.Height - 43));
                G.FillRectangle(hb, new Rectangle(6, 26, this.Width - 13, this.Height - 43));
                G.DrawString(base.FindForm().Text, new Font(base.Font, FontStyle.Bold), new SolidBrush(Color.FromArgb(25, 150, 255)), new Point(30, 6));
                var suffering = G.MeasureString("© Dreaming In Digital 2015 - 2016", new Font(base.Font.FontFamily, 7));
                G.DrawString("© Dreaming In Digital 2015 - 2016", new Font(base.Font.FontFamily, 7), this.TextBrush,
                    new Point((int)((base.FindForm().Width / 2) - (suffering.Width / 2)), (int)(base.FindForm().Height - suffering.Height)));
            }
            else
            {
                G.FillRectangle(this.BackColorBrush, new Rectangle(6, 26, this.Width - 13, this.Height - 33));
                G.FillRectangle(hb, new Rectangle(6, 26, this.Width - 13, this.Height - 33));
                G.DrawString(base.FindForm().Text, new Font(base.Font, FontStyle.Bold), new SolidBrush(Color.FromArgb(25, 150, 255)), new Point(30, 6));
            }

            G.DrawRectangle(this.Border, new Rectangle(0, 0, this.Width - 1, this.Height - 1));

            G.DrawIcon(base.FindForm().Icon, new Rectangle(10, 5, 16, 16));
        }
    }

    class WhiteScaleButton : ThemeControl154
    {
        public enum SpecialType { NORMAL, MINIMIZE, CLOSE };

        private Color ButtonColor;
        private Brush ButtonOver;
        private Brush ButtonDown;
        private Brush TextBrush;
        private Pen Border;

        public SpecialType Type = SpecialType.NORMAL;

        public WhiteScaleButton()
        {
            base.SetColor("Button", Color.FromArgb(250, 250, 250));
            base.SetColor("ButtonDown", Color.FromArgb(240, 240, 240));
            base.SetColor("ButtonOver", Color.White);
            base.SetColor("Text", Color.Black);
            base.SetColor("Border", Color.LightGray);
        }

        protected override void ColorHook()
        {
            this.ButtonColor = base.GetColor("Button");
            this.ButtonOver = base.GetBrush("ButtonDown");
            this.ButtonDown = base.GetBrush("ButtonOver");
            this.TextBrush = base.GetBrush("Text");
            this.Border = base.GetPen("Border");
        }

        protected override void PaintHook()
        {
            switch (this.Type)
            {
                case SpecialType.MINIMIZE:
                case SpecialType.CLOSE:
                case SpecialType.NORMAL:
                default:
                    G.Clear(this.ButtonColor);

                    switch (base.State)
                    {
                        case MouseState.Down:
                            G.FillRectangle(this.ButtonDown, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
                            break;
                        case MouseState.Over:
                            G.FillRectangle(this.ButtonOver, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
                            break;
                        case MouseState.None:
                        default:
                            break;
                    }
                    break;
            }

            G.DrawRectangle(this.Border, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
            base.DrawText(this.TextBrush, System.Windows.Forms.HorizontalAlignment.Center, 0, 0);
        }
    }

    class WhiteScaleProgressBar : ThemeControl154
    {
        private int maxValue = 100;
        public int MaxValue
        {
            get { return this.maxValue; }
            set
            {
                if (value > this.MinValue)
                    this.maxValue = value;

                Invalidate();
            }
        }

        private int minValue = 0;
        public int MinValue
        {
            get { return this.minValue; }
            set
            {
                if (value < this.MaxValue)
                    this.minValue = value;

                Invalidate();
            }
        }

        private int value;
        public int Value
        {
            get { return this.value; }
            set
            {
                if (value >= this.MinValue && value <= this.MaxValue)
                    this.value = value;

                Invalidate();
            }
        }

        private Color FillColor;
        private Color FillColor2;
        private Pen Border;

        public WhiteScaleProgressBar()
        {
            this.Value = 0;

            base.BackColor = Color.WhiteSmoke;

            base.SetColor("Fill", Color.FromArgb(25, 150, 255));
            base.SetColor("Fill2", Color.FromArgb(25, 100, 255));
            base.SetColor("Border", Color.LightGray);
        }

        protected override void ColorHook()
        {
            this.FillColor = base.GetColor("Fill");
            this.FillColor2 = base.GetColor("Fill2");
            this.Border = base.GetPen("Border");
        }

        protected override void PaintHook()
        {
            G.Clear(base.BackColor);

            var lgb = new LinearGradientBrush(new Rectangle(0, 0, this.Width - 1, this.Height - 1), FillColor, FillColor2, 90F);

            var graphicsValue = ((this.Value * this.Width) / this.MaxValue);

            G.FillRectangle(lgb, new Rectangle(0, 0, (int)graphicsValue, this.Height - 1));
            G.DrawRectangle(Border, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

    }

    class WhiteScaleControlBox : ThemeContainer154
    {
        public event EventHandler SettingsButtonClicked
        {
            add
            {
                this.SettingsButton.Click += value;
            }
            remove
            {
                this.SettingsButton.Click -= value;
            }
        }

        private bool settingsButton;

        public bool SettingsButtonVisible
        {
            get { return this.settingsButton; }
            set
            {
                if (value)
                {
                    this.SettingsButton = new WhiteScaleButton();
                    this.SettingsButton.Name = "CB_SETTINGS";
                    this.SettingsButton.Size = new Size(24, 20);
                    this.SettingsButton.Location = new Point(0, 0);
                    this.SettingsButton.Text = "⚙";

                    this.Controls.Add(this.SettingsButton);
                }
                else
                {
                    this.Controls.Remove(this.SettingsButton);
                }

                this.settingsButton = value;
            }
        }

        private WhiteScaleButton MinimizeButton;
        private WhiteScaleButton CloseButton;
        private WhiteScaleButton SettingsButton;

        public WhiteScaleControlBox()
        {
            this.ControlMode = true;

            this.Transparent = true;
            this.Size = new Size(72, 20);
            this.Sizable = false;

            this.BackColor = Color.Transparent;

            this.Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top);

            this.MinimizeButton = new WhiteScaleButton();
            this.MinimizeButton.Size = new Size(24, 20);
            this.MinimizeButton.Location = new Point(24, 0);
            this.MinimizeButton.Text = "_";
            this.MinimizeButton.Type = WhiteScaleButton.SpecialType.MINIMIZE;
            this.MinimizeButton.Click += (sender, e) => { base.FindForm().WindowState = System.Windows.Forms.FormWindowState.Minimized; };

            this.CloseButton = new WhiteScaleButton();
            this.CloseButton.Size = new Size(24, 20);
            this.CloseButton.Location = new Point(48, 0);
            this.CloseButton.Text = "X";
            this.CloseButton.Type = WhiteScaleButton.SpecialType.CLOSE;
            this.CloseButton.Click += (sender, e) => { base.FindForm().Close(); };

            this.SettingsButton = new WhiteScaleButton();

            this.Controls.AddRange(new WhiteScaleButton[] { this.MinimizeButton, this.CloseButton });
        }

        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            
        }
    }
}
