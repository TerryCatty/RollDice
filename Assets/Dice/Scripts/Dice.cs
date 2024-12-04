using System;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

   [SerializeField] private Transform[] sidesPoints;   
    [SerializeField] private int[] valuesSide;
    public int value; 
    public bool isChecked;

    public DiceType diceType;

    [SerializeField] private float rangeToCheck;
    private float timer;
    private Vector3 tempRotation;


   private void Start(){
        SetCheck();
   }

   public void SetCheck(){
        timer = rangeToCheck;
        tempRotation = transform.rotation.eulerAngles;
        isChecked = false;
   }

   private void Update(){
        Check();
   }


   private void Check(){
        timer -= Time.deltaTime;

        if(timer <= 0){
            if(transform.rotation.eulerAngles != tempRotation) {
                tempRotation = transform.rotation.eulerAngles;
                timer = rangeToCheck;
            }
            else{
                value  = NearValue();
                isChecked = true;
            }
        }
   }


   private int NearValue(){
        int value = 0;

        for(int i = 0; i < sidesPoints.Length; i++){
            if(sidesPoints[i].position.y > sidesPoints[value].position.y) value = i;
        }
          Debug.Log(value);
          value = valuesSide[value];
        return value;
   }


}
