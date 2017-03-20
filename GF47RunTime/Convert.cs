using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GF47RunTime 
{
    public static class Convert
    {
        public static bool ToBool(string value)
        {
            bool r;
            if (!bool.TryParse(value, out r))
            {
                return false;
            }
            return r;
        }
        public static int ToInt32(string value)
        {
            int r;
            if (!int.TryParse(value, out r))
            {
                return 0;
            }
            return r;
        }

        public static float ToFloat(string value)
        {
            float r;
            if (!float.TryParse(value, out r))
            {
                return 0f;
            }
            return r;
        }

        public static double ToDouble(string value)
        {
            double r;
            if (!double.TryParse(value, out r))
            {
                return 0d;
            }
            return r;
        }

        public static Vector2 ToVector2(string value)
        {
            string[] array = StringInBrackets(value).Split(',');
            float[] f = { 0f, 0f };
            for (int i = 0; i < array.Length && i < 2; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Vector2(f[0], f[1]);
        }

        public static Vector3 ToVector3(string value)
        {
            string[] array = StringInBrackets(value).Split(',');
            float[] f = { 0f, 0f, 0f };
            for (int i = 0; i < array.Length && i < 3; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Vector3(f[0], f[1], f[2]);
        }

        public static Vector4 ToVector4(string value)
        {
            string[] array = StringInBrackets(value).Split(',');
            float[] f = { 0f, 0f, 0f, 0f };
            for (int i = 0; i < array.Length && i < 4; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Vector4(f[0], f[1], f[2], f[3]);
        }

        public static Color ToColor(string value)
        {
            string[] array = StringInBrackets(value).Split(',');
            float[] f = { 0f, 0f, 0f, 1f };
            for (int i = 0; i < array.Length && i < 4; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Color(f[0], f[1], f[2], f[3]);
        }

        public static Color32 ToColor32(string value)
        {
            string[] array = StringInBrackets(value).Split(',');
            byte[] c = { 0, 0, 0, 255 };
            for (int i = 0; i < array.Length && i < 4; i++)
            {
                byte.TryParse(array[i], out c[i]);
            }
            return new Color32(c[0], c[1], c[2], c[3]);
        }
        public static Rect ToRect(string value)
        {
            string[] array = StringInBrackets(value).Split(',');
            float[] c = { 0f, 0f, 0f, 0f };
            for (int i = 0; i < array.Length && i < 4; i++)
            {
                float.TryParse(array[i], out c[i]);
            }
            return new Rect(c[0], c[1], c[2], c[3]);
        }

        public static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structObj, structPtr, false);
            Marshal.Copy(structPtr, bytes, 0, size);
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }

        public static object BytesToStruct(byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length) { return null; }

            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, structPtr, size);
            object obj = Marshal.PtrToStructure(structPtr, type);
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }

        public static string StringInBrackets(string s, char leftBracket = '(', char rightBracket = ')')
        {
            int left = s.IndexOf(leftBracket) + 1;
            int right = s.LastIndexOf(rightBracket);
            right = right > -1 ? right : s.Length;
            return s.Substring(left, right - left);

            // string tmp = s.Substring(s.IndexOf("(", StringComparison.Ordinal) + 1);
            // int last = tmp.LastIndexOf(")", StringComparison.Ordinal);
            // last = last > -1 ? last : tmp.Length;
            // tmp = tmp.Substring(0, last);
            // return tmp;
        }

        public static T[] StringToArray<T>(string s, char splitChar = ',')
        {
            if (string.IsNullOrEmpty(s)) { return null; }
            string[] strArray = s.Split(splitChar);
            T[] array = new T[strArray.Length];
            string type = typeof(T).Name;
            for (int i = 0; i < strArray.Length; i++)
            {
                object value = ConvertTo(type, strArray[i]);
                array[i] = value == null ? default(T) : (T)value;
            }
            return array;
        }

        #region 类型转换

        private const string StringType0 = "string";
        private const string StringType1 = "String";
        private const string StringType2 = "System.String";
        private const string BooleanType0 = "bool";
        private const string BooleanType1 = "Boolean";
        private const string BooleanType2 = "System.Boolean";
        private const string IntType0 = "int";
        private const string IntType1 = "Int32";
        private const string IntType2 = "System.Int32";
        private const string FloatType0 = "float";
        private const string FloatType1 = "Single";
        private const string FloatType2 = "System.Single";
        private const string DoubleType0 = "double";
        private const string DoubleType1 = "Double";
        private const string DoubleType2 = "System.Double";
        private const string Vector2Type0 = "vector2";
        private const string Vector2Type1 = "Vector2";
        private const string Vector2Type2 = "UnityEngine.Vector2";
        private const string Vector3Type0 = "vector3";
        private const string Vector3Type1 = "Vector3";
        private const string Vector3Type2 = "UnityEngine.Vector3";
        private const string Vector4Type0 = "vector4";
        private const string Vector4Type1 = "Vector4";
        private const string Vector4Type2 = "UnityEngine.Vector4";
        private const string ColorType0 = "color";
        private const string ColorType1 = "Color";
        private const string ColorType2 = "UnityEngine.Color";
        private const string Color32Type0 = "color32";
        private const string Color32Type1 = "Color32";
        private const string Color32Type2 = "UnityEngine.Color32";
        private const string RectType0 = "rect";
        private const string RectType1 = "Rect";
        private const string RectType2 = "UnityEngine.Rect";
        public static object ConvertTo(string type, string value)
        {
            object result = null;
            switch (type)
            {
                case StringType0:
                case StringType1:
                case StringType2:
                    result = value;
                    break;
                case BooleanType0:
                case BooleanType1:
                case BooleanType2:
                    result = bool.Parse(value);
                    break;
                case IntType0:
                case IntType1:
                case IntType2:
                    result = int.Parse(value);
                    break;
                case FloatType0:
                case FloatType1:
                case FloatType2:
                    result = float.Parse(value);
                    break;
                case DoubleType0:
                case DoubleType1:
                case DoubleType2:
                    result = double.Parse(value);
                    break;
                case Vector2Type0:
                case Vector2Type1:
                case Vector2Type2:
                    result = ToVector2(value);
                    break;
                case Vector3Type0:
                case Vector3Type1:
                case Vector3Type2:
                    result = ToVector3(value);
                    break;
                case Vector4Type0:
                case Vector4Type1:
                case Vector4Type2:
                    result = ToVector4(value);
                    break;
                case ColorType0:
                case ColorType1:
                case ColorType2:
                    result = ToColor(value);
                    break;
                case Color32Type0:
                case Color32Type1:
                case Color32Type2:
                    result = ToColor32(value);
                    break;
                case RectType0:
                case RectType1:
                case RectType2:
                    result = ToRect(value);
                    break;
            }
            return result;
        }
        #endregion
    }
}
