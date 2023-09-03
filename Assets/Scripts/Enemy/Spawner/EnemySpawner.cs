using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private GameObject[] _enemiesPrefabs = null;
    [SerializeField] private float _dely = 5.0f;
    [SerializeField] private Transform _target;

    private float _currentDely = 0;
    private Transform[] _spawnpoints = null;
    private int _currentWaveIndex = 0;


    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        if (_spawnpoints == null) return;
        if (_enemiesPrefabs == null) return;
        if (_currentWaveIndex >= _waves.Length) return;
        
        if (_currentDely > _dely)
        {
            SpawnWave();
            _currentDely = 0;
        }
        _currentDely += Time.deltaTime;
    }

    private void GetReferences()
    {
        Transform[] spawnpoints = GetComponentsInChildren<Transform>();
        _spawnpoints = new Transform[spawnpoints.Length - 1];
        int i = 0;
        foreach (Transform t in spawnpoints)
        {
            if (transform == t) continue;
            _spawnpoints[i++] = t;
        }
    }

    private void SpawnNewEnemy()
    {
        int maxSpawnpoinIndex = _spawnpoints.Length;
        int maxEnemyPrefabIndex = _enemiesPrefabs.Length;

        int spawnpointIndex = Random.Range(0, maxSpawnpoinIndex);
        int enemyIndex = Random.Range(0, maxEnemyPrefabIndex);

        ZombieAI zombieAI = Instantiate(_enemiesPrefabs[enemyIndex], _spawnpoints[spawnpointIndex]).GetComponent<ZombieAI>();
        zombieAI.SetTarget(_target);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < _waves[_currentWaveIndex].EnemiesAmount; i++)
        {
            SpawnNewEnemy();
        }
        _currentWaveIndex++;
    }

}
