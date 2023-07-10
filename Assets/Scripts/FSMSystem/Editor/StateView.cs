using FSMSystem.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace HFSM.Editor
{
    public class StateView : Node
    {
        public State State { get; }

        public StateView() { }
        
        public new class UxmlFactory : UxmlFactory<StateView, UxmlTraits> { }

        private Port inputPort;
        private Port outputPort;
        
        public StateView(State viewState)
        {
            State = viewState;
            title = viewState.Label;
            SetPosition(GetPosition());

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateInputPorts()
        {
            inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            
            if (inputPort != null)
            {
                inputPort.portName = string.Empty;
                inputContainer.Add(inputPort);
            }
        }
        
        private void CreateOutputPorts()
        {
            outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

            if (outputPort != null)
            {
                outputPort.portName = string.Empty;
                outputContainer.Add(outputPort);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            State.Position = newPos.min;
        }

        public override Rect GetPosition()
        {
            Rect rect = base.GetPosition();
            rect.min = State.Position;
            return rect;
        }
    }
}