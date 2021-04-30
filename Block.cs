using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{



    public GameObject[] bricks;
    public bool isMoving;



    public abstract void RemoveAvailableNodes();

    public abstract void Rotate90();


}
