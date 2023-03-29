using System;
using System.Linq;
using EntiCS.Entities;
using EntiCS.Entities.Components;
using UnityEditor;
using UnityEngine;

namespace EntiCS.Editor.CustomEditors
{
    [CustomEditor(typeof(MonoEntity))]
    public class MonoEntityEditor : UnityEditor.Editor
    {
        private MonoEntity Target => (MonoEntity)target;

        private Type[] _componentTypes;
        private string[] _componentNames;
        private int _initialIndex = -1;
        private int _componentIndex = 0;

        public void OnEnable()
        {
            _componentTypes = GetComponentTypes();
            _componentNames = GetComponentNames(_componentTypes);
        }

        public override void OnInspectorGUI()
        {
            InitializeIndexSafe();
            base.OnInspectorGUI();

            GUILayout.Label("Add non-mono components as a serialized reference");
            _componentIndex = EditorGUILayout.Popup(_componentIndex, _componentNames);

            GUILayout.Space(10);
            if (GUILayout.Button("Add Component"))
            {
                var type = _componentTypes[_componentIndex];
                var component = Activator.CreateInstance(type);

                Target.AddSerializedComponent((IEntityComponent)component);

                Save();
            }
        }

        private void InitializeIndexSafe()
        {
            if (_initialIndex > -1)
            {
                return;
            }

            var existingConcreteTypes = Target.SerializedComponents
                .Where(e => e!= null)
                .Select(e => e.GetType());

            var preselected = _componentTypes.FirstOrDefault(e => existingConcreteTypes.Contains(e));

            if (preselected != null)
            {
                _initialIndex = Array.IndexOf(_componentNames, preselected.Name);
                _componentIndex = _initialIndex;
            }
        }

        private void Save()
        {
            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        private Type[] GetComponentTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(IsValidComponentType)
                .OrderBy(e => e.Name)
                .ToArray();
        }

        private string[] GetComponentNames(Type[] types)
        {
            return types
                .Select(e => e.Name)
                .ToArray();
        }

        private bool IsValidComponentType(Type type)
        {
            return type != null &&
                   type.GetInterface(nameof(IEntityComponent)) != null &&
                   !type.IsInterface &&
                   type != typeof(EntityComponent) &&
                   type != typeof(MonoEntityComponent) &&
                   !type.IsSubclassOf(typeof(UnityEngine.Object)); // UnityEngine.Object cannot be used in SerializedReference
        }
    }
}