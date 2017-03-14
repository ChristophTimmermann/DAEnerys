using Extensions;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public class HWAnimation : HWElement
    {
        public static List<HWAnimation> Animations = new List<HWAnimation>();
        public static float AnimationTime;

        public override string FormattedName
        {
            get
            {
                string start = "";
                string end = "";
                string loopStart = "";
                string loopEnd = "";

                if (Type == AnimationType.TIME)
                {
                    start = "_ST[" + StartTime.ToString(CultureInfo.InvariantCulture) + "]";
                    end = "_EN[" + EndTime.ToString(CultureInfo.InvariantCulture) + "]";

                    loopStart = "_LS[" + LoopStartTime.ToString(CultureInfo.InvariantCulture) + "]";
                    loopEnd = "_LE[" + LoopEndTime.ToString(CultureInfo.InvariantCulture) + "]";
                }
                else
                {
                    start = "_STF[" + StartFrame + "]";
                    end = "_ENF[" + EndFrame + "]";

                    loopStart = "_LSF[" + LoopStartFrame + "]";
                    loopEnd = "_LEF[" + LoopEndFrame + "]";
                }

                return "ANIM[" + Name + "]" + start + end + loopStart + loopEnd;
            }
        }

        public float StartTime = 0;
        public int StartFrame = 0;

        public float EndTime = 0;
        public int EndFrame = 0;

        public float LoopStartTime = 0;
        public int LoopStartFrame = 0;

        public float LoopEndTime = 0;
        public int LoopEndFrame = 0;

        public AnimationType Type = AnimationType.TIME;

        public List<HWJoint> AnimatedJoints = new List<HWJoint>();

        public HWAnimation(string name, float startTime, int startFrame, float endTime, int endFrame, float loopStartTime, int loopStartFrame, float loopEndTime, int loopEndFrame, AnimationType type) : base(name, HWJoint.Root)
        {
            StartTime = startTime;
            StartTime = Math.Max(StartTime, 0);

            StartFrame = startFrame;

            EndTime = endTime;
            EndTime = Math.Max(EndTime, 0);

            EndFrame = endFrame;

            LoopStartTime = loopStartTime;
            LoopStartTime = Math.Max(LoopStartTime, 0);

            LoopStartFrame = loopStartFrame;

            LoopEndTime = loopEndTime;
            LoopEndTime = Math.Max(LoopEndTime, 0);

            LoopEndFrame = loopEndFrame;

            Type = type;

            //Calculate time from frames
            if (Type == AnimationType.FRAME)
            {
                StartTime = StartFrame / Importer.Framerate;
                EndTime = EndFrame / Importer.Framerate;

                LoopStartTime = LoopStartFrame / Importer.Framerate;
                LoopEndTime = LoopEndFrame / Importer.Framerate;
            }

            Animations.Add(this);
            Program.main.AddAnimation(this);
        }

        public static void Update()
        {
            AnimationTime = Math.Min(AnimationTime, Program.main.SelectedAnimation.EndTime);

            foreach(HWJoint joint in Program.main.SelectedAnimation.AnimatedJoints)
            {
                Matrix4 rotationMatrix = UpdateRotation(joint);
                Matrix4 translationMatrix = UpdateTranslation(joint);

                Matrix4 newMatrix = rotationMatrix * translationMatrix;
                joint.AnimationMatrix = newMatrix;
                joint.Invalidate();
            }

            AnimationTime += (float)Program.ElapsedSeconds;

            if (AnimationTime > Program.main.SelectedAnimation.EndTime)
            {
                Program.main.AnimationPlaying = false;
                foreach (HWJoint joint in HWJoint.Joints)
                {
                    joint.AnimationMatrix = Matrix4.Identity;
                    joint.Invalidate();
                }
            }

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private static Matrix4 UpdateTranslation(HWJoint joint)
        {
            Vector3 pos = joint.LocalPosition;

            for (int i = 0; i < 3; i++)
            {
                if (joint.PositionChannel.Axes[i].Times.Count == 0)
                    return joint.LocalWorldMatrix.ClearRotation().ClearScale();

                int keyIndex = 0;
                for (int t = 0; t < joint.PositionChannel.Axes[i].Times.Count - 1; t++)
                    if (AnimationTime >= joint.PositionChannel.Axes[i].Times[t] && AnimationTime < joint.PositionChannel.Axes[i].Times[t + 1])
                    {
                        keyIndex = t;
                        break;
                    }

                float blend = 0;
                float frameLength = 0;

                float highestTimeInAnimation = 0;
                for (int t = 0; t < joint.PositionChannel.Axes[i].Times.Count; t++)
                    highestTimeInAnimation = Math.Max(highestTimeInAnimation, joint.PositionChannel.Axes[i].Times[t]);

                if (joint.PositionChannel.Axes[i].Times.Count > 1)
                {
                    if (AnimationTime < highestTimeInAnimation)
                    {
                        frameLength = joint.PositionChannel.Axes[i].Times[keyIndex + 1] - joint.PositionChannel.Axes[i].Times[keyIndex];
                        blend = (AnimationTime - joint.PositionChannel.Axes[i].Times[keyIndex]) / frameLength;
                    }
                    else //When animation is over
                    {
                        keyIndex = joint.PositionChannel.Axes[i].Times.Count - 2;
                        blend = 1;
                    }
                }

                blend = Math.Max(blend, 0);

                if (joint.PositionChannel.Axes[i].Times.Count > 1)
                {

                    if (joint.RotationChannel.Axes[i].Values.Count <= keyIndex)
                        continue;

                    AnimationInterpolation interpolation = AnimationInterpolation.LINEAR;
                    if (joint.PositionChannel.Axes[i].KeyInterpolationTypes.Count > keyIndex)
                        interpolation = joint.PositionChannel.Axes[i].KeyInterpolationTypes[keyIndex];

                    float value = 0;
                    if (interpolation == AnimationInterpolation.LINEAR)
                    {
                        value = Utilities.Lerp(joint.PositionChannel.Axes[i].Values[keyIndex], joint.PositionChannel.Axes[i].Values[keyIndex + 1], blend);
                    }
                    else if (interpolation == AnimationInterpolation.BEZIER)
                    {
                        Vector2 p0 = new Vector2(joint.PositionChannel.Axes[i].Times[keyIndex], joint.PositionChannel.Axes[i].Values[keyIndex]);
                        Vector2 p1 = new Vector2(joint.PositionChannel.Axes[i].Times[keyIndex + 1], joint.PositionChannel.Axes[i].Values[keyIndex + 1]);

                        Vector2 point = Utilities.CalculateBezierPoint(p0, p1, joint.PositionChannel.Axes[i].OutTangents[keyIndex], joint.PositionChannel.Axes[i].InTangents[keyIndex + 1], blend);
                        value = point.Y;
                    }

                    switch (i)
                    {
                        case 0:
                            pos.X = value;
                            break;
                        case 1:
                            pos.Y = value;
                            break;
                        case 2:
                            pos.Z = value;
                            break;
                    }
                }
            }
            return Matrix4.CreateTranslation(pos);
        }
        private static Matrix4 UpdateRotation(HWJoint joint)
        {
            Vector3 rot = joint.LocalRotation;

            for (int i = 0; i < 3; i++)
            {
                if (joint.RotationChannel.Axes[i].Times.Count == 0)
                    return joint.LocalWorldMatrix.ClearTranslation().ClearScale();

                int keyIndex = 0;
                for (int t = 0; t < joint.RotationChannel.Axes[i].Times.Count - 1; t++)
                    if (AnimationTime >= joint.RotationChannel.Axes[i].Times[t] && AnimationTime < joint.RotationChannel.Axes[i].Times[t + 1])
                    {
                        keyIndex = t;
                        break;
                    }

                float blend = 0;
                float frameLength = 0;

                float highestTimeInAnimation = 0;
                for (int t = 0; t < joint.RotationChannel.Axes[i].Times.Count; t++)
                    highestTimeInAnimation = Math.Max(highestTimeInAnimation, joint.RotationChannel.Axes[i].Times[t]);

                if (joint.RotationChannel.Axes[i].Times.Count > 1)
                {
                    if (AnimationTime < highestTimeInAnimation)
                    {
                        frameLength = joint.RotationChannel.Axes[i].Times[keyIndex + 1] - joint.RotationChannel.Axes[i].Times[keyIndex];
                        blend = (AnimationTime - joint.RotationChannel.Axes[i].Times[keyIndex]) / frameLength;
                    }
                    else //When animation is over
                    {
                        keyIndex = joint.RotationChannel.Axes[i].Times.Count - 2;
                        blend = 1;
                    }
                }

                blend = Math.Max(blend, 0);

                if (joint.RotationChannel.Axes[i].Times.Count > 1)
                {
                    if (joint.RotationChannel.Axes[i].Values.Count <= keyIndex)
                        continue;

                    AnimationInterpolation interpolation = AnimationInterpolation.LINEAR;
                    if (joint.RotationChannel.Axes[i].KeyInterpolationTypes.Count > keyIndex)
                        interpolation = joint.RotationChannel.Axes[i].KeyInterpolationTypes[keyIndex];

                    float value = 0;
                    if (interpolation == AnimationInterpolation.LINEAR)
                    {
                        value = Utilities.LerpAngleDegrees(joint.RotationChannel.Axes[i].Values[keyIndex], joint.RotationChannel.Axes[i].Values[keyIndex + 1], blend);
                    }
                    else if (interpolation == AnimationInterpolation.BEZIER)
                    {
                        Vector2 p0 = new Vector2(joint.RotationChannel.Axes[i].Times[keyIndex], joint.RotationChannel.Axes[i].Values[keyIndex]);
                        Vector2 p1 = new Vector2(joint.RotationChannel.Axes[i].Times[keyIndex + 1], joint.RotationChannel.Axes[i].Values[keyIndex + 1]);

                        Vector2 point = Utilities.CalculateBezierPoint(p0, p1, joint.RotationChannel.Axes[i].OutTangents[keyIndex], joint.RotationChannel.Axes[i].InTangents[keyIndex + 1], blend);
                        value = point.Y;
                    }

                    value = MathHelper.DegreesToRadians(value);

                    switch (i)
                    {
                        case 0:
                            rot.X = value;
                            break;
                        case 1:
                            rot.Y = value;
                            break;
                        case 2:
                            rot.Z = value;
                            break;
                    }
                }
            }
            return Matrix4.CreateRotationX(rot.X) * Matrix4.CreateRotationY(rot.Y) * Matrix4.CreateRotationZ(rot.Z);
        }

        public static void UpdateAnimatedJoints()
        {
            foreach (HWAnimation anim in Animations)
            {
                anim.AnimatedJoints.Clear();
                foreach (HWJoint joint in HWJoint.Joints)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        foreach (float time in joint.PositionChannel.Axes[i].Times)
                        {
                            if (time >= anim.StartTime && time <= anim.EndTime)
                            {
                                anim.AnimatedJoints.Add(joint);
                                goto Exit;
                            }
                        }
                        foreach (float time in joint.RotationChannel.Axes[i].Times)
                        {
                            if (time >= anim.StartTime && time <= anim.EndTime)
                            {
                                anim.AnimatedJoints.Add(joint);
                                goto Exit;
                            }
                        }
                        foreach (float time in joint.ScalingChannel.Axes[i].Times)
                        {
                            if (time >= anim.StartTime && time <= anim.EndTime)
                            {
                                anim.AnimatedJoints.Add(joint);
                                goto Exit;
                            }
                        }
                    }
                Exit:
                    ;
                }

                HWJoint[] animatedJoints = anim.AnimatedJoints.ToArray();
                foreach (HWJoint joint in animatedJoints)
                    anim.AddJointToAnimatedRecursive(joint, false);
            }
        }

        private void AddJointToAnimatedRecursive(HWJoint joint, bool addParent)
        {
            if(addParent)
                AnimatedJoints.Add(joint);

            foreach (Element child in joint.Children)
                if(child is HWJoint)
                    AddJointToAnimatedRecursive((HWJoint)child, true);
        }
    }

    public class HWAnimationChannel
    {
        public HWAnimationAxis[] Axes = new HWAnimationAxis[3];

        public HWAnimationChannel()
        {
            for (int i = 0; i < 3; i++)
                Axes[i] = new HWAnimationAxis();
        }
    }
    public class HWAnimationAxis
    {
        public List<float> Times = new List<float>();
        public List<AnimationInterpolation> KeyInterpolationTypes = new List<AnimationInterpolation>();
        public List<Vector2> InTangents = new List<Vector2>();
        public List<Vector2> OutTangents = new List<Vector2>();
        public List<float> Values = new List<float>();
    }

    public enum AnimationType
    {
        TIME,
        FRAME,
    }

    public enum AnimationInterpolation
    {
        LINEAR,
        BEZIER,
    }
}
