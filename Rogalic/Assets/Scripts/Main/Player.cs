using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    private const string MONEY_KEY = "MONEY";
    private const string LIVES_KEY = "LIVES";

    private const string USE_KEYBORD_KEY = "USE_KEYBORD_KEY";

    public string wordToSave;

    private GameController gameController;
    private StatusInterface statusInterface;
    private GameObject controllInterface;


    [SerializeField] private float speed;
    [SerializeField] private float jumpHight;

    [SerializeField] private int maxLivesCount;
    private int currentLivesCount;

    private Rigidbody2D rb;
    private Animator animator;

    private Interactable interactableObj;

    private bool grounded = true;
    private bool wasRunning;
    private bool isDead;

    private int money;

    public bool useKeyBoard;

    public float Speed { get => speed; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        statusInterface = GameObject.FindGameObjectWithTag("StatusInterface").GetComponent<StatusInterface>();

        controllInterface = GameObject.FindGameObjectWithTag("ControllInterface");
        if (controllInterface != null)
        {
            controllInterface.GetComponent<ControllInterface>().Player = this;
        }

        LoadData();
    }

    void Update()
    {
        if(isDead) { return; }

        if (useKeyBoard)
        {
            float direction = Input.GetAxis("Horizontal");

            Move(direction);

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithObj();
            }
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            gameController.ResetData();
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(MONEY_KEY, money);
        PlayerPrefs.SetInt(LIVES_KEY, currentLivesCount);

        PlayerPrefs.SetInt(USE_KEYBORD_KEY, useKeyBoard ? 1 : 0);

    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(MONEY_KEY))
        {
            PlayerPrefs.GetInt(MONEY_KEY);
        }

        currentLivesCount = PlayerPrefs.HasKey(LIVES_KEY) ? PlayerPrefs.GetInt(LIVES_KEY) : 0;

        if (PlayerPrefs.HasKey(USE_KEYBORD_KEY))
        {
            useKeyBoard = PlayerPrefs.GetInt(USE_KEYBORD_KEY) == 1;
        }

        statusInterface.ShowMoneyCount(money);

        currentLivesCount = currentLivesCount == 0 ? maxLivesCount : currentLivesCount;
        statusInterface.ShowLivesCount(currentLivesCount);
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey(MONEY_KEY);
        money = 0;

        PlayerPrefs.DeleteKey(USE_KEYBORD_KEY);
        useKeyBoard = true;

        PlayerPrefs.DeleteKey(LIVES_KEY);
        currentLivesCount = maxLivesCount;

        statusInterface.ShowMoneyCount(money);
        statusInterface.ShowLivesCount(currentLivesCount);
    }

    public void Jump()
    {
        AnimateJump();

        rb.velocity = new Vector2(0, jumpHight);
    }

    private void AnimateJump()
    {
        animator.SetTrigger("StopAction");
        animator.ResetTrigger("StopAction");
        animator.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (!grounded)
            {
                animator.ResetTrigger("Jump");
                animator.SetTrigger("Landing");
            }

            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            AnimateJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            interactableObj = collision.gameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            interactableObj = null;
        }
    }

    public void InteractWithObj()
    {
        if (interactableObj != null)
        {
            interactableObj.Interact();
        }
    }

    public void Move(float direction)
    {
        wasRunning = direction != 0;

        animator.SetBool("Run", grounded && wasRunning);

        if (!wasRunning) { return; }

        if (direction > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction < 0)
        {
           GetComponent<SpriteRenderer>().flipX = true;
        }

        float step = speed * direction;

        rb.velocity = new Vector2(step, rb.velocity.y);

    }

    public void StopMoving()
    {
        StartCoroutine(StopRoutine());
    }

    private IEnumerator StopRoutine()
    {
        float lastDir = rb.velocity.x;
        float stopDir = lastDir > 0 ? 0.1f : -0.1f;

        Move(stopDir);

        yield return new WaitForSeconds(0.1f);

        Move(0);
    }

    public void Dead()
    {
        if (isDead) { return; }

        isDead = true;
        animator.SetBool("Run", false);

        animator.SetTrigger("StopAction");
        animator.SetInteger("Hurt", 1);

        ChangeLivesCount(-1);
    }

    private void ChangeLivesCount(int count)
    {
        currentLivesCount += count;
        if (currentLivesCount < 0)
        {
            currentLivesCount = 0;
        } 
        else if (currentLivesCount > maxLivesCount)
        {
            currentLivesCount = maxLivesCount;
        }

        statusInterface.ShowLivesCount(currentLivesCount);

        SaveData();

        if (currentLivesCount <= 0)
        {
            gameController.ResetData();
        } 
        else if (count < 0) 
        { 
            gameController.LoseGame(); 
        }
    }

    public void AddMoney(int count)
    {
        money += count;

        statusInterface.ShowMoneyCount(money);
    }
}
