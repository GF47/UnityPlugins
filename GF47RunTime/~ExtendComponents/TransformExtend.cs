using System.Collections.Generic;

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

        public static List<int> GetIndexRelativeTo(this Transform t, Transform root, bool isChildToRoot = false)
        {
            List<int> list = new List<int>();
            Transform parent = null;
            while (parent != root)
            {
                parent = t.parent;
                int i = t.GetSiblingIndex();
                t = parent;
            }
            if (!isChildToRoot)
            {
                list.Reverse();
            }
            return list;
        }

        public static Transform GetChildByIndexList(this Transform root, IList<int> index, bool isChildToRoot = false)
        {
            if (isChildToRoot)
            {
                for (int i = index.Count -1; i > -1; i--)
                {
                    root = root.GetChild(index[i]);
                }
            }
            else
            {
                for (int i = 0; i < index.Count; i++)
                {
                    root = root.GetChild(index[i]);
                }
            }
            return root;
        }
    }
}