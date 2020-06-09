using UnityEngine;

public class Chest : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject loot;
    [SerializeField] private Transform shootPos;
    [SerializeField] private float shootForce;

    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    public void PutLoot()
    {
        GameObject obj = Instantiate(loot, shootPos.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0.1f, shootForce);
    }

}
