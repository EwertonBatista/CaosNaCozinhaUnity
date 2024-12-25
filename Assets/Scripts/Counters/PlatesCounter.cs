using System;
using Unity.Netcode;
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
        if (!IsServer)
        {
            return;
        }
        spawnPlatesTimer += Time.deltaTime;
        if(GameManager.Instance.IsGamePlaying() && spawnPlatesTimer > spawnPlatesTimerMax)
        {
            spawnPlatesTimer = 0f;
            if(plateSpawnedAmount < plateSpawnedAmountMax)
            {
                SpawnPlateServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlateServerRpc()
    {
        SpawnPlateClientRpc();
    }

    [ClientRpc]
    private void SpawnPlateClientRpc()
    {
        plateSpawnedAmount++;
        OnPlateSpawned?.Invoke(this, new EventArgs());
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(plateSpawnedAmount > 0) {
                
                KitchenObject.SpawnKitchenObject(plateKitchenObjectsSO, player);
                InteractServerRpc();
            }
        }
        else
        {

        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractServerRpc()
    {
        InteractClientRpc();
    }

    [ClientRpc]
    private void InteractClientRpc()
    {
        plateSpawnedAmount--;
        OnPlateRemoved?.Invoke(this, new EventArgs());
    }
}
