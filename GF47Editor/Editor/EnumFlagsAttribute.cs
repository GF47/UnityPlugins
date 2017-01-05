// /* ****************************************************************
//  * @File Name   :   EnumFlagsAttributes
//  * @Author      :   GF47
//  * @Date        :   2015/7/23 14:52:05
//  * @Description :   枚举的显示面板效果
//  * @Edit        :   2015/7/23 14:52:05
//  * ***************************************************************/
// using UnityEditor;
// using UnityEngine;
// 
// [CustomPropertyDrawer(typeof(GF47RunTime.EnumFlagsAttribute))]
// public class EnumFlagsAttributeDrawer : PropertyDrawer
// {
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
//     }
// }

// TODO 把这个脚本放进Unity工程的Editor文件夹中