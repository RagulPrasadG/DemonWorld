using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxView : MonoBehaviour
{
    public ParticleSystem vfx;

    private VfxController vfxController;

    public void SetController(VfxController controller) => this.vfxController = controller;

    public void Update()
    {
        if(!vfx.isEmitting)
        {
            vfxController.Stop();
        }
    }


}
