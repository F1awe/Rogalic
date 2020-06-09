using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    private GameObject player;


    void Start()
    {
        player = transform.parent.gameObject;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit();
            Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
            playerBody.velocity = new Vector2(playerBody.velocity.x, bounceForce);
        }
    }
}
