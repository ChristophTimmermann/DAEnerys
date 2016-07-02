using System;
using System.Drawing;
using System.Windows.Forms;

namespace DAEnerys
{
    class TeamColorButton : Button
    {
        public enum PresetColors
        {
            Default,
            Kushan,
            Taiidan,
            Somtaaw,
            Beast,
            TaiidanImperialist,
            TaiidanLoyalist,
            KithManaan,
            KithNabaal,
            KithSjet,
            Hiigaran,
            VaygrSP,
            VaygrMP,
            Custom
        }

        private Color teamColor;
        private Color stripeColor;
        private string title;
        private System.ComponentModel.IContainer components;
        private ToolTip toolTip1;
        private PresetColors preset;

        public Color TeamColor
        {
            get
            {
                return teamColor;
            }
            set
            {
                preset = PresetColors.Custom;
                teamColor = value;
                Invalidate();
            }
        }

        public Color StripeColor
        {
            get
            {
                return stripeColor;
            }
            set
            {
                preset = PresetColors.Custom;
                stripeColor = value;
                Invalidate();
            }
        }

        public void SetColors(Color team, Color stripe)
        {
            preset = PresetColors.Custom;
            teamColor = team;
            stripeColor = stripe;
            Invalidate();
        }

        public PresetColors UsePreset
        {
            get
            {
                return preset;
            }
            set
            {
                preset = value;
                switch (preset)
                {
                    case PresetColors.Default:
                        teamColor = Color.FromArgb(0, 127, 255);
                        stripeColor = Color.SpringGreen;
                        title = "Default";
                        break;
                    case PresetColors.Kushan:
                        teamColor = Color.FromArgb(142, 159, 161);
                        stripeColor = Color.White;
                        title = "Kushan";
                        break;
                    case PresetColors.Taiidan:
                        teamColor = Color.FromArgb(255, 210, 0);
                        stripeColor = Color.Red;
                        title = "Taiidan";
                        break;
                    case PresetColors.Somtaaw:
                        teamColor = Color.FromArgb(66, 108, 202);
                        stripeColor = Color.White;
                        title = "Kith Somtaaw";
                        break;
                    case PresetColors.Beast:
                        teamColor = Color.FromArgb(150, 128, 128);
                        stripeColor = Color.FromArgb(200, 0, 0);
                        title = "Beast";
                        break;
                    case PresetColors.TaiidanImperialist:
                        teamColor = Color.FromArgb(253, 237, 37);
                        stripeColor = Color.FromArgb(171, 45, 20);
                        title = "Taiidan Imperialists";
                        break;
                    case PresetColors.TaiidanLoyalist:
                        teamColor = Color.White;
                        stripeColor = Color.FromArgb(0, 128, 128);
                        title = "Taiidan Loyalists";
                        break;
                    case PresetColors.KithManaan:
                        teamColor = Color.FromArgb(55, 151, 8);
                        stripeColor = Color.FromArgb(179, 247, 101);
                        title = "Kith Manaan";
                        break;
                    case PresetColors.KithNabaal:
                        teamColor = Color.FromArgb(227, 173, 83);
                        stripeColor = Color.FromArgb(152, 111, 29);
                        title = "Kith Nabaal";
                        break;
                    case PresetColors.KithSjet:
                        teamColor = Color.FromArgb(200, 0, 0);
                        stripeColor = Color.FromArgb(0, 0, 255);
                        title = "Kith S'jet";
                        break;
                    case PresetColors.Hiigaran:
                        teamColor = Color.FromArgb(93, 141, 170);
                        stripeColor = Color.FromArgb(204, 204, 204);
                        title = "Hiigaran";
                        break;
                    case PresetColors.VaygrSP:
                        teamColor = Color.FromArgb((int)229.5, (int)229.5, (int)229.5);
                        stripeColor = Color.FromArgb((int)25.5, (int)25.5, (int)25.5);
                        title = "VaygrSP";
                        break;
                    case PresetColors.VaygrMP:
                        teamColor = Color.FromArgb(200, 165, 111);
                        stripeColor = Color.FromArgb(204, 204, 204);
                        title = "VaygrMP";
                        break;
                    case PresetColors.Custom:
                        teamColor = Color.FromArgb(0, 127, 255);
                        stripeColor = Color.SpringGreen;
                        title = "Custom";
                        break;
                }
                toolTip1.SetToolTip(this, title);
                Invalidate();
            }
        }

        public string PresetName
        {
            get
            {
                return title;
            }
        }

        public TeamColorButton()
        {
            InitializeComponent();
            UsePreset = PresetColors.Custom;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            SolidBrush teamBrush = new SolidBrush(teamColor);
            SolidBrush stripeBrush = new SolidBrush(stripeColor);
            Point[] points = {
                new Point((int)(Width * 0.8), 0),
                new Point(Width, 0),
                new Point(Width, (int)(Height * 0.2)-1),
                new Point((int)(Width * 0.2), Height-1),
                new Point(0, Height-1),
                new Point(0, (int)(Height * 0.8)-1)
            };
            graphics.FillRectangle(teamBrush, 0, 0, Width, Height);
            graphics.FillPolygon(stripeBrush, points);
            graphics.DrawRectangle(new Pen(Color.Black, 1), 0, 0, Width - 1, Height - 1);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
            // 
            // TeamColorButton
            // 
            this.Size = new System.Drawing.Size(284, 261);
            this.ResumeLayout(false);
        }
    }
}
