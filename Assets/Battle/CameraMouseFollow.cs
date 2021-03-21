using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        camera.transform.Translate(new Vector3(xAxisValue, zAxisValue, 0.0f));
    }
}
