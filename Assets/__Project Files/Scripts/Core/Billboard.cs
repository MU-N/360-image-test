using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform cam;

    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}