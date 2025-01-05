using UnityEditor;
using UnityEngine;

// [CustomEditor(typeof(SwingingDoorController))]
// public class SwingingDoorEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         // Let Unity draw all the default inspector fields
//         base.OnInspectorGUI();

//         // Draw the default inspector for serialized fields
//         // DrawDefaultInspector();

//         SwingingDoorController doorController = (SwingingDoorController)target;

//         EditorGUILayout.LabelField("Customize Door Behavior", EditorStyles.boldLabel);
//         // Custom Pivot Option
//         doorController.UseCustomPivot = EditorGUILayout.Toggle("Use Custom Pivot", doorController.UseCustomPivot);

//         if (doorController.UseCustomPivot)
//         {            
//             // Target the actual serialized object and backing field
//             SerializedObject serializedDoor = new(doorController);
//             SerializedProperty pivotPointProp = serializedDoor.FindProperty("_pivotPoint");

//             // Draw the object field for the pivot point using the serialized property
//             EditorGUILayout.PropertyField(pivotPointProp, new GUIContent("Pivot Point"));
//             serializedDoor.ApplyModifiedProperties();  // Apply changes to the serialized object
//         }

//         EditorGUILayout.Space();

//         // // Show OpenAmount Slider Option
//         // doorController.ShowOpenAmountSlider = EditorGUILayout.Toggle("Allow Partial Opening", doorController.ShowOpenAmountSlider);

//         // if (doorController.ShowOpenAmountSlider)
//         // {
//         //     // Open Amount Slider (only visible if ShowOpenAmountSlider is true)
//         //     EditorGUILayout.LabelField("Door Open Amount (Preview)", EditorStyles.boldLabel);
//         //     doorController.OpenAmount = EditorGUILayout.Slider("Open Amount", doorController.OpenAmount, 0f, 1f);
//         // }
//     }
// }
