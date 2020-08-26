using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorshopScript : InboxGameobject
{
    public List<int> inputID;
    public List<int> inputAmount;
    public ChemData chemdata;
    public int numchem;
    public GameObject InputWindow;
    public InventorySystem backpack;
    void Start(){
        inputID = new List<int>();
        inputAmount = new List<int>();
    }
    public override void GetItem(int _id){
        Debug.Log("Sir gut");
        int l=inputID.Count;
        for(int i = 0;i<l ;i++){
            if(inputID[i]==_id){
                inputAmount[i]++;
                return;
            }
        }
        inputID.Add(_id);
        inputAmount.Add(1);
    }

    //Open/Close inputWindow
    public void OpenInWin(){
        InputWindow.SetActive(true);
    }
    public void CloseInWin(){
        InputWindow.SetActive(false);
    }
    /*private void OnMouseDown(){
       OpenInWin();
    }*/


    public void Remove(int _id){
        int l=inputID.Count;
        for(int i=0;i<l;i++){
            if(inputID[i]==_id){
                if(inputAmount[i]<1){
                    inputAmount[i]--;
                }else{
                    inputAmount.Remove(inputAmount[i]);
                    inputID.Remove(inputID[i]);
                }
                return;
            }
        }
    }
    private int Check(int index){
        for(int i=0;i<inputID.Count;i++){
            if(inputID[i]==chemdata.chemdata[index].input[i].id){
                if(inputAmount[i]==chemdata.chemdata[index].input[i].amount){
                    continue;
                }else if(inputAmount[i]>chemdata.chemdata[index].input[i].amount){
                    return -1;
                }else return 1;
            }else if(inputID[i]>chemdata.chemdata[index].input[i].id){
                return -1;
            }else return 1;
        }
        return 0;
    }
    public int Find(){
        int up = numchem-1;
        int down = 0;
        int ck;
        while(up>=down){
            int index = (down+up+1)/2;
            ck=Check(index);
            if(ck==0){
                return index;
            }else if(ck==1){
                up = index-1;   
            }else{
                down = index+1;
            }
        }
        return -1;
    }
    public void Show(){
        int index = Find();
        if(index==-1){
            Debug.Log("------");
        }else{
            Debug.Log("ChemId "+index);
        }
    }
    public void Result(){

    }
}
