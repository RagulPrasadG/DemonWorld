using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerViewUI : MonoBehaviour
{
    [SerializeField] Image towerImage;
    [SerializeField] TMPro.TMP_Text towerNameText;
    [SerializeField] TMPro.TMP_Text costText;
    [SerializeField] Toggle toggle;
    [SerializeField] Button button;
    [SerializeField] EventServiceScriptableObject eventServiceScriptableObject;

    private TowerDataUI towerDataUI;

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Init(TowerDataUI towerDataUI)
    {
        this.towerDataUI = towerDataUI;
        towerImage.sprite = this.towerDataUI.towerSprite;
        costText.text = this.towerDataUI.cost.ToString();
        towerNameText.text = this.towerDataUI.towerName;
    }

    public void OnButtonClicked()
    {
        eventServiceScriptableObject.OnTowerSelected.RaiseEvent(towerDataUI.towerType);
        toggle.isOn = !toggle.isOn;      
    }

    public void Deselect() => toggle.isOn = false;
 

}
