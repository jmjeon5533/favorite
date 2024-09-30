using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance { get; private set; }
    public GameObject[] buildPrefab;
    public Vector3Int lastPos;
    public float curPreviewTime = -1;
    public int[,] grid;
    public GameObject[,] gridObj;
    public int previewIndex = -1;

    [SerializeField]
    private GameObject previewObject;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        grid = new int[100, 100];
        gridObj = new GameObject[100, 100];
    }
    void Update()
    {
        MouseRay();
        PreviewMove();
    }
    void MouseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3Int lerpPos
                = Utility.RoundVector(hit.point);
            if (lastPos != lerpPos) curPreviewTime = 0;
            lastPos = lerpPos;
        }
    }
    void PreviewMove()
    {
        previewObject.transform.position
            = Vector3.Lerp(previewObject.transform.position, lastPos, Utility.OutQuad(curPreviewTime));
        curPreviewTime += Time.deltaTime * 2;
    }
    public void BuildObject()
    {
        if (previewIndex != -1)
        {
            print($"{lastPos.x},{lastPos.z}");
            print($"{grid[lastPos.x, lastPos.z]}");
            if (grid[lastPos.x, lastPos.z] == 0)
            {

                grid[lastPos.x, lastPos.z] = 1;
                gridObj[lastPos.x, lastPos.z]
                = Instantiate(buildPrefab[previewIndex], lastPos, Quaternion.identity);
            }
        }
    }
    public void RemoveObject()
    {
        if (grid[lastPos.x, lastPos.z] == 1)
        {
            grid[lastPos.x, lastPos.z] = 0;
            Destroy(gridObj[lastPos.x, lastPos.z]);
        }
    }
}
