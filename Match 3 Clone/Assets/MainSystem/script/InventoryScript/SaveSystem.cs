using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveInventory(InventorySystem _inventory){
        Debug.Log("SaveInventory");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/DataInventory.save";
        FileStream stream = new FileStream(path,FileMode.Create);
        
        //format to basic data structure
         Basic_InventorySystem Dummy = new  Basic_InventorySystem();
        int size = _inventory.container.Count;
        Dummy.id = new int[size];
        Dummy.amount = new int[size];
        Dummy.size = size;
        for(int i=0;i<size;i++){
            Dummy.id[i] = _inventory.container[i].id;
            Dummy.amount[i] = _inventory.container[i].amount;
        }
        
        formatter.Serialize(stream,Dummy);
        stream.Close();
    }
    public static void LoadInventory(InventorySystem _inventory){
        Debug.Log("LoadInventory");
        string path = Application.persistentDataPath + "/DataInventory.save";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
             Basic_InventorySystem Dummy = formatter.Deserialize(stream) as  Basic_InventorySystem;
            stream.Close();
            
            //format to inventorySystem
            _inventory.container.Clear();
            int size = Dummy.size;
            for(int i=0;i<size;i++){
                slot tem = new slot(Dummy.id[i],"",Dummy.amount[i]);
                _inventory.container.Add(tem);
            }           
        }else{
           Debug.LogError("Save file not found in " + path);
        }
    }
    public static void SaveProgress(Basic_ProgressData _Progress,string pp){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + pp;
        FileStream stream = new FileStream(path,FileMode.Create);
        formatter.Serialize(stream,_Progress);
        stream.Close();
    }
    public static Basic_ProgressData LoadProgress(string pp){
        string path = Application.persistentDataPath + pp;
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            Basic_ProgressData ret = formatter.Deserialize(stream) as  Basic_ProgressData;
            stream.Close();
            return ret;
        }else{
           Debug.LogError("Save file not found in " + path);
           return null;
        }
    }
    public static void SaveGraphData(Basic_GraphData _input,string pp){
        Debug.Log("SaveGraphData");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + pp;
        FileStream stream = new FileStream(path,FileMode.Create);
        formatter.Serialize(stream,_input);
        stream.Close();
    }
    public static Basic_GraphData LoadGraphData(string pp){
        Debug.Log("LoadGraphData");
        string path = Application.persistentDataPath + pp;
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            Basic_GraphData ret = formatter.Deserialize(stream) as  Basic_GraphData;
            stream.Close();
            return ret;
        }else{
           Debug.LogError("Save file not found in " + path);
           return null;
        }
    }
}
[System.Serializable]
public class Basic_InventorySystem{
    public int size;
    public int[] id;
    public int[] amount;
}
