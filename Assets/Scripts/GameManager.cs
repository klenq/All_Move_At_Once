using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 4;
    [SerializeField] private Node nodePrefab;
    [SerializeField] private SpriteRenderer boardPrefab;
    [SerializeField] private Block blockPrefab;
    [SerializeField] private List<BlockType> types;

    private List<Node> nodes;
    private List<Block> blocks;
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

        SpawnBlocks(1, "Red");
        SpawnBlocks(1, "Blue");
        SpawnBlocks(1, "Yellow");
    }

    void SpawnBlocks(int amount, string color)
    {
        var freeNodes = nodes.Where(n => n.OccupiedBlock == null).OrderBy(b => Random.value);

        foreach (var node in freeNodes.Take(amount))
        {
            var block = Instantiate(blockPrefab, node.Pos, Quaternion.identity);
            block.Init(GetBlockTypeByColor(color));
        }


/*        for (int i = 0; i < amount; i++)
        {
            var block = Instantiate(blockPrefab);

        }*/

        if (freeNodes.Count() == 1)
        {
            //lost the game
            return;
        }

    }

    void Shift()
    {

    }

}

[Serializable]
public struct BlockType
{
    public string colorText;
    public Color color;
}

public enum GameState
{
    GenerateLevel,
    WaitingInput,
    Moving,
    Win,
    Lose
}