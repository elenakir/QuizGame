using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class UIWinPanel : MonoBehaviour
{
    [SerializeField] private Transform _winPanel;
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private Button _restart;

    public UnityEvent onWin;
    public UnityEvent onRestart;

    private void Start()
    {
        _winPanel.gameObject.SetActive(false);

        onWin.AddListener(ShowWinPanel);
        _restart.onClick.AddListener(Restart);
    }

    private void ShowWinPanel()
    {
        _winPanel.gameObject.SetActive(true);

        _fadeScreen.raycastTarget = true;
        _fadeScreen.DOFade(1, 2);
    }

    private void HideWinPanel()
    {
        _winPanel.gameObject.SetActive(false);

        _fadeScreen.DOFade(0, 3).SetEase(Ease.InCirc);
        StartCoroutine(FadeScreeDisable());
    }

    private void Restart()
    {
        HideWinPanel();
        onRestart?.Invoke();
    }

    IEnumerator FadeScreeDisable()
    {
        yield return new WaitForSeconds(3.5f);
        _fadeScreen.raycastTarget = false;
    }
}
