using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector3Int RoundVector(Vector3 pos)
    {
        return new Vector3Int(Mathf.RoundToInt(pos.x),Mathf.RoundToInt(pos.y),Mathf.RoundToInt(pos.z));
    }
    public static float OutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }
}
