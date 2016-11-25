using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DAEnerys
{
    public static class Exporter
    {
        private static XNamespace ns = "http://www.collada.org/2005/11/COLLADASchema";
        private static XElement[] lodRootElements;
        private static Dictionary<HWJoint, XElement> jointElements = new Dictionary<HWJoint, XElement>();

        public static void ExportToFile(string path)
        {
            AddedMesh.AddedMeshes.Clear();
            lodRootElements = new XElement[4];
            jointElements.Clear();

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "true"));

            XElement collada = new XElement(ns + "COLLADA");
            collada.SetAttributeValue("version", "1.4.1");
            doc.Add(collada);

            #region asset
            XElement asset = new XElement(ns + "asset");
            collada.Add(asset);

            XElement contributor = new XElement(ns + "contributor");
            asset.Add(contributor);

            XElement author = new XElement(ns + "author");
            author.SetValue("");
            contributor.Add(author);

            XElement tool = new XElement(ns + "authoring_tool");
            tool.SetValue("DAEnerys b" + Program.main.BUILD);
            contributor.Add(tool);

            XElement comments = new XElement(ns + "comments");
            comments.SetValue("");
            contributor.Add(comments);

            XElement created = new XElement(ns + "created");
            created.SetValue(DateTime.Now.ToString());
            asset.Add(created);

            XElement keywords = new XElement(ns + "keywords");
            keywords.SetValue("");
            asset.Add(keywords);

            XElement modified = new XElement(ns + "modified");
            modified.SetValue(DateTime.Now.ToString());
            asset.Add(modified);

            XElement revision = new XElement(ns + "revision");
            revision.SetValue("");
            asset.Add(revision);

            XElement subject = new XElement(ns + "subject");
            subject.SetValue("Meant for Homeworld Remastered");
            asset.Add(subject);

            XElement title = new XElement(ns + "title");
            title.SetValue("");
            asset.Add(title);

            XElement unit = new XElement(ns + "unit");
            unit.SetAttributeValue("meter", "1.0");
            unit.SetAttributeValue("unit", "centimeter");
            asset.Add(unit);

            XElement axis = new XElement(ns + "up_axis");
            axis.SetValue("Y_UP");
            asset.Add(axis);
            #endregion

            #region images
            XElement libImages = new XElement(ns + "library_images");
            collada.Add(libImages);

            List<string> addedImages = new List<string>();
            foreach (HWImage image in HWImage.Images)
            {
                if (image.ColladaName.Length == 0)
                    continue;

                while (addedImages.Contains(image.FormattedName))
                {
                    image.Suffix++;
                }

                XElement imageElement = new XElement(ns + "image");
                imageElement.SetAttributeValue("id", image.FormattedName + "-image");
                imageElement.SetAttributeValue("name", image.FormattedName);
                libImages.Add(imageElement);

                XElement initFrom = new XElement(ns + "init_from");
                initFrom.SetValue(image.Path);
                imageElement.Add(initFrom);

                addedImages.Add(image.FormattedName);
            }
            #endregion

            #region materials
            XElement libMaterials = new XElement(ns + "library_materials");
            collada.Add(libMaterials);

            List<string> addedMaterials = new List<string>();
            foreach (HWMaterial material in HWMaterial.Materials)
            {
                if (material.Name.Length == 0 || material.Name.Contains("[") || material.Name.Contains("]"))
                    continue;

                while(addedMaterials.Contains(material.FormattedName))
                {
                    material.Suffix++;
                }

                if (!material.FormattedName.StartsWith("MAT["))
                    continue;

                XElement materialElement = new XElement(ns + "material");
                materialElement.SetAttributeValue("id", material.FormattedName);
                materialElement.SetAttributeValue("name", material.FormattedName);
                libMaterials.Add(materialElement);

                XElement instanceEffect = new XElement(ns + "instance_effect");
                instanceEffect.SetAttributeValue("url", "#" + material.FormattedName + "-fx");
                materialElement.Add(instanceEffect);

                addedMaterials.Add(material.FormattedName);
            }
            #endregion

            #region effects
            XElement libEffects = new XElement(ns + "library_effects");
            collada.Add(libEffects);

            foreach (HWMaterial material in HWMaterial.Materials)
            {
                if (material.Name.Length == 0)
                    continue;

                if (!material.FormattedName.StartsWith("MAT["))
                    continue;

                XElement effectElement = new XElement(ns + "effect");
                effectElement.SetAttributeValue("id", material.FormattedName + "-fx");
                effectElement.SetAttributeValue("name", material.FormattedName);
                libEffects.Add(effectElement);

                XElement profile = new XElement(ns + "profile_COMMON");
                effectElement.Add(profile);

                XElement technique = new XElement(ns + "technique");
                technique.SetAttributeValue("sid", "standard");
                profile.Add(technique);

                XElement phong = new XElement(ns + "phong");
                technique.Add(phong);

                XElement diffuse = new XElement(ns + "diffuse");
                phong.Add(diffuse);

                if (material.Images.Count > 0)
                {
                    XElement texture = new XElement(ns + "texture");
                    texture.SetAttributeValue("texture", material.Images[0].FormattedName + "-image");
                    texture.SetAttributeValue("texcoord", "CHANNEL0"); //TODO: Support other channels
                    diffuse.Add(texture);
                }
            }
            #endregion

            #region geometries
            XElement libGeometries = new XElement(ns + "library_geometries");
            collada.Add(libGeometries);

            foreach (HWMesh mesh in HWMesh.Meshes)
            {
                if (!mesh.FormattedName.StartsWith("MULT") && !mesh.FormattedName.StartsWith("COL") && !mesh.FormattedName.StartsWith("ETSH") && !mesh.FormattedName.StartsWith("GLOW"))
                    continue;

                AddedMesh addedMesh = AddedMesh.GetByName(mesh.FormattedName);

                XElement geometry;
                XElement meshElement;

                if (addedMesh == null)
                {
                    geometry = new XElement(ns + "geometry");
                    geometry.SetAttributeValue("id", mesh.FormattedName + "-lib");
                    geometry.SetAttributeValue("name", mesh.FormattedName);
                    libGeometries.Add(geometry);

                    meshElement = new XElement(ns + "mesh");
                    geometry.Add(meshElement);

                    #region position source
                    XElement source = new XElement(ns + "source");
                    source.SetAttributeValue("id", mesh.FormattedName + "-POSITION");
                    meshElement.Add(source);

                    XElement posArray = new XElement(ns + "float_array");
                    posArray.SetAttributeValue("id", mesh.FormattedName + "-POSITION-array");
                    posArray.SetAttributeValue("count", mesh.VertexCount * 3);
                    StringBuilder positions = new StringBuilder("\n");
                    foreach (Vector3 vertex in mesh.Vertices)
                        positions.AppendLine(vertex.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Z.ToString(CultureInfo.InvariantCulture));
                    posArray.SetValue(positions.ToString());
                    source.Add(posArray);

                    XElement technique = new XElement(ns + "technique_common");
                    source.Add(technique);

                    XElement posAccessor = new XElement(ns + "accessor");
                    posAccessor.SetAttributeValue("source", "#" + mesh.FormattedName + "-POSITION-array");
                    posAccessor.SetAttributeValue("count", mesh.VertexCount);
                    posAccessor.SetAttributeValue("stride", "3");
                    technique.Add(posAccessor);

                    XElement parameter = new XElement(ns + "param");
                    parameter.SetAttributeValue("name", "X");
                    parameter.SetAttributeValue("type", "float");
                    posAccessor.Add(parameter);
                    parameter = new XElement(ns + "param");
                    parameter.SetAttributeValue("name", "Y");
                    parameter.SetAttributeValue("type", "float");
                    posAccessor.Add(parameter);
                    parameter = new XElement(ns + "param");
                    parameter.SetAttributeValue("name", "Z");
                    parameter.SetAttributeValue("type", "float");
                    posAccessor.Add(parameter);
                    #endregion

                    #region normal source
                    source = new XElement(ns + "source");
                    source.SetAttributeValue("id", mesh.FormattedName + "-Normal0");
                    meshElement.Add(source);

                    XElement normalArray = new XElement(ns + "float_array");
                    normalArray.SetAttributeValue("id", mesh.FormattedName + "-Normal0-array");
                    normalArray.SetAttributeValue("count", mesh.VertexCount * 3);
                    StringBuilder normals = new StringBuilder("\n");
                    foreach (Vector3 normal in mesh.Normals)
                        normals.AppendLine(normal.X.ToString(CultureInfo.InvariantCulture) + " " + normal.Y.ToString(CultureInfo.InvariantCulture) + " " + normal.Z.ToString(CultureInfo.InvariantCulture));
                    normalArray.SetValue(normals.ToString());
                    source.Add(normalArray);

                    technique = new XElement(ns + "technique_common");
                    source.Add(technique);

                    XElement normalAccessor = new XElement(ns + "accessor");
                    normalAccessor.SetAttributeValue("source", "#" + mesh.FormattedName + "-Normal0-array");
                    normalAccessor.SetAttributeValue("count", mesh.VertexCount);
                    normalAccessor.SetAttributeValue("stride", "3");
                    technique.Add(normalAccessor);

                    parameter = new XElement(ns + "param");
                    parameter.SetAttributeValue("name", "X");
                    parameter.SetAttributeValue("type", "float");
                    normalAccessor.Add(parameter);
                    parameter = new XElement(ns + "param");
                    parameter.SetAttributeValue("name", "Y");
                    parameter.SetAttributeValue("type", "float");
                    normalAccessor.Add(parameter);
                    parameter = new XElement(ns + "param");
                    parameter.SetAttributeValue("name", "Z");
                    parameter.SetAttributeValue("type", "float");
                    normalAccessor.Add(parameter);
                    #endregion

                    #region uv0 source
                    XElement uv0Array = null;
                    XElement uv0Accessor = null;
                    if (mesh.UVCount > 0)
                    {
                        source = new XElement(ns + "source");
                        source.SetAttributeValue("id", mesh.FormattedName + "-UV0");
                        meshElement.Add(source);

                        uv0Array = new XElement(ns + "float_array");
                        uv0Array.SetAttributeValue("id", mesh.FormattedName + "-UV0-array");
                        uv0Array.SetAttributeValue("count", mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder("\n");
                        foreach (Vector2 uv in mesh.UV0)
                            uvs.AppendLine(uv.X.ToString(CultureInfo.InvariantCulture) + " " + uv.Y.ToString(CultureInfo.InvariantCulture));
                        uv0Array.SetValue(uvs.ToString());
                        source.Add(uv0Array);

                        technique = new XElement(ns + "technique_common");
                        source.Add(technique);

                        uv0Accessor = new XElement(ns + "accessor");
                        uv0Accessor.SetAttributeValue("source", "#" + mesh.FormattedName + "-UV0-array");
                        uv0Accessor.SetAttributeValue("count", mesh.VertexCount);
                        uv0Accessor.SetAttributeValue("stride", "2");
                        technique.Add(uv0Accessor);

                        parameter = new XElement(ns + "param");
                        parameter.SetAttributeValue("name", "S");
                        parameter.SetAttributeValue("type", "float");
                        uv0Accessor.Add(parameter);
                        parameter = new XElement(ns + "param");
                        parameter.SetAttributeValue("name", "T");
                        parameter.SetAttributeValue("type", "float");
                        uv0Accessor.Add(parameter);
                    }
                    #endregion

                    #region uv1 source
                    XElement uv1Array = null;
                    XElement uv1Accessor = null;
                    if (mesh.UVCount > 1)
                    {
                        source = new XElement(ns + "source");
                        source.SetAttributeValue("id", mesh.FormattedName + "-UV1");
                        meshElement.Add(source);

                        uv1Array = new XElement(ns + "float_array");
                        uv1Array.SetAttributeValue("id", mesh.FormattedName + "-UV1-array");
                        uv1Array.SetAttributeValue("count", mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder("\n");
                        foreach (Vector2 uv1 in mesh.UV1)
                            uvs.AppendLine(uv1.X.ToString(CultureInfo.InvariantCulture) + " " + uv1.Y.ToString(CultureInfo.InvariantCulture));
                        uv1Array.SetValue(uvs.ToString());
                        source.Add(uv1Array);

                        technique = new XElement(ns + "technique_common");
                        source.Add(technique);

                        uv1Accessor = new XElement(ns + "accessor");
                        uv1Accessor.SetAttributeValue("source", "#" + mesh.FormattedName + "-UV1-array");
                        uv1Accessor.SetAttributeValue("count", mesh.VertexCount);
                        uv1Accessor.SetAttributeValue("stride", "2");
                        technique.Add(uv1Accessor);

                        parameter = new XElement(ns + "param");
                        parameter.SetAttributeValue("name", "S");
                        parameter.SetAttributeValue("type", "float");
                        uv1Accessor.Add(parameter);
                        parameter = new XElement(ns + "param");
                        parameter.SetAttributeValue("name", "T");
                        parameter.SetAttributeValue("type", "float");
                        uv1Accessor.Add(parameter);
                    }
                    #endregion

                    XElement vertices = new XElement(ns + "vertices");
                    vertices.SetAttributeValue("id", mesh.FormattedName + "-VERTEX");
                    meshElement.Add(vertices);
                    XElement vertexInput = new XElement(ns + "input");
                    vertexInput.SetAttributeValue("semantic", "POSITION");
                    vertexInput.SetAttributeValue("source", "#" + mesh.FormattedName + "-POSITION");
                    vertices.Add(vertexInput);

                    new AddedMesh(mesh.FormattedName, mesh, meshElement, posArray, posAccessor, normalArray, normalAccessor, uv0Array, uv0Accessor, uv1Array, uv1Accessor);
                }
                else
                {
                    meshElement = addedMesh.MeshElement;

                    #region position source
                    int lastCount = int.Parse(addedMesh.PositionArray.Attribute("count").Value);
                    addedMesh.PositionArray.SetAttributeValue("count", lastCount + mesh.VertexCount * 3);
                    StringBuilder positions = new StringBuilder(addedMesh.PositionArray.Value);
                    foreach (Vector3 vertex in mesh.Vertices)
                        positions.AppendLine(vertex.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Z.ToString(CultureInfo.InvariantCulture));
                    addedMesh.PositionArray.SetValue(positions.ToString());

                    lastCount = int.Parse(addedMesh.PositionAccessor.Attribute("count").Value);
                    addedMesh.PositionAccessor.SetAttributeValue("count", lastCount + mesh.VertexCount);
                    #endregion

                    #region normal source
                    lastCount = int.Parse(addedMesh.NormalArray.Attribute("count").Value);
                    addedMesh.NormalArray.SetAttributeValue("count", lastCount + mesh.VertexCount * 3);
                    StringBuilder normals = new StringBuilder(addedMesh.NormalArray.Value);
                    foreach (Vector3 normal in mesh.Normals)
                        normals.AppendLine(normal.X.ToString(CultureInfo.InvariantCulture) + " " + normal.Y.ToString(CultureInfo.InvariantCulture) + " " + normal.Z.ToString(CultureInfo.InvariantCulture));
                    addedMesh.NormalArray.SetValue(normals.ToString());

                    lastCount = int.Parse(addedMesh.NormalAccessor.Attribute("count").Value);
                    addedMesh.NormalAccessor.SetAttributeValue("count", lastCount + mesh.VertexCount);
                    #endregion

                    #region uv0 source
                    if (mesh.UVCount > 0)
                    {
                        lastCount = int.Parse(addedMesh.UV0Array.Attribute("count").Value);
                        addedMesh.UV0Array.SetAttributeValue("count", lastCount + mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder(addedMesh.UV0Array.Value);
                        foreach (Vector2 uv0 in mesh.UV0)
                            uvs.AppendLine(uv0.X.ToString(CultureInfo.InvariantCulture) + " " + uv0.Y.ToString(CultureInfo.InvariantCulture));
                        addedMesh.UV0Array.SetValue(uvs.ToString());

                        lastCount = int.Parse(addedMesh.UV0Accessor.Attribute("count").Value);
                        addedMesh.UV0Accessor.SetAttributeValue("count", lastCount + mesh.VertexCount);
                    }
                    #endregion

                    #region uv1 source
                    if (mesh.UVCount > 1)
                    {
                        lastCount = int.Parse(addedMesh.UV1Array.Attribute("count").Value);
                        addedMesh.UV1Array.SetAttributeValue("count", lastCount + mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder(addedMesh.UV1Array.Value);
                        foreach (Vector2 uv1 in mesh.UV1)
                            uvs.AppendLine(uv1.X.ToString(CultureInfo.InvariantCulture) + " " + uv1.Y.ToString(CultureInfo.InvariantCulture));
                        addedMesh.UV1Array.SetValue(uvs.ToString());

                        lastCount = int.Parse(addedMesh.UV1Accessor.Attribute("count").Value);
                        addedMesh.UV1Accessor.SetAttributeValue("count", lastCount + mesh.VertexCount);
                    }
                    #endregion
                }

                byte inputCount = 2;

                XElement triangles = new XElement(ns + "triangles");
                triangles.SetAttributeValue("count", mesh.FaceCount);
                if (mesh.Material.Name.Length != 0)
                    triangles.SetAttributeValue("material", mesh.Material.FormattedName);
                meshElement.Add(triangles);
                XElement input = new XElement(ns + "input");
                input.SetAttributeValue("semantic", "VERTEX");
                input.SetAttributeValue("offset", "0");
                input.SetAttributeValue("source", "#" + mesh.FormattedName + "-VERTEX");
                triangles.Add(input);
                input = new XElement(ns + "input");
                input.SetAttributeValue("semantic", "NORMAL");
                input.SetAttributeValue("offset", "1");
                input.SetAttributeValue("source", "#" + mesh.FormattedName + "-Normal0");
                triangles.Add(input);

                if (mesh.UVCount > 0)
                {
                    input = new XElement(ns + "input");
                    input.SetAttributeValue("semantic", "TEXCOORD");
                    input.SetAttributeValue("offset", "2");
                    input.SetAttributeValue("set", "0");
                    input.SetAttributeValue("source", "#" + mesh.FormattedName + "-UV0");
                    triangles.Add(input);
                    inputCount++;
                }
                if (mesh.UVCount > 1)
                {
                    input = new XElement(ns + "input");
                    input.SetAttributeValue("semantic", "TEXCOORD");
                    input.SetAttributeValue("offset", "3");
                    input.SetAttributeValue("set", "1");
                    input.SetAttributeValue("source", "#" + mesh.FormattedName + "-UV1");
                    triangles.Add(input);
                    inputCount++;
                }

                int indexOffset = 0;
                if (addedMesh != null)
                    indexOffset = addedMesh.IndexOffset;

                XElement p = new XElement(ns + "p");
                StringBuilder faces = new StringBuilder("\n");

                for (int f = 0; f < mesh.IndexCount; f += 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int c = 0; c < inputCount; c++)
                        {
                            faces.Append(" " + (int)(mesh.GetIndices(indexOffset)[f + i]));
                        }
                    }
                    faces.AppendLine();
                }
                p.SetValue(faces.ToString());
                triangles.Add(p);

                if(addedMesh != null)
                    addedMesh.IndexOffset += mesh.VertexCount;
            }
            #endregion

            #region animations
            XElement libAnimations = new XElement(ns + "library_animations");
            libAnimations.SetValue("");
            collada.Add(libAnimations);
            #endregion

            #region visual scenes
            XElement libVisualScenes = new XElement(ns + "library_visual_scenes");
            collada.Add(libVisualScenes);

            XElement visualScene = new XElement(ns + "visual_scene");
            visualScene.SetAttributeValue("id", "scene");
            visualScene.SetAttributeValue("name", "scene");
            libVisualScenes.Add(visualScene);

            #region ROOT_INFO
            XElement rootInfo = AddNode(visualScene, "ROOT_INFO", Matrix4.Identity);
            AddNode(rootInfo, "Class[MultiMesh]_Version[512]", Matrix4.Identity);

            int uvSets = 1;
            foreach (HWShipMesh shipMesh in HWShipMesh.ShipMeshes)
                foreach (HWShipMeshLOD shipMeshLOD in shipMesh.Meshes)
                    if (shipMeshLOD.UVCount > uvSets)
                        uvSets = shipMeshLOD.UVCount;
            AddNode(rootInfo, "UVSets[" + uvSets + "]", Matrix4.Identity);
            #endregion

            #region ROOT_LOD[X]
            for (int lod = 0; lod < lodRootElements.Length; lod++)
            {
                bool lodExists = false;
                foreach (HWShipMesh shipMesh in HWShipMesh.ShipMeshes)
                    foreach (HWShipMeshLOD lodMesh in shipMesh.Meshes)
                        if (lodMesh.LOD == lod)
                            lodExists = true;

                foreach (HWEngineGlow engineGlow in HWEngineGlow.EngineGlows)
                    foreach (HWEngineGlowLOD lodMesh in engineGlow.Meshes)
                        if (lodMesh.LOD == lod)
                            lodExists = true;

                if (lodExists || lod == 0)
                {
                    //Offset the LOD roots so they are clean when importing with 3d software
                    Vector3 pos = new Vector3(100 * lod, 0, 0);
                    Matrix4 transform = Matrix4.CreateTranslation(pos);
                    lodRootElements[lod] = AddNode(visualScene, "ROOT_LOD[" + lod + "]", transform);
                }
            }
            #endregion

            //Joints
            jointElements.Add(HWJoint.Root, lodRootElements[0]);

            foreach(HWJoint joint in HWJoint.Root.Children)
            {
                AddJointRecursive(lodRootElements[0], joint);
            }

            //Ship meshes
            foreach(HWShipMesh shipMesh in HWShipMesh.ShipMeshes)
            {
                if (shipMesh.LODMeshes[0].Count == 0)
                    continue;

                AddNode(jointElements[shipMesh.Parent], shipMesh.LODMeshes[0][0].FormattedName, shipMesh.LODMeshes[0][0].Transform, shipMesh.LODMeshes[0].ToArray());

                for(int lod = 1; lod <= 3; lod++)
                {
                    if (shipMesh.LODMeshes[lod].Count == 0)
                        continue;

                    AddNode(lodRootElements[lod], shipMesh.LODMeshes[lod][0].FormattedName, shipMesh.LODMeshes[lod][0].Transform, shipMesh.LODMeshes[lod].ToArray());
                }
            }

            //Engine glows
            foreach (HWEngineGlow engineGlow in HWEngineGlow.EngineGlows)
            {
                if (engineGlow.LODMeshes[0].Count == 0)
                    continue;

                AddNode(jointElements[engineGlow.Parent], engineGlow.LODMeshes[0][0].FormattedName, engineGlow.LODMeshes[0][0].Transform, engineGlow.LODMeshes[0].ToArray());

                for (int lod = 1; lod <= 3; lod++)
                {
                    if (engineGlow.LODMeshes[lod].Count == 0)
                        continue;

                    AddNode(lodRootElements[lod], engineGlow.LODMeshes[lod][0].FormattedName, engineGlow.LODMeshes[lod][0].Transform, engineGlow.LODMeshes[lod].ToArray());
                }
            }

            //Engine shapes
            foreach(HWEngineShape engineShape in HWEngineShape.EngineShapes)
            {
                AddNode(jointElements[engineShape.Parent], engineShape.FormattedName, engineShape.Transform, new HWMesh[] { engineShape });
            }

            //Markers
            foreach(HWMarker marker in HWMarker.Markers)
            {
                AddNode(jointElements[(HWJoint)marker.Parent], marker.FormattedName, marker.RelativeWorldMatrix);
            }

            //Navlights
            foreach(HWNavLight navLight in HWNavLight.NavLights)
            {
                AddNode(jointElements[(HWJoint)navLight.Parent], navLight.FormattedName, navLight.RelativeWorldMatrix);
            }

            //Dockpaths
            XElement holdDockElement = AddNode(lodRootElements[0], "HOLD_DOCK", Matrix4.Identity);
            
            foreach(HWDockpath dockpath in HWDockpath.Dockpaths)
            {
                XElement dockpathElement = AddNode(holdDockElement, dockpath.FormattedName, Matrix4.Identity);
                foreach(HWDockSegment segment in dockpath.Segments)
                {
                    XElement segmentElement = AddNode(dockpathElement, segment.FormattedName, segment.RelativeWorldMatrix);
                }
            }

            //Engine burns
            foreach (HWEngineBurn engineBurn in HWEngineBurn.EngineBurns)
            {
                XElement engineBurnElement = AddNode(jointElements[(HWJoint)engineBurn.Parent], engineBurn.FormattedName, engineBurn.RelativeWorldMatrix);
                foreach (HWEngineFlame flame in engineBurn.Flames)
                {
                    XElement flameElement = AddNode(engineBurnElement, flame.FormattedName, flame.RelativeWorldMatrix);
                }
            }

            #region ROOT_COL
            if (HWCollisionMesh.CollisionMeshes.Count > 0)
            {
                //Offset the COL root so it's clean when importing with 3d software
                Vector3 pos = new Vector3(-100, 0, 0);
                Matrix4 transform = Matrix4.CreateTranslation(pos);

                XElement rootCol = AddNode(visualScene, "ROOT_COL", transform);
                foreach (HWCollisionMesh collisionMesh in HWCollisionMesh.CollisionMeshes)
                    AddNode(rootCol, collisionMesh.FormattedName, collisionMesh.Parent.WorldMatrix, new HWMesh[] { collisionMesh });
            }
            #endregion

            #endregion

            #region scene
            XElement scene = new XElement(ns + "scene");
            collada.Add(scene);

            XElement visualSceneInstance = new XElement(ns + "instance_visual_scene");
            visualSceneInstance.SetAttributeValue("url", "#scene");
            scene.Add(visualSceneInstance);
            #endregion

            doc.Save(path);
        }

        private static XElement AddNode(XElement parentElement, string name, Matrix4 transform)
        {
            return AddNode(parentElement, name, transform, new HWMesh[0]);
        }
        private static XElement AddNode(XElement parentElement, string name, Matrix4 transform, HWMesh[] meshes)
        {
            XElement nodeElement = new XElement(ns + "node");
            nodeElement.SetAttributeValue("name", name);
            nodeElement.SetAttributeValue("id", name);
            nodeElement.SetAttributeValue("sid", name);

            Vector3 pos = transform.ExtractTranslation();

            Vector3 rot = Vector3.Zero;
            float angle = 0;
            transform.ExtractRotation().ToAxisAngle(out rot, out angle);
            rot *= angle;
            float x = MathHelper.RadiansToDegrees(rot.X);
            float y = MathHelper.RadiansToDegrees(rot.Y);
            float z = MathHelper.RadiansToDegrees(rot.Z);
            rot = new Vector3(x, y, z);

            XElement translate = new XElement(ns + "translate");
            translate.SetAttributeValue("sid", "translate");
            translate.SetValue(pos.X.ToString(CultureInfo.InvariantCulture) + " " + pos.Y.ToString(CultureInfo.InvariantCulture) + " " + pos.Z.ToString(CultureInfo.InvariantCulture));
            nodeElement.Add(translate);

            XElement rotate = new XElement(ns + "rotate");
            rotate.SetAttributeValue("sid", "rotateZ");
            rotate.SetValue("0 0 1 " + rot.Z.ToString(CultureInfo.InvariantCulture));
            nodeElement.Add(rotate);

            rotate = new XElement(ns + "rotate");
            rotate.SetAttributeValue("sid", "rotateY");
            rotate.SetValue("0 1 0 " + rot.Y.ToString(CultureInfo.InvariantCulture));
            nodeElement.Add(rotate);

            rotate = new XElement(ns + "rotate");
            rotate.SetAttributeValue("sid", "rotateX");
            rotate.SetValue("1 0 0 " + rot.X.ToString(CultureInfo.InvariantCulture));
            nodeElement.Add(rotate);

            //TODO: Export scale?

            Dictionary<string, XElement> addedTechniques = new Dictionary<string, XElement>();
            foreach (HWMesh mesh in meshes)
            {
                XElement geometryInstance = null;

                if (!addedTechniques.Keys.Contains(mesh.FormattedName))
                {
                    geometryInstance = new XElement(ns + "instance_geometry");
                    geometryInstance.SetAttributeValue("url", "#" + mesh.FormattedName + "-lib");
                    nodeElement.Add(geometryInstance);
                }

                if (mesh.Material.Name.Length != 0)
                {
                    XElement technique = null;

                    if (!addedTechniques.Keys.Contains(mesh.FormattedName))
                    {
                        XElement bindMaterial = new XElement(ns + "bind_material");
                        geometryInstance.Add(bindMaterial);

                        technique = new XElement(ns + "technique_common");
                        bindMaterial.Add(technique);

                        addedTechniques.Add(mesh.FormattedName, technique);
                    }
                    else
                        technique = addedTechniques[mesh.FormattedName];

                    XElement materialInstance = new XElement(ns + "instance_material");
                    materialInstance.SetAttributeValue("symbol", mesh.Material.FormattedName);
                    materialInstance.SetAttributeValue("target", "#" + mesh.Material.FormattedName);
                    technique.Add(materialInstance);
                }
            }

            parentElement.Add(nodeElement);

            return nodeElement;
        }

        private static void AddJointRecursive(XElement parentElement, HWJoint joint)
        {
            XElement newElement = AddNode(parentElement, joint.FormattedName, joint.RelativeWorldMatrix);
            jointElements.Add(joint, newElement);

            foreach (HWJoint childJoint in joint.Children)
                AddJointRecursive(newElement, childJoint);
        }

        private class AddedMesh
        {
            public static List<AddedMesh> AddedMeshes = new List<AddedMesh>();

            public string Name;
            public HWMesh Mesh;
            public XElement MeshElement;
            public XElement PositionArray;
            public XElement PositionAccessor;
            public XElement NormalArray;
            public XElement NormalAccessor;
            public XElement UV0Array;
            public XElement UV0Accessor;
            public XElement UV1Array;
            public XElement UV1Accessor;

            public int IndexOffset;

            public AddedMesh(string name, HWMesh mesh, XElement meshElement, XElement positionArray, XElement positionAccessor, XElement normalArray, XElement normalAccessor, XElement uv0Array, XElement uv0Accessor, XElement uv1Array, XElement uv1Accessor)
            {
                Name = name;
                Mesh = mesh;
                MeshElement = meshElement;
                PositionArray = positionArray;
                PositionAccessor = positionAccessor;
                NormalArray = normalArray;
                NormalAccessor = normalAccessor;
                UV0Array = uv0Array;
                UV0Accessor = uv0Accessor;
                UV1Array = uv1Array;
                UV1Accessor = uv1Accessor;

                IndexOffset = Mesh.VertexCount;
                AddedMeshes.Add(this);
            }

            public static AddedMesh GetByName(string name)
            {
                foreach(AddedMesh mesh in AddedMeshes)
                {
                    if (mesh.Name == name)
                        return mesh;
                }

                return null;
            }
        }
    }
}
