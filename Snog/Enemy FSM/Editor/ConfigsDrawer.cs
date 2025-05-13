[CustomPropertyDrawer(typeof(EnemyConfig), true)]
[CustomPropertyDrawer(typeof(WanderConfig), true)]
[CustomPropertyDrawer(typeof(ChaseConfig), true)]
[CustomPropertyDrawer(typeof(AmbushConfig), true)]
public class ConfigsDrawer : PropertyDrawer
{
    #region Overrides
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw object field
        position = EditorGUI.PrefixLabel(position, label);
        EditorGUI.PropertyField(position, property, GUIContent.none);

        // If assigned, draw a small "Open" button
        if (property.objectReferenceValue != null)
        {
            var btnRect = new Rect(position.xMax - 50, position.y, 50, position.height);
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
