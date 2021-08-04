using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator
{
    public List<CardData> GenerateBundle(List<CardBundleData> bundles)
    {
        List<CardData> _currentBundle = new List<CardData>();
        int rand = Randomizer(0, bundles.Count);

        foreach (var item in bundles[rand].CardData)
        {
            _currentBundle.Add(item);
        }
        return _currentBundle;
    }

    public CardData GetRandomItem(List<CardData> source)
    {
        return source[Randomizer(0, source.Count)];
    }

    public string GetAnswer(List<CardData> source)
    {
        return source[Randomizer(0, source.Count)].Value;
    }

    private int Randomizer(int from, int to)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(from, to);
    }
}
