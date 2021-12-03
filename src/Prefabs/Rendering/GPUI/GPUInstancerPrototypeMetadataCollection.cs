#region

using System.Collections.Generic;
using Appalachia.Core.Scriptables;
using Appalachia.Rendering.Prefabs.Rendering.Collections;
using Appalachia.Utility.Extensions;
using GPUInstancer;
using Sirenix.OdinInspector;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace Appalachia.Rendering.Prefabs.Rendering.GPUI
{
    public class GPUInstancerPrototypeMetadataCollection : SingletonAppalachiaObject<
        GPUInstancerPrototypeMetadataCollection>
    {
        private const string _PRF_PFX = nameof(GPUInstancerPrototypeMetadataCollection) + ".";

        [FormerlySerializedAs("_metadatas")]
        [SerializeField]
        [ListDrawerSettings(
            Expanded = true,
            DraggableItems = false,
            HideAddButton = true,
            HideRemoveButton = true,
            NumberOfItemsPerPage = 3
        )]
        private GPUInstancerPrototypeMetadataLookup _state;

        internal GPUInstancerPrototypeMetadataLookup State => _state;

        protected override void WhenEnabled()
        {
            if (State == null)
            {
                _state = new GPUInstancerPrototypeMetadataLookup();
#if UNITY_EDITOR
               this.MarkAsModified();
#endif
            }

#if UNITY_EDITOR
            State.SetMarkModifiedAction(this.MarkAsModified);
#endif
        }

        public void ConfirmPrototype(GPUInstancerPrototypeMetadata metadata)
        {
            State.AddIfKeyNotPresent(metadata.prototype.prefabObject.name, metadata);
        }

#if UNITY_EDITOR
        private static readonly ProfilerMarker _PRF_FindOrCreate =
            new(_PRF_PFX + nameof(FindOrCreate));

        public GPUInstancerPrototypeMetadata FindOrCreate(
            GameObject gameObject,
            GPUInstancerPrefabManager gpui,
            Dictionary<GPUInstancerPrefabPrototype, RegisteredPrefabsData> prototypeLookup)
        {
            using (_PRF_FindOrCreate.Auto())
            {
                if (State.ContainsKey(gameObject.name))
                {
                    var metadata = State[gameObject.name];

                    metadata.CreatePrototypeIfNecessary(gameObject, gpui, prototypeLookup);
                    return metadata;
                }

                var newPrototypePair = AppalachiaObject.LoadOrCreateNew<GPUInstancerPrototypeMetadata>(
                    gameObject.name,
                    true,
                    false
                );

                newPrototypePair.CreatePrototypeIfNecessary(gameObject, gpui, prototypeLookup);

                State.AddOrUpdate(gameObject.name, newPrototypePair);

                newPrototypePair.MarkAsModified();
               this.MarkAsModified();

                return newPrototypePair;
            }
        }
#endif
    }
}
