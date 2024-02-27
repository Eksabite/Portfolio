using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesignerScript : MonoBehaviour
{
    public static LevelDesignerScript instance;
    public GameObject[] Bricks;


    private int bricksToSpawn;
    private int bricksSpawned;
    private int rowsToSpawn;
    private int rowsSpawned;
    private float deltaX = 1.2f;
    private float deltaY = 0.75f;
    private float posX = 0;
    private float posY = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }
    public void GenerateLevel()
    {

        
        posY = 0;
        rowsSpawned = 0;
        rowsToSpawn = Random.Range(3,8);

        while (rowsSpawned < rowsToSpawn)
        {
            posX = 0;
            bricksSpawned = 0;
            bricksToSpawn = Random.Range(2, 5);
            while (bricksSpawned < bricksToSpawn)
            {
                if (GameManagerScript.instance != null)
                {
                    if (rowsSpawned == 0 && GameManagerScript.instance.level > 3)
                    {
                        Instantiate(Bricks[1], new Vector3(posX, posY, 0), new Quaternion(0, 0, 0, 0));
                        if (posX != 0)
                        {
                            Instantiate(Bricks[1], new Vector3(-posX, posY, 0), new Quaternion(0, 0, 0, 0));
                        }
                    }
                    else
                    {
                        GameManagerScript.instance.AddToList(Instantiate(Bricks[0], new Vector3(posX, posY, 0), new Quaternion(0, 0, 0, 0)));    
                        if (posX != 0)
                        {
                            GameManagerScript.instance.AddToList(Instantiate(Bricks[0], new Vector3(-posX, posY, 0), new Quaternion(0, 0, 0, 0)));
                        }
                    }
                }
                else
                {
                    GameManagerScript.instance.AddToList(Instantiate(Bricks[0], new Vector3(posX, posY, 0), new Quaternion(0, 0, 0, 0)));
                    if (posX != 0)
                    {
                        GameManagerScript.instance.AddToList(Instantiate(Bricks[0], new Vector3(-posX, posY, 0), new Quaternion(0, 0, 0, 0)));
                    }
                }
                
          
                posX += deltaX;                
                bricksSpawned++;
            }
            posY += deltaY;
            rowsSpawned++;
        }
        
    
    }

}
