using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DAEnerys
{
    [Designer(typeof(LabeledNumericUpDownControlDesigner))]
    public class LabeledNumericUpDown : UserControl
    {
        private Label label1;
        private NumericUpDown numericUpDown1;

        private string labelText;
        private int decimalPlaces;
        private decimal increment;
        private decimal minimum;
        private decimal maximum;
        private decimal value;
        private int upDownWidth;

        [Bindable(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return labelText; }
            set { labelText = label1.Text = value; }
        }

        [RefreshProperties(RefreshProperties.All)]
        public int NumericUpDownWidth
        {
            get { return upDownWidth; }
            set
            {
                upDownWidth = numericUpDown1.Width = value;
                numericUpDown1.Left = Width - upDownWidth;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public decimal Minimum
        {
            get { return minimum; }
            set { minimum = numericUpDown1.Minimum = value; }
        }

        [RefreshProperties(RefreshProperties.All)]
        public decimal Maximum
        {
            get { return maximum; }
            set { maximum = numericUpDown1.Maximum = value; }
        }
        
        public decimal Increment
        {
            get { return increment; }
            set { increment = numericUpDown1.Increment = value; }
        }

        [Bindable(true)]
        public decimal Value
        {
            get { return value; }
            set { this.value = numericUpDown1.Value = value; }
        }

        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get { return decimalPlaces; }
            set { decimalPlaces = numericUpDown1.DecimalPlaces = value; }
        }

        public LabeledNumericUpDown()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(64, 0);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabeledNumericUpDown
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "LabeledNumericUpDown";
            this.Size = new System.Drawing.Size(128, 20);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        public event EventHandler ValueChanged;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            value = numericUpDown1.Value;
            ValueChanged.Invoke(sender, e);
        }
    }

    public class LabeledNumericUpDownControlDesigner : ControlDesigner
    {
        public LabeledNumericUpDownControlDesigner() { }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            LabeledNumericUpDown control1 = this.Component as LabeledNumericUpDown;
            control1.Text = control1.ToString().Split(" ".ToCharArray())[0];
        }
    }
}
