using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount != 1)
        {
            return;
        }

        Touch touch = Input.touches[0];

        Vector3 pos = touch.position;
        if(touch.phase == TouchPhase.Began)
        {
            // Debug.Log("touched");
            //touch happened
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                // Debug.Log("ray detected");
                if (hit.collider.tag == "Block")
                {
                    // Debug.Log("touched block");
                    Block block =  hit.transform.GetComponent<Block>();
                    block.LoopingDirections();
                }
            }
        }
    }
}
