using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    void Start()
    {
        //Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;        
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
