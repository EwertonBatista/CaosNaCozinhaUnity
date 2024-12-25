using System;
using Unity.Netcode;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public event EventHandler OnTrashThrow;
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            KitchenObject.DestroyKitchenObject(player.GetKitchenObject());
            InteractServerRpc();
        }
    }

    [ServerRpc(RequireOwnership =false)]
    private void InteractServerRpc()
    {
        InteractClientRpc();
    }

    [ClientRpc]
    private void InteractClientRpc()
    {
        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        OnTrashThrow?.Invoke(this, EventArgs.Empty);
    }
}
 