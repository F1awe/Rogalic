using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().AddMoney(value);

            Destroy(gameObject);
        }
    }

}
