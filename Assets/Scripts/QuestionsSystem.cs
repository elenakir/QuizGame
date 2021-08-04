using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class QuestionsSystem : MonoBehaviour
{
    [SerializeField] private int _startCount;
    [SerializeField] private int _step;
    [SerializeField] private int _answerCountForWin;
    [SerializeField] private UIQuestion _uiQuestion;
    [SerializeField] private UIWinPanel _uiWinPanel;
    [SerializeField] private CellSpawner _spawner;
    [SerializeField] private List<CardBundleData> _bundles;

    private QuestionGenerator _generator;
    private List<CardData> _currentBundle;
    private List<CardData> _questionsList; 
    private List<string> _usedList; 
    private List<Cell> _cells;
    private string _rightAnswer;
    private int _rightCount;
    private int count;

    private void Start()
    {
        count = _startCount;
        _rightCount = 0;

        _generator = new QuestionGenerator();
        _questionsList = new List<CardData>();
        _currentBundle = new List<CardData>();
        _usedList = new List<string>();
        _cells = new List<Cell>();

        _uiWinPanel.onRestart.AddListener(Restart);
        NextQuestion();

        foreach (var item in _cells) item.Show();
    }

    private void FillGrid()
    {
        for (int i = 0; i < count; i++)
        {
            var target = _generator.GetRandomItem(_currentBundle);

            Cell cell = _spawner.Spawn();
            cell.transform.SetParent(_uiQuestion.Grid.transform);
            cell.Icon.sprite = target.Icon;
            cell.ClickArea.onClick.AddListener(() =>
            {
                CheckAnswer(cell, target.Value);
            });
            _cells.Add(cell);
            _questionsList.Add(target);
            _currentBundle.Remove(target);
        }
    }

    private void NextQuestion()
    {
        _currentBundle.Clear();
        _questionsList.Clear();

        _currentBundle = _generator.GenerateBundle(_bundles);
        if (_currentBundle.Count < count)
        {
            _uiWinPanel.onWin?.Invoke();
            return;
        }
        else FillGrid();

        _rightAnswer = _generator.GetAnswer(_questionsList);
        while (!CheckUnique(_rightAnswer))
        {
            _rightAnswer = _generator.GetAnswer(_questionsList);
        }

        _usedList.Add(_rightAnswer);
        _uiQuestion.onTitleChanged?.Invoke(_rightAnswer);
    }

    private void CheckAnswer(Cell cell, string selected)
    {
        if (_rightAnswer.Equals(selected))
        {
            _rightCount++;
            _rightAnswer = "";
            StartCoroutine(SuccessAnim(cell));
        }
        else
        {
            cell.Icon.transform.DOShakePosition(0.2f, 10f)
                    .SetEase(Ease.InBounce)
                    .SetLoops(10, LoopType.Yoyo);
        }
    }

    private void Restart()
    {
        count = _startCount;
        _rightCount = 0;
        _usedList.Clear();
        _cells.Clear();
        NextQuestion();

        foreach (var item in _cells) item.Show();
    }

    private bool CheckUnique(string answer)
    {
        for (int i = 0; i < _usedList.Count; i++)
        {
            if (_usedList[i].Equals(answer))
                return false;
        }
        return true;
    }

    IEnumerator SuccessAnim(Cell cell)
    {
        cell.Icon.transform.DOMoveY(cell.transform.position.y + 25f, .2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(6, LoopType.Yoyo);

        _uiQuestion.Particles.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        _uiQuestion.Particles.gameObject.SetActive(false);
        _uiQuestion.onResetGrid?.Invoke();
        if (_rightCount == _answerCountForWin)
        {
            _uiWinPanel.onWin?.Invoke();
        }
        else
        {
            count += _step;
            NextQuestion();
        }
    }
}
