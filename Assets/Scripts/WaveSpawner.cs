using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    
    public Wave[] waves;
    
    public Transform spawnPoint;
    
    public TextMeshProUGUI waveCountdownText;
    private int _waveIndex = 0;

    public float timeBetweenWaves = 20f;
    private float _countDown = 2f;

    public GameManager gameManager;
    
    private void Update()
    {
        if (EnemiesAlive > 0)
            return;
        
        if (_countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countDown = timeBetweenWaves;
            return;
        }

        _countDown -= Time.deltaTime;

        _countDown = Mathf.Clamp(_countDown, 0f, Mathf.Infinity);

        waveCountdownText.text = $"{_countDown:00.00}";
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[_waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        
        _waveIndex++;

        if (_waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
