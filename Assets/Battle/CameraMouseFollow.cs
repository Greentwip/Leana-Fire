using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    // Start is called before the first frame update
    //Camera camera;

    public int Boundary = 50; // distance from edge scrolling starts
    public int speed = 5;
    private int theScreenWidth = 0;
    private int theScreenHeight = 0;

    void Start()
    {
        //camera = GetComponent<Camera>();
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;

    }

    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xAxisValue, zAxisValue, 0.0f));

#if UNITY_EDITOR
        if (Input.mousePosition.x <= 1 || Input.mousePosition.y <= 1 || Input.mousePosition.x >= UnityEditor.Handles.GetMainGameViewSize().x - 1 || Input.mousePosition.y >= UnityEditor.Handles.GetMainGameViewSize().y - 1) return;
#else
    if (Input.mousePosition.x <= 1 || Input.mousePosition.y <= 1 || Input.mousePosition.x >= Screen.width - 1 || Input.mousePosition.y >= Screen.height - 1) return;
#endif

        float xTranslate = 0.0f;
        float yTranslate = 0.0f;

        if (Input.mousePosition.x > theScreenWidth - Boundary)
        {
            xTranslate = speed * Time.deltaTime;
        }
        if (Input.mousePosition.x < 0 + Boundary)
        {
            xTranslate = -speed * Time.deltaTime;
        }
        if (Input.mousePosition.y > theScreenHeight - Boundary)
        {
            yTranslate = speed * Time.deltaTime;
        }
        if (Input.mousePosition.y < 0 + Boundary)
        {
            yTranslate = -speed * Time.deltaTime;
        }

        transform.Translate(xTranslate, yTranslate, 0.0f); 

        if (transform.position.y > 170)
        {
            transform.Translate(0.0f, -yTranslate, 0.0f); 
        }
        
        if (transform.position.y < 10)
        {
            transform.Translate(0.0f, -yTranslate, 0.0f); 
        }
        
        if (transform.position.x > 170)
        {
            transform.Translate(-xTranslate, 0.0f, 0.0f); 
        }
        
        if (transform.position.x < -60)
        {
            transform.Translate(-xTranslate, 0.0f, 0.0f); 
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            GetComponent<Camera>().orthographicSize -= 10;
            if(GetComponent<Camera>().orthographicSize <= 20)
            {
                GetComponent<Camera>().orthographicSize = 20;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            GetComponent<Camera>().orthographicSize += 10;
            if (GetComponent<Camera>().orthographicSize >= 50)
            {
                GetComponent<Camera>().orthographicSize = 50;
            }
        }


    }
}
