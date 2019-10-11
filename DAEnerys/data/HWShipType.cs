using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWShipType
    {
        public static Dictionary<string, HWShipType> ShipTypes = new Dictionary<string, HWShipType>();
        public static HWShipType LoadedShip = null;

        public static void LoadShip(string shipName)
        {
            LoadedShip = null;

            if (ShipTypes.Keys.Count == 0)
            {
                return;
            }

            if (ShipTypes.Keys.Contains(shipName))
            {
                LoadedShip = ShipTypes[shipName];
            }
            else
            {
                MessageBox.Show("Could not find ship type \"" + shipName + "\".ship", "Failed to find ship type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string Name;
        public string Path;
        public List<TargetBox> TargetBoxes = new List<TargetBox>();

        public HWShipType(string name, string path)
        {
            Name = name;
            Path = path;

            ShipTypes.Add(name, this);
            HWData.ShipTypes.Add(this);
        }

        public class TargetBox
        {
            public static List<TargetBox> TargetBoxes = new List<TargetBox>();

            public HWShipType ShipType;

            public int Index;
            public Vector3 Min;
            public Vector3 Max;

            public int ItemIndex = -1;
            public EditorCube EditorCube;

            public Vector3 CubeMin;
            public Vector3 CubeMax;

            public TargetBox(HWShipType shipType, int index, Vector3 min, Vector3 max)
            {
                ShipType = shipType;
                Index = index;
                Min = min;
                Max = max;

                if(shipType != null)
                    shipType.TargetBoxes.Add(this);

                TargetBoxes.Add(this);
            }

            public void Destroy()
            {
                if (ShipType != null)
                    ShipType.TargetBoxes.Remove(this);
                ShipType = null;

                TargetBoxes.Remove(this);
                EditorCube.Destroy();
                EditorCube = null;
                ItemIndex = -1;
            }

            public static TargetBox GetByItemIndex(int itemIndex)
            {
                foreach(TargetBox targetBox in TargetBoxes)
                {
                    if(targetBox.ItemIndex == itemIndex)
                    {
                        return targetBox;
                    }
                }
                return null;
            }
        }
    }
}
