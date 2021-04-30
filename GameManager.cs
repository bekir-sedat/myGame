using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject backGroundQuad;
    public static Node[,] grid;
    public static List<Node> listofAvailableNodes = new List<Node>();



    public static float moveTime = 0.5f;
    private Camera camera1;

    private GameObject currentBlock;
    private Vector2 spawnPosition;
    private static List<int> YList = new List<int>();

    public int width = 10;
    public int height = 20;
    private GameObject backgroundQuadHolder;

    #region Initilazition

    private void Awake()
    {
        camera1 = Camera.main;
        spawnPosition = new Vector2((int)width / 2 - 1, (int)height - 2);
        backgroundQuadHolder = new GameObject("Map");
        CreateBackGroundImage();

        grid = new Node[width, height + 6];
        SpawnNewBlock();
        InitializeListOfAvailableNodes();
        PlaceCamera();


    }

    private void PlaceCamera()
    {

        Vector3 cameraPosition = new Vector3((int)width / 2, (int)height / 2, -10);
        camera1.transform.position = cameraPosition;
    }

    private void CreateBackGroundImage()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                GameObject g = Instantiate(backGroundQuad, new Vector3(x, y, 100), Quaternion.identity) as GameObject;
                g.transform.parent = backgroundQuadHolder.transform;

            }
        }

    }

    private void InitializeListOfAvailableNodes()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height + 6; y++)
            {
                Node n = new Node(x, y);
                grid[x, y] = n;

                listofAvailableNodes.Add(n);

            }
        }
    }


    #endregion



    #region Update GetInput
    private void Update()
    {
        if (currentBlock.GetComponent<Block>().isMoving)
        {

            GetInput();
        }
        else
        {
            RemoveLines();

            if (YList.Count != 0)
            {
                MoveLinesFromTopToBottom();
                UpdateAvailableNodeList();
            }
            SpawnNewBlock();
        }
    }



    void GetInput()
    {



        if (Input.GetButtonDown("Left"))
        {


            for (int i = 0; i < 4; i++)
            {

                GameObject g = currentBlock.GetComponent<Block>().bricks[i];

                if ((int)g.transform.position.x == 0)
                {

                    return;
                }

                if (!GameManager.listofAvailableNodes.Contains(grid[(int)g.transform.position.x - 1, (int)g.transform.position.y]))
                {

                    return;

                }

            }


            currentBlock.transform.position += Vector3.left;


        }
        else if (Input.GetButtonDown("Right"))
        {
            for (int i = 0; i < 4; i++)
            {

                GameObject g = currentBlock.GetComponent<Block>().bricks[i];

                if ((int)g.transform.position.x == width - 1)
                {

                    return;  ///  may be BREAK HERE *******************************************************
                }

                if (!GameManager.listofAvailableNodes.Contains(grid[(int)g.transform.position.x + 1, (int)g.transform.position.y]))
                {


                    return;

                }

            }

            // if next node available then move here.
            currentBlock.transform.position += Vector3.right;


        }
        else if (Input.GetButton("Down"))
        {



            float dump = 0;


            do
            {
                dump += Time.deltaTime / 4;


                for (int i = 0; i < 4; i++)
                {

                    GameObject g = currentBlock.GetComponent<Block>().bricks[i];

                    if ((int)g.transform.position.y == 0)
                    {

                        return;  ///  may be BREAK HERE *******************************************************
                    }

                    if (!GameManager.listofAvailableNodes.Contains(grid[(int)g.transform.position.x, (int)g.transform.position.y - 1]))
                    {


                        return;

                    }

                }
                // if next node available then move here.
                currentBlock.transform.position += Vector3.down;


            } while (dump < 0.000000000005f);
        }




        else if (Input.GetKeyDown(KeyCode.Space))
        {


            currentBlock.GetComponent<Block>().Rotate90();
        }

    }


    #endregion



    #region Utilities



    void SpawnNewBlock()
    {

        int random = UnityEngine.Random.Range(0, blocks.Length);

        currentBlock = Instantiate(blocks[random], spawnPosition, Quaternion.identity);


    }






    private void RemoveLines()
    {


        for (int y = 0; y < height; y++)
        {
            int count = 0;

            for (int x = 0; x < width; x++)
            {

                if (grid[x, y].brick != null)
                {
                    count++;

                }
                else
                {
                    break;
                }

                if (count == width)
                {
                    YList.Add(y);
                    // for scoring purpose use YList
                    // clear line objects
                    for (int i = 0; i < width; i++)
                    {

                        Destroy(GameManager.grid[i, y].brick);
                        listofAvailableNodes.Add(grid[i, y]);
                    }

                    // update availbale node list.
                }
            }



        }

    }

    private void MoveLinesFromTopToBottom()
    {
        for (int y = YList[0] + YList.Count; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y].brick != null)
                {


                    GameObject b = grid[x, y].brick;
                    b.transform.position += Vector3.down * YList.Count;

                    int xPos = (int)b.transform.position.x;
                    int yPos = (int)b.transform.position.y;

                    listofAvailableNodes.Add(grid[x, y]);  // check this 
                    grid[x, y].brick = null;
                    listofAvailableNodes.Remove(grid[xPos, yPos]);
                    grid[xPos, yPos].brick = b;

                }


            }
        }


        YList.Clear();
    }

    private void UpdateAvailableNodeList()
    {


    }




    #endregion
}
