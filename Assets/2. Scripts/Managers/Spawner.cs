using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Spawner spawns Enemy
    public Transform[] spawnPoints;

    private List<EnemyData> enemyDatas = new List<EnemyData>();
    private float coolTime;

    string xmlFileName = "EnemyData";

    private void Start()
    {
        LoadXML(xmlFileName);
    }

    private void LoadXML(string fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load(fileName);
        if (txtAsset == null)
        {
            Debug.LogError("Failed to load XML file: " + fileName);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);

        XmlNodeList all_nodes = xmlDoc.SelectNodes("root/Sheet1");
        foreach (XmlNode node in all_nodes)
        {
            EnemyData newData = new EnemyData();

            newData.mobType = int.Parse(node.SelectSingleNode("mobType").InnerText);
            newData.health = float.Parse(node.SelectSingleNode("health").InnerText);
            newData.maxHealth = newData.health;
            newData.speed = float.Parse(node.SelectSingleNode("speed").InnerText);
            newData.attack = float.Parse(node.SelectSingleNode("attack").InnerText);
            newData.attackRange = float.Parse(node.SelectSingleNode("attackRange").InnerText);
            newData.attackSpeed = float.Parse(node.SelectSingleNode("attackSpeed").InnerText);

            enemyDatas.Add(newData);
        }
    }

    private void Update()
    {
        coolTime += Time.deltaTime;

        if(coolTime > GameManager.Instance.spawnTime)
        {
            Spawn(0);
            coolTime = 0;
        }
    }

    private void Spawn(int idx)
    {
        GameObject obj = GameManager.Instance.pool.Get(idx);
        obj.GetComponent<EnemyController>().Init(enemyDatas[idx]);
        obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}
