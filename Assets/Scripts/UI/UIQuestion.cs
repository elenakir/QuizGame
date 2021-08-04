using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIQuestion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private ParticleSystem _particles;

    public ParticleSystem Particles => _particles;
    public TextMeshProUGUI Title
    {
        get => _title;
        set => _title = value;
    }
    public GridLayoutGroup Grid => _grid;

    public UnityEvent onResetGrid;
    public UnityEvent<string> onTitleChanged;

    private void Awake()
    {
        onResetGrid.AddListener(ResetGrid);
        onTitleChanged.AddListener(ChangeTitle);

        Title.DOFade(1, 3);
    }

    private void ResetGrid()
    {
        _title.text = "";
        foreach (Transform child in _grid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ChangeTitle(string newTitle)
    {
        _title.text = "Find " + newTitle;
    }
}
