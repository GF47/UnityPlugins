﻿using GF47RunTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void 字符串转为数组()
        {
            string a = "1,2,3,4,5,6";
            int[] array = Convert.StringToArray<int>(a);
            Assert.AreEqual(1,array[0]);
            Assert.AreEqual(2,array[1]);
            Assert.AreEqual(3,array[2]);
            Assert.AreEqual(4,array[3]);
            Assert.AreEqual(5,array[4]);
            Assert.AreEqual(6,array[5]);
        }

        [TestMethod]
        public void 颜色转化为字符串()
        {
            Color c = Color.blue;
            string cString = c.ToString();
            Color32 c32 = Color.blue;
            string c32String = c32.b.ToString();
            Assert.AreEqual(c32String, "255");
        }

        [TestMethod]
        public void 字符串测试()
        {
            string a = "10.2,100.43,2.124,1.732";
            float[] array = Convert.StringToArray<float>(a);
            Assert.AreEqual(10.2f, array[0]);
            Assert.AreEqual(1.732f, array[3]);
        }
    }
}
