using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HomeworldDAEEditor
{
    public class HWMesh
    {
        public HWNode Parent;

        public string Name;
        public bool Visible = false;
        public bool Shaded = true;

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public HWMaterial Material = new HWMaterial();

        public int VertexCount { get { return mesh.VertexCount; } }
        public int IndiceCount { get { return mesh.GetIndices().Length; } }

        Mesh mesh;

        public HWMesh(Mesh mesh)
        {
            this.mesh = mesh;
            Name = mesh.Name;

            HWScene.Meshes.Add(this);
        }

        public void ParseMesh()
        {
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
            else if (Parent.Name.StartsWith("GOBG")) //If goblin mesh
            {
                string name = "";
                List<GoblinMeshTag> tags = new List<GoblinMeshTag>();

                string[] splitted = Parent.Name.Split('[');
                int end = -1;
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("GOBG")) //Name
                        {
                            name = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("TAGS")) //Tags
                        {
                            string tagsString = splitted[i].Substring(0, end);
                            string[] tagsStrings = tagsString.Split(' ');

                            foreach (string tag in tagsStrings)
                            {
                                tags.Add((GoblinMeshTag)Enum.Parse(typeof(GoblinMeshTag), tag.ToUpper()));
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

                HWGoblinMesh newGoblinMesh = null;
                foreach (HWGoblinMesh goblinMesh in HWScene.GoblinMeshes)
                {
                    if (goblinMesh.Name == name)
                    {
                        newGoblinMesh = goblinMesh;
                        break;
                    }
                }

                if (newGoblinMesh == null) //If a goblin mesh does not already exist with that name
                    newGoblinMesh = new HWGoblinMesh(parentJoint, name, tags);
                else
                {
                    if (parentJoint != null)
                        newGoblinMesh.Parent = parentJoint;
                }

                newGoblinMesh.AddMesh(this);
            }

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
                colorData[i] = new Vector3(Color.Purple.R, Color.Purple.G, Color.Purple.B);
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
            ModelMatrix = Parent.WorldMatrix;
        }
    }
}
