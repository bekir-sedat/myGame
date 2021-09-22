using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBlock : Block
{


    // public GameObject[] bricks; from Inheritance
    public GameObject brick;
    // public bool isMoving; From Inheritance
    public Color color;
    public BlockType blockType;

    public bool isClockWise;
    static float timer;

    private void Awake()
    {
        isClockWise = true;
        isMoving = true;
        color = Color.red;
        bricks = new GameObject[4];
        PlaceBricks();

        blockType = BlockType.LONG;
    }

    private void Update()
    {
        AutoDown();
    }




    public void PlaceBricks()
    {


        for (int i = 0; i < 4; i++)
        {
            Vector2 parent = transform.position;
            GameObject g = Instantiate(brick, new Vector2(parent.x, parent.y + i), Quaternion.identity) as GameObject;


            g.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
            g.GetComponent<Renderer>().material.color = color;
            g.transform.parent = this.transform;
            bricks[i] = g;


        }
    }



    public enum BlockType
    {


        LONG, L, R_L, Z, S, E, O


    }


    void AutoDown()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;


            if (timer > GameManager.moveTime)
            {

                for (int i = 0; i < 4; i++)
                {

                    GameObject g = bricks[i];



                    if (!GameManager.listofAvailableNodes.Contains(new Node((int)g.transform.position.x, (int)g.transform.position.y - 1)))
                    {

                        isMoving = false;
                        RemoveAvailableNodes();
                        return;

                    }

                }

                transform.position += new Vector3(0, -1, 0);


                timer = 0;

            }

        }
    }

    public override void RemoveAvailableNodes()
    {
        for (int i = 0; i < 4; i++)
        {

            GameObject g = bricks[i];

            int xx = (int)g.transform.position.x;
            int yy = (int)g.transform.position.y;
            Node node = GameManager.grid[xx, yy];
            node.brick = g;



            GameManager.listofAvailableNodes.Remove(new Node((int)g.transform.position.x, (int)g.transform.position.y));


        }
    }

    public override void Rotate90()
    {


        if (isClockWise)
        {

            int newX0 = (int)bricks[0].transform.position.x + 1;
            int newY0 = (int)bricks[0].transform.position.y + 1;

            int newX1 = (int)bricks[1].transform.position.x;
            int newY1 = (int)bricks[1].transform.position.y;

            int newX2 = (int)bricks[2].transform.position.x - 1;
            int newY2 = (int)bricks[2].transform.position.y - 1;

            int newX3 = (int)bricks[3].transform.position.x - 2;
            int newY3 = (int)bricks[3].transform.position.y - 2;


            if (!GameManager.listofAvailableNodes.Contains(new Node(newX0, newY0)) || !GameManager.listofAvailableNodes.Contains(new Node(newX1, newY1)) || !GameManager.listofAvailableNodes.Contains(new Node(newX2, newY2)) || !GameManager.listofAvailableNodes.Contains(new Node(newX3, newY3)))
            {

                return;



            }
            else
            {

                bricks[0].transform.position = new Vector3(newX0, newY0, 0);
                bricks[1].transform.position = new Vector3(newX1, newY1, 0);
                bricks[2].transform.position = new Vector3(newX2, newY2, 0);
                bricks[3].transform.position = new Vector3(newX3, newY3, 0);

            }


            isClockWise = false;
        }
        else
        {
            int newX0 = (int)bricks[0].transform.position.x - 1;
            int newY0 = (int)bricks[0].transform.position.y - 1;

            int newX1 = (int)bricks[1].transform.position.x;
            int newY1 = (int)bricks[1].transform.position.y;

            int newX2 = (int)bricks[2].transform.position.x + 1;
            int newY2 = (int)bricks[2].transform.position.y + 1;

            int newX3 = (int)bricks[3].transform.position.x + 2;
            int newY3 = (int)bricks[3].transform.position.y + 2;


            if (!GameManager.listofAvailableNodes.Contains(new Node(newX0, newY0)) || !GameManager.listofAvailableNodes.Contains(new Node(newX1, newY1)) || !GameManager.listofAvailableNodes.Contains(new Node(newX2, newY2)) || !GameManager.listofAvailableNodes.Contains(new Node(newX3, newY3)))
            {

                return;



            }
            else
            {

                bricks[0].transform.position = new Vector3(newX0, newY0, 0);
                bricks[1].transform.position = new Vector3(newX1, newY1, 0);
                bricks[2].transform.position = new Vector3(newX2, newY2, 0);
                bricks[3].transform.position = new Vector3(newX3, newY3, 0);

            }




            isClockWise = true;
        }





    }
}
