using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotaion : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] LayerMask interactablesLayer;


    bool isDragging = false;

    float startMouseX;
    float startMouseY;

    Camera cam;

    Ray ray;
    RaycastHit hit;
    private  Vector3 tpLocation = new Vector3(250,0,0);  
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
                
                hit.collider.gameObject.GetComponent<IIntractable>().Interact();
                if(hit.collider.gameObject.CompareTag("Teleport"))
                {
                    transform.position = tpLocation;
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

#endif
}
