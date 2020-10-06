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
    public GameObject mix;
    public InventorySystem backpack;


    public int up,down,ck;
    public int bug;
    void Start(){
        inputID = new List<int>();
        inputAmount = new List<int>();
        numchem = chemdata.chemdata.Count;

      //  bug=0;
      //  up = numchem-1;
      //  down = 0;
    }
    public override void GetItem(int _id,int _amount){
        //Debug.Log("Sir gut");
        int l=inputID.Count;
        for(int i = 0;i<l ;i++){
            if(inputID[i]==_id){
                inputAmount[i]++;
                mix.GetComponent<InputMix>().Display(0);
                return;
            }
        }
        inputID.Add(_id);
        inputAmount.Add(1);
        mix.GetComponent<InputMix>().Display(0);
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
                if(inputAmount[i]>1){
                    inputAmount[i]--;
                }else{
                    inputAmount.Remove(inputAmount[i]);
                    inputID.Remove(inputID[i]);
                }
                mix.GetComponent<InputMix>().Display(0);
                return;
            }
        }
    }
    public int Checkeq(int index){
        int k = chemdata.chemdata[index].input.Count;
        for(int i=0;i<inputID.Count;i++){
            if(i==k)return-1;
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
        //int ck;
        while(up>=down){
            int index = (down+up+1)/2;
            //Debug.Log("index "+index+"\n\tup "+up+" down "+down);
            ck=Checkeq(index);
            if(ck==0){
              //  Debug.Log("Found");
              //  Debug.Log("in "+inputID.Count+" eq "+chemdata.chemdata[index].input.Count);
               if(inputID.Count==chemdata.chemdata[index].input.Count) return index;
               if(inputID.Count<chemdata.chemdata[index].input.Count) down = index+1;
            }else if(ck==1){
                up = index-1;   
            }else{
                down = index+1;
            }
            //Debug.Log("ck "+ck);
        }
        return -1;
    }
    public void Show(){
        preinp();
        chemdata.chemdata.Sort(sorteq);
        int index = -1;
        index = Find();
        //Debug.Log("eq "+bug+" : "+Find());
        //bug++;
        if(index==-1){
            Debug.Log("------");
        }else{
            Debug.Log("ChemId "+index);
            product(index);
        }
        
    }
    private void product(int index){
        inputID.Clear();
        inputAmount.Clear();
        int n = chemdata.chemdata[index].output.Count;
        Debug.Log(chemdata.chemdata.Count);
        for(int i=0;i<n;i++){
            Debug.Log("index "+index+" i "+i);
            inputID.Add(chemdata.chemdata[index].output[i].id);
            inputAmount.Add(chemdata.chemdata[index].output[i].amount);
        }
        mix.GetComponent<InputMix>().Display(0);
    }
    private void preinp(){
        List<pair> lip = new List<pair>();
        int k = inputID.Count;
        for(int i=0; i<k; i++){
            lip.Add(new pair(inputID[i],inputAmount[i]));
        }
        lip.Sort(sortinp);
        for(int i=0; i<k;i++){
            inputID[i]=lip[i].first;
            inputAmount[i]=lip[i].second;
        }

    }
    private int sorteq(ChemEq a, ChemEq b){
        var ina = a.input;
        var inb = b.input;
        int al = ina.Count;
        int bl = inb.Count;
        for(int i = 0;i<al;i++){
            if(ina[i].id>inb[i].id)return 1;
            if(ina[i].id<inb[i].id)return -1;

            if(ina[i].amount>inb[i].amount)return 1;
            if(ina[i].amount<inb[i].amount)return -1;

            if(i==bl-1)return 1;
        }
        return -1;
    }
    private int sortinp(pair a, pair b){
        //Debug.Log("Id "+a.first+" Amount "+a.second+"\n"+"\tId "+b.first+" Amount "+b.second);
        if(a.first > b.first)return 1;
        if(a.first < b.first)return -1;
        if(a.second > b.second)return 1;
        if(a.second < b.second)return -1;
        return 0;
    }
}
public class pair{
    public int first;
    public int second;
    public pair(int a, int b){
        first =a;
        second = b;
    }
}
