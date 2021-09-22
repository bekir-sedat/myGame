using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZBlock : Block
{

    // public GameObject[] bricks; from Inheritance
    public GameObject brick;
    // public bool isMoving; From Inheritance
    public Color color;
    public BlockType blockType;

    public bool isClockWise;
    static float timer;
    private int direction = 1;
    private void Awake()
    {

        isMoving = true;
        color = Color.green;
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

        Vector2 parent = transform.position;


        GameObject g0 = Instantiate(brick, new Vector2(parent.x, parent.y), Quaternion.identity) as GameObject;
        g0.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
        g0.GetComponent<Renderer>().material.color = color;
        g0.transform.parent = this.transform;
        bricks[0] = g0;


        GameObject g1 = Instantiate(brick, new Vector2(parent.x, parent.y + 1), Quaternion.identity) as GameObject;
        g1.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
        g1.GetComponent<Renderer>().material.color = color;
        g1.transform.parent = this.transform;
        bricks[1] = g1;



        GameObject g2 = Instantiate(brick, new Vector2(parent.x + 1, parent.y + 1), Quaternion.identity) as GameObject;
        g2.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
        g2.GetComponent<Renderer>().material.color = color;
        g2.transform.parent = this.transform;
        bricks[2] = g2;

        GameObject g3 = Instantiate(brick, new Vector2(parent.x + 1, parent.y + 2), Quaternion.identity) as GameObject;
        g3.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
        g3.GetComponent<Renderer>().material.color = color;
        g3.transform.parent = this.transform;
        bricks[3] = g3;


        g0.name = "0";
        g1.name = "1";
        g2.name = "2";
        g3.name = "3";
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
                //foreach (GameObject g in bricks)
                //{
                //    // if next node available then move here.
                //    g.transform.position += new Vector3(0, -1, 0);

                //}

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

        switch (direction)
        {

            case 1:
                int newX0 = (int)bricks[0].transform.position.x + 1;
                int newY0 = (int)bricks[0].transform.position.y;

                int newX1 = (int)bricks[1].transform.position.x;
                int newY1 = (int)bricks[1].transform.position.y - 1;

                int newX2 = (int)bricks[2].transform.position.x - 1;
                int newY2 = (int)bricks[2].transform.position.y;

                int newX3 = (int)bricks[3].transform.position.x - 2;
                int newY3 = (int)bricks[3].transform.position.y - 1;


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
                direction = 2;
                break;
            case 2:

                int newXX0 = (int)bricks[0].transform.position.x;
                int newYY0 = (int)bricks[0].transform.position.y + 2;

                int newXX1 = (int)bricks[1].transform.position.x + 1;
                int newYY1 = (int)bricks[1].transform.position.y + 1;

                int newXX2 = (int)bricks[2].transform.position.x;
                int newYY2 = (int)bricks[2].transform.position.y;

                int newXX3 = (int)bricks[3].transform.position.x + 1;
                int newYY3 = (int)bricks[3].transform.position.y - 1;


                if (!GameManager.listofAvailableNodes.Contains(new Node(newXX0, newYY0)) || !GameManager.listofAvailableNodes.Contains(new Node(newXX1, newYY1)) || !GameManager.listofAvailableNodes.Contains(new Node(newXX2, newYY2)) || !GameManager.listofAvailableNodes.Contains(new Node(newXX3, newYY3)))
                {

                    return;



                }
                else
                {

                    bricks[0].transform.position = new Vector3(newXX0, newYY0, 0);
                    bricks[1].transform.position = new Vector3(newXX1, newYY1, 0);
                    bricks[2].transform.position = new Vector3(newXX2, newYY2, 0);
                    bricks[3].transform.position = new Vector3(newXX3, newYY3, 0);

                }
                direction = 3;
                break;

            case 3:

                int newXXX0 = (int)bricks[0].transform.position.x - 2;
                int newYYY0 = (int)bricks[0].transform.position.y - 1;

                int newXXX1 = (int)bricks[1].transform.position.x - 1;
                int newYYY1 = (int)bricks[1].transform.position.y;

                int newXXX2 = (int)bricks[2].transform.position.x;
                int newYYY2 = (int)bricks[2].transform.position.y - 1;

                int newXXX3 = (int)bricks[3].transform.position.x + 1;
                int newYYY3 = (int)bricks[3].transform.position.y;


                if (!GameManager.listofAvailableNodes.Contains(new Node(newXXX0, newYYY0)) || !GameManager.listofAvailableNodes.Contains(new Node(newXXX1, newYYY1)) || !GameManager.listofAvailableNodes.Contains(new Node(newXXX2, newYYY2)) || !GameManager.listofAvailableNodes.Contains(new Node(newXXX3, newYYY3)))
                {

                    return;



                }
                else
                {

                    bricks[0].transform.position = new Vector3(newXXX0, newYYY0, 0);
                    bricks[1].transform.position = new Vector3(newXXX1, newYYY1, 0);
                    bricks[2].transform.position = new Vector3(newXXX2, newYYY2, 0);
                    bricks[3].transform.position = new Vector3(newXXX3, newYYY3, 0);

                }
                direction = 4;
                break;

            case 4:

                int newXXXX0 = (int)bricks[0].transform.position.x + 1;
                int newYYYY0 = (int)bricks[0].transform.position.y - 1;

                int newXXXX1 = (int)bricks[1].transform.position.x;
                int newYYYY1 = (int)bricks[1].transform.position.y;

                int newXXXX2 = (int)bricks[2].transform.position.x + 1;
                int newYYYY2 = (int)bricks[2].transform.position.y + 1;

                int newXXXX3 = (int)bricks[3].transform.position.x;
                int newYYYY3 = (int)bricks[3].transform.position.y + 2;


                if (!GameManager.listofAvailableNodes.Contains(new Node(newXXXX0, newYYYY0)) || !GameManager.listofAvailableNodes.Contains(new Node(newXXXX1, newYYYY1)) || !GameManager.listofAvailableNodes.Contains(new Node(newXXXX2, newYYYY2)) || !GameManager.listofAvailableNodes.Contains(new Node(newXXXX3, newYYYY3)))
                {

                    return;



                }
                else
                {

                    bricks[0].transform.position = new Vector3(newXXXX0, newYYYY0, 0);
                    bricks[1].transform.position = new Vector3(newXXXX1, newYYYY1, 0);
                    bricks[2].transform.position = new Vector3(newXXXX2, newYYYY2, 0);
                    bricks[3].transform.position = new Vector3(newXXXX3, newYYYY3, 0);

                }
                direction = 1;
                break;





        }






    }
}
