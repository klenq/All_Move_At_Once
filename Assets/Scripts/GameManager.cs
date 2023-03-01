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

        //Origin
        SpawnBlocks(Vector2.up, "Red", new Vector2(0,0));
        SpawnBlocks(Vector2.right, "Blue", new Vector2(1,1));
        SpawnBlocks(Vector2.down, "Yellow", new Vector2(0,2));


        //Destination
        SpawnDestinationBlocks("Red_D", new Vector2(0, 3));
        SpawnDestinationBlocks("Blue_D", new Vector2(3, 1));
        SpawnDestinationBlocks("Yellow_D", new Vector2(3, 2));
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