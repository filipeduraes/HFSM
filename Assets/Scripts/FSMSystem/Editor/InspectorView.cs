using UnityEngine.UIElements;

namespace HFSM.Editor
{
    public class InspectorView : VisualElement
    {
        public new class UXMLFactory : UxmlFactory<InspectorView, UxmlTraits> { }
        
        public InspectorView() { }
    }
}
