using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Services;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button playButton;
    [SerializeField] Button exitButton;


    [Header("Panels")]
    [SerializeField] RectTransform mainMenuPanel;



    [Header("Health_HUD")]
    [Space(10)]
    [SerializeField] Image barImage;
    [SerializeField] TMP_Text coinText;

    [Header("Wave_HUD")]
    [Space(10)]
    [SerializeField] Button startWaveButton;
    [SerializeField] RectTransform waveNumberPanel;
    [SerializeField] TMP_Text waveNumberText;


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
        SetEvents();
    }

    public void SetEvents()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    public void OnTowerSelected(TowerType towerType)
    {
        previewService.SetPreviewMesh(towerDataScriptableObject.GetPreviewMesh(towerType));
        towerService.SetSelectedTower(towerType);
    }
    public void ShowWaveNumber()
    { 
        Sequence sequence = DOTween.Sequence();
        sequence.Append(waveNumberPanel.DOMoveY(50, 1f));
        sequence.AppendInterval(2f);
        sequence.Append(waveNumberPanel.DOMoveY(-50, 1f));
    }
    public void SetWaveNumber(int number)
    {
        waveNumberText.text = $"Wave : {number}";
    }

    public void EnableStartWaveButton() => startWaveButton.interactable = true;

    public void ToggleMainMenuPanel(bool toggle)
    {
        mainMenuPanel.gameObject.SetActive(toggle);
    }

    public void SetHealthBar(float amount)
    {
        float fillValue = amount/100;
        barImage.fillAmount = fillValue;
    }

    public void SetCoinText(int amount)
    {
        coinText.text = amount.ToString();
    }

    public void OnPlayButtonClicked()
    {
        ToggleMainMenuPanel(false);
        eventServiceScriptableObject.OnStartGame.RaiseEvent();
    }

    public void OnExitButtonClicked()
    {
        eventServiceScriptableObject.OnExitGame.RaiseEvent();
    }

    public void OnWaveButtonClicked()
    {
        startWaveButton.interactable = false;
        ShowWaveNumber();
        GameService.Instance.StartWave();
    }


}
