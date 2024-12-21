using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Transform pressToRebind;

    [SerializeField] private Button soundsFXsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundsFXsButtonText;
    [SerializeField] private TextMeshProUGUI musicButtonText;

    // Buttons Key Binding
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interact;
    [SerializeField] private Button interactAlternate;
    [SerializeField] private Button pause;
    [SerializeField] private Button gamepadInteract;
    [SerializeField] private Button gamepadInteractAlternate;
    [SerializeField] private Button gamepadPause;
     
    // Buttons Texts
    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAlternateButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private TextMeshProUGUI gamepadInteractButtonText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateButtonText;
    [SerializeField] private TextMeshProUGUI gamepadPauseButtonText;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        soundsFXsButton.onClick.AddListener(() => { SoundManager.Instance.ChangeVolume(); UpdateVisual(); });
        musicButton.onClick.AddListener(() =>{MusicManager.Instance.ChangeVolume(); UpdateVisual(); });
        closeButton.onClick.AddListener(() =>{Hide(); onCloseButtonAction(); });
        moveUpButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_up);});
        moveDownButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_down);});
        moveLeftButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_left);});
        moveRightButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_right);});
        interact.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Interact);});
        interactAlternate.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.InteractAlternate);});
        pause.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Pause);});
        gamepadInteract.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Interact); });
        gamepadInteractAlternate.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_InteractAlternate); });
        gamepadPause.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Pause); });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
       Hide();
    }

    private void UpdateVisual()
    {
        soundsFXsButtonText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicButtonText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_up);
        moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_down);
        moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_left);
        moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_right);
        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        soundsFXsButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebind.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebind.gameObject.SetActive(false);
    }

    private void HideAndUpdateVisual()
    {
        HidePressToRebindKey();
        UpdateVisual();
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, HideAndUpdateVisual);
    }
}
