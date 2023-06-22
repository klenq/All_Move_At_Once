using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseControl : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Down");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mouse control
        Vector3 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("mouse clicked");
            Ray cubeRay = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit cubeHit;

            if (Physics.Raycast(cubeRay, out cubeHit))
            {
                Debug.Log("We hit " + cubeHit.collider.name);
                if (cubeHit.collider.tag == "Block")
                {
                    //Debug.Log("touched block");
                    Block block = cubeHit.transform.GetComponent<Block>();
                    block.LoopingDirections();
                }
            }
        }
    }
}
