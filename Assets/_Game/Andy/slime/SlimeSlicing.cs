using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeSlicing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject childSlime;
    public SlimeSizeControl _sizeControl;
    [SerializeField] private Transform _navMeshPlane;
    void Start()
    {
        GameManager.OnSliced += SliceSlime;
        _sizeControl.childSlime = childSlime;
        _navMeshPlane.position = _sizeControl.transform.position;
    }

    // Update is called once per frame
    private void SliceSlime(bool slicing)
    {
        if(slicing)
        {
            SliceToParts();
        }
        else
        {
            MergeToOne();
        }
    }
    private void SliceToParts()
    {
        _sizeControl.Resize(0);
        for (int i = 0; i < GameManager.Instance.SizePoints; i++)
        {
            GameObject temp = Instantiate(childSlime, _sizeControl.transform.position, Quaternion.identity);
            GameManager.Instance.ChildrenSlime.Add(temp);
        }
    }
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    [SerializeField] private float _movTime = 2;
    private List<Vector3> _points;
    private bool _firstCome = false;
    private void MergeToOne()
    {
        _firstCome = false;
        
        for (int i = 0; i < GameManager.Instance.ChildrenSlime.Count; i++)
        {
            _points = new List<Vector3>();
            _path = new NavMeshPath();
            _agent = GameManager.Instance.ChildrenSlime[i].GetComponent<NavMeshAgent>();
            NavMesh.CalculatePath(_agent.transform.position, _sizeControl.transform.position, NavMesh.AllAreas, _path);
            for (int ji = 0; ji < _path.corners.Length; ji++)
            {
                _points.Add(_path.corners[ji]);
            }
            _points.Add(_sizeControl.transform.position);
            _agent.transform.DOPath(_points.ToArray(), _movTime, PathType.CatmullRom, PathMode.Sidescroller2D).OnComplete(()=>
            { if (!_firstCome) { _firstCome = true; _sizeControl.Resize(GameManager.Instance.SizePoints); } });
            Destroy(_agent.gameObject, _movTime*1.1f);
        }
        GameManager.Instance.ChildrenSlime = new List<GameObject>();
    }
}
