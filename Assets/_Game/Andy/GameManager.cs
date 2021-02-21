using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum EndGame
    {
       proceed,win,Lose
    }
    private EndGame _end = EndGame.proceed;
    private int _sizePoints = 0;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float _maxEnemies, _maxEarned;
    private GameObject _player;
    public int SizePoints { get => _sizePoints; set { OnResize(value, _sizePoints); _sizePoints = value;} }
    public GameObject Player { get => _player; set => _player = value; }
    public static GameManager Instance { get => _instance; set => _instance = value; }
    public bool IsSliced { get => _isSliced; set { _isSliced = value; OnSliced(value); } }

    public List<GameObject> ChildrenSlime { get => _childrenSlime; set => _childrenSlime = value; }
    public EndGame End { get => _end; set { _end = value; } }

    private bool _isSliced;
    private List<GameObject> _childrenSlime = new List<GameObject>();

    public int enemiesDefeated = 0;
    public int level = 0;
    public delegate void Resized(int size,int oldsize);
    public static event Resized OnResize;
    public delegate void Sliced(bool smaller);
    public static event Sliced OnSliced;
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
        OnResize(_sizePoints, _sizePoints-1);
    }
    private void EndOfGame()
    {
        canvas.SetActive(true);
        canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _end.ToString();
        canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "You earned :"+_sizePoints.ToString()+" from: "+_maxEarned.ToString();
        canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "You deafeted :" + enemiesDefeated.ToString() + " from: " + _maxEnemies.ToString();
    }

}
