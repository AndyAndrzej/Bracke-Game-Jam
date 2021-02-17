using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private int _sizePoints = 0;
    private GameObject _player;
    public int SizePoints { get => _sizePoints; set { _sizePoints = value; OnResize(value); } }
    public GameObject Player { get => _player; set => _player = value; }
    public static GameManager Instance { get => _instance; set => _instance = value; }

    public int enemiesDefeated = 0;
    public int level = 0;
    public delegate void Resized(int size);
    public static event Resized OnResize;
    private static GameManager _instance = null;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    public void AddPlayer(GameObject source)
    {
        _player = source;
    }
    public GameObject GivePlayer()
    {
        return _player;
    }
    public void AddOneSizePoint()
    {
        _sizePoints++;
        OnResize(_sizePoints);
    }

}
