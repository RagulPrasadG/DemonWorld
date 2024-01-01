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
    [SerializeField] Button pauseButton;


    [Header("Panels")]
    [SerializeField] RectTransform mainMenuPanel;
    [SerializeField] RectTransform pauseMenuPanel;



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

    private GameService gameService;
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

    public void Init(GameService gameService, TowerService towerService,PreviewService previewService)
    {
        this.gameService = gameService;
        this.towerService = towerService;
        this.previewService = previewService;
        SetEvents();
    }

    public void SetEvents()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    public void OnTowerSelected(TowerType towerType)
    {
        gameService.soundService.PlaySfx(SoundType.ButtonClick);
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

    public void ToggleStartWaveButton(bool toggle) => startWaveButton.interactable = toggle;

    public void ToggleMainMenuPanel(bool toggle)
    {
        mainMenuPanel.gameObject.SetActive(toggle);
    }

    public void TogglePauseMenuPanel(bool toggle)
    {
        pauseMenuPanel.gameObject.SetActive(toggle);
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
        gameService.soundService.PlaySfx(SoundType.ButtonClick);
        ToggleMainMenuPanel(false);
        eventServiceScriptableObject.OnStartGame.RaiseEvent();
    }

    public void OnPauseButtonClicked()
    {
        TogglePauseMenuPanel(true);
        gameService.soundService.PlaySfx(SoundType.ButtonClick);
    }

    public void OnExitButtonClicked()
    {
        gameService.soundService.PlaySfx(SoundType.ButtonClick);
        eventServiceScriptableObject.OnExitGame.RaiseEvent();
    }

    public void OnWaveButtonClicked()
    {
        gameService.soundService.PlaySfx(SoundType.ButtonClick);
        startWaveButton.interactable = false;
        ShowWaveNumber();
        gameService.StartWave();
    }


}
