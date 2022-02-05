using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Teleport : MonoBehaviour,IIntractable
{


    [SerializeField] public Transform teleportLocation;

    Vector3 localScale;
    bool isHover = false;


    WaitForSeconds waitForSeconds = new WaitForSeconds(.5f);

    void Start()
    {
        localScale = transform.localScale;
    }

    public void Hover()
    {
        if (!isHover)
        {
            isHover = true;
            transform.DOScale(localScale * 1.5f, .25f).SetEase(Ease.OutBounce).SetLoops(1);
            StartCoroutine(WaitForsec());
        }
    }

    private void EndHover()
    {
        transform.DOScale(localScale, .25f).SetEase(Ease.OutBounce).SetLoops(1);

        isHover = false;

    }

    public void Interact()
    {
                
    }


    IEnumerator WaitForsec()
    {
        yield return waitForSeconds;
        EndHover();

    }
}
