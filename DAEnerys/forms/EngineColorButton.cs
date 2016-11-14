using System.Drawing;
using System.Windows.Forms;

namespace DAEnerys
{
    class EngineColorButton : Button
    {
        public enum PresetColors
        {
            Hiigaran,
            Vaygr,
            Kushan,
            Taiidan,
            Progenitor,
            Kadeshi,
            Custom,
        }

        private Color engineColor;
        private string title;
        private System.ComponentModel.IContainer components;
        private ToolTip toolTip1;
        private PresetColors preset;

        public Color EngineColor
        {
            get
            {
                return engineColor;
            }
            set
            {
                preset = PresetColors.Custom;
                engineColor = value;
                Invalidate();
            }
        }

        public void SetColor(Color engine)
        {
            preset = PresetColors.Custom;
            engineColor = engine;
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
                    case PresetColors.Hiigaran:
                        engineColor = Color.FromArgb(64, 69, 120, 176);
                        title = "Hiigaran";
                        break;
                    case PresetColors.Vaygr:
                        engineColor = Color.FromArgb(64, 235, 54, 23);
                        title = "Vaygr";
                        break;
                    case PresetColors.Kushan:
                        engineColor = Color.FromArgb(64, 179, 140, 89);
                        title = "Kushan";
                        break;
                    case PresetColors.Taiidan:
                        engineColor = Color.FromArgb(64, 69, 120, 176);
                        title = "Taiidan";
                        break;
                    case PresetColors.Progenitor:
                        engineColor = Color.FromArgb(51, 255, 120, 0);
                        title = "Progenitor";
                        break;
                    case PresetColors.Kadeshi:
                        engineColor = Color.FromArgb(26, 242, 89, 46);
                        title = "Kadeshi";
                        break;
                    case PresetColors.Custom:
                        engineColor = Color.FromArgb(64, 69, 120, 176);
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

        public EngineColorButton()
        {
            InitializeComponent();
            UsePreset = PresetColors.Custom;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Color brushColor = Color.FromArgb(255, engineColor.R, engineColor.G, engineColor.B); //The button colors look weird when the alpha is set too
            SolidBrush engineBrush = new SolidBrush(brushColor);

            graphics.FillRectangle(engineBrush, 0, 0, Width, Height);
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
