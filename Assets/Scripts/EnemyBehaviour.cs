using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("NoiseMeter")]
    [SerializeField] IntVariables _noiseTracker;
    private float _maxNoiseBeforeTrigger = 100;


    #region Essentials
    private Rigidbody _rbEnemy;
    private GameObject _player;
    private Renderer _enemyMat;
    #endregion

    #region Detection Data
    private bool _playerCanBeHeard;
    private bool _playerIsHeard;
    private bool _playerIsDetected;
    [SerializeField] private Transform[] _raySpot;
    [SerializeField] private Transform _rayOrigHead;
    [SerializeField] LayerMask _groundMask;

    float waitDuration = 0.5f;
    float timerCounterMeter = 0f;

    #endregion

    #region Loot Data
    private Vector3 _posToGo;
    [SerializeField] private Vector3 _hisLootPos;
    #endregion



    private void Awake()
    {
        _rbEnemy = GetComponentInChildren<Rigidbody>();
        _player = GameObject.Find("Azri");
        _enemyMat = GetComponentInChildren<Renderer>();
        _noiseTracker.value = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_noiseTracker.value >= _maxNoiseBeforeTrigger)
        {
            SoundDetected();
        }

        if((transform.position - _player.transform.position).magnitude < 10)
        {
            _playerCanBeHeard = true;
            Debug.Log("can be heard");
        }
        else { Debug.Log("can NOT be heard"); _playerCanBeHeard = false; }

        if (_playerCanBeHeard && _player.GetComponent<PlayerController>()._isMoving)
        {
            timerCounterMeter += Time.deltaTime;
            //Debug.Log("timercounter" + timerCounterMeter);
        }

        if (timerCounterMeter > waitDuration)
        {
            _noiseTracker.value += 2;
            timerCounterMeter = 0;
        }

        if (_noiseTracker.value > 100)
        {
            Debug.Log("DETECTED");
            _enemyMat.material.color = Color.red;
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

    IEnumerator AddSoundUnits()
    {
        yield return new WaitForSeconds(1);
        _noiseTracker.value += 2;
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
