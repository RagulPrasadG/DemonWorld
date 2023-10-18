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
        VfxController vfx = GetItem();
        if(vfx != null)
        {
            vfx.Init(vfxData);
        }
        return vfx;
    }

    protected override VfxController CreateItem()
    {
        VfxController vfxController = new VfxController();
        return vfxController;
    }

}
