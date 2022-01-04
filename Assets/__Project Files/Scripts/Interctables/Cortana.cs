using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cortana : MonoBehaviour , IIntractable
{
    [Header("Music Event")]
    [SerializeField] GameEvent playMusic;

    GameObject cortanaObject;

    Vector3 localScale;
    bool isHover = false;
    int currentIndex = 0;


    WaitForSeconds waitForSeconds = new WaitForSeconds(.5f);

    void Start()
    {
        localScale = transform.localScale;
        cortanaObject = transform.GetChild(0).gameObject;
    }

    public void Hover()
    {
        if (!isHover)
        {
            isHover = true;
            transform.DOScale(localScale *1.5f, .25f).SetEase(Ease.OutBounce).SetLoops(1); 
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
        playMusic.Raise();

        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        cortanaObject.transform.transform.DOScale(localScale * 1.5f, 1f).SetEase(Ease.InBounce).SetLoops(4,LoopType.Yoyo);

    }

    IEnumerator WaitForsec()
    {
        yield return waitForSeconds;
        EndHover();

    }

}
