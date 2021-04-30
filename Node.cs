using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public Vector3 worldPos;
    public GameObject brick;
    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
        worldPos = new Vector3(x, y, 0);
    }
    public override int GetHashCode()
    {
        return x * 11 + y * 211;
    }
    
    public override bool Equals(object obj)
    {
        return x == ((Node)obj).x && y == ((Node)obj).y;
    }
}
