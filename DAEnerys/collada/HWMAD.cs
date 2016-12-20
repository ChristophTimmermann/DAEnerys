using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace DAEnerys
{
    /// <summary>
    /// Homeworld Model Animation Definition
    /// </summary>
    public static class HWMAD
    {
        private struct Keyframe : IComparable<Keyframe>, IFFWriteable, IFFReadable
        {
            public float Time;
            public float Value;
            public Vector2 InTangent;
            public Vector2 OutTangent;

            public int CompareTo(Keyframe other)
            {
                return Time.CompareTo(other.Time);
            }

            public void Write(IFFWriter iff)
            {
                iff.Write((double)Time);
                iff.Write((double)Value);
                iff.Write(InTangent.X);
                iff.Write(InTangent.Y);
                iff.Write(OutTangent.X);
                iff.Write(OutTangent.Y);
            }

            public override string ToString()
            {
                return "{ " + Time + " : " + Value + " }";
            }

            public void Read(IFFReader iff)
            {
                Time = (float)iff.ReadDouble();
                Value = (float)iff.ReadDouble();
                InTangent.X = iff.ReadSingle();
                InTangent.Y = iff.ReadSingle();
                OutTangent.X = iff.ReadSingle();
                OutTangent.Y = iff.ReadSingle();
            }
        }

        private class KeyframeList : List<Keyframe>
        {
            public new Keyframe this[int index]
            {
                get { return base[index]; }
                set
                {
                    base[index] = value;
                    Sort();
                }
            }

            public new void Add(Keyframe item)
            {
                base.Add(item);
                Sort();
            }

            public new void Insert(int index, Keyframe item)
            {
                Add(item);
            }
        }

        private enum InfinityType
        {
            Constant,
            Linear,
            Cycle,
            CycleWithOffset,
            Oscillate
        }

        private enum AnimationChannel
        {
            TranslateX,
            TranslateY,
            TranslateZ,
            RotateX,
            RotateY,
            RotateZ,
            ScaleX,
            ScaleY,
            ScaleZ
        }

        private class AnimationCurve : IFFReadable
        {
            private string name;
            private AnimationChannel channel;
            public string Name
            {
                get { return name; }
                set
                {
                    if (value.EndsWith("_translateX")) channel = AnimationChannel.TranslateX;
                    else if (value.EndsWith("_translateY")) channel = AnimationChannel.TranslateY;
                    else if (value.EndsWith("_translateZ")) channel = AnimationChannel.TranslateZ;
                    else if (value.EndsWith("_rotateX")) channel = AnimationChannel.RotateX;
                    else if (value.EndsWith("_rotateY")) channel = AnimationChannel.RotateY;
                    else if (value.EndsWith("_rotateZ")) channel = AnimationChannel.RotateZ;
                    else if (value.EndsWith("_scaleX")) channel = AnimationChannel.ScaleX;
                    else if (value.EndsWith("_scaleY")) channel = AnimationChannel.ScaleY;
                    else if (value.EndsWith("_scaleZ")) channel = AnimationChannel.ScaleZ;
                    else throw new ArgumentException();
                    name = value;
                }
            }
            public AnimationChannel Channel => channel;
            public KeyframeList Keyframes = new KeyframeList();
            public InfinityType PreInfinity = InfinityType.Constant;
            public InfinityType PostInfinity = InfinityType.Constant;

            public int KeyframeCount => Keyframes.Count;
            public bool HasKeyframes => Keyframes.Count > 0;

            public AnimationCurve(string name, HWAnimationAxis axis)
            {
                Name = name;

                if (axis.Times.Count != axis.Values.Count)
                {
                    new Problem(ProblemTypes.ERROR, "keyframe counts do not match for animation curve " + name);
                    return;
                }

                if (axis.Times.Count == 0) return;

                int lastIndex = axis.Times.Count - 1;

                for (int i = 0; i <= lastIndex; ++i)
                {
                    Keyframe kf = new Keyframe();
                    kf.Time = axis.Times[i];
                    kf.Value = axis.Values[i];
                    if (Channel == AnimationChannel.RotateX ||
                        Channel == AnimationChannel.RotateY ||
                        Channel == AnimationChannel.RotateZ)
                        kf.Value = MathHelper.DegreesToRadians(kf.Value);
                    Keyframes.Add(kf);
                }

                //for (int i = Keyframes.Count - 2; i >= 0; --i)
                //{
                //    Keyframe kf0 = Keyframes[i];
                //    Keyframe kf1 = Keyframes[i + 1];
                //    if (kf1.Value == kf0.Value)
                //        Keyframes.RemoveAt(i + 1);
                //}

                Keyframe first = Keyframes[0];
                Keyframe last = Keyframes[Keyframes.Count - 1];
                first.InTangent = Vector2.UnitX;
                last.OutTangent = Vector2.UnitX;

                for (int i = 0; i < Keyframes.Count - 1; ++i)
                {
                    Keyframe kf0 = Keyframes[i];
                    Keyframe kf1 = Keyframes[i + 1];
                    Vector2 v = new Vector2(
                        kf1.Time - kf0.Time,
                        kf1.Value - kf0.Value
                    ).Normalized();
                    kf0.OutTangent = v;
                    kf1.InTangent = v;
                }

            }

            public AnimationCurve()
            {
            }

            public override bool Equals(object x)
            {
                return (x is AnimationCurve) && ((AnimationCurve)x).name.Equals(name);
            }

            public override int GetHashCode()
            {
                return name.GetHashCode();
            }

            public override string ToString()
            {
                return name;
            }

            public void Read(IFFReader iff)
            {
                int KeyframeCount = iff.ReadInt32();
                for (int i = 0; i < KeyframeCount; ++i)
                {
                    Keyframe kf = new Keyframe();
                    kf.Read(iff);
                    Keyframes.Add(kf);
                }

                PreInfinity = (InfinityType)iff.ReadInt32();
                PostInfinity = (InfinityType)iff.ReadInt32();
            }
        }

        private class AnimatedJoint : IFFReadable
        {
            public string Name;

            public AnimationCurve TranslateX;
            public AnimationCurve TranslateY;
            public AnimationCurve TranslateZ;
            public AnimationCurve RotateX;
            public AnimationCurve RotateY;
            public AnimationCurve RotateZ;
            public AnimationCurve ScaleX;
            public AnimationCurve ScaleY;
            public AnimationCurve ScaleZ;

            public int[] Indices { get; set; }

            public AnimatedJoint(HWJoint joint)
            {
                Name = joint.Name;
                TranslateX = new AnimationCurve(Name + "_translateX", joint.PositionChannel.Axes[0]);
                TranslateY = new AnimationCurve(Name + "_translateY", joint.PositionChannel.Axes[1]);
                TranslateZ = new AnimationCurve(Name + "_translateZ", joint.PositionChannel.Axes[2]);
                RotateX = new AnimationCurve(Name + "_rotateX", joint.RotationChannel.Axes[0]);
                RotateY = new AnimationCurve(Name + "_rotateY", joint.RotationChannel.Axes[1]);
                RotateZ = new AnimationCurve(Name + "_rotateZ", joint.RotationChannel.Axes[2]);
                ScaleX = new AnimationCurve(Name + "_scaleX", joint.ScalingChannel.Axes[0]);
                ScaleY = new AnimationCurve(Name + "_scaleY", joint.ScalingChannel.Axes[1]);
                ScaleZ = new AnimationCurve(Name + "_scaleZ", joint.ScalingChannel.Axes[2]);
            }

            public AnimatedJoint()
            {
            }

            public int ChannelCount
            {
                get
                {
                    int count = 0;
                    if (TranslateX.Keyframes.Count > 0) ++count;
                    if (TranslateY.Keyframes.Count > 0) ++count;
                    if (TranslateZ.Keyframes.Count > 0) ++count;
                    if (RotateX.Keyframes.Count > 0) ++count;
                    if (RotateY.Keyframes.Count > 0) ++count;
                    if (RotateZ.Keyframes.Count > 0) ++count;
                    if (ScaleX.Keyframes.Count > 0) ++count;
                    if (ScaleY.Keyframes.Count > 0) ++count;
                    if (ScaleZ.Keyframes.Count > 0) ++count;
                    return count;
                }
            }

            public int KeyframeCount
            {
                get
                {
                    int count = 0;
                    count += TranslateX.Keyframes.Count;
                    count += TranslateY.Keyframes.Count;
                    count += TranslateZ.Keyframes.Count;
                    count += RotateX.Keyframes.Count;
                    count += RotateY.Keyframes.Count;
                    count += RotateZ.Keyframes.Count;
                    count += ScaleX.Keyframes.Count;
                    count += ScaleY.Keyframes.Count;
                    count += ScaleZ.Keyframes.Count;
                    return count;
                }
            }

            public void Read(IFFReader iff)
            {
                int IndexCount = iff.ReadInt32();
                Indices = new int[IndexCount];
                for (int i = 0; i < IndexCount; ++i)
                    Indices[i] = iff.ReadInt32();
            }
        }

        private class Animation : IFFReadable
        {
            public string Name;
            public float StartTime;
            public float EndTime;
            public float LoopStartTime;
            public float LoopEndTime;
            public List<AnimatedJoint> Joints = new List<AnimatedJoint>();

            public Animation(HWAnimation anim)
            {
                Name = anim.Name;
                StartTime = anim.StartTime;
                EndTime = anim.EndTime;
                LoopStartTime = anim.LoopStartTime;
                LoopEndTime = anim.LoopEndTime;
            }

            public Animation()
            {
            }

            public void Read(IFFReader iff)
            {
                StartTime = iff.ReadSingle();
                EndTime = iff.ReadSingle();
                LoopStartTime = iff.ReadSingle();
                LoopEndTime = iff.ReadSingle();

                int JointCount = iff.ReadInt32();
                for (int i = 0; i < JointCount; ++i)
                    Joints.Add(new AnimatedJoint());
            }
        }

        private class AnimationCurveComparer : IEqualityComparer<AnimationCurve>
        {
            public bool Equals(AnimationCurve x, AnimationCurve y)
            {
                return x.Name.Equals(y.Name);
            }

            public int GetHashCode(AnimationCurve x)
            {
                return x.Name.GetHashCode();
            }
        }

        public static readonly string MAD_Name = "Homeworld2 MAD File";

        private static string Names = "";

        private static int FPS;

        private static List<Animation> Animations = new List<Animation>();
        private static int AnimationCount => Animations.Count;

        private static List<AnimatedJoint> Joints = new List<AnimatedJoint>();
        private static int JointCount => Joints.Count;

        private static List<AnimationCurve> Curves = new List<AnimationCurve>();
        private static int CurveCount => Curves.Count;

        private static int ChannelCount = 0;

        private static void Clear()
        {
            Names = "";
            Animations.Clear();
            Joints.Clear();
            Curves.Clear();
            ChannelCount = 0;
        }

        private static void PrepareForExport()
        {
            Clear();

            HashSet<string> names = new HashSet<string>();
            HashSet<AnimationCurve> curves = new HashSet<AnimationCurve>(new AnimationCurveComparer());

            foreach (HWAnimation anim in HWAnimation.Animations)
            {
                Animation a = new Animation(anim);
                Animations.Add(a);
                names.Add(a.Name);

                foreach (HWJoint joint in anim.AnimatedJoints)
                {
                    AnimatedJoint j = new AnimatedJoint(joint);
                    if (j.ChannelCount > 0)
                    {
                        a.Joints.Add(j);
                        Joints.Add(j);
                        names.Add(j.Name);

                        ChannelCount += j.ChannelCount;
                        if (j.TranslateX.HasKeyframes) curves.Add(j.TranslateX);
                        if (j.TranslateY.HasKeyframes) curves.Add(j.TranslateY);
                        if (j.TranslateZ.HasKeyframes) curves.Add(j.TranslateZ);
                        if (j.RotateX.HasKeyframes) curves.Add(j.RotateX);
                        if (j.RotateY.HasKeyframes) curves.Add(j.RotateY);
                        if (j.RotateZ.HasKeyframes) curves.Add(j.RotateZ);
                        if (j.ScaleX.HasKeyframes) curves.Add(j.ScaleX);
                        if (j.ScaleY.HasKeyframes) curves.Add(j.ScaleY);
                        if (j.ScaleZ.HasKeyframes) curves.Add(j.ScaleZ);

                    }
                }
            }
            Curves = curves.ToList();

            foreach (AnimationCurve curve in Curves)
                names.Add(curve.Name);

            Names = string.Join("\0", names) + '\0';
        }

        public static void Export(string filename)
        {
            PrepareForExport();

            IFFWriter iff = new IFFWriter(filename);

            iff.Push("MAD ", IFFChunkType.Form);
            iff.Push("NAME"); iff.Write(MAD_Name, MAD_Name.Length); iff.Pop();
            iff.Push("VERS"); iff.Write(0x0104); iff.Pop();

            ExportInfo(iff);
            ExportStrings(iff);
            ExportMarkers(iff);
            ExportCurves(iff);

            iff.Pop();

            iff.Close();
            iff.Dispose();
        }

        private static void ExportInfo(IFFWriter iff)
        {
            int joints = 0, channels = 0;

            foreach (Animation anim in Animations)
            {
                joints += anim.Joints.Count;
                foreach (AnimatedJoint joint in anim.Joints)
                    channels += joint.ChannelCount;
            }

            iff.Push("INFO");
            iff.Write(30);
            iff.Write(Animations.Count);
            iff.Write(Curves.Count);
            iff.Write(joints);
            iff.Write(channels);
            iff.Pop();
        }

        private static void ExportStrings(IFFWriter iff)
        {
            iff.Push("STRI");
            iff.Write(Names, Names.Length);
            iff.Pop();
        }

        private static void ExportMarkers(IFFWriter iff)
        {
            iff.Push("MARK");
            foreach (Animation anim in Animations)
            {
                iff.Write(Names.IndexOf(anim.Name));
                iff.Write(anim.StartTime);
                iff.Write(anim.EndTime);
                iff.Write(anim.LoopStartTime);
                iff.Write(anim.LoopEndTime);
                iff.Write(anim.Joints.Count);

                foreach (AnimatedJoint joint in anim.Joints)
                {
                    iff.Write(Names.IndexOf(joint.Name));
                    iff.Write(joint.ChannelCount);

                    if (joint.TranslateX.HasKeyframes) iff.Write(Curves.IndexOf(joint.TranslateX));
                    if (joint.TranslateY.HasKeyframes) iff.Write(Curves.IndexOf(joint.TranslateY));
                    if (joint.TranslateZ.HasKeyframes) iff.Write(Curves.IndexOf(joint.TranslateZ));
                    if (joint.RotateX.HasKeyframes) iff.Write(Curves.IndexOf(joint.RotateX));
                    if (joint.RotateY.HasKeyframes) iff.Write(Curves.IndexOf(joint.RotateY));
                    if (joint.RotateZ.HasKeyframes) iff.Write(Curves.IndexOf(joint.RotateZ));
                    if (joint.ScaleX.HasKeyframes) iff.Write(Curves.IndexOf(joint.ScaleX));
                    if (joint.ScaleY.HasKeyframes) iff.Write(Curves.IndexOf(joint.ScaleY));
                    if (joint.ScaleZ.HasKeyframes) iff.Write(Curves.IndexOf(joint.ScaleZ));
                }
            }
            iff.Pop();
        }

        private static void ExportCurves(IFFWriter iff)
        {
            iff.Push("CURV");
            foreach (AnimationCurve curve in Curves)
            {
                //curve.CalculateTangents();

                iff.Write(Names.IndexOf(curve.Name));

                iff.Write(curve.KeyframeCount);
                foreach (Keyframe kf in curve.Keyframes)
                    iff.Write(kf);

                iff.Write((int)curve.PreInfinity);
                iff.Write((int)curve.PostInfinity);
            }
            iff.Pop();
        }

        public static void Import(string filename)
        {
            Clear();

            IFFReader iff = new IFFReader(filename);
            iff.AddHandler("MAD ", IFFChunkType.Form, ReadMAD);
            iff.Parse();

            PrepareForImport();

            Program.main.ClearAnimations();
            HWAnimation.Animations.Clear();

            foreach (Animation anim in Animations)
            {
                HWAnimation hwAnim = new HWAnimation(anim.Name,
                    anim.StartTime, (int)(anim.StartTime * FPS),
                    anim.EndTime, (int)(anim.EndTime * FPS),
                    anim.LoopStartTime, (int)(anim.LoopStartTime * FPS),
                    anim.LoopEndTime, (int)(anim.LoopEndTime * FPS),
                    AnimationType.TIME
                );

                foreach (AnimatedJoint joint in anim.Joints)
                {
                    HWJoint animJoint = HWJoint.GetByName(joint.Name);
                    if (animJoint == null)
                        new Problem(ProblemTypes.WARNING, "The joint \"" + joint.Name + "\" referenced in the imported MAD does not exist in the DAE.");
                    else
                        hwAnim.AnimatedJoints.Add(animJoint);
                }
            }

            foreach (AnimatedJoint joint in Joints)
            {
                HWJoint animJoint = HWJoint.GetByName(joint.Name);
                if (animJoint == null)
                    new Problem(ProblemTypes.WARNING, "The joint \"" + joint.Name + "\" referenced in the imported MAD does not exist in the DAE.");
                else
                {
                    HWAnimationChannel translate = new HWAnimationChannel();
                    HWAnimationChannel rotate = new HWAnimationChannel();
                    HWAnimationChannel scale = new HWAnimationChannel();

                    if (joint.TranslateX != null)
                        foreach (Keyframe k in joint.TranslateX.Keyframes)
                        {
                            translate.Axes[0].Times.Add(k.Time);
                            translate.Axes[0].Values.Add(k.Value);
                            translate.Axes[0].InTangents.Add(k.InTangent);
                            translate.Axes[0].OutTangents.Add(k.OutTangent);
                            translate.Axes[0].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.TranslateY != null)
                        foreach (Keyframe k in joint.TranslateY.Keyframes)
                        {
                            translate.Axes[1].Times.Add(k.Time);
                            translate.Axes[1].Values.Add(k.Value);
                            translate.Axes[1].InTangents.Add(k.InTangent);
                            translate.Axes[1].OutTangents.Add(k.OutTangent);
                            translate.Axes[1].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.TranslateZ != null)
                        foreach (Keyframe k in joint.TranslateZ.Keyframes)
                        {
                            translate.Axes[2].Times.Add(k.Time);
                            translate.Axes[2].Values.Add(k.Value);
                            translate.Axes[2].InTangents.Add(k.InTangent);
                            translate.Axes[2].OutTangents.Add(k.OutTangent);
                            translate.Axes[2].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.RotateX != null)
                        foreach (Keyframe k in joint.RotateX.Keyframes)
                        {
                            rotate.Axes[0].Times.Add(k.Time);
                            rotate.Axes[0].Values.Add(MathHelper.RadiansToDegrees(k.Value));
                            rotate.Axes[0].InTangents.Add(k.InTangent);
                            rotate.Axes[0].OutTangents.Add(k.OutTangent);
                            rotate.Axes[0].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.RotateY != null)
                        foreach (Keyframe k in joint.RotateY.Keyframes)
                        {
                            rotate.Axes[1].Times.Add(k.Time);
                            rotate.Axes[1].Values.Add(MathHelper.RadiansToDegrees(k.Value));
                            rotate.Axes[1].InTangents.Add(k.InTangent);
                            rotate.Axes[1].OutTangents.Add(k.OutTangent);
                            rotate.Axes[1].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.RotateZ != null)
                        foreach (Keyframe k in joint.RotateZ.Keyframes)
                        {
                            rotate.Axes[2].Times.Add(k.Time);
                            rotate.Axes[2].Values.Add(MathHelper.RadiansToDegrees(k.Value));
                            rotate.Axes[2].InTangents.Add(k.InTangent);
                            rotate.Axes[2].OutTangents.Add(k.OutTangent);
                            rotate.Axes[2].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.ScaleX != null)
                        foreach (Keyframe k in joint.ScaleX.Keyframes)
                        {
                            scale.Axes[0].Times.Add(k.Time);
                            scale.Axes[0].Values.Add(k.Value);
                            scale.Axes[0].InTangents.Add(k.InTangent);
                            scale.Axes[0].OutTangents.Add(k.OutTangent);
                            scale.Axes[0].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.ScaleY != null)
                        foreach (Keyframe k in joint.ScaleY.Keyframes)
                        {
                            scale.Axes[1].Times.Add(k.Time);
                            scale.Axes[1].Values.Add(k.Value);
                            scale.Axes[1].InTangents.Add(k.InTangent);
                            scale.Axes[1].OutTangents.Add(k.OutTangent);
                            scale.Axes[1].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }
                    if (joint.ScaleZ != null)
                        foreach (Keyframe k in joint.ScaleZ.Keyframes)
                        {
                            scale.Axes[2].Times.Add(k.Time);
                            scale.Axes[2].Values.Add(k.Value);
                            scale.Axes[2].InTangents.Add(k.InTangent);
                            scale.Axes[2].OutTangents.Add(k.OutTangent);
                            scale.Axes[2].KeyInterpolationTypes.Add(AnimationInterpolation.LINEAR);
                        }

                    animJoint.PositionChannel = translate;
                    animJoint.RotationChannel = rotate;
                    animJoint.ScalingChannel = scale;

                }
            }
        }

        private static void ReadMAD(IFFReader iff, IFFChunk chunk)
        {
            iff.AddHandler("NAME", IFFChunkType.Default, ReadMADName);
            iff.AddHandler("VERS", IFFChunkType.Default, ReadMADVersion);
            iff.AddHandler("INFO", IFFChunkType.Default, ReadInfo);
            iff.AddHandler("STRI", IFFChunkType.Default, ReadNames);
            iff.AddHandler("MARK", IFFChunkType.Default, ReadMarkers);
            iff.AddHandler("CURV", IFFChunkType.Default, ReadCurves);
            iff.Parse();
        }

        private static void ReadMADName(IFFReader iff, IFFChunk chunk)
        {
            Trace.Assert(string.Compare(MAD_Name, iff.ReadString(chunk.Size)) == 0, "NAME chunk ID test failed.");
        }

        private static void ReadMADVersion(IFFReader iff, IFFChunk chunk)
        {
            Trace.Assert(iff.ReadInt32() == 0x0104, "VERS chunk ID test failed.");
        }

        private static void ReadInfo(IFFReader iff, IFFChunk chunk)
        {
            FPS = iff.ReadInt32();
            int AnimationCount = iff.ReadInt32();
            int CurveCount = iff.ReadInt32();
            int JointCount = iff.ReadInt32();
            int ChannelCount = iff.ReadInt32();

            for (int i = 0; i < AnimationCount; ++i)
                Animations.Add(new Animation());

            for (int i = 0; i < CurveCount; ++i)
                Curves.Add(new AnimationCurve());
        }

        private static void ReadNames(IFFReader iff, IFFChunk chunk)
        {
            Names = iff.ReadString(chunk.Size);
        }

        private static string GetName(int index)
        {
            if (index < 0 || index >= Names.Length)
                throw new ArgumentOutOfRangeException("index");

            int pos = Names.IndexOf('\0', index);
            string name = Names.Substring(index, pos - index);
            return name;
        }

        private static void ReadMarkers(IFFReader iff, IFFChunk chunk)
        {
            foreach (Animation anim in Animations)
            {
                anim.Name = GetName(iff.ReadInt32());
                anim.Read(iff);

                foreach (AnimatedJoint joint in anim.Joints)
                {
                    joint.Name = GetName(iff.ReadInt32());
                    joint.Read(iff);
                    Joints.Add(joint);
                }
            }
        }

        private static void ReadCurves(IFFReader iff, IFFChunk chunk)
        {
            foreach (AnimationCurve curve in Curves)
            {
                curve.Name = GetName(iff.ReadInt32());
                curve.Read(iff);
            }
        }

        private static void PrepareForImport()
        {
            foreach (Animation anim in Animations)
            {
                foreach (AnimatedJoint joint in anim.Joints)
                {
                    foreach (int index in joint.Indices)
                    {
                        AnimationCurve curve = Curves[index];
                        switch (curve.Channel)
                        {
                            case AnimationChannel.TranslateX: joint.TranslateX = curve; break;
                            case AnimationChannel.TranslateY: joint.TranslateY = curve; break;
                            case AnimationChannel.TranslateZ: joint.TranslateZ = curve; break;
                            case AnimationChannel.RotateX: joint.RotateX = curve; break;
                            case AnimationChannel.RotateY: joint.RotateY = curve; break;
                            case AnimationChannel.RotateZ: joint.RotateZ = curve; break;
                            case AnimationChannel.ScaleX: joint.ScaleX = curve; break;
                            case AnimationChannel.ScaleY: joint.ScaleY = curve; break;
                            case AnimationChannel.ScaleZ: joint.ScaleZ = curve; break;
                        }
                    }
                }
            }
        }
    }
}
