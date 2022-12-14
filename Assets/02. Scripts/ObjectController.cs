using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ObjectController : MonoBehaviour
{
    public MeshFilter coinMesh;
    public MeshCollider meshCollider;
    public int curGrade = 0;
    public int coinValue;

    public void NextLevel(int grade)
    {
        coinMesh.mesh = ObjPool.instance.coinMeshes[grade];
        meshCollider.sharedMesh = ObjPool.instance.coinMeshes[grade];
        coinValue = 0;
        coinValue += grade * 5;
    }
    public void CoinInitialize()
    {
        curGrade = 0;
        coinValue = curGrade + 1;
        coinMesh.mesh = ObjPool.instance.coinMeshes[curGrade];
        meshCollider.sharedMesh = ObjPool.instance.coinMeshes[curGrade];
    }

}
