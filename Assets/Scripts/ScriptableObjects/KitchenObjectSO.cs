using UnityEngine;
[CreateAssetMenu(fileName = "New Kitchen Object", menuName = "Kitchen Object", order = 1)]
public class KitchenObjectsSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
