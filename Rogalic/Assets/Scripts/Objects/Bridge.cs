using UnityEngine;

public class Bridge : MonoBehaviour, Interactable
{
    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }
}
