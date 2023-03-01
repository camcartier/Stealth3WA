using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("NoiseMeter")]
    private float _maxNoiseBeforeTrigger = 100;
    private float _noiseTracker;
    [Header("Essentials")]
    private Rigidbody _rbEnemy;

    private bool _playerIsHeard;
    private bool _playerIsDetected;

    private void Awake()
    {
        _rbEnemy = GetComponentInChildren<Rigidbody>();
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
            _playerIsHeard = true;
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

    private void SoundDetection()
    {

    }

    private void VisualDetection()
    {
        //send raycasts in the enemy's forward
        //if raycast hits player, launch gotoloot
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
