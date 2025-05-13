using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyStateMachine))]
public class EnemyStateMachineEditor : Editor
{
    #region Fields
    private SerializedProperty _playerProp;
    private SerializedProperty _enemyConfigProp;
    private SerializedProperty _wanderConfigProp;
    private SerializedProperty _chaseConfigProp;
    private SerializedProperty _investigateConfigProp;
    private SerializedProperty _searchConfigProp;
    private SerializedProperty _recoverConfigProp;
    private SerializedProperty _stalkConfigProp;
    private SerializedProperty _movementControllerProp;
    private SerializedProperty _visionProp;
    private SerializedProperty _noiseProp;
    private bool _showDebugFoldout;
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        _playerProp             = serializedObject.FindProperty("player");
        _enemyConfigProp        = serializedObject.FindProperty("enemyConfig");
        _wanderConfigProp       = serializedObject.FindProperty("wanderConfig");
        _chaseConfigProp        = serializedObject.FindProperty("chaseConfig");
        _investigateConfigProp  = serializedObject.FindProperty("investigateConfig");
        _searchConfigProp       = serializedObject.FindProperty("searchConfig");
        _recoverConfigProp      = serializedObject.FindProperty("recoverConfig");
        _stalkConfigProp        = serializedObject.FindProperty("stalkConfig");
        _movementControllerProp = serializedObject.FindProperty("movementController");
        _visionProp             = serializedObject.FindProperty("vision");
        _noiseProp              = serializedObject.FindProperty("noise");
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
        EditorGUILayout.PropertyField(_investigateConfigProp);
        EditorGUILayout.PropertyField(_searchConfigProp);
        EditorGUILayout.PropertyField(_recoverConfigProp);
        EditorGUILayout.PropertyField(_stalkConfigProp);
    }

    private void DrawSensorsSection()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Sensors", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_visionProp);
        EditorGUILayout.PropertyField(_noiseProp);
    }

    private void DrawDebugSection()
    {
        _showDebugFoldout = EditorGUILayout.Foldout(_showDebugFoldout, "Debug Options");
        if (_showDebugFoldout)
        {
            var fsm = (EnemyStateMachine)target;
            EditorGUILayout.LabelField("Current State:", string.IsNullOrEmpty(fsm.CurrentStateName) ? "None" : fsm.CurrentStateName);
            if (GUILayout.Button("Force Wander State"))
                fsm.ReturnToWander();
            if (GUILayout.Button("Force Chase State"))
                fsm.EngageChase();
        }
    }
    #endregion

    #region Validation
    public override bool RequiresConstantRepaint() => _showDebugFoldout;
    #endregion
}
