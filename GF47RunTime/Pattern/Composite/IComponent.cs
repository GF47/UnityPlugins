/***************************************************************
 * @File Name       : IComponent
 * @Author          : GF47
 * @Description     : 组合模式的部件接口
 * @Date            : 2017/2/13/星期一 16:07:12
 * @Edit            : none
 **************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GF47RunTime.Pattern.Composite
{
    public interface IComponent<T> where T : IComponent<T>
    {
        string Name { get; set; }
        bool IsExpand { get; set; }
        IComponent<T> Parent { get; set; }
        List<IComponent<T>> Children { get; set; }
        void Show(int depth);
    }
}
