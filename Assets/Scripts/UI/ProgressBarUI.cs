using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHashProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHashProgress>();
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        HideBar();
    }

    private void HasProgress_OnProgressChanged(object sender, IHashProgress.OnProgressChangedEventArts e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            HideBar();
        }
        else
        {
            ShowBar();
        }
    }

    private void ShowBar() { 
        gameObject.SetActive(true);
    }

    private void HideBar()
    {
        gameObject.SetActive(false);
    }
}
