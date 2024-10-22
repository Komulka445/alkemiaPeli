using UnityEngine;

public class CameraPOVChange : MonoBehaviour
{
    public Camera playerCamera;
    private Vector3 FirstPersonPOV;
    private Vector3 ThirdPersonPOV;
    private Vector3 currentPOS;
    private bool moveToThird = false;
    private bool moveToFirst = false;
    private float speed = 2.0f;
    private float rotateTime = 2.0f;
    private bool puppetTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        FirstPersonPOV = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ThirdPersonPOV = new Vector3(transform.position.x, transform.position.y + 6.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        currentPOS = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.P) && currentPOS != ThirdPersonPOV)
        {
            puppetTurn = true;
            moveToThird = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && currentPOS == ThirdPersonPOV)
        {
            moveToFirst = true;
        }
        if (moveToFirst == true)
        {
            playerCamera.transform.position = Vector3.MoveTowards(transform.position, FirstPersonPOV, speed * Time.deltaTime);
            if (currentPOS == FirstPersonPOV) { moveToFirst = false; }
        }
        if (moveToThird == true)
        {
            playerCamera.transform.position = Vector3.MoveTowards(transform.position, ThirdPersonPOV, speed * Time.deltaTime);
            if (puppetTurn == true)
            {
                Debug.Log("läpikäännös" + rotateTime);
                playerCamera.transform.Rotate(Time.deltaTime * -45, 0, 0);
                rotateTime -= Time.deltaTime;
                if (rotateTime <= 0.0f)
                {
                    Debug.Log("falsaaläpi");
                    puppetTurn = false;
                }
            }
            if (currentPOS == ThirdPersonPOV) { moveToThird = false; }
        }
    }
}