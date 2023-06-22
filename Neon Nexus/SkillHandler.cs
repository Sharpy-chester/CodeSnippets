using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Menu;

public class SkillHandler : MonoBehaviour
{
    [SerializeField] public List<Item> skills { get; private set; } = new List<Item>();
    [SerializeField] Skill[] allSkills;
    

    void Start()
    {
        LoadGame();
        DontDestroyOnLoad(gameObject);
    }

    public void AddItems(Item item)
    {
        if(!skills.Contains(item))
        {
            skills.Add(item);
        }
    }

    public void AddItems(List<Item> items)
    {
        skills.AddRange(items);

    }

    public void SaveGame(List<int> skillsBought)
    {
        BinaryFormatter bf = new BinaryFormatter();
        File.Delete(Application.persistentDataPath + Path.DirectorySeparatorChar + "playerData.dat");
        FileStream file = File.Create(Application.persistentDataPath + Path.DirectorySeparatorChar + "playerData.dat");
        SkillSave data = new SkillSave();
        data.SkillList = skillsBought.ToArray();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "playerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "playerData.dat", FileMode.OpenOrCreate);
            SkillSave data = (SkillSave)bf.Deserialize(file);
            file.Close();
            int[] ids = data.SkillList;
            foreach (int i in ids)
            {
                if(allSkills != null)
                {
                    foreach (Skill s in allSkills)
                    {
                        if (i == s.id)
                        {
                            s.FreePurchase();
                            AddItems(s.itemToGivePlayer);
                        }
                    }
                }
            }
        }
    }
}

[Serializable]
class SkillSave
{
    public int[] SkillList;
}
