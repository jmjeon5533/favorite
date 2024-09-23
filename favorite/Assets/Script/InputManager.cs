using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float zoomScale = 10;

    [SerializeField] private Transform camPivot;
    private Transform camRot;
    private Transform camObj;
    private float camSpeed = 25;

    public float curCamMoveTime;
    void Start()
    {
        camRot = camPivot.GetChild(0);
        camObj = camRot.GetChild(0);
    }
    void Update()
    {
        CamMove();
    }
    void CamMove()
    {
        var moveX = Input.GetAxis("Horizontal") * Time.deltaTime * camSpeed;
        var moveY = Input.GetAxis("Vertical") * Time.deltaTime * camSpeed;
        
        var wheel = -Input.GetAxisRaw("Mouse ScrollWheel") * 1000 * Time.deltaTime;
        if(zoomScale != zoomScale + wheel) curCamMoveTime = 0;
        zoomScale += wheel;
        zoomScale = Mathf.Clamp(zoomScale,3,30);
        
        camPivot.transform.position += camPivot.transform.TransformDirection(new Vector3(1 * moveX,0,1 * moveY));
        var camY = Mathf.Lerp(camObj.transform.localPosition.y,zoomScale,Utility.OutQuad(curCamMoveTime));
        camObj.transform.localPosition = new Vector3(0,camY,0);
        curCamMoveTime += Time.deltaTime;

        if (Input.GetMouseButton(2))
        {
            var x = -Input.GetAxis("Mouse Y");
            var y = Input.GetAxis("Mouse X");

            camPivot.transform.eulerAngles += new Vector3(0, y);
            camRot.transform.eulerAngles += new Vector3(x, 0);
        }

    }
}
