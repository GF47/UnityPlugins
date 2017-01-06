namespace GF47RunTime
{
    using UnityEngine;

    public static class TransformExtend
    {
        /// <summary> �ж� child �Ƿ��� parent ��������
        /// </summary>
        /// <param name="parent">�ٶ��ĸ�����</param>
        /// <param name="child">�ٶ���������</param>
        /// <returns>�Ƿ���������</returns>
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