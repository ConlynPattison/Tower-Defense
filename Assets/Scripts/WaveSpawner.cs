using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public TextMeshProUGUI waveCountdownText;
    public float timeBetweenWaves = 5f;
    
    private float _countDown = 2f;
    private int _waveIndex = 0;
    
    private void Update()
    {
        if (_countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countDown = timeBetweenWaves;
        }

        _countDown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Ceil(_countDown).ToString();
    }

    IEnumerator SpawnWave()
    {
        _waveIndex++;
        
        for (int i = 0; i < _waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
