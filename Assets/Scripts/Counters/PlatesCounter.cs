using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectsSO plateKitchenObjectsSO;
    private float spawnPlatesTimer;
    private float spawnPlatesTimerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 5;

    private void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        if(GameManager.Instance.IsGamePlaying() && spawnPlatesTimer > spawnPlatesTimerMax)
        {
            spawnPlatesTimer = 0f;
            if(plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, new EventArgs());
                //KitchenObject.SpawnKitchenObject(plateKitchenObjectsSO, this);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(plateSpawnedAmount > 0) {
                plateSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectsSO, player);
                OnPlateRemoved?.Invoke(this, new EventArgs());
            }
        }
        else
        {

        }
    }
}
