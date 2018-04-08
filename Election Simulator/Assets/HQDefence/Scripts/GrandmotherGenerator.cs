using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GrandmotherGenerator : MonoBehaviour
{

    public Transform FirstLaneSpawnCoord;
    public Transform SecondLaneSpawnCoord;
    public Transform ThirdLaneSpawnCoord;
    public Transform FourthLaneSpawnCoord;
    public Rigidbody GrandMotherPrefab;

    [Range(1, 20)]
    public float ReloadTimeToSpawn;

    [Range(1, 20)]
    public int MaxGrandMotherCount;

    private Random _randomizer;
    private Transform[] _spawnCoords;
    private float _reloadTime;
    private int _currentCountGrandMothers;

    void Start()
    {
        _randomizer = new Random();
        _spawnCoords = new[] {FirstLaneSpawnCoord, SecondLaneSpawnCoord, ThirdLaneSpawnCoord, FourthLaneSpawnCoord};
        _reloadTime = ReloadTimeToSpawn;
    }

    void Update()
    {
        if (_reloadTime <= 0 && _currentCountGrandMothers < MaxGrandMotherCount)
        {
            _reloadTime = ReloadTimeToSpawn;
            SpawnGrandMother();
        }

        _reloadTime -= Time.deltaTime;
    }

    void SpawnGrandMother()
    {
        var randomLineIndex = GetRandomLane();
        var tmp = _spawnCoords[randomLineIndex].position;
        var spawnCoordinates = new Vector3(tmp.x, tmp.y, tmp.z - randomLineIndex - 0.01F);
        Instantiate(GrandMotherPrefab, spawnCoordinates, Quaternion.identity);
        _currentCountGrandMothers++;
    }

    public void RemoveGrandMother()
    {
        _currentCountGrandMothers--;
    }

    int GetRandomLane()
    {
        return _randomizer.Next(0, _spawnCoords.Length);
    }
}
