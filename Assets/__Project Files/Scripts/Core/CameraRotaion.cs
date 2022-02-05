using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRotaion : MonoBehaviour
{


    [SerializeField] LayerMask interactablesLayer;


    bool isDragging = false;

    float startMouseX;
    float startMouseY;

    Camera cam;

    Ray ray;
    RaycastHit hit;

    const string teleport = "Teleport";

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !isDragging)
        {

            isDragging = true;
            startMouseX = Input.mousePosition.x;
            startMouseY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
           
        }

        // -------------------- for hover --------------------- //
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 1000f, interactablesLayer))
        {
            if (hit.collider.gameObject.GetComponent<IIntractable>() == null)
                return;

            hit.collider.gameObject.GetComponent<IIntractable>().Hover();

            if (Input.GetMouseButtonDown(0))
            {
                
               var teleport =  hit.collider.gameObject.GetComponent<Teleport>();
                if(teleport)
                {
                    transform.position = teleport.teleportLocation.position;
                }
            }
        }


    }

    void LateUpdate()
    {
        if (isDragging)
        {
            float endMouseX = Input.mousePosition.x;
            float endMouseY = Input.mousePosition.y;

            float diffX = endMouseX - startMouseX;
            float diffY = endMouseY - startMouseY;

            float newCenterX = Screen.width / 2 + diffX;
            float newCenterY = Screen.height / 2 + diffY;

            Vector3 LookHerePoint = cam.ScreenToWorldPoint(new Vector3(newCenterX, newCenterY, cam.nearClipPlane));

            transform.LookAt(LookHerePoint);

            startMouseX = endMouseX;
            startMouseY = endMouseY;
        }
    }

    public void MovePlayerX(int x  )
    {
        Vector3 localPos = transform.position + new Vector3(x, 0, 0);
       
        transform.DOMove(localPos, .25f).SetEase(Ease.InOutSine);
    }
    public void MovePlayerZ(int z  )
    {
        Vector3 localPos = transform.position + new Vector3(0, 0, z);
        transform.DOMove(localPos, .25f).SetEase(Ease.InOutSine);
    }

}
