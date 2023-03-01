using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shift : MonoBehaviour
{

    GameObject[] blocksObjects;
    private List<Block> blocks;
    [SerializeField] Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator StartShift()
    {
        //disable the button
        button.interactable = false;

        //shifting all the blocks
        Debug.Log("Shifting: " + blocks.Count + " blocks");
        int width = FindObjectOfType<GameManager>().width;
        int height = FindObjectOfType<GameManager>().height;
        Debug.Log("Current Grid dimention is " + width + "x" + height);


        //for each block, we shift one step a time, until it hits the edge
        //we should also roll back the blocks position when there is a conflict 
        for (int i = 0; i < Mathf.Max(width,height)-1; i++)
        {
            Debug.Log("Round #" + (i+1));
            //shifting all the blocks
            foreach (var block in blocks)
            {
                if (block.direction.Equals(Vector2.up) && block.Pos.y == height - 1) { continue; }
                else if (block.direction.Equals(Vector2.down) && block.Pos.y == 0) { continue; }
                else if (block.direction.Equals(Vector2.right) && block.Pos.x == width - 1) { continue; }
                else if (block.direction.Equals(Vector2.left) && block.Pos.x == 0) { continue; }
                StepMove(1, block);
            }
            //TODO
            //Check whether any of the block is occupying the same node, roll back if that happend

            //wait for some time for the next moving round
            yield return new WaitForSeconds(1);
        }

        //re-enable the button
        button.interactable = true;   

    }

    public void OnShiftStart()
    {
        blocks = new List<Block>();
        //On Click Event
        //This function will be trigger once the shift button is pressed 
        //blocks = FindObjectsOfType<Block>();
        blocksObjects = GameObject.FindGameObjectsWithTag("Block");
        Debug.Log("Found " + blocksObjects.Length + " Blocks");
        foreach (var blocksObject in blocksObjects)
        {
            blocks.Add(blocksObject.GetComponent<Block>());
        }
        StartCoroutine(StartShift());



    }

    void StepMove(int step, Block block)
    {
        
        var direction = block.direction;
        block.transform.position = block.Pos + direction;
        Debug.Log("block position after shift is: " + block.Pos);
        // Debug.Log(block.colorText + "block moving: " + block.direction);
        

    }
}
