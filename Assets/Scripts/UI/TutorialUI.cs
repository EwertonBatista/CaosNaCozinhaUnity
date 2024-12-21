using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteract;
    [SerializeField] private TextMeshProUGUI keyInteractGamepad;
    [SerializeField] private TextMeshProUGUI keyInteractAlternate;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateGamepad;
    [SerializeField] private TextMeshProUGUI keyPause;
    [SerializeField] private TextMeshProUGUI keyPauseGamepad;

    private void Start()
    {
        UpdateVisual();
        Show();
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_up);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_left);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_down);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_right);
        keyInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractGamepad.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        keyInteractAlternate.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyInteractAlternateGamepad.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        keyPause.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyPauseGamepad.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
