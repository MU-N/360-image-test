using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayStopButton : MonoBehaviour, IIntractable
{
    [SerializeField] GameObject[] playStopButtons;
    [Header("Music Event")]
    [SerializeField] GameEvent playMusic;


    Vector3 localScale;
    bool isHover = false;
    int currentIndex = 0;


    WaitForSeconds waitForSeconds = new WaitForSeconds(.5f);

    void Start()
    {
        playStopButtons[1].transform.localScale = Vector3.zero;
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
        playMusic.Raise();

        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        playStopButtons[currentIndex].transform.DOScale(Vector3.zero, .2f);
        currentIndex = (currentIndex + 1) % 2;
        playStopButtons[currentIndex].transform.DOScale(Vector3.one * 100, .2f).SetEase(Ease.OutBounce);
        
    }

    IEnumerator WaitForsec()
    {
        yield return waitForSeconds;
        EndHover();

    }
}
