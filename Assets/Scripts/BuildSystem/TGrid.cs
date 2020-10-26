using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGrid<T>
{
    int width, height;
    float cellSize;
    public T[,] gridArray;

    Vector3 originPos;

    public TGrid(int width, int height, float cellSize, Vector3 originPos)
    //if width or height is negative its turned back to its positive equivalent.
    {
        //width = Mathf.Abs(width);
        //height = Mathf.Abs(height);
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new T[width, height];

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int y)
    //This function takes a cell coords and turn it into a world position. [From: Grid, To: World]
    {
        return new Vector3(x,y) * cellSize + originPos;
    }


    #region From World Mouse Position To Grid

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    //This function takes a world position and turn it into the corresponding cell x and y value. [From: World, To: Grid]
    {
        x = Mathf.FloorToInt((worldPosition - originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPos).y / cellSize);
        
    }

    public Vector2Int GetCell(Vector3 worldPosition)
    //This function takes a world position and return the corresponding x and y position in the grid as a Vector2. [From : World, To: Grid]

    //CAREFUL! 
    //This function return a Vector2Int which doesnt throw an error when stored in a Vector2 but it will convert x and y into float nonetheless and might create bugs afterward
    {
        Vector2Int res = new Vector2Int();
        res.x = Mathf.FloorToInt((worldPosition - originPos).x / cellSize);
        res.y = Mathf.FloorToInt((worldPosition - originPos).y / cellSize);
        
        //In the case we are trying to get a cell that doesnt exist in our grind it will return the closest one on the border of the grid instead of throwing an error.
        res.x = Mathf.Clamp(res.x, 0, width-1);
        res.y = Mathf.Clamp(res.y, 0, height-1);

        return res;
    }

    public static Vector3 GetMouseWorldPos()
    //This function returns the mouse positon in the world
    {
        Camera worldCam = Camera.main;
        Vector3 screenPos = Input.mousePosition;
        Vector3 worldPosistion = worldCam.ScreenToWorldPoint(screenPos);
        worldPosistion.z = 0f;
        return worldPosistion;
    }

    public void SetValue(int x, int y, T value){
        x = Mathf.Clamp(x, 0, width-1);
        y = Mathf.Clamp(y, 0, height-1);

        gridArray[x,y] = value;

        Debug.Log($"VALUE OF POS ({x}, {y}) CHANGED SUCCESFULLY AND IS WORTH : {gridArray[x,y]}\tPog");
        
    }

    public void SetValue(Vector3 worldPosition, T value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    //HOW TO USE THIS IN CODE: with a Grid<int> example
    /*
    {
        int width = 8;
        int height = 5;

        float cellSize = 1;  
            IMPORTANT-> the cellsize MUST BE the same value as the tilemap grid for it to work properly with the build system. 
            (maybe ill implement something for that later idk)
        
        Vector3 originPos = new Vector3(5, 5, 0);
            (Whatever you want to be the start of the grid. Make sure the originPos is the same as the bottomleft corner of the map since its the purpose of this value)
            (Additionally, try to make it so x and y are int value (no decimal part) cause idk how it can goes later on in the game if its not and z = 0 is mandatory)
        
        TGrid<int> world = TGrid<int>(width, height, cellSize, originPos);
        int value = 26;
        SetValue(Tgrid<int>.GetMouseWorldPos(), value);
    }

    For this example its just with int but you can use it for example with the tilemap that handles walls in the game as a reference in the script
    and make it so each different value put a different sprite on the map or if the value is different than 0 (or whatever int you want),
    the AI or whatever will be in the game cant go through
    ^ on this note you can add a compositeCollider2d to a tilemap so every sprite which is on the tilemap will have a rigidbody and become an obstacle.



    */


    #endregion
}


//THIS WAS MADE BY THE AMAZING DILEMMA
//LINK TO MY PAYPAL WILL BE ADDED SOON :)
//DONT FORGET TO LIKE AND SUBSCRIBE FOR MORE CONTENT IT HELPS THE CHANNEL A LOT!

