using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class DestroyBrick : MonoBehaviour
{
    public Camera cam;
    public float speedCam = 5f;

    private gamemanager _gamemanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gamemanager = GetComponent<gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            Ray cursorRay = cam.ScreenPointToRay(screenPos);
            bool rayHitSomething = Physics.Raycast(cursorRay, out RaycastHit screenHitInfo);
            if (rayHitSomething && screenHitInfo.transform.gameObject.CompareTag("Brick"))
            {
                Destroy(screenHitInfo.transform.gameObject);
            }
            else if (rayHitSomething && screenHitInfo.transform.gameObject.CompareTag("QuestionBlock"))
            {
                _gamemanager.addCoin();
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            cam.transform.Translate(Vector3.right * (Time.deltaTime * speedCam));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            cam.transform.Translate(Vector3.left * (Time.deltaTime * speedCam));
        }
    }
}
