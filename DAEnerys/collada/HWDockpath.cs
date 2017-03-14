using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using System;
using Extensions;

namespace DAEnerys
{
    public class HWDockpath : HWElement
    {
        public static List<HWDockpath> Dockpaths = new List<HWDockpath>();

        public static bool PreviewPlaying;
        private static int previewSegment;
        private static float previewBlend;
        private static Vector3 previewVelocity;
        private static float segmentDistance;
        private static Vector3 segmentDirection;
        private static float mainEngineAccelerationBlend;
        private static float mainEngineBrakeBlend;
        private static bool oldAccelerating;
        private static bool accelerating;
        private static float mainEngineSpeed;
        private static float segmentTime;
        private static float timePassed;
        private static HWDockpath previewDockpath;
        private static EditorDockpathPreviewElement previewElement;
        private static EditorDockpathPreviewModel previewMesh;

        private const float thrusterMaxSpeed = 348;
        private const float mainEngineMaxSpeed = 348;
        private const float mainEngineAccelTime = 2.3f;

        public static void InitPreview()
        {
            previewElement = new EditorDockpathPreviewElement(HWJoint.Root);
            previewMesh = new EditorDockpathPreviewModel(null);
        }
        public static void StartPreview(HWDockpath dockpath)
        {
            previewDockpath = dockpath;
            previewSegment = -1;
            previewBlend = 0;
            previewVelocity = Vector3.Zero;
            segmentDistance = 0;
            timePassed = 0;
            previewElement.Parent = dockpath;
            previewMesh.Parent = dockpath;
            accelerating = false;
            oldAccelerating = false;
            previewMesh.Visible = true;
            PreviewPlaying = true;
        }

        public static void UpdatePreview()
        {
            if(previewSegment == -1)
            {
                accelerating = true;
                previewSegment = 0;
                segmentDistance = (previewDockpath.Segments[previewSegment].LocalPosition - previewDockpath.Segments[previewSegment + 1].LocalPosition).Length;
                float speed = Math.Max(previewDockpath.Segments[previewSegment].Speed, 20);
                previewMesh.LocalPosition = previewDockpath.Segments[previewSegment].LocalPosition;
                segmentTime = segmentDistance / speed;
            }

            if(previewBlend >= 1)
            {
                if (previewSegment >= previewDockpath.Segments.Count - 2)
                {
                    previewSegment = 0;
                    timePassed = 0;
                    segmentDistance = (previewDockpath.Segments[previewSegment].LocalPosition - previewDockpath.Segments[previewSegment + 1].LocalPosition).Length;
                    float speed = Math.Max(previewDockpath.Segments[previewSegment].Speed, 20);
                    segmentTime = segmentDistance / speed;
                }
                else
                {
                    previewSegment++;
                    timePassed = 0;
                    segmentDistance = (previewDockpath.Segments[previewSegment].LocalPosition - previewDockpath.Segments[previewSegment + 1].LocalPosition).Length;
                    float speed = Math.Max(previewDockpath.Segments[previewSegment].Speed, 20);
                    segmentTime = segmentDistance / speed;
                }
            }

            if(oldAccelerating != accelerating)
            {
                mainEngineAccelerationBlend = 0;

                if (!accelerating)
                    mainEngineAccelerationBlend = mainEngineSpeed / mainEngineMaxSpeed;
                else
                    mainEngineBrakeBlend = 1 - mainEngineSpeed / mainEngineMaxSpeed;
            }

            if(accelerating)
            {
                mainEngineAccelerationBlend += (float)Program.ElapsedSeconds / mainEngineAccelTime;
                mainEngineAccelerationBlend = Math.Min(mainEngineAccelerationBlend, 1);
                mainEngineSpeed = Utilities.Lerp(0, mainEngineMaxSpeed, mainEngineAccelerationBlend);
            }

            segmentDirection = (previewMesh.LocalPosition - previewDockpath.Segments[previewSegment + 1].LocalPosition).Normalized();

            previewBlend = timePassed / segmentTime;
            previewVelocity = Vector3.Zero;
            previewVelocity += previewMesh.LocalWorldMatrix.ExtractRotation() * Vector3.UnitZ * mainEngineSpeed;
            Vector3 rot = Vector3.Zero;

            previewMesh.LocalPosition += previewVelocity * (float)Program.ElapsedSeconds;
            previewMesh.LocalRotation = rot;

            timePassed += (float)Program.ElapsedSeconds;

            oldAccelerating = accelerating;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        public override string FormattedName
        {
            get
            {
                string fams = "";
                if (Families.Count > 0)
                {
                    fams = "_Fam[";
                    for (int i = 0; i < Families.Count; i++)
                    {
                        fams += Families[i];
                        if (i < Families.Count - 1)
                            fams += ", ";
                    }
                    fams += "]";
                }

                string flags = "";
                if (Flags.Count > 0)
                {
                    flags = "_Flags[";
                    for (int i = 0; i < Flags.Count; i++)
                    {
                        flags += Flags[i];
                        if (i < Flags.Count - 1)
                            flags += " ";
                    }
                    flags += "]";
                }

                string links = "";
                if (Links.Count > 0)
                {
                    links = "_Link[";
                    for (int i = 0; i < Links.Count; i++)
                    {
                        if (string.IsNullOrEmpty(Links[i]))
                            continue;

                        links += Links[i];
                        if (i < Links.Count - 1)
                            links += ", ";
                    }
                    links += "]";
                }

                string animIndex = "";
                if (AnimationIndex > 0)
                {
                    animIndex = "_MAD[" + AnimationIndex + "]";
                }

                return "DOCK[" + Name + "]" + fams + flags + links + animIndex;
            }
        }

        public List<string> Families = new List<string>();
        public List<string> Links = new List<string>();
        public List<DockpathFlag> Flags = new List<DockpathFlag>();
        public int AnimationIndex;
        public List<HWDockSegment> Segments = new List<HWDockSegment>();
        public List<EditorLine> Lines = new List<EditorLine>();

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

                foreach(HWDockSegment segment in Segments)
                {
                    segment.EditorDockSegment.Visible = value;
                }
            }
        }

        public HWDockpath(string name, string[] families, string[] links, DockpathFlag[] flags, int animationIndex) : base(name, HWJoint.Root)
        {
            Name = name;
            Families.AddRange(families);
            Links.AddRange(links);
            Flags.AddRange(flags);
            AnimationIndex = animationIndex;

            Dockpaths.Add(this);
            Program.main.AddDockpath(this);
        }

        public static HWDockpath GetByName(string name)
        {
            foreach (HWDockpath path in Dockpaths)
                if (path.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return path;

            return null;
        }

        public override void Destroy()
        {
            Dockpaths.Remove(this);
            Program.main.RemoveDockpath(this);
            foreach (EditorLine line in Lines)
                line.Destroy();
            Lines.Clear();

            HWDockSegment[] segments = Segments.ToArray();
            for (int i = 0; i < segments.Length; i++)
                segments[i].Destroy();

            base.Destroy();
        }

        public void SetupVisualization()
        {
            foreach (EditorLine line in Lines)
                line.Destroy();
            Lines.Clear();

            for(int i = 0; i < Segments.Count - 1; i++) //1 line less than segments
            {
                EditorLine line = new EditorLine(Segments[i].GlobalPosition, Segments[i + 1].GlobalPosition, new Vector3(1, 0, 0), new Vector3(1, 0, 0), null);
                Lines.Add(line);
            }
        }
    }

    public enum DockpathFlag
    {
        Exit = 1,
        Latch = 2,
        Anim = 3,
        Ajar = 4,
    }
}
