using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControllerUI
{
    private TowerViewUI towerViewUI;

    public TowerControllerUI(TowerViewUI towerViewUI)
    {
        this.towerViewUI = Object.Instantiate(towerViewUI);

    }

}
