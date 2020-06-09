using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform enterPoint;
    [SerializeField] private Transform exitPoint;

    private GameObject playerCam;
    private float cameraCorrection;

    private GameObject background;
    [SerializeField] private float bgrBordersWidth;

    private Player player;

    private float distance;
    private float distancePercent;

    private float bgrSpriteWidth;

    void Start()
    {
        playerCam = Camera.main.gameObject;
        background = playerCam.transform.GetChild(0).gameObject;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cameraCorrection = playerCam.transform.position.x - player.transform.position.x;

        SpriteRenderer spr = background.transform.GetChild(0).GetComponent<SpriteRenderer>();
        bgrSpriteWidth = spr.bounds.size.x;

        distance = Vector2.Distance(enterPoint.position, exitPoint.position);
        distancePercent = distance / 100;
    }

    void Update()
    {
        MoveCamera();
        MoveBackground();
    }

    private void MoveCamera()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        float playerX = player.transform.position.x;
        Vector3 curCamraPos = playerCam.transform.position;

        playerCam.transform.position = Vector3.MoveTowards(
            curCamraPos, new Vector3(playerX + cameraCorrection, curCamraPos.y, curCamraPos.z), player.Speed);
    }

    private void MoveBackground()
    {
        float curFinishedPath = Vector2.Distance(enterPoint.position, player.transform.position);
        float finishedPathInPercent = curFinishedPath / distancePercent;

        float offset = finishedPathInPercent / 100 * (bgrSpriteWidth - bgrBordersWidth);

        background.transform.localPosition = new Vector3(-offset,
            background.transform.localPosition.y, background.transform.localPosition.z);
    }
}
