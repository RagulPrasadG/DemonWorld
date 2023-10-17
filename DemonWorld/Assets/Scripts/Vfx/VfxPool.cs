using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Utilities;

public class VfxPool : GenericObjectPool<VfxController>
{
    private VfxData vfxData;

    public VfxController GetVfx(VfxData vfxData)
    {
        this.vfxData = vfxData;
        return GetItem();
    }

    protected override VfxController CreateItem()
    {
        VfxController vfxController = new VfxController(vfxData);
        return vfxController;
    }

}
