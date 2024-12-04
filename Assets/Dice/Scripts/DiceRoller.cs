
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{

    [SerializeField] private float maxRandomForceValue, startRollingForce;
    [SerializeField] private Spawner diceSpawner;
    [SerializeField]  private List<RollingDice> rollDiceList;
    [SerializeField] private int valuesDices;

    [SerializeField] private bool Reroll;
    private float forceX, forceY, forceZ;

    [SerializeField] private List<Dice> diceList;   
    private List<Dice> spawnedDiceList;
    private bool isRolled;
    private bool isSpawn;

    private void Start(){

        spawnedDiceList = new List<Dice>();
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            StartRoll();
        }

        if(isRolled){
            CheckRolled();
        }
    }

    private void StartRoll(){
        if(isRolled) return;

        if(!Reroll || spawnedDiceList.Count == 0) SpawnDices();


        valuesDices = 0;
        foreach(Dice dice in spawnedDiceList){
            RollDice(dice);
        }

        isSpawn = true;
        isRolled = true;
    }

    private void SpawnDices(){


        if(isSpawn)CleanDices();
        spawnedDiceList = new List<Dice>();

        foreach(RollingDice diceRoll in rollDiceList){

            GameObject spawnedDice = Instantiate(diceList.First(dice => dice.diceType == diceRoll.diceType).gameObject, diceSpawner.GetSpawnPosition(), Quaternion.identity);
            spawnedDiceList.Add(spawnedDice.GetComponent<Dice>());

        }

    }

    private void CleanDices(){

        foreach(Dice dice in spawnedDiceList){
            Destroy(dice.gameObject);
        }

        spawnedDiceList = new List<Dice>();

        isSpawn = false;
    }

    private void CheckRolled(){

        int notCheckedCount = 0;

        foreach(Dice dice in spawnedDiceList){
            if(dice.isChecked == false) notCheckedCount++;
        }

        isRolled = notCheckedCount != 0;

        if(isRolled == false){
           
            foreach(Dice dice in spawnedDiceList){
                valuesDices += dice.value;
            }
        }
    }

   private void RollDice(Dice dice){
        dice.SetCheck();

        forceX = Random.Range(-maxRandomForceValue, maxRandomForceValue);
        forceY = Random.Range(-maxRandomForceValue, maxRandomForceValue);
        forceZ = Random.Range(-maxRandomForceValue, maxRandomForceValue);

        dice.GetComponent<Rigidbody>().AddForce(Vector3.up * startRollingForce);
        dice.GetComponent<Rigidbody>().AddTorque(forceX, forceY, forceZ);
   }
}


[System.Serializable]

public struct RollingDice{
    public DiceType diceType;

}

[System.Serializable]
public enum DiceType{
    d6,
    d8,
    d10,
    d12,
    d20,
    d100
}