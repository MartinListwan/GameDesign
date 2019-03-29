using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;

    private static bool cameraExists;

    public BoxCollider2D BoundBox; // each map has it's own!
    Vector3 minBounds;
    Vector3 maxBounds;

    Camera Camera;
    float halfHeight;
    float halfWidth;

	// Use this for initialization
	void Start () {

        //DontDestroyOnLoad(transform.gameObject);

        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SetBounds();
        this.Camera = this.GetComponent<Camera>();
        halfHeight = Camera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }


    // Update is called once per frame
    void Update () {

        targetPos = new Vector3(
            followTarget.transform.position.x,
            followTarget.transform.position.y,
            transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x,minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX,clampedY,this.transform.position.z);
    }

    public void SetBounds()
    {
        BoundBox = GameObject.FindGameObjectWithTag("BoundsBox").GetComponent<BoxCollider2D>();
        minBounds = BoundBox.bounds.min;
        maxBounds = BoundBox.bounds.max;
    }
    private void OnLevelWasLoaded(int level)
    {
        SetBounds();
    }
}
