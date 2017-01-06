namespace GF47RunTime
{
    public static class Int32Extend
    {
        /// <summary> �������� min �� max ֮��ȡһ�������
        /// </summary>
        /// <param name="min">��Сֵ</param>
        /// <param name="max">���ֵ</param>
        /// <returns></returns>
        public static int RandomRange(int min, int max)
        {
            if (min == max)
            {
                return min;
            }
            return UnityEngine.Random.Range(min, max + 1);
        }

        /// <summary>
        /// ��������ֵ��������ֵ��
        /// </summary>
        public static int Clamp(this int t, int min, int max)
        {
            if (t >= max)
            {
                return max;
            }
            if (t <= min)
            {
                return min;
            }
            return t;
        }
    }
}