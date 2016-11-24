using Assimp;
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
        private static Dictionary<HWNode, XElement> LODRootElements = new Dictionary<HWNode, XElement>();

        public static void ExportToFile(string path)
        {
            AddedMesh.AddedMeshes.Clear();
            LODRootElements.Clear();

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
            foreach (HWImage image in HWScene.Images)
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
            foreach (HWMaterial material in HWScene.Materials)
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

            foreach (HWMaterial material in HWScene.Materials)
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

            foreach (HWMesh mesh in HWScene.Meshes)
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
                    foreach (Vertex vertex in mesh.Vertices)
                        positions.AppendLine(vertex.Position.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Position.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Position.Z.ToString(CultureInfo.InvariantCulture));
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
                    foreach (Vertex vertex in mesh.Vertices)
                        normals.AppendLine(vertex.Normal.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Normal.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Normal.Z.ToString(CultureInfo.InvariantCulture));
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
                    if (mesh.TextureCoordinateChannelCount > 0)
                    {
                        source = new XElement(ns + "source");
                        source.SetAttributeValue("id", mesh.FormattedName + "-UV0");
                        meshElement.Add(source);

                        uv0Array = new XElement(ns + "float_array");
                        uv0Array.SetAttributeValue("id", mesh.FormattedName + "-UV0-array");
                        uv0Array.SetAttributeValue("count", mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder("\n");
                        foreach (Vertex vertex in mesh.Vertices)
                            uvs.AppendLine(vertex.UV0.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.UV0.Y.ToString(CultureInfo.InvariantCulture));
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
                    if (mesh.TextureCoordinateChannelCount > 1)
                    {
                        source = new XElement(ns + "source");
                        source.SetAttributeValue("id", mesh.FormattedName + "-UV1");
                        meshElement.Add(source);

                        uv1Array = new XElement(ns + "float_array");
                        uv1Array.SetAttributeValue("id", mesh.FormattedName + "-UV1-array");
                        uv1Array.SetAttributeValue("count", mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder("\n");
                        foreach (Vertex vertex in mesh.Vertices)
                            uvs.AppendLine(vertex.UV1.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.UV1.Y.ToString(CultureInfo.InvariantCulture));
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
                    foreach (Vertex vertex in mesh.Vertices)
                        positions.AppendLine(vertex.Position.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Position.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Position.Z.ToString(CultureInfo.InvariantCulture));
                    addedMesh.PositionArray.SetValue(positions.ToString());

                    lastCount = int.Parse(addedMesh.PositionAccessor.Attribute("count").Value);
                    addedMesh.PositionAccessor.SetAttributeValue("count", lastCount + mesh.VertexCount);
                    #endregion

                    #region normal source
                    lastCount = int.Parse(addedMesh.NormalArray.Attribute("count").Value);
                    addedMesh.NormalArray.SetAttributeValue("count", lastCount + mesh.VertexCount * 3);
                    StringBuilder normals = new StringBuilder(addedMesh.NormalArray.Value);
                    foreach (Vertex vertex in mesh.Vertices)
                        normals.AppendLine(vertex.Normal.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Normal.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Normal.Z.ToString(CultureInfo.InvariantCulture));
                    addedMesh.NormalArray.SetValue(normals.ToString());

                    lastCount = int.Parse(addedMesh.NormalAccessor.Attribute("count").Value);
                    addedMesh.NormalAccessor.SetAttributeValue("count", lastCount + mesh.VertexCount);
                    #endregion

                    #region uv0 source
                    if (mesh.TextureCoordinateChannelCount > 0)
                    {
                        lastCount = int.Parse(addedMesh.UV0Array.Attribute("count").Value);
                        addedMesh.UV0Array.SetAttributeValue("count", lastCount + mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder(addedMesh.UV0Array.Value);
                        foreach (Vertex vertex in mesh.Vertices)
                            uvs.AppendLine(vertex.UV0.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.UV0.Y.ToString(CultureInfo.InvariantCulture));
                        addedMesh.UV0Array.SetValue(uvs.ToString());

                        lastCount = int.Parse(addedMesh.UV0Accessor.Attribute("count").Value);
                        addedMesh.UV0Accessor.SetAttributeValue("count", lastCount + mesh.VertexCount);
                    }
                    #endregion

                    #region uv1 source
                    if (mesh.TextureCoordinateChannelCount > 1)
                    {
                        lastCount = int.Parse(addedMesh.UV1Array.Attribute("count").Value);
                        addedMesh.UV1Array.SetAttributeValue("count", lastCount + mesh.VertexCount * 2);
                        StringBuilder uvs = new StringBuilder(addedMesh.UV1Array.Value);
                        foreach (Vertex vertex in mesh.Vertices)
                            uvs.AppendLine(vertex.UV1.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.UV1.Y.ToString(CultureInfo.InvariantCulture));
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

                if (mesh.TextureCoordinateChannelCount > 0)
                {
                    input = new XElement(ns + "input");
                    input.SetAttributeValue("semantic", "TEXCOORD");
                    input.SetAttributeValue("offset", "2");
                    input.SetAttributeValue("set", "0");
                    input.SetAttributeValue("source", "#" + mesh.FormattedName + "-UV0");
                    triangles.Add(input);
                    inputCount++;
                }
                if (mesh.TextureCoordinateChannelCount > 1)
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

                foreach (Face face in mesh.Faces)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int c = 0; c < inputCount; c++)
                        {
                            faces.Append(" " + (int)(face.Indices[i] + indexOffset));
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

            for (int i = 0; i < HWNode.RootLODs.Length; i++)
            {
                bool lodExists = false;
                foreach (HWShipMesh shipMesh in HWScene.ShipMeshes)
                    foreach (HWShipMeshLOD lodMesh in shipMesh.Meshes)
                        if (lodMesh.LOD == i)
                            lodExists = true;

                foreach (HWEngineGlow engineGlow in HWScene.EngineGlows)
                    foreach (HWEngineGlowLOD lodMesh in engineGlow.Meshes)
                        if (lodMesh.LOD == i)
                            lodExists = true;

                if (HWNode.RootLODs[i] == null)
                {
                    if(lodExists)
                        HWNode.RootLODs[i] = new HWNode(null, "ROOT_LOD[" + i + "]");
                }

                if(lodExists)
                    LODRootElements.Add(HWNode.RootLODs[i], AddNode(visualScene, HWNode.RootLODs[i]));
            }

            foreach (HWNode rootNode in HWNode.RootLODs)
                if (rootNode != null)
                    AddNodeChildrenRecursive(LODRootElements[rootNode], rootNode);

            if (HWNode.RootINFO == null)
            {
                HWNode.RootINFO = new HWNode(null, "ROOT_INFO");
                new HWNode(HWNode.RootINFO, "Class[MultiMesh]_Version[512]");
                int uvSets = 1;
                foreach (HWShipMesh shipMesh in HWScene.ShipMeshes)
                    foreach (HWShipMeshLOD shipMeshLOD in shipMesh.Meshes)
                        if (shipMeshLOD.TextureCoordinateChannelCount > uvSets)
                            uvSets = shipMeshLOD.TextureCoordinateChannelCount;
                new HWNode(HWNode.RootINFO, "UVSets[" + uvSets + "]");
            }

            AddNodeRecursive(visualScene, HWNode.RootINFO);

            if (HWNode.RootCOL == null)
            {
                if (HWScene.CollisionMeshes.Count > 0)
                {
                    HWNode.RootCOL = new HWNode(null, "ROOT_COL");
                    foreach (HWCollisionMesh collisionMesh in HWScene.CollisionMeshes)
                        collisionMesh.Parent.Parent = HWNode.RootCOL;
                }
            }

            if (HWScene.CollisionMeshes.Count > 0)
                AddNodeRecursive(visualScene, HWNode.RootCOL);

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

        private static XElement AddNode(XElement parentElement, HWNode node)
        {
            XElement nodeElement = new XElement(ns + "node");
            nodeElement.SetAttributeValue("name", node.FormattedName);
            nodeElement.SetAttributeValue("id", node.FormattedName);
            nodeElement.SetAttributeValue("sid", node.FormattedName);

            HWShipMeshLOD lodMesh = null;
            foreach (HWShipMesh shipMesh in HWScene.ShipMeshes)
                foreach (HWShipMeshLOD shipMeshLOD in shipMesh.Meshes)
                    if (shipMeshLOD.LOD > 0)
                        if (node.Meshes.Contains(shipMeshLOD))
                        {
                            lodMesh = shipMeshLOD;
                            break;
                        }

            if (lodMesh == null)
                parentElement.Add(nodeElement);
            else
                LODRootElements[HWNode.RootLODs[lodMesh.LOD]].Add(nodeElement);

            Vector3 pos = node.RelativePosition;
            Vector3 rot = node.RelativeRotation;
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
            foreach (HWMesh mesh in node.Meshes)
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
            return nodeElement;
        }

        private static void AddNodeRecursive(XElement parentElement, HWNode node)
        {
            XElement newNodeElement = AddNode(parentElement, node);

            foreach (HWNode childNode in node.Children)
                AddNodeRecursive(newNodeElement, childNode);
        }

        private static void AddNodeChildrenRecursive(XElement parentElement, HWNode parentNode)
        {
            foreach (HWNode childNode in parentNode.Children)
                AddNodeRecursive(parentElement, childNode);
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
