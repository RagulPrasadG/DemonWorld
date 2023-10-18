using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Services;

public class UIService : MonoBehaviour
{
    [SerializeField] TowerDataScriptableObject towerDataScriptableObject;
    [SerializeField] EventServiceScriptableObject eventServiceScriptableObject;
    private TowerService towerService;
    private PreviewService previewService;

    private void OnEnable()
    {
        eventServiceScriptableObject.OnTowerSelected.AddListener(OnTowerSelected);
    }

    private void OnDisable()
    {
        eventServiceScriptableObject.OnTowerSelected.RemoveListener(OnTowerSelected);
    }

    public void Init(TowerService towerService,PreviewService previewService)
    {
        this.towerService = towerService;
        this.previewService = previewService;   
    }

    public void OnTowerSelected(TowerType towerType)
    {
        previewService.SetPreviewMesh(towerDataScriptableObject.GetPreviewMesh(towerType));
        towerService.SetSelectedTower(towerType);
    }

}
