using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }
        else
        {
            Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        }
    }

    private void Player_OnAnyPlayerSpawned(object sender, EventArgs e)
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChanged -= Player_OnSelectedCounterChanged;
            Player.LocalInstance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            ShowGameObject();
        }
        else
        {
            HideGameObject();
        }
    }

    private void ShowGameObject()
    {
        for (int i = 0; i < visualGameObjectArray.Length; i++)
        {
            visualGameObjectArray[i].SetActive(true);
        }
    }

    private void HideGameObject()
    {
        for (int i = 0; i < visualGameObjectArray.Length; i++)
        {
            visualGameObjectArray[i].SetActive(false);

        }
    }
}
