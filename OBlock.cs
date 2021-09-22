using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBlock : Block
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
        color = Color.blue;
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


        GameObject g1 = Instantiate(brick, new Vector2(parent.x + 1, parent.y), Quaternion.identity) as GameObject;
        g1.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
        g1.GetComponent<Renderer>().material.color = color;
        g1.transform.parent = this.transform;
        bricks[1] = g1;



        GameObject g2 = Instantiate(brick, new Vector2(parent.x, parent.y + 1), Quaternion.identity) as GameObject;
        g2.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
        g2.GetComponent<Renderer>().material.color = color;
        g2.transform.parent = this.transform;
        bricks[2] = g2;

        GameObject g3 = Instantiate(brick, new Vector2(parent.x + 1, parent.y + 1), Quaternion.identity) as GameObject;
        g3.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
        g3.GetComponent<Renderer>().material.color = color;
        g3.transform.parent = this.transform;
        bricks[3] = g3;

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




    }






}
