using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MARDEK.Core;
using MARDEK.Movement;

namespace MARDEK.Battle
{
    public class RandomEncounterGenerator : MonoBehaviour
    {
        [SerializeField] Movable movable;
        [SerializeField] EncounterSet areaEncounterSet = null;
        [SerializeField] int minSteps = 20;
        [SerializeField] int maxSteps = 30;
        [SerializeField] UnityEvent onTriggerBattle;
        [SerializeField] GameObject BlueAlertBalloonPrefab;
        [SerializeField] GameObject PlayerPosition;
        [SerializeField] GameObject CloneBlueAlertBalloonPrefab;
        int stepsTaken = 0;
        int requiredSteps;

        IEnumerator InitiateBattle()
        {
            CloneBlueAlertBalloonPrefab = Instantiate(BlueAlertBalloonPrefab, (PlayerPosition.transform.position + new Vector3(0, 1.25f, 0)), Quaternion.identity);
            movable.MovementSpeed = 0;
            movable.StopAnimator();
            yield return new WaitForSeconds(.5f);
            BattleManager.encounter = areaEncounterSet;
            Debug.Log("Battle Starts!");
            onTriggerBattle.Invoke();
        }

        private void Start()
        {
            if (areaEncounterSet == null)
            {
                enabled = false;
                return;
            }
            GenerateRequiredSteps();
            movable.OnEndMove += Step;
        }
        private void LateUpdate()
        {
            if (stepsTaken < requiredSteps)
                return;
            stepsTaken = 0;
            GenerateRequiredSteps();
            TriggerBattle();
        }
        void TriggerBattle()
        {
            StartCoroutine(InitiateBattle());
        }
        void Step()
        {
            if (PlayerLocks.isPlayerLocked)
                return;
            stepsTaken++;
        }
        void GenerateRequiredSteps()
        {
            requiredSteps = Random.Range(minSteps, maxSteps + 1);
        }
        void Update()
        {
            if(Input.GetKeyDown("e"))
            {
                StopAllCoroutines();
                Destroy(CloneBlueAlertBalloonPrefab, 0);
                movable.MovementSpeed = 5;
            } 
        }
    }
}