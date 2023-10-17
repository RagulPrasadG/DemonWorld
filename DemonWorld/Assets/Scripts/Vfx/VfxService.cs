using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxService
{
    private VfxPool vfxPool;
    private VfxDataScriptableObject vfxDataScriptableObject;

    public VfxService(VfxDataScriptableObject vfxDataScriptableObject)
    {
        this.vfxPool = new VfxPool();
        this.vfxDataScriptableObject = vfxDataScriptableObject;
    }
    public void PlayVfx(VfxType vfxType,Vector3 positionToplay)
    {
        VfxData vfxData = vfxDataScriptableObject.GetVfxData(vfxType);
        VfxController vfxController = vfxPool.GetVfx(vfxData);
        vfxController.PlayAt(positionToplay);
    }

    public void ReturnVfxToPool(VfxController vfxController)
    {
        vfxPool.ReturnItem(vfxController);
    }

}
