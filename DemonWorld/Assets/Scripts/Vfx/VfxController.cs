using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxController
{
    private VfxView vfxView;
    public VfxData vfxData;

    public VfxController(VfxData vfxData)
    {
        this.vfxData = vfxData;
        this.vfxView = Object.Instantiate(this.vfxData.vfxPrefab);
        this.vfxView.SetController(this);
    }
    
    public void PlayAt(Vector3 positionToPlay)
    {
        this.vfxView.transform.position = positionToPlay;
        this.vfxView.gameObject.SetActive(true);
        this.vfxView.vfx.Play();
    }

    public void Stop()
    {
        GameService.Instance.vfxService.ReturnVfxToPool(this);
        this.vfxView.gameObject.SetActive(false);
    }

}
