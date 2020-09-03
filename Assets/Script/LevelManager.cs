using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();

    private int lastIndex = -1;
    private GameObject lastLevel;

    public void NextLevel()
    {
        PrepareLevel(lastIndex + 1);
    }
    public void CurrentLevel()
    {
        PrepareLevel(lastIndex);
    }

    public void PrepareLevel(int index)
    {
        if (lastLevel) Destroy(lastLevel);
        lastIndex = index % levels.Count;
        lastLevel=Instantiate(levels[lastIndex], transform);

        text?.SetText("Level " + (int)(lastIndex+1));
    }
}
