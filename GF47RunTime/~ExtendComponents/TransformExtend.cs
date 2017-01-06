namespace GF47RunTime
{
    using UnityEngine;

    public static class TransformExtend
    {
        /// <summary> 判断 child 是否是 parent 的子物体
        /// </summary>
        /// <param name="parent">假定的父物体</param>
        /// <param name="child">假定的子物体</param>
        /// <returns>是否是子物体</returns>
        public static bool IsTheParentOf(this Transform parent, Transform child)
        {
            if (parent == null || child == null) return false;

            while (child != null)
            {
                if (child == parent) return true;
                child = child.parent;
            }
            return false;
        }
    }
}