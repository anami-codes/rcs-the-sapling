using UnityEngine;
using UnityEditor;
using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Toolkit
{
    /*
    [CustomEditor(typeof(PageManager))]
    public class PageManagerEditor : Editor
    {

        SerializedProperty transitions;
        ReorderableList list;

        private void OnEnable()
        {
            transitions = serializedObject.FindProperty("transitions");
            list = new ReorderableList(serializedObject, transitions, true, true, true, true);
            list.drawElementCallback = DrawListItems;
            list.drawHeaderCallback = DrawHeader;
        }

        //This is the function that makes the custom editor work
        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();

            // Update the array property's representation in the inspector
            // Have the ReorderableList do its work
            // Apply any property modification
        }

        void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
        }

        void DrawHeader(Rect rect)
        {
        }
    }
    */
}