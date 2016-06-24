using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWMesh
    {
        private static bool goblinWarningShown;

        public HWNode Parent;

        public string Name;
        public bool Visible = false;
        public bool Shaded = true;
        public bool Translucent = false;
        public bool VertexColored = true;
        public Vector3 Scale = Vector3.One;

        public Matrix4 ModelMatrix;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public Vector3[] Vertices;
        public Vector3[] Normals;
        public int[] Indices;
        public Vector3[] Colors;
        public Vector2[] TextureCoords;
        public Vector3[] Tangents;
        public Vector3[] BiTangents;

        public HWMaterial Material = new HWMaterial();

        public int VertexCount { get { return mesh.VertexCount; } }
        public int IndiceCount { get { return Indices.Length; } }

        Mesh mesh;

        public HWMesh(Mesh mesh)
        {
            this.mesh = mesh;
            Name = mesh.Name;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
            Tangents = GetTangents();
            BiTangents = GetBiTangents();

            HWScene.Meshes.Add(this);
        }

        public void ParseMesh()
        {
            #region ShipMesh
            if (Parent.Name.StartsWith("MULT")) //If visible ship mesh
            {
                string name = "";
                int lod = 0;
                List<ShipMeshTag> tags = new List<ShipMeshTag>();

                string[] splitted = Parent.Name.Split('[');
                int end = -1;
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("MULT")) //Name
                        {
                            name = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("LOD")) //Level of detail
                        {
                            lod = int.Parse(splitted[i].Substring(0, end));
                        }
                        else if (splitted[i - 1].EndsWith("TAGS")) //Tags
                        {
                            string tagsString = splitted[i].Substring(0, end);
                            string[] tagsStrings = tagsString.Split(' ');

                            foreach (string tag in tagsStrings)
                            {
                                tags.Add((ShipMeshTag)Enum.Parse(typeof(ShipMeshTag), tag.ToUpper()));
                            }
                        }
                    }
                }

                HWJoint parentJoint = null;

                if (Parent.Parent != null)
                {
                    if (Parent.Parent.Joint != null)
                        parentJoint = Parent.Parent.Joint;
                }

                HWShipMesh newShipMesh = null;
                foreach (HWShipMesh shipMesh in HWScene.ShipMeshes)
                {
                    if (shipMesh.Name == name)
                    {
                        newShipMesh = shipMesh;
                        break;
                    }
                }

                if (newShipMesh == null) //If a ship mesh does not already exist with that name
                    newShipMesh = new HWShipMesh(parentJoint, name, tags);
                else
                {
                    if (parentJoint != null)
                        newShipMesh.Parent = parentJoint;
                }

                HWShipMeshLOD newLOD = new HWShipMeshLOD(newShipMesh, this, lod);
                newShipMesh.AddLODMesh(newLOD);
            }
            #endregion
            #region GoblinMesh
            else if (Parent.Name.StartsWith("GOBG")) //If goblin mesh (deprecated)
            {
                if (!goblinWarningShown)
                {
                    new Problem(ProblemTypes.ERROR, "Goblins detected, remove them or your game will crash.");
                    goblinWarningShown = true;
                }
            }
            #endregion
            #region CollisionMesh
            else if (Parent.Name.StartsWith("COL")) //If collision mesh
            {
                string name = "";

                string[] splitted = Parent.Name.Split('[');
                int end = -1;
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("COL")) //Name
                        {
                            name = splitted[i].Substring(0, end);
                        }
                    }
                }

                HWJoint parentJoint = null;

                if (Parent.Parent != null)
                {
                    if (Parent.Parent.Joint != null)
                        parentJoint = Parent.Parent.Joint;
                }

                HWCollisionMesh newCollisionMesh = new HWCollisionMesh(this, parentJoint, name);
            }
            #endregion
            #region EngineGlow
            if (Parent.Name.StartsWith("GLOW")) //If visible glow mesh
            {
                string name = "";
                int lod = 0;

                string[] splitted = Parent.Name.Split('[');
                int end = -1;
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("GLOW")) //Name
                        {
                            name = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("LOD")) //Level of detail
                        {
                            lod = int.Parse(splitted[i].Substring(0, end));
                        }
                    }
                }

                HWJoint parentJoint = null;

                if (Parent.Parent != null)
                {
                    if (Parent.Parent.Joint != null)
                        parentJoint = Parent.Parent.Joint;
                }

                HWEngineGlow newGlowMesh = null;
                foreach (HWEngineGlow glowMesh in HWScene.EngineGlows)
                {
                    if (glowMesh.Name == name)
                    {
                        newGlowMesh = glowMesh;
                        break;
                    }
                }

                if (newGlowMesh == null) //If a glow mesh does not already exist with that name
                    newGlowMesh = new HWEngineGlow(parentJoint, name);
                else
                {
                    if (parentJoint != null)
                        newGlowMesh.Parent = parentJoint;
                }

                HWEngineGlowLOD newLOD = new HWEngineGlowLOD(newGlowMesh, this, lod);
                newGlowMesh.AddLODMesh(newLOD);
            }
            #endregion
            #region EngineShape
            else if (Parent.Name.StartsWith("ETSH")) //If engine shape
            {
                string name = "";

                string[] splitted = Parent.Name.Split('[');
                int end = -1;
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("ETSH")) //Name
                        {
                            name = splitted[i].Substring(0, end);
                        }
                    }
                }

                HWJoint parentJoint = null;

                if (Parent.Parent != null)
                {
                    if (Parent.Parent.Joint != null)
                        parentJoint = Parent.Parent.Joint;
                }

                HWEngineShape newEngineShape = new HWEngineShape(this, parentJoint, name);
            }
            #endregion
        }

        public Vector3[] GetVertices()
        {
            List<Vector3> verticesList = new List<Vector3>();
            foreach (Vector3D vertex in mesh.Vertices)
            {
                verticesList.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
            }
            return verticesList.ToArray();
        }

        public Vector3[] GetNormals()
        {
            List<Vector3> normalsList = new List<Vector3>();
            foreach (Vector3D normal in mesh.Normals)
            {
                normalsList.Add(new Vector3(normal.X, normal.Y, normal.Z));
            }
            return normalsList.ToArray();
        }

        public Vector3[] GetTangents()
        {
            List<Vector3> tangentsList = new List<Vector3>();
            foreach (Vector3D tangent in mesh.Tangents)
            {
                tangentsList.Add(new Vector3(tangent.X, tangent.Y, tangent.Z));
            }
            return tangentsList.ToArray();
        }

        public Vector3[] GetBiTangents()
        {
            List<Vector3> biTangentsList = new List<Vector3>();
            foreach (Vector3D biTangent in mesh.BiTangents)
            {
                biTangentsList.Add(new Vector3(biTangent.X, biTangent.Y, biTangent.Z));
            }
            return biTangentsList.ToArray();
        }

        public int[] GetIndices(int offset = 0)
        {
            int[] indices = mesh.GetIndices();

            if(offset != 0)
            {
                for(int i = 0; i < indices.Length; i++)
                {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public Vector3[] GetColorData()
        {
            Vector3[] colorData = new Vector3[VertexCount];
            for(int i = 0; i < VertexCount; i++)
            {
                colorData[i] = new Vector3(Color.White.R, Color.White.G, Color.White.B);
            }

            return colorData;
        }

        public Vector2[] GetTextureCoords()
        {
            if (mesh.TextureCoordinateChannelCount > 0)
            {
                List<Vector2> coords = new List<Vector2>();

                foreach (Vector3D coord in mesh.TextureCoordinateChannels[0])
                {
                    coords.Add(new Vector2(coord.X, coord.Y));
                }

                return coords.ToArray();
            }
            else
            {
                return new Vector2[VertexCount];
            }
        }

        /// <summary>
        /// Calculates the model matrix from transforms
        /// </summary>
        public void CalculateModelMatrix()
        {
            //ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateFromQuaternion(Parent.AbsoluteRotation) * Matrix4.CreateTranslation(Parent.AbsolutePosition);
            ModelMatrix = Matrix4.CreateScale(Scale);

            //ModelMatrix *= Matrix4.CreateRotationX((float)Math.PI / 2);

            //if(Name.StartsWith("COL"))
                //ModelMatrix *= Matrix4.CreateFromQuaternion(Parent.AbsoluteRotation.Inverted());

            if(Parent.Parent != null) //Ignore first parent
                ModelMatrix *= Parent.Parent.WorldMatrix;

            //ModelMatrix *= Matrix4.CreateTranslation(Parent.AbsolutePosition);

            //if (Scale != Vector3.One)
            //ModelMatrix = Matrix4.CreateScale(Scale);
        }
    }
}
