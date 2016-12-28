using OpenTK;
using System.Collections.Generic;
using System.Drawing;

namespace DAEnerys
{
    public class HWEngineBurn : HWElement
    {
        public static List<HWEngineBurn> EngineBurns = new List<HWEngineBurn>();

        public override string FormattedName
        {
            get
            {
                return "BURN[" + Name + "]";
            }
        }

        public List<EditorLine> Lines = new List<EditorLine>();
        public List<HWEngineFlame> Flames = new List<HWEngineFlame>();

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                foreach(EditorLine line in Lines)
                {
                    line.Visible = value;
                }

                foreach(HWEngineFlame flame in Flames)
                {
                    flame.Cube.Visible = value;
                }
            }
        }

        public HWEngineBurn(string name, HWJoint parent, Matrix4 transform) : base(name, parent, transform)
        {
            Name = name;

            EngineBurns.Add(this);
            Program.main.AddEngineBurn(this);
        }

        public void SetupVisualization()
        {
            foreach (EditorLine line in Lines)
                line.Destroy();

            Lines.Clear();

            for(int i = 0; i < Flames.Count - 1; i++) //1 line less than flames
            {
                EditorLine line = new EditorLine(Flames[i].GlobalPosition, Flames[i + 1].GlobalPosition, Color.White, Color.White);
                Lines.Add(line);
            }
        }
    }

    public class HWEngineFlame : HWElement
    {
        public static List<HWEngineFlame> EngineFlames = new List<HWEngineFlame>();

        public int SpriteIndex;
        public int DivIndex;

        public override string FormattedName
        {
            get
            {
                return "Flame[" + SpriteIndex + "]_Div[" + DivIndex + "]";
            }
        }

        public HWEngineBurn EngineBurn;
        public EditorCube Cube;

        public HWEngineFlame(HWEngineBurn engineBurn, Matrix4 transform, int id, int spriteIndex) : base("", engineBurn, transform)
        {
            EngineBurn = engineBurn;
            DivIndex = id;
            SpriteIndex = spriteIndex;

            EngineBurn.Flames.Add(this);

            Cube = new EditorCube(this, Vector3.One);
            Cube.Scale = new Vector3(0.25f, 0.25f, 0.25f);

            EngineFlames.Add(this);

            EngineBurn.SetupVisualization();
        }
    }
}
