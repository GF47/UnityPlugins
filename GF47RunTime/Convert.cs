using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GF47RunTime
{
    public static class Convert
    {
        public static int StringToInt(string value)
        {
            return System.Convert.ToInt32(value);
        }

        public static float StringToFloat(string value)
        {
            return System.Convert.ToSingle(value);
        }

        public static double StringToDouble(string value)
        {
            return System.Convert.ToDouble(value);
        }

        public static Vector2 StringToVector2(string value)
        {
            string[] array = StringInParentheses(value).Split(',');
            float[] f = { 0f, 0f };
            for (int i = 0; i < array.Length && i < 2; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Vector2(f[0], f[1]);
        }

        public static Vector3 StringToVector3(string value)
        {
            string[] array = StringInParentheses(value).Split(',');
            float[] f = { 0f, 0f, 0f };
            for (int i = 0; i < array.Length && i < 3; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Vector3(f[0], f[1], f[2]);
        }

        public static Vector4 StringToVector4(string value)
        {
            string[] array = StringInParentheses(value).Split(',');
            float[] f = { 0f, 0f, 0f, 0f };
            for (int i = 0; i < array.Length && i < 4; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Vector4(f[0], f[1], f[2], f[3]);
        }

        public static byte[] StructToBytes(object structObj, int size)
        {
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

        public static Color StringToColor(string value)
        {
            string[] array = StringInParentheses(value).Split(',');
            float[] f = { 0f, 0f, 0f, 255f };
            for (int i = 0; i < array.Length && i < 4; i++)
            {
                float.TryParse(array[i], out f[i]);
            }
            return new Color(f[0] / 255f, f[1] / 255f, f[2] / 255f, f[3] / 255f);
        }

        private static string StringInParentheses(string s)
        {
            string tmp = s.Substring(s.IndexOf("(", StringComparison.Ordinal) + 1);
            int last = tmp.LastIndexOf(")", StringComparison.Ordinal);
            last = last > -1 ? last : tmp.Length;
            tmp = tmp.Substring(0, last);
            return tmp;
        }

        public static T[] StringToArray<T>(string s, char splitChar = ',')
        {
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
        public static System.Object ConvertTo(string type, string value)
        {
            System.Object result = null;
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
                    result = StringToVector2(value);
                    break;
                case Vector3Type0:
                case Vector3Type1:
                case Vector3Type2:
                    result = StringToVector3(value);
                    break;
                case Vector4Type0:
                case Vector4Type1:
                case Vector4Type2:
                    result = StringToVector4(value);
                    break;
                case ColorType0:
                case ColorType1:
                case ColorType2:
                    result = StringToColor(value);
                    break;
            }
            return result;
        }
        #endregion
    }
}