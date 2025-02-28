﻿using UnityEditor;
using UnityEngine;

namespace Appalachia.Rendering.Shading.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(AppalachiaInteractiveAttribute))]
    public class AppalachiaInteractiveAttributeDrawer : PropertyDrawer
    {
        #region Fields and Autoproperties

        public int Type;
        private AppalachiaInteractiveAttribute a;

        private int Value;
        private string Keywork;

        #endregion

        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //var height = -2;

            //if (Type == 0)
            //{
            //    height = 16;
            //}

            return -2;
        }

        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (AppalachiaInteractiveAttribute)attribute;

            Value = a.Value;
            Keywork = a.Keyword;
            Type = a.Type;

            if (Type == 0)
            {
                //EditorGUI.PropertyField(position, property);

                if (property.intValue == Value)
                {
                    GUI.enabled = true;
                }
                else
                {
                    GUI.enabled = false;
                }
            }
            else if (Type == 1)
            {
                if (Keywork == "ON")
                {
                    GUI.enabled = true;
                }
                else if (Keywork == "OFF")
                {
                    GUI.enabled = false;
                }
            }
        }
    }
}
