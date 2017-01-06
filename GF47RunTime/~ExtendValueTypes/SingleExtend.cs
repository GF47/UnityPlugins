namespace GF47RunTime
{
    public static class SingleExtend
    {
        /// <summary> �ڸ����� min �� max ֮��ȡһ�������
        /// </summary>
        /// <param name="min">��Сֵ</param>
        /// <param name="max">���ֵ</param>
        /// <returns></returns>
        public static float RandomRange(float min, float max)
        {
            if (System.Math.Abs(min - max) < 1e-6f)
            {
                return min;
            }
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// ����������ֵ��������ֵ��
        /// </summary>
        public static float Clamp(this float t, float min, float max)
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