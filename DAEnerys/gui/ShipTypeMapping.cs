using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DAEnerys
{
    public static class ShipTypeMapping
    {
        public static Dictionary<string, string> ShipTypeMappings = new Dictionary<string, string>();
        public static Dictionary<string, int> DAEFileRows = new Dictionary<string, int>();
        public static HWShipType ActiveShipType;

        public static DataGridView DataGridShipTypes;

        public static DataGridViewTextBoxColumn ColumnDAEFile;
        public static DataGridViewComboBoxColumn ColumnShipFile;

        private static DataGridViewCellStyle activeCellStyle;
        private static int activeRowIndex = -1;


        public static void Init(DataGridView dataGridShipTypes)
        {
            DataGridShipTypes = dataGridShipTypes;

            activeCellStyle = new DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.DarkGreen,
                SelectionBackColor = System.Drawing.Color.DarkGreen
            };

            ColumnDAEFile = (DataGridViewTextBoxColumn)DataGridShipTypes.Columns[0];
            ColumnShipFile = (DataGridViewComboBoxColumn)DataGridShipTypes.Columns[1];
            foreach(string shipType in HWShipType.ShipTypes.Keys)
            {
                ColumnShipFile.Items.Add(shipType);
            }

            ColumnDAEFile.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColumnShipFile.SortMode = DataGridViewColumnSortMode.NotSortable;

            DataGridShipTypes.CellValueChanged += DataGridShipTypes_CellValueChanged;
            DataGridShipTypes.RowsRemoved += DataGridShipTypes_RowsRemoved;

            LoadMappings();
        }

        private static void DataGridShipTypes_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            string[] keys = DAEFileRows.Keys.ToArray();
            foreach (string daeFile in keys)
            {
                if (DAEFileRows[daeFile] == e.RowIndex)
                {
                    if (DAEFileRows.ContainsKey(daeFile))
                    {
                        DAEFileRows.Remove(daeFile);
                    }

                    if (ShipTypeMappings.ContainsKey(daeFile))
                    {
                        ShipTypeMappings.Remove(daeFile);
                    }
                }
            }
        }

        private static void DataGridShipTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string[] keys = DAEFileRows.Keys.ToArray();
            foreach(string daeFile in keys)
            {
                if(DAEFileRows[daeFile] == e.RowIndex)
                {
                    string newDaeFile = ""; 
                    string newShipFile = "";

                    if(DataGridShipTypes.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        newDaeFile = DataGridShipTypes.Rows[e.RowIndex].Cells[0].Value.ToString().ToLower();
                    }
                    if (DataGridShipTypes.Rows[e.RowIndex].Cells[1].Value != null)
                    {
                        newShipFile = DataGridShipTypes.Rows[e.RowIndex].Cells[1].Value.ToString().ToLower();
                    }

                    if(newDaeFile.Length == 0 || newShipFile.Length == 0)
                    {
                        if (DAEFileRows.ContainsKey(daeFile))
                        {
                            DAEFileRows.Remove(daeFile);
                        }
                        if (ShipTypeMappings.ContainsKey(daeFile))
                        {
                            ShipTypeMappings.Remove(daeFile);
                        }

                        DataGridShipTypes.Rows.RemoveAt(e.RowIndex);
                    }

                    if (e.ColumnIndex == 0) //DAE file
                    {
                        if (DAEFileRows.ContainsKey(daeFile) && !DAEFileRows.ContainsKey(newDaeFile))
                        {
                            DAEFileRows.Remove(daeFile);
                        }

                        if(!DAEFileRows.ContainsKey(newDaeFile))
                        {
                            DAEFileRows.Add(newDaeFile, e.RowIndex);
                        }
                        else
                        {
                            DataGridShipTypes.Rows[e.RowIndex].Cells[0].Value = daeFile;

                            MessageBox.Show("This DAE file already has been assigned to a ship file.", "DAE mapping already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (e.ColumnIndex == 1) //SHIP file
                    {
                        if (ShipTypeMappings.ContainsKey(daeFile))
                        {
                            ShipTypeMappings[daeFile] = newShipFile;
                        }
                    }
                }
            }
        }

        public static void AddDAEFile(string daeFile, string shipFile, bool activeRow = false)
        {
            daeFile = daeFile.ToLower();
            shipFile = shipFile.ToLower();
            HWShipType newActiveShipType = null;

            if(activeRow && activeRowIndex != -1)
            {
                DataGridShipTypes.Rows[activeRowIndex].Cells[0].Style = null;
                DataGridShipTypes.Rows[activeRowIndex].Cells[1].Style = null;
                activeRowIndex = -1;
            }

            int newRow = -1;
            if(!ShipTypeMappings.ContainsKey(daeFile))
            {
                if (HWShipType.ShipTypes.ContainsKey(shipFile))
                {
                    ShipTypeMappings.Add(daeFile, shipFile);

                    newRow = DataGridShipTypes.Rows.Add(new object[] { daeFile, shipFile });
                    DAEFileRows.Add(daeFile, newRow);
                }
            }
            else
            {
                newRow = DAEFileRows[daeFile];
            }

            if (activeRow)
            {
                activeRowIndex = newRow;

                if (activeRowIndex != -1)
                {
                    DataGridShipTypes.Rows[activeRowIndex].Cells[0].Style = activeCellStyle;
                    DataGridShipTypes.Rows[activeRowIndex].Cells[1].Style = activeCellStyle;

                    if (HWShipType.ShipTypes.ContainsKey(ShipTypeMappings[daeFile]))
                    {
                        newActiveShipType = HWShipType.ShipTypes[ShipTypeMappings[daeFile]];
                    }
                    else
                    {
                        newActiveShipType = null;
                    }
                }
            }

            SetActiveShipType(newActiveShipType);
        }

        public static void SetActiveShipType(HWShipType activeShipType)
        {
            ActiveShipType = activeShipType;
            TargetBoxManager.LoadFromShipType(ActiveShipType);
        }

        public static void SaveMappings()
        {
            XElement mappings = new XElement("shipTypeMappings");

            foreach (string daeFile in ShipTypeMappings.Keys)
            {
                XElement element = new XElement("shipType");
                element.SetAttributeValue("daeFile", daeFile);
                element.SetAttributeValue("shipFile", ShipTypeMappings[daeFile]);
                mappings.Add(element);
            }

            File.WriteAllText(Path.Combine(Program.EXECUTABLE_PATH, "shipTypeMappings.xml"), mappings.ToString());
        }
        public static void LoadMappings()
        {
            if (!File.Exists(Path.Combine(Program.EXECUTABLE_PATH, "shipTypeMappings.xml")))
            {
                Log.WriteLine("No shipTypeMappings.xml found, nothing loaded.");
                return;
            }

            try
            {
                string file = File.ReadAllText(Path.Combine(Program.EXECUTABLE_PATH, "shipTypeMappings.xml"));
                XElement mappings = XElement.Parse(file);

                foreach (XElement element in mappings.Elements())
                {
                    AddDAEFile(element.Attribute("daeFile").Value.ToLower(), element.Attribute("shipFile").Value.ToLower());
                }
            }
            catch
            {
                Log.WriteLine("Failed to load \"" + Path.Combine(Program.EXECUTABLE_PATH, "shipTypeMappings.xml") + "\".");
            }
        }
    }
}
