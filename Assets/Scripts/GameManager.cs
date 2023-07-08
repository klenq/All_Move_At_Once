using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int width = 4;
    [SerializeField] public int height = 4;
    [SerializeField] private Node nodePrefab;
    [SerializeField] private SpriteRenderer boardPrefab;
    // [SerializeField] private Block blockPrefab;
    [SerializeField] private List<BlockType> types;

    private List<Node> nodes;
    public List<Block> blocks;
    private BlockType GetBlockTypeByColor(string color) => types.First(t => t.colorText == color);

    //Pixel Texture for generating the level
    public Texture2D levelTexture;


    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        nodes = new List<Node>();
        blocks = new List<Block>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var node = Instantiate(nodePrefab, new Vector2(x,y), Quaternion.identity);
                nodes.Add(node);
            }
        }

        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);

        var board = Instantiate(boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(width, height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);


        GenerateLevel();
        //Origin
        // SpawnBlocks(Vector2.up, "Red", new Vector2(0,0));
        // SpawnBlocks(Vector2.right, "Blue", new Vector2(1,1));
        // SpawnBlocks(Vector2.down, "Yellow", new Vector2(0,2));


        //Destination
        // SpawnDestinationBlocks("Red_D", new Vector2(0, 3));
        // SpawnDestinationBlocks("Blue_D", new Vector2(3, 1));
        // SpawnDestinationBlocks("Yellow_D", new Vector2(3, 2));
    }

    void GenerateLevel()
    {
        //Each 9x9 pixel block represents a block
        //Color represents the color of the block
        //If the color is in the center of the 3x3 square block, it is the destination block
        //If the color is not in the center of the 3x3 square block, we generte the origin block with direction base of the color's position
        //The pixel in the conner of the 9x9 block represents nothing
        //Texture2D texture; // The input texture
        int textureWidth = levelTexture.width;
        int textureHeight = levelTexture.height;

        int squareSize = 3;
        int numSquaresX = textureWidth / squareSize;
        int numSquaresY = textureHeight / squareSize;

        // Color[,] squares = new Color[numSquaresY, numSquaresX];

        for (int y = 0; y < numSquaresY; y++)
        {
            for (int x = 0; x < numSquaresX; x++)
            {
                Color[,] pixelSquare = new Color[squareSize, squareSize];

                for (int yOffset = 0; yOffset < squareSize; yOffset++)
                {
                    for (int xOffset = 0; xOffset < squareSize; xOffset++)
                    {
                        pixelSquare[yOffset, xOffset] = levelTexture.GetPixel(x * squareSize + xOffset, y * squareSize + yOffset);
                        
                    }
                }
                CheckBlock(pixelSquare, squareSize, x, y);
                
                // squares[y, x] = pixelSquare;
            }
        }
        // CheckBlock(squares, squareSize);
    }

    void CheckBlock(Color[,] blockTexture, int squareSize, int x, int y){

        Debug.Log("Checking block at " + y + ", " + x);
        //Function for checking the 9x9 grid
        //Return the status of the block, including the color and the direction, and whether it is a origin block or a destination block
        
        // for (int i = 0; i < squaresSize, i++)
        // {
        //     for (int j = 0; j < squareSize; j++)
        //     {
                
        //     }
        // }


        //Solid blue. RGBA is (0, 0, 1, 1).
        //Solid red. RGBA is (1, 0, 0, 1).
        //Solid yellow. RGBA is (1, 0.92, 0.016, 1).
        //Completely transparent. RGBA is (0, 0, 0, 0).

        if(blockTexture[1,1] != Color.clear){
            if(blockTexture[1,1] == Color.red){
                // Debug.Log("Red_Destination");
                SpawnDestinationBlocks("Red_D", new Vector2(x, y));
                return;
            }
            if(blockTexture[1,1] == Color.blue){
                SpawnDestinationBlocks("Blue_D", new Vector2(x, y));
                return;
            }
            if(blockTexture[1,1] == Color.yellow){
                SpawnDestinationBlocks("Yellow_D", new Vector2(x, y));
                return;
            }
        }
        if(blockTexture[0,1] != Color.clear){
            // Debug.Log("Current Color Detected At Pixel [0,1]"+blockTexture[0,1]);
            if(blockTexture[0,1] == Color.red){
                // Debug.Log("Red_Origin UP");
                SpawnBlocks(Vector2.down, "Red", new Vector2(x, y));
                return;
            }
            if(blockTexture[0,1] == Color.blue){
                // Debug.Log("Blue_Origin UP");
                SpawnBlocks(Vector2.down, "Blue", new Vector2(x, y));
                return;
            }
            if(blockTexture[0,1] == Color.yellow){
                SpawnBlocks(Vector2.down, "Yellow", new Vector2(x, y));
                return;
            }
        }
        if(blockTexture[2,1] != Color.clear){
            if(blockTexture[2,1] == Color.red){
                SpawnBlocks(Vector2.up, "Red", new Vector2(x, y));
                return;
            }
            if(blockTexture[2,1] == Color.blue){
                // Debug.Log("Blue_Origin DOWN");
                SpawnBlocks(Vector2.up, "Blue", new Vector2(x, y));
                return;
            }
            if(blockTexture[2,1] == Color.yellow){
                SpawnBlocks(Vector2.up, "Yellow", new Vector2(x, y));
                return;
            }
        }
        if(blockTexture[1,0] != Color.clear){
            if(blockTexture[1,0] == Color.red){
                SpawnBlocks(Vector2.left, "Red", new Vector2(x, y));
                return;
            }
            if(blockTexture[1,0] == Color.blue){
                SpawnBlocks(Vector2.left, "Blue", new Vector2(x, y));
                return;
            }
            if(blockTexture[1,0] == Color.yellow){
                SpawnBlocks(Vector2.left, "Yellow", new Vector2(x, y));
                return;
            }
        }
        if(blockTexture[1,2] != Color.clear){
            if(blockTexture[1,2] == Color.red){
                SpawnBlocks(Vector2.right, "Red", new Vector2(x, y));
                return;
            }
            if(blockTexture[1,2] == Color.blue){
                SpawnBlocks(Vector2.right, "Blue", new Vector2(x, y));
                return;
            }
            if(blockTexture[1,2] == Color.yellow){
                SpawnBlocks(Vector2.right, "Yellow", new Vector2(x, y));
                return;
            }
        }
        
    }   

    void SpawnBlocks(Vector2 direction, string color, Vector2 pos)
    {

        //Generate block on randomly selected node position 

        var block = Instantiate(GetBlockTypeByColor(color).blockPrefab, pos, Quaternion.identity);
        block.Init(GetBlockTypeByColor(color), direction);
        //Rotate the visual of the block
        block.transform.Find("Visual").transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        blocks.Add(block);


    }
    void SpawnDestinationBlocks(string color, Vector2 pos)
    {

        //Generate block on randomly selected node position 

        var block = Instantiate(GetBlockTypeByColor(color).blockPrefab, pos, Quaternion.identity);
        block.Init(GetBlockTypeByColor(color), Vector2.up);
        blocks.Add(block);


    }

}

[Serializable]
public struct BlockType
{
    public string colorText;
    // public Color color;
    public Block blockPrefab;
}

public enum GameState
{
    GenerateLevel,
    WaitingInput,
    Moving,
    Win,
    Lose
}