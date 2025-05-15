using UnityEditor;
using UnityEngine;
using Snog.EnemyFSM.Configs;

[CustomEditor(typeof(EnemyConfig))]
public class EnemyConfigEditor : Editor
{
    private SerializedProperty wanderSpeed, investigateSpeed, chaseSpeed, recoverSpeed;
    private SerializedProperty viewDistance, viewAngle, timeToSpotPlayer, noiseDetectionRadius;
    private SerializedProperty chaseLoseTime;
    private SerializedProperty minRecoverDuration, maxChaseDurationCap, minChaseDurationCap;
    private SerializedProperty maxViewDistance, minTimeToSpotPlayer;
    private SerializedProperty maxWanderSpeed, maxChaseSpeed, maxInvestigateSpeed;
    private SerializedProperty maxBiasChancePercentage, maxBiasAccuracy;

    private bool showMovement = true;
    private bool showVision = true;
    private bool showChase = true;
    private bool showIntensity = true;

    private void OnEnable()
    {
        wanderSpeed = serializedObject.FindProperty("wanderSpeed");
        investigateSpeed = serializedObject.FindProperty("investigateSpeed");
        chaseSpeed = serializedObject.FindProperty("chaseSpeed");
        recoverSpeed = serializedObject.FindProperty("recoverSpeed");

        viewDistance = serializedObject.FindProperty("viewDistance");
        viewAngle = serializedObject.FindProperty("viewAngle");
        timeToSpotPlayer = serializedObject.FindProperty("timeToSpotPlayer");
        noiseDetectionRadius = serializedObject.FindProperty("noiseDetectionRadius");

        chaseLoseTime = serializedObject.FindProperty("chaseLoseTime");

        minRecoverDuration = serializedObject.FindProperty("minRecoverDuration");
        maxChaseDurationCap = serializedObject.FindProperty("maxChaseDurationCap");
        minChaseDurationCap = serializedObject.FindProperty("minChaseDurationCap");
        maxViewDistance = serializedObject.FindProperty("maxViewDistance");
        minTimeToSpotPlayer = serializedObject.FindProperty("minTimeToSpotPlayer");
        maxWanderSpeed = serializedObject.FindProperty("maxWanderSpeed");
        maxChaseSpeed = serializedObject.FindProperty("maxChaseSpeed");
        maxInvestigateSpeed = serializedObject.FindProperty("maxInvestigateSpeed");
        maxBiasChancePercentage = serializedObject.FindProperty("maxBiasChancePercentage");
        maxBiasAccuracy = serializedObject.FindProperty("maxBiasAccuracy");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        showMovement = EditorGUILayout.BeginFoldoutHeaderGroup(showMovement, "Movement Speeds");
        if (showMovement)
        {
            EditorGUILayout.PropertyField(wanderSpeed);
            EditorGUILayout.PropertyField(investigateSpeed);
            EditorGUILayout.PropertyField(chaseSpeed);
            EditorGUILayout.PropertyField(recoverSpeed);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showVision = EditorGUILayout.BeginFoldoutHeaderGroup(showVision, "Vision & Hearing");
        if (showVision)
        {
            EditorGUILayout.PropertyField(viewDistance);
            EditorGUILayout.PropertyField(viewAngle);
            EditorGUILayout.PropertyField(timeToSpotPlayer);
            EditorGUILayout.PropertyField(noiseDetectionRadius);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showChase = EditorGUILayout.BeginFoldoutHeaderGroup(showChase, "Chase Settings");
        if (showChase)
        {
            EditorGUILayout.PropertyField(chaseLoseTime);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showIntensity = EditorGUILayout.BeginFoldoutHeaderGroup(showIntensity, "Intensity Increase Limits");
        if (showIntensity)
        {
            EditorGUILayout.PropertyField(minRecoverDuration);
            EditorGUILayout.PropertyField(maxChaseDurationCap);
            EditorGUILayout.PropertyField(minChaseDurationCap);
            EditorGUILayout.PropertyField(maxViewDistance);
            EditorGUILayout.PropertyField(minTimeToSpotPlayer);
            EditorGUILayout.PropertyField(maxWanderSpeed);
            EditorGUILayout.PropertyField(maxChaseSpeed);
            EditorGUILayout.PropertyField(maxInvestigateSpeed);
            EditorGUILayout.PropertyField(maxBiasChancePercentage);
            EditorGUILayout.PropertyField(maxBiasAccuracy);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
