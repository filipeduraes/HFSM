using System.Collections.Generic;
using System.Linq;
using FSMSystem.Core;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace HFSM.Editor
{
    public class FSMGraphView : GraphView
    {
        private const string StylePath = "Assets/Scripts/FSMSystem/Editor/FSMEditor.uss";
        private StateMachine fsm;
        
        public FSMGraphView()
        {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylePath);
            styleSheets.Add(styleSheet);
        }
        
        public new class UxmlFactory : UxmlFactory<FSMGraphView, UxmlTraits> { }

        public void PopulateGraph(StateMachine stateMachine)
        {
            graphViewChanged -= CheckElementsToRemove;
            DeleteElements(graphElements);
            graphViewChanged += CheckElementsToRemove;
            fsm = stateMachine;

            foreach (State state in fsm.States)
                CreateStateView(state);
        }

        private GraphViewChange CheckElementsToRemove(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (GraphElement elementToRemove in graphViewChange.elementsToRemove)
                {
                    if (elementToRemove is StateView stateView)
                        DeleteExistingState(stateView.State);
                }
            }

            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Create State", CreateNewState);
        }

        private void CreateNewState(DropdownMenuAction obj)
        {
            State newState = ScriptableObject.CreateInstance<State>();
            newState.Label = "New State";
            newState.name = newState.Label;
            newState.GUID = GUID.Generate().ToString();
            
            fsm.AddState(newState);
            AssetDatabase.AddObjectToAsset(newState, fsm);
            AssetDatabase.SaveAssets();

            CreateStateView(newState);
        }

        private void DeleteExistingState(State state)
        {
            fsm.RemoveState(state);
            AssetDatabase.RemoveObjectFromAsset(state);
            AssetDatabase.SaveAssets();
        }

        private void CreateStateView(State state)
        {
            StateView stateView = new(state);
            AddElement(stateView);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            bool ConnectionIsValid(Port endPort)
            {
                bool differentDirection = endPort.direction != startPort.direction;
                bool differentPorts = endPort.node != startPort.node;
                return differentDirection && differentPorts;
            }

            return ports.Where(ConnectionIsValid).ToList();
        }
    }
}
