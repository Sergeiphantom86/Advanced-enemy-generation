using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _prefabs;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<GameObject> _targetPoints;

    private ObjectPool<Enemy> _pool;

    private int _index;
    private int _poolCapacity = 20;
    private int _poolMaxSize = 20;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            Create,
            GetFromPool,
            ReleaseInPool,
            Destroy,
            true,
            _poolCapacity,
            _poolMaxSize);
    }

    private void Start()
    {
        CreateTargetPoint();
        StartCoroutine(WinWithDelay());
    }

    private Enemy Create()
    {
        GetIndex();

        return Instantiate(_prefabs[_index], _spawnPoints[_index].position, Quaternion.identity);
    }

    private void CreateTargetPoint()
    {
        for (int i = 0; i < _targetPoints.Count; i++)
        {
            _targetPoints[i] = Instantiate(_targetPoints[i]);
        }
    }

    private void GetFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);

        enemy.SetTargetPosition(_targetPoints[_index].transform);
    }

    private void ReleaseInPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void GetIndex()
    {
        _index = Random.Range(0, _spawnPoints.Count);
    }

    private IEnumerator WinWithDelay()
    {
        int repeatRate = 2;

        while (_pool.CountAll < _poolMaxSize)
        {
            _pool.Get();

            yield return new WaitForSeconds(repeatRate);
        }
    }
}