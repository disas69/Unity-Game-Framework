﻿using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    public class CustomEditorBase<T> : UnityEditor.Editor where T : Object
    {
        protected T Target { get; private set; }
        protected GUIStyle HeaderStyle { get; private set; }
        protected GUIStyle LabelStyle { get; private set; }
        protected GUIStyle ErrorStyle { get; private set; }

        protected virtual void OnEnable()
        {
            Target = target as T;
            
            HeaderStyle = new GUIStyle
            {
                normal = {textColor = Color.gray},
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };

            LabelStyle = new GUIStyle
            {
                normal = {textColor = Color.gray},
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.MiddleCenter
            };

            ErrorStyle = new GUIStyle
            {
                normal = { textColor = Color.red },
                fontStyle = FontStyle.BoldAndItalic,
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            serializedObject.Update();
            DrawInspector();
            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(Target);
            }
        }

        protected virtual void DrawInspector()
        {
        }

        protected void RecordObject(string changeDescription = "Custom editor change")
        {
            Undo.RecordObject(serializedObject.targetObject, changeDescription);
        }
    }
}