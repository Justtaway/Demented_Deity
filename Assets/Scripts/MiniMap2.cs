using UnityEngine;
using System.Collections;

public class MiniMap2 : MonoBehaviour
{
    public Camera camera;
    public Camera camera2;
    void Start()
    {
        camera.enabled = true;
        camera2.enabled = false;
    }
    void Update()
    {
        //This will toggle the enabled state of the two cameras between true and false each time
        if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            //camera.enabled = !camera.enabled;
            camera2.enabled = !camera2.enabled;
            //camera.enabled = false;
            //camera2.enabled = true;
        }
    }
}