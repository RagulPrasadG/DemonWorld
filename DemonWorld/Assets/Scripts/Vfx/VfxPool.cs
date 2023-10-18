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
        return  GetItem();
    }

    public override VfxController GetItem()
    {
        if (pooledItems.Count > 0)
        {
            foreach (PooledItem<VfxController> pooledItem in pooledItems)
            {
                if (pooledItem.Item.vfxData.vfxType == this.vfxData.vfxType
                    && !pooledItem.isUsed
                    )
                {
                    pooledItem.isUsed = true;
                    return pooledItem.Item;
                }
            }
        }
        return CreateNewPooledItem();
    }

    protected override VfxController CreateItem()
    {
        VfxController vfxController = new VfxController(this.vfxData);
        return vfxController;
    }

}
