using System;
using FSMSystem.Core;
using HFSM.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FSMEditor : EditorWindow
{
    [SerializeField] private VisualTreeAsset visualTree;
    
    private FSMGraphView graphView;
    
    [MenuItem("Tools/HFSM Editor")]
    public static void ShowExample()
    {
        FSMEditor window = GetWindow<FSMEditor>();
        window.titleContent = new GUIContent("FSMEditor");
    }

    public void CreateGUI()
    {
        visualTree.CloneTree(rootVisualElement);
        graphView = rootVisualElement.Q<FSMGraphView>();
    }

    private void OnSelectionChange()
    {
        if (Selection.activeObject is StateMachine stateMachine)
        {
            graphView.PopulateGraph(stateMachine);
        }
    }
}