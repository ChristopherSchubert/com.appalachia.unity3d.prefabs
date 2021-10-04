#region

using System;
using Appalachia.Editing.Attributes;
using GPUInstancer;
using Sirenix.OdinInspector;
using Unity.Profiling;

#endregion

namespace Appalachia.Prefabs.Rendering.Options.Rendering
{
#if UNITY_EDITOR

    [Serializable]
    public class EditorRenderingOptions : RenderingOptionsBase<EditorRenderingOptions>
    {
        public enum EditorRenderCamera
        {
            SceneView = 0,
            MainCamera = 10
        }

        private const string _PRF_PFX = nameof(EditorRenderingOptions) + ".";

        private static readonly ProfilerMarker _PRF_ApplyTo = new(_PRF_PFX + nameof(ApplyTo));

        [OnValueChanged(nameof(MarkDirty), true)]
        [SmartLabel]
        public EditorRenderCamera renderCamera;

        [OnValueChanged(nameof(MarkDirty), true)]
        [SmartLabel]
        public bool renderOnlySelectedCamera;

        public void ApplyTo(GPUInstancerEditorSimulator simulator)
        {
            using (_PRF_ApplyTo.Auto())
            {
                if (_applied)
                {
                    return;
                }

                _applied = true;

                simulator.cameraName = renderCamera.ToString();
                simulator.cameraData.renderOnlySelectedCamera = renderOnlySelectedCamera;
            }
        }
    }
#endif
}
