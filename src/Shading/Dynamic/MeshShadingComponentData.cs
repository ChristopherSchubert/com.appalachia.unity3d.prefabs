#region

using Appalachia.Core.Scriptables;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Appalachia.Rendering.Shading.Dynamic
{
    public class MeshShadingComponentData : EmbeddedScriptableObject<MeshShadingComponentData>
    {
        [HideLabel]
        [InlineEditor(Expanded = true)]
        public MeshShadingMetadata metadata;

        public Vector3 boundsCenterOffsetPercentage;
        public bool updateChildMeshes;
    }
}
