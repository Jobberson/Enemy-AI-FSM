[CustomEditor(typeof(EnemyStateMachine))]
public class EnemyStateMachineEditor : Editor
{
    #region Fields
    private SerializedProperty _playerProp;
    private SerializedProperty _enemyConfigProp;
    private SerializedProperty _wanderConfigProp;
    private SerializedProperty _chaseConfigProp;
    private SerializedProperty _movementControllerProp;
    private SerializedProperty _visionProp;
    private SerializedProperty _audioProp;
    private bool _showDebugFoldout;
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        // Cache SerializedProperties for performance
        _playerProp             = serializedObject.FindProperty("_player");
        _enemyConfigProp        = serializedObject.FindProperty("_enemyConfig");
        _wanderConfigProp       = serializedObject.FindProperty("_wanderConfig");
        _chaseConfigProp        = serializedObject.FindProperty("_chaseConfig");
        _movementControllerProp = serializedObject.FindProperty("_movementController");
        _visionProp             = serializedObject.FindProperty("_vision");
        _audioProp              = serializedObject.FindProperty("_audio");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawReferencesSection();
        DrawConfigsSection();
        DrawSensorsSection();
        DrawDebugSection();

        serializedObject.ApplyModifiedProperties();
    }
    #endregion

    #region Draw Sections
    private void DrawReferencesSection()
    {
        EditorGUILayout.LabelField("General References", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_playerProp);
        EditorGUILayout.PropertyField(_movementControllerProp);
    }

    private void DrawConfigsSection()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Configurations", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_enemyConfigProp);
        EditorGUILayout.PropertyField(_wanderConfigProp);
        EditorGUILayout.PropertyField(_chaseConfigProp);
    }

    private void DrawSensorsSection()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Sensors", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_visionProp);
        EditorGUILayout.PropertyField(_audioProp);
    }

    private void DrawDebugSection()
    {
        _showDebugFoldout = EditorGUILayout.Foldout(_showDebugFoldout, "Debug Options");
        if (_showDebugFoldout)
        {
            var fsm = (EnemyStateMachine)target;
            EditorGUILayout.LabelField("Current State:", fsm.CurrentStateName);
            if (GUILayout.Button("Force Wander State"))
                fsm.ReturnToWander();
            if (GUILayout.Button("Force Chase State"))
                fsm.EngageChase();
        }
    }
    #endregion

    #region Validation
    // Optionally override RequiresConstantRepaint to update state label live
    public override bool RequiresConstantRepaint() => _showDebugFoldout;
    #endregion
}
