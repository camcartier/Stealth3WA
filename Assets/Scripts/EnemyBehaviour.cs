using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("NoiseMeter")]
    private float _maxNoiseBeforeTrigger = 100;
    private float _noiseTracker;

    #region Essentials
    private Rigidbody _rbEnemy;
    private GameObject _player;
    #endregion

    #region Detection Data
    private bool _playerIsHeard;
    private bool _playerIsDetected;
    [SerializeField] private Transform[] _raySpot;
    [SerializeField] private Transform _rayOrigHead;
    [SerializeField] LayerMask _groundMask;
    #endregion

    #region Loot Data
    private Vector3 _posToGo;
    [SerializeField] private Vector3 _hisLootPos;
    #endregion


    private void Awake()
    {
        _rbEnemy = GetComponentInChildren<Rigidbody>();
        _player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_noiseTracker >= _maxNoiseBeforeTrigger)
        {
            SoundDetected();
        }
    }

    private void DefaultBehavior()
    {
        //patrol
    }
    private void TriggeredBehavior()
    {
        //if the noise tracker goes over the max threeshold
        //launch MoveTowardsSoundLocation()
        //when in position of sound detection triggered launch Inspect()
        //go back to patrol zone OR
        //if sound detected too many times in new zone
        //start patrol in new zone AND
        //if detected too many times in new zone
        //call more enemies

        //if visual detection is triggered
        //GoToLoot();
    }

    private void Patrol()
    {

    }

    private void SoundDetected()
    {
        _playerIsHeard = true;
        _posToGo = _player.transform.position;
        //l'enemi doit se retourner et attendre un certain temps avant de bouger
    }

    private void VisualDetection()
    {
        RaycastHit hit;
        foreach (Transform t in _raySpot)
        {
            if (Physics.Raycast(t.position, t.position - _rayOrigHead.position, out hit, _groundMask))
            {
                if (hit.collider == _player)
                {
                    Debug.Log("player is detected!");
                }
            }
        }

        //send raycasts in the enemy's forward
        //if raycast hits player, launch gotoloot
    }
    private void OnDrawGizmos()
    {
        
    }

    private void MoveTowardsSoundLocation()
    {
        //go to position where player triggered sound detection 

    }
    private void Inspect()
    {
        //look around for (duration)
    }
    private void GoToLoot()
    {
        //if playerIsDetected = true
        //go to loot position and leave
    }
}
