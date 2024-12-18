using UnityEngine;

public class ContainerCounterSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private KitchenObjectsSO objectSO;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = objectSO.sprite;
    }
}
