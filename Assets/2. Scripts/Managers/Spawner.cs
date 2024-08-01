using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Spawner : MonoBehaviour
{ // �̴Ͼ� ���� ����
    public Transform[] redSpawnPoints;
    public Transform[] blueSpawnPoints;
    public Transform blueFinalPoint; // ������� ��ǥ
    public Transform redFinalPoint; // �������� ��ǥ
    public Transform checkPointL;
    public Transform checkPointR;

    private List<EnemyData> enemyDatas = new List<EnemyData>();
    private float coolTime = 60f;

    string xmlFileName = "EnemyData";

    private void Start()
    {
        LoadXML(xmlFileName);
        InvokeRepeating("SpawnLoop", 0f, coolTime);
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

    private void SpawnLoop()
    {
        for (int i = 0; i < 6; i++)
            Spawn(0, true, false);
        for (int i = 0; i < 6; i++)
            Spawn(0, true, true);
        for(int i = 0; i < 3; i++)
            Spawn(1, false, false);
        for(int i = 0; i < 3; i++)
            Spawn(1, false, true);
    }

    private void Spawn(int idx, bool isRed, bool isLeft)
    {
        GameObject obj = GameManager.Instance.pool.Get(idx);
        if (isRed) //red team ai
        {
            obj.GetComponent<MinionController>().finalPoint = redFinalPoint;
            obj.GetComponent<MinionController>().Init(enemyDatas[idx]);
            if (isLeft)
            {
                obj.GetComponent<MinionController>().checkPoint = checkPointL;
                obj.transform.position = redSpawnPoints[0].position; //Left Red spawn
            }
            else
            {
                obj.GetComponent<MinionController>().checkPoint = checkPointR;
                obj.transform.position = redSpawnPoints[1].position; //Right Red spawn
            }
        }
        else // blue team ai
        {
            // TODO : �Ʊ� �����Ϳ� �и�
            obj.GetComponent<MinionController>().finalPoint = blueFinalPoint;
            obj.GetComponent<MinionController>().Init(enemyDatas[0]); // TODO : ������ �߰��ϰ� idx�� �ٲٱ�
            if (isLeft)
            {
                obj.GetComponent<MinionController>().checkPoint = checkPointR;
                obj.transform.position = blueSpawnPoints[0].position; //Left Blue spawn
                Debug.Log(obj.transform.position + " to " + blueSpawnPoints[0].position);
            }
            else
            {
                obj.GetComponent<MinionController>().checkPoint = checkPointL;
                obj.transform.position = blueSpawnPoints[1].position; //Right Blue spawn
            }
        }

        obj.SetActive(true); // �ʱ�ȭ�� �Ϸ�Ǳ� ���� navAgent�� ����Ǵ� ���� ���� ���� �ڵ�
        // ��Ȱ��ȭ�� �������� Ǯ������ ������ ��� �ʱ�ȭ�� ��ģ �� Ȱ��ȭ��Ű�� ��
    }
}
