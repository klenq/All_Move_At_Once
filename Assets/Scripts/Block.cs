using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string colorText;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private TextMeshPro text;
    public void Init(BlockType type)
    {
        colorText = type.colorText;
        renderer.color = type.color;
        text.text = type.text.ToString();
    }
}
