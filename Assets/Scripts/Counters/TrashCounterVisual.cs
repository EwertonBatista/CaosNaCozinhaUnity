using UnityEngine;

public class TrashCounterVisual : MonoBehaviour
{
    private const string TRASH_THROW = "TrashThrow";
    [SerializeField] private TrashCounter trashCounter;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        trashCounter.OnTrashThrow += TrashCounter_OnTrashThrow;
    }

    private void TrashCounter_OnTrashThrow(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TRASH_THROW);
    }
}
