using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject[] Buildings;
    public Vector3Int lastPos;
    public float curPreviewTime;

    [SerializeField]
    private GameObject previewObject;

    void Update()
    {
        MouseRay();
        PreviewMove();
    }
    void MouseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, Mathf.Infinity,LayerMask.GetMask("Ground")))
        {
            Vector3Int lerpPos 
                = Utility.RoundVector(hit.point);
            if(lastPos != lerpPos) curPreviewTime = 0;
            lastPos = lerpPos;
        }
    }
    void PreviewMove()
    {
        previewObject.transform.position 
            = Vector3.Lerp(previewObject.transform.position,lastPos,Utility.OutQuad(curPreviewTime));

        curPreviewTime += Time.deltaTime;
    }
}
