using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string colorText;
    public Vector2 Pos => transform.position;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private TextMeshPro text;
    public Vector2 direction = Vector2.right;
    private int loopCount = 0;
    public void Init(BlockType type)
    {
        colorText = type.colorText;
        renderer.color = type.color;
        direction = Vector2.right;
    }

    private void Start()
    {
        ModifyBlockDisplay();
    }
    private void Update()
    {
        ModifyBlockDisplay();
    }

    void ModifyBlockDisplay()
    {
        if(direction == Vector2.up)
        {
            text.text = "N";
            return;
        }
        if(direction == Vector2.down)
        {
            text.text = "S";
            return;
        }
        if(direction == Vector2.left)
        {
            text.text = "W";
            return;
        }
        if(direction == Vector2.right)
        {
            text.text = "E";
            return;
        }
    }

    public void LoopingDirections()
    {
        loopCount = loopCount == 3 ? 0 : loopCount + 1;
        switch (loopCount)
        {
            case 0:
                direction = Vector2.right;
                break;
            case 1:
                direction = Vector2.down;
                break;
            case 2:
                direction = Vector2.left;
                break;
            case 3:
                direction = Vector2.up;
                break;
            default:
                break;
        }
    }
}
