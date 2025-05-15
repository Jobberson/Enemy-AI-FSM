using UnityEditor;
using UnityEngine;
using Snog.EnemyFSM.Configs;

[CustomPropertyDrawer(typeof(EnemyConfig), true)]
[CustomPropertyDrawer(typeof(WanderConfig), true)]
[CustomPropertyDrawer(typeof(ChaseConfig), true)]
//[CustomPropertyDrawer(typeof(AmbushConfig), true)]
[CustomPropertyDrawer(typeof(InvestigateConfig), true)]
[CustomPropertyDrawer(typeof(SearchConfig), true)]
[CustomPropertyDrawer(typeof(RecoverConfig), true)]
[CustomPropertyDrawer(typeof(StalkConfig), true)]
public class ConfigsDrawer : PropertyDrawer
{
    #region Overrides
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw object field
        var fieldRect = new Rect(position.x, position.y, position.width - 55, position.height);
        var btnRect = new Rect(position.xMax - 50, position.y, 50, position.height);

        EditorGUI.PropertyField(fieldRect, property, label);

        // If assigned, draw a small "Open" button
        if (property.objectReferenceValue != null)
        {
            if (GUI.Button(btnRect, "Open"))
            {
                Selection.activeObject = property.objectReferenceValue;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        => base.GetPropertyHeight(property, label) + 2;
    #endregion
}
