using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cortana : MonoBehaviour,IIntractable
{
    [SerializeField] GameObject[] cortanaObjects;
    [Header("Music Event")]
    [SerializeField] GameEvent playMusic;
    [Header("Rotaion")]
    [SerializeField] Vector3 targetRotation;



    Vector3 localScale;
    bool isHover = false;
    int currentIndex = 0;


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
            transform.DOScale(localScale, .25f).SetEase(Ease.OutBounce).SetLoops(1); 
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
        cortanaObjects[currentIndex].transform.DORotate(targetRotation, 1.75f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        cortanaObjects[currentIndex].transform.DORotate(-1 * targetRotation, 1.75f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

    }

    IEnumerator WaitForsec()
    {
        yield return waitForSeconds;
        EndHover();

    }

}
