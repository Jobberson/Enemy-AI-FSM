using UnityEditor;
using UnityEngine;
using Snog.EnemyFSM.Enemy;

public class FSMVisualizerWindow : EditorWindow
{
    private EnemyStateMachine _target;
    private string _currentState;
    private Vector2 _scroll;

    [MenuItem("Tools/Enemy FSM/FSM Visualizer")]
    public static void ShowWindow()
    {
        GetWindow<FSMVisualizerWindow>("FSM Visualizer");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Enemy FSM Visualizer", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        _target = (EnemyStateMachine)EditorGUILayout.ObjectField("Target Enemy", _target, typeof(EnemyStateMachine), true);

        if (_target == null)
        {
            EditorGUILayout.HelpBox("Assign an EnemyStateMachine in the scene to visualize its FSM.", MessageType.Info);
            return;
        }

        if (Application.isPlaying)
        {
            _currentState = _target.CurrentStateName;
            EditorGUILayout.LabelField("Current State:", string.IsNullOrEmpty(_currentState) ? "None" : _currentState, EditorStyles.helpBox);
        }
        else
        {
            EditorGUILayout.HelpBox("Enter Play Mode to see real-time state updates.", MessageType.Warning);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Debug Controls", EditorStyles.boldLabel);

        if (GUILayout.Button("Force Wander"))
            _target.ReturnToWander();

        if (GUILayout.Button("Force Chase"))
            _target.EngageChase();
    }

    private void Update()
    {
        if (_target != null && Application.isPlaying)
        {
            Repaint(); // Refresh the window every frame in Play Mode
        }
    }
}
