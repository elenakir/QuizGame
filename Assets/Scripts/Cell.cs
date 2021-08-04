using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _clickArea;

    public Image Icon
    {
        get => _image;
        set => _image = value;
    }
    public Button ClickArea => _clickArea;

    public void Show()
    {
        transform.localScale = new Vector3(0, 0, 0);

        DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), .5f))
            .Append(transform.DOScale(new Vector3(.9f, .9f, .9f), .5f))
            .Append(transform.DOScale(new Vector3(1f, 1f, 1f), .5f));
    }
}
