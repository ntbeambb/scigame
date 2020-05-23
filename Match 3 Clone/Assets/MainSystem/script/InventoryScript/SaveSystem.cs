using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   
   public static void SaveInventory(InventorySystem _inventory){
       BinaryFormatter formatter = new BinaryFormatter();
       string path = Application.persistentDataPath + "/DataInventory.save";
       FileStream stream = new FileStream(path,FileMode.Create);

       formatter.Serialize(stream,_inventory);
       stream.Close();
   }
   public static InventorySystem LoadInventory(){
       string path = Application.persistentDataPath + "/DataInventory.save";
       if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            InventorySystem data = formatter.Deserialize(stream) as InventorySystem;
            stream.Close();

            return data;
       }else{
           Debug.LogError("Save file not found in " + path);
           return null;
       }
   }
}
