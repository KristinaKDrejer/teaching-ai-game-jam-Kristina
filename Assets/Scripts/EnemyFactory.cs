using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Prompt _promptPrefab;
    private List<PromptData> _promptDataList = new List<PromptData>();

    private void Awake()
    {
        LoadPromptData();
    }

    public GameObject GetRandomEnemy()
    {
        int index = Random.Range(0, _enemyPrefabs.Length);
        return _enemyPrefabs[index];
    }

    private PromptData GetRandomPromptData()
    {
        if (_promptDataList.Count == 0) return null;
        int index = Random.Range(0, _promptDataList.Count);
        return _promptDataList[index];
    }
    
    private void LoadPromptData()
    {
        PromptData[] dataArray = Resources.LoadAll<PromptData>("Prompts");
        _promptDataList.AddRange(dataArray);
    }

    public void CreateEnemy(GameObject enemy, Vector3 position)
    {
        GameObject enemyInstance = Instantiate(enemy, position, Quaternion.identity);
        Prompt prompt = Instantiate(_promptPrefab);
        prompt.SetData(GetRandomPromptData());
        prompt.transform.parent = enemyInstance.transform;
        prompt.transform.localPosition = new Vector3(0.05f, 1.25f, 0);
    }
}
