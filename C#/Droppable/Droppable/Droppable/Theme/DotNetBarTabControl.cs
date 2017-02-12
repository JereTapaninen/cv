using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class DotNetBarTabControl : TabControl
{

    //// CREDITS //
    //This tabcontrol is coded by Mavamaarten~

    public DotNetBarTabControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        DoubleBuffered = true;
        SizeMode = TabSizeMode.Fixed;
        ItemSize = new Size(44, 136);
    }
    protected override void CreateHandle()
    {
        base.CreateHandle();
        Alignment = TabAlignment.Left;
    }

    Color C1 = Color.FromArgb(80, 127, 207);
    public Color Color1
    {
        get { return C1; }
        set
        {
            C1 = value;
            Invalidate();
        }
    }

    Color C2 = Color.FromArgb(100, 147, 227);
    public Color Color2
    {
        get { return C2; }
        set
        {
            C2 = value;
            Invalidate();
        }
    }

    Pen BC = new Pen(Color.FromArgb(170, 187, 204));
    public Color BorderColor
    {
        get { return BC.Color; }
        set
        {
            BC.Color = value;
            Invalidate();
        }
    }

    Color C3 = Color.FromArgb(246, 248, 252);
    public Color Color3
    {
        get { return C3; }
        set
        {
            C3 = value;
            Invalidate();
        }
    }

    bool OB = false;
    public bool ShowOuterBorders
    {
        get { return OB; }
        set
        {
            OB = value;
            Invalidate();
        }
    }

    private Bitmap B { get; set; }
    private Graphics G { get; set; }

    protected override void OnPaint(PaintEventArgs e)
    {
        using (B = new Bitmap(Width, Height))
        {
            using (G = Graphics.FromImage(B))
            {
                try
                {
                    SelectedTab.BackColor = Color.White;
                }
                catch
                { }

                G.Clear(Color.White);

                using (var sb = new SolidBrush(C3))
                {
                    G.FillRectangle(sb, new Rectangle(0, 0, ItemSize.Height + 4, Height));
                }

                if (OB)
                {
                    G.DrawLine(BC, new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
                    //comment out to get rid of the borders
                    G.DrawLine(BC, new Point(ItemSize.Height + 1, 0), new Point(Width - 1, 0));
                    //comment out to get rid of the borders
                    G.DrawLine(BC, new Point(ItemSize.Height + 3, Height - 1), new Point(Width - 1, Height - 1));
                    //comment out to get rid of the borders
                }

                G.DrawLine(BC, new Point(ItemSize.Height + 3, 0), new Point(ItemSize.Height + 3, 999));

                for (var i = 0; i <= TabCount - 1; i++)
                {
                    if (i == SelectedIndex)
                    {
                        Rectangle x2 = new Rectangle(new Point(GetTabRect(i).Location.X - 2, GetTabRect(i).Location.Y - 2), new Size(GetTabRect(i).Width + 3, GetTabRect(i).Height - 1));
                        ColorBlend myBlend = new ColorBlend();

                        myBlend.Colors = new Color[]
                        {
                            C2,
                            C2,
                            C2
                        };

                        myBlend.Positions = new float[]
                        {
                            0f,
                            0.5f,
                            1f
                        };

                        using (LinearGradientBrush lgBrush = new LinearGradientBrush(x2, Color.Black, Color.Black, 90f))
                        {
                            lgBrush.InterpolationColors = myBlend;
                            G.FillRectangle(lgBrush, x2);
                            G.DrawRectangle(BC, x2);

                            G.SmoothingMode = SmoothingMode.HighQuality;

                            Point[] p =
                            {
                                new Point(ItemSize.Height - 3, GetTabRect(i).Location.Y + 20),
                                new Point(ItemSize.Height + 4, GetTabRect(i).Location.Y + 14),
                                new Point(ItemSize.Height + 4, GetTabRect(i).Location.Y + 27)
                            };

                            G.FillPolygon(Brushes.White, p);
                            G.DrawPolygon(BC, p);

                            using (var sf = new StringFormat
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            })
                            {
                                if (ImageList != null)
                                {
                                    try
                                    {
                                        if (ImageList.Images[TabPages[i].ImageIndex] != null)
                                        {
                                            G.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(x2.Location.X + 8, x2.Location.Y + 6));
                                            G.DrawString("  " + TabPages[i].Text, Font, Brushes.White, x2, sf);
                                        }
                                        else
                                        {
                                            G.DrawString(TabPages[i].Text, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Brushes.White, x2, sf);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        G.DrawString(TabPages[i].Text, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Brushes.White, x2, sf);
                                    }
                                }
                                else
                                {
                                    G.DrawString(TabPages[i].Text, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Brushes.White, x2, sf);
                                }
                            }
                        }

                    }
                    else
                    {
                        Rectangle x2 = new Rectangle(new Point(GetTabRect(i).Location.X - 2, GetTabRect(i).Location.Y - 2), new Size(GetTabRect(i).Width + 3, GetTabRect(i).Height + 1));
                        using (var sb = new SolidBrush(C3))
                        {
                            G.FillRectangle(sb, x2);
                        }
                        G.DrawLine(BC, new Point(x2.Right, x2.Top), new Point(x2.Right, x2.Bottom));

                        using (var sf = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        })
                        {
                            if (ImageList != null)
                            {
                                try
                                {
                                    if (ImageList.Images[TabPages[i].ImageIndex] != null)
                                    {
                                        G.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(x2.Location.X + 8, x2.Location.Y + 6));
                                        G.DrawString("  " + TabPages[i].Text, Font, Brushes.DimGray, x2, sf);
                                    }
                                    else
                                    {
                                        G.DrawString(TabPages[i].Text, Font, Brushes.DimGray, x2, sf);
                                    }
                                }
                                catch (Exception)
                                {
                                    G.DrawString(TabPages[i].Text, Font, Brushes.DimGray, x2, sf);
                                }
                            }
                            else
                            {
                                G.DrawString(TabPages[i].Text, Font, Brushes.DimGray, x2, sf);
                            }
                        }
                    }
                }

                using (var clone = (Image)B.Clone())
                {
                    e.Graphics.DrawImage(clone, 0, 0);
                }
            }
        }
    }
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
