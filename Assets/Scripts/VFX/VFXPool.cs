using CosmicCuration.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{

    public class VFXPool : GenericObjectPool<VFXController>
    {
        private VFXView vfxPrefab;

        public VFXPool(VFXView vfxPrefab) => this.vfxPrefab = vfxPrefab;

        public VFXController GetVFX() => GetItem<VFXController>();

        protected override VFXController CreateItem<T>() => new VFXController(vfxPrefab);
    }
}
