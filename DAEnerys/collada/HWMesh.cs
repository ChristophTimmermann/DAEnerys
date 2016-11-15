using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace DAEnerys
{
    public class HWVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 Color;
        public Vector2 UV0;
        public Vector2 UV1;
        public Vector3 Tangent;
        public Vector3 Binormal;

        public HWVertex() { }
        public HWVertex(HWVertex vtx)
        {
            this.Position = vtx.Position;
            this.Normal = vtx.Normal;
            this.Color = vtx.Color;
            this.UV0 = vtx.UV0;
            this.UV1 = vtx.UV1;
            this.Tangent = vtx.Tangent;
            this.Binormal = vtx.Binormal;
        }
    }

    public class HWMesh
    {
        private static bool goblinWarningShown;

        private HWNode parent;
        public HWNode Parent
        {
            get { return parent; }
            set { parent = value; value.Name = FormattedName; Renderer.InvalidateView(); Renderer.Invalidate(); }
        }

        public string Name;
        public string FormattedName
        {
            get
            {
                if (Prefix == "")
                    return Name;

                string lod = "";
                if (LOD != -1)
                    lod = "_LOD[" + LOD + "]";

                string tags = "";
                if (DoScars)
                    tags = "_TAGS[DoScar]";

                return Prefix + "[" + Name + "]" + lod + tags;
            }
        }
        public int LOD = -1;

        private bool doScars = false;
        public bool DoScars { get { return doScars; } set { doScars = value; Parent.Name = FormattedName; } }
        public string Prefix = "";

        public bool Visible = false;
        public bool Shaded = true;
        public bool Translucent = false;
        public bool VertexColored = true;
        public Vector3 Scale = Vector3.One;

        public Matrix4 ModelMatrix;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public HWVertex[] Vertices;
        public int[] Indices;

        private HWMaterial material = new HWMaterial();
        public HWMaterial Material { get { return material; } set { material = value; Renderer.Invalidate(); } }

        public int VertexCount { get { return mesh.VertexCount; } }
        public int IndiceCount { get { return Indices.Length; } }
        public int FaceCount { get { return mesh.FaceCount; } }
        public int TextureCoordinateChannelCount { get { return mesh.TextureCoordinateChannelCount; } }
        public List<Face> Faces { get { return mesh.Faces; } }

        public int AnimationCount { get { return mesh.MeshAnimationAttachmentCount; } }

        private bool MinMaxSet = false;
        private Vector3 _max, _min;
        public Vector3 Max
        {
            get
            {
                if (!MinMaxSet)
                {
                    _max = -float.MaxValue * Vector3.One;
                    _min = float.MaxValue * Vector3.One;
                    foreach (HWVertex vtx in Vertices)
                    {
                        _max = Vector3.Max(_max, vtx.Position);
                        _min = Vector3.Min(_min, vtx.Position);
                    }
                    MinMaxSet = true;
                }
                return _max;
            }
            private set { }
        }
        public Vector3 Min
        {
            get
            {
                if (!MinMaxSet)
                {
                    _max = -float.MaxValue * Vector3.One;
                    _min = float.MaxValue * Vector3.One;
                    foreach (HWVertex vtx in Vertices)
                    {
                        _max = Vector3.Max(_max, vtx.Position);
                        _min = Vector3.Min(_min, vtx.Position);
                    }
                    MinMaxSet = true;
                }
                return _min;
            }
            private set { }
        }

        Mesh mesh;

        public HWMesh(Mesh mesh)
        {
            this.mesh = mesh;
            Name = mesh.Name;

            GetData();

            HWScene.Meshes.Add(this);
        }

        public void GetData()
        {
            List<HWVertex> vtxs = new List<HWVertex>();
            Vector3[] Positions = GetVertices();
            Vector3[] Normals = GetNormals();
            Vector3[] Colors = GetColorData();
            Vector2[] TextureCoords = GetTextureCoords();
            Vector2[] TextureCoordsUV1 = GetTextureCoordsUV1();
            Vector3[] Tangents = GetTangents();
            Vector3[] BiTangents = GetBiTangents();

            for (int i = 0; i < Positions.Length; ++i)
            {
                HWVertex vtx = new HWVertex();
                vtx.Position = Positions[i];
                if (Normals.Length - 1 >= i)
                    vtx.Normal = Normals[i];
                vtx.Color = Colors[i];
                vtx.UV0 = TextureCoords[i];
                vtx.UV1 = TextureCoordsUV1[i];
                if (Tangents.Length > 0)
                    vtx.Tangent = Tangents[i];
                if (BiTangents.Length > 0)
                    vtx.Binormal = BiTangents[i];
                vtxs.Add(vtx);
            }

            Vertices = vtxs.ToArray();
            Indices = GetIndices();

            if (Normals.Length <= 0)
                RecalculateNormals();
        }

        public void SetMesh(Mesh mesh)
        {
            this.mesh = mesh;
            GetData();
            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        public void ParseMesh()
        {
            #region ShipMesh
            if (Parent.Name.StartsWith("MULT")) //If visible ship mesh
            {
                string name = "";
                int lod = 0;
                ObservableCollection<ShipMeshTag> tags = new ObservableCollection<ShipMeshTag>();

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
                            bool success = int.TryParse(splitted[i].Substring(0, end), out lod);
                            if(!success)
                            {
                                new Problem(ProblemTypes.ERROR, "Failed to parse LOD of ship mesh \"" + Parent.Name + "\".");
                                return;
                            }
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

                if (name == "")
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of ship mesh \"" + Parent.Name + "\".");
                    return;
                }

                if(!Parent.IsDescendantOf(HWNode.Roots[lod]))
                {
                    new Problem(ProblemTypes.WARNING, "Ship mesh \"" + Parent.Name + "\" is marked with LOD " + lod + ", but is not under \"ROOT_LOD[" + lod + "]\".");
                    return;
                }

                HWJoint parentJoint = null;
                Name = name;
                Prefix = "MULT";

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

                if (name == "")
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of collision mesh \"" + Parent.Name + "\".");
                    return;
                }

                Name = name;
                Prefix = "COL";

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
            else if (Parent.Name.StartsWith("GLOW")) //If visible glow mesh
            {
                string name = "";
                int lod = -1;

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
                            bool success = int.TryParse(splitted[i].Substring(0, end), out lod);
                            if (!success)
                            {
                                new Problem(ProblemTypes.ERROR, "Failed to parse LOD of engine glow \"" + Parent.Name + "\".");
                            }
                        }
                    }
                }

                if (name == "")
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of engine glow \"" + Parent.Name + "\".");
                    return;
                }

                if (!Parent.IsDescendantOf(HWNode.Roots[lod]))
                {
                    new Problem(ProblemTypes.WARNING, "Engine glow \"" + Parent.Name + "\" is marked with LOD " + lod + ", but is not under \"ROOT_LOD[" + lod + "]\".");
                    return;
                }

                Name = name;
                Prefix = "GLOW";

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

                if (name == "")
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of engine shape \"" + Parent.Name + "\".");
                    return;
                }

                Name = name;
                Prefix = "ETSH";

                HWJoint parentJoint = null;

                if (Parent.Parent != null)
                {
                    if (Parent.Parent.Joint != null)
                        parentJoint = Parent.Parent.Joint;
                }

                HWEngineShape newEngineShape = new HWEngineShape(this, parentJoint, name);
            }
            #endregion
            else
            {
                new Problem(ProblemTypes.WARNING, "Failed to parse mesh \"" + Parent.Name + "\".");
            }
        }

        public void Destroy()
        {
            HWScene.Meshes.Remove(this);
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

            if (offset != 0)
            {
                for (int i = 0; i < indices.Length; i++)
                {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public Vector3[] GetColorData()
        {
            Vector3[] colorData = new Vector3[VertexCount];
            for (int i = 0; i < VertexCount; i++)
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

        public Vector2[] GetTextureCoordsUV1()
        {
            if (mesh.TextureCoordinateChannelCount > 1)
            {
                List<Vector2> coords = new List<Vector2>();

                foreach (Vector3D coord in mesh.TextureCoordinateChannels[1])
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
            ModelMatrix = Matrix4.CreateScale(Scale);

            if(Parent != null)
                if (Parent.Parent != null) //Ignore first parent
                    ModelMatrix *= Parent.WorldMatrix;

            ModelViewProjectionMatrix = ModelMatrix * Renderer.ViewProjection;
        }

        private void RecalculateNormals()
        {
            // See if we have anything to do.
            if (Vertices.Length == 0 || Indices.Length == 0) return;

            Vector3[] normals = new Vector3[Vertices.Length];
            for (int i = 0; i < Indices.Length; i += 3)
            {
                int ind1 = Indices[i + 0];
                int ind2 = Indices[i + 1];
                int ind3 = Indices[i + 2];
                Vector3 v1 = Vertices[ind1].Position;
                Vector3 v2 = Vertices[ind2].Position;
                Vector3 v3 = Vertices[ind3].Position;
                Vector3 v4 = Vector3.Cross(v2 - v1, v3 - v1);
                normals[ind1] += v4;
                normals[ind2] += v4;
                normals[ind3] += v4;
            }
            for (int i = 0; i < normals.Length; ++i)
            {
                normals[i].Normalize();
                Vertices[i].Normal = normals[i];
            }
        }

        private void RecalculateTangents()
        {
            // float tolerance = 0.01f;
            foreach (HWVertex vtx in Vertices)
            {
                vtx.Tangent = new Vector3();
                vtx.Binormal = new Vector3();
            }
            int[] altv = new int[Vertices.Length];
            int[] handedness = new int[Vertices.Length];
            for (int i = 0; i < Vertices.Length; ++i)
            {
                altv[i] = -1;
                handedness[i] = 0;
            }
            for (int i = 0; i < Indices.Length; i += 3)
            {
                Vector3 fT, fB;
                int i1 = Indices[i + 0];
                int i2 = Indices[i + 1];
                int i3 = Indices[i + 2];
                int fH, N;
                _CalcFaceTangents(out fT, out fB, out fH, i1, i2, i3);
                _UpdateVertTangents(altv, ref handedness, fT, fB, fH, i1, out N);
                Indices[i1] = N;
                _UpdateVertTangents(altv, ref handedness, fT, fB, fH, i2, out N);
                Indices[i2] = N;
                _UpdateVertTangents(altv, ref handedness, fT, fB, fH, i3, out N);
                Indices[i3] = N;
            }
            for (int i = 0; i < Vertices.Length; ++i)
                _NormaliseVertTangents(handedness[i], i);
        }

        private void _CalcFaceTangents(
            out Vector3 tangent, out Vector3 bitangent, out int hand,
            int i1, int i2, int i3)
        {
            Vector3 X, Y, Z, S, T;
            Vector3 norm, temp, v1, v2, v3;
            float R, dp;

            X.X = Vertices[i2].Position.X - Vertices[i1].Position.X;
            X.Y = Vertices[i3].Position.X - Vertices[i1].Position.X;

            Y.X = Vertices[i2].Position.Y - Vertices[i1].Position.Y;
            Y.Y = Vertices[i3].Position.Y - Vertices[i1].Position.Y;

            Z.X = Vertices[i2].Position.Z - Vertices[i1].Position.Z;
            Z.Y = Vertices[i3].Position.Z - Vertices[i1].Position.Z;

            S.X = Vertices[i2].UV0.X - Vertices[i1].UV0.X;
            S.Y = Vertices[i3].UV0.X - Vertices[i1].UV0.X;

            T.X = Vertices[i2].UV0.Y - Vertices[i1].UV0.Y;
            T.Y = Vertices[i3].UV0.Y - Vertices[i1].UV0.Y;

            R = S.X * T.Y - S.Y * T.X;

            if (R != 0)
                R = 1f / R;

            tangent.X = (T.Y * X.X - T.X * X.Y) * R;
            tangent.Y = (T.Y * Y.X - T.X * Y.Y) * R;
            tangent.Z = (T.Y * Z.X - T.X * Z.Y) * R;

            if (tangent.X == 0 && tangent.Y == 0 && tangent.Z == 0)
            {
                // no actual texture variation so use vertex vector to help prevent things breaking
                tangent.X = X.X;
                tangent.Y = Y.X;
                tangent.Z = Z.X;
                // Zero tangent found, aligning with vector between first and second vertex
            }

            tangent.Normalize();

            bitangent.X = (S.X * X.Y - S.Y * X.X) * R;
            bitangent.Y = (S.X * Y.Y - S.Y * Y.X) * R;
            bitangent.Z = (S.X * Z.Y - S.Y * Z.X) * R;

            if (bitangent.X == 0 && bitangent.Y == 0 && bitangent.Z == 0)
            {
                // no actual texture variation so use vertex vector to help prevent things breaking
                bitangent.X = X.Y;
                bitangent.Y = Y.Y;
                bitangent.Z = Z.Y;
                // Zero bitangent found, aligning with vector between first and third vertex
            }
            bitangent.Normalize();

            // calculate face normal
            v1 = Vertices[i1].Position;
            v2 = Vertices[i2].Position;
            v3 = Vertices[i3].Position;
            norm = Vector3.Cross(v2 - v1, v3 - v1);
            norm.Normalize();

            // calculate face handedness
            temp = Vector3.Cross(norm, tangent);
            dp = Vector3.Dot(temp, bitangent);
            if (dp < 0)
                hand = -1;
            else
                hand = 1;
        }

        private void _UpdateVertTangents(int[] altv, ref int[] handedness, Vector3 fT, Vector3 fB, int fH, int v, out int n)
        {
            n = v;
            Vertices[n].Tangent += fT;
            Vertices[n].Binormal += fB;
            //if (handedness[v] != 0 && fH != handedness[v])
            //{
            //    // need to search for or create an alternately handed vertex
            //    n = altv[v];
            //    if (n == -1)
            //    {
            //        // no alternate vertex found, clone current, reset tangents incase they have been set
            //        HWVertex[] tempV = Vertices;
            //        Vertices = new HWVertex[Vertices.Length + 1];
            //        Array.Copy(tempV, Vertices, tempV.Length);
            //        n = Vertices.Length - 1;
            //        Vertices[n] = new HWVertex(Vertices[v]);
            //        Vertices[n].Tangent = new Vector3();
            //        Vertices[n].Binormal = new Vector3();

            //        int[] temp = handedness;
            //        handedness = new int[handedness.Length + 1];
            //        Array.Copy(temp, handedness, temp.Length);
            //        handedness[n] = fH;
            //        altv[v] = n;
            //    }
            //    Vertices[n].Tangent += fT;
            //    Vertices[n].Binormal += fB;
            //}
            //else
            //{
            //    Vertices[v].Tangent += fT;
            //    Vertices[v].Binormal += fB;
            //    handedness[v] = fH;
            //    n = v;
            //}
        }

        private void _NormaliseVertTangents(int handedness, int i)
        {
            // normalise (average) accumulated tangents
            Vector3 nTangent = Vector3.Normalize(Vertices[i].Tangent);
            Vector3 nBitangent = Vector3.Normalize(Vertices[i].Binormal);

            Vector3 norm;
            // Gram-Schmidt orthogonalize
            norm.X = Vertices[i].Normal.X;
            norm.Y = Vertices[i].Normal.Y;
            norm.Z = Vertices[i].Normal.Z;
            norm.Normalize(); // Paranoia!

            float dp = Vector3.Dot(norm, nTangent);
            if (dp == 1 || dp == -1)
                Log.WriteLine("Vertex" + i + ": Tangent generation failed, normal and tangent parallel.");
            Vector3 temp = norm * dp;
            temp = nTangent - temp;
            Vertices[i].Tangent = Vector3.Normalize(temp);
            Vertices[i].Binormal = Vector3.Cross(norm, Vertices[i].Tangent);

            // Flip Bitangent according to local handedness
            temp = Vector3.Cross(norm, nTangent);
            dp = Vector3.Dot(temp, nBitangent);
            if (dp < 0)
                Vertices[i].Binormal *= -1;
        }
    }
}
