using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace DAEnerys
{
    internal abstract class TimeArray<T> : SortedList<float, T> where T : struct
    {
        public abstract void Add(float time, params float[] array);
        public T At(float time)
        {
            if (Count == 0) return new T();
            if (time <= Keys.First()) return Values.First();
            if (time >= Keys.Last()) return Values.Last();
            if (Keys.Contains(time)) return this[time];

            for (int i = 0; i < Count - 1; ++i)
            {
                if (Keys[i] < time && time < Keys[i + 1])
                {
                    float k = (time - Keys[i]) / (Keys[i + 1] - Keys[i]);
                    return add(Values[i], mult(k, sub(Values[i + 1], Values[i])));
                }
            }
            throw new ArgumentOutOfRangeException("time is invalid");
        }
        protected abstract T sub(T left, T right);
        protected abstract T mult(float left, T right);
        protected abstract T add(T left, T right);
    }

    internal class TimeColorArray : TimeArray<Vector4>
    {
        public override void Add(float time, params float[] array)
        {
            Add(time, new Vector4(array[0], array[1], array[2], array[3]));
        }

        protected override Vector4 add(Vector4 left, Vector4 right)
        {
            return left + right;
        }

        protected override Vector4 mult(float left, Vector4 right)
        {
            return left * right;
        }

        protected override Vector4 sub(Vector4 left, Vector4 right)
        {
            return left - right;
        }
    }

    internal class TimeVector3Array : TimeArray<Vector3>
    {
        public override void Add(float time, params float[] array)
        {
            Add(time, new Vector3(array[0], array[1], array[2]));
        }

        protected override Vector3 add(Vector3 left, Vector3 right)
        {
            return left + right;
        }

        protected override Vector3 mult(float left, Vector3 right)
        {
            return left * right;
        }

        protected override Vector3 sub(Vector3 left, Vector3 right)
        {
            return left - right;
        }
    }

    internal class TimeFloatArray : TimeArray<float>
    {
        public override void Add(float time, params float[] array)
        {
            ((SortedList<float, float>)this).Add(time, array[0]);
        }

        protected override float add(float left, float right)
        {
            return left + right;
        }

        protected override float mult(float left, float right)
        {
            return left * right;
        }

        protected override float sub(float left, float right)
        {
            return left - right;
        }
    }
}
