using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumBehavior : MonoBehaviour {

    void Start()
    {
      /*  gameobject.getcomponent<boxcollider2d>().size = new vector2(
    gameobject.getcomponent<recttransform>().sizedelta.x,
    gameobject.getcomponent<recttransform>().sizedelta.y
    );*/
        //gameObject.GetComponent<BoxCollider2D>().offset = gameObject.GetComponent<RectTransform>().anchoredPosition;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(0,0,0);
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null)
            GameManager.instance.ChangeStateToOne(gameObject.name);

        //GameManager.instance.ChangeStateToOne(gameObject.name);

        /*
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);


        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            Debug.Log("HIT");
            if (raycastHit.collider.name == gameObject.name)
            {
                Debug.Log("Dobule HIT");

                GameManager.instance.ChangeStateToOne(gameObject.name);
            }
        }
        */
    }

    void FixedUpdate()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.name == gameObject.name)
                {
                    GameManager.instance.ChangeStateToOne(gameObject.name);
                }                
            }
        }
    }

    

}
