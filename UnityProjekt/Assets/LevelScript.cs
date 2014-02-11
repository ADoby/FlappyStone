using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour {

    //public GameObject prefab;

    public float minY = -4, maxY = 4;


    public GameObject[] HindernissPrefabs;
    public List<GameObject> hindernisse;

    public float speed = 2.0f;
    private float meterTimer = 0.0f;

    public float createNewHindernissEveryMeter = 2.0f;

    public Vector3 startPos;

    private float lastY;

    public float speedChange = 0.5f;
    public float maxSpeed = 10.0f;

    public Transform player;

    void Awake()
    {
        foreach (GameObject prefab in HindernissPrefabs)
        {
            GameObjectPool.Instance.CreatePool(prefab.GetComponent<Hinderniss>().poolName, prefab, null, 3);
        }
    }

	// Use this for initialization
	void Start () {
        
	}

    public float PlayerLastX = 0.0f;

	// Update is called once per frame
	void Update () {
        startPos = new Vector3(player.position.x, 0, 0) + Vector3.right * 10.0f;
        
        Queue<GameObject> delete = new Queue<GameObject>();

        foreach (GameObject hinderNissObject in hindernisse)
        {
            if (hinderNissObject.transform.position.x <= player.position.x - 10)
            {
                delete.Enqueue(hinderNissObject);
                GameObjectPool.Instance.Despawn(hinderNissObject.GetComponent<Hinderniss>().poolName, hinderNissObject);
            }
        }
        foreach (GameObject item in delete)
        {
            hindernisse.Remove(item);
        }


        meterTimer += player.position.x - PlayerLastX;
        PlayerLastX = player.position.x;
        if (meterTimer >= createNewHindernissEveryMeter)
        {
            meterTimer = 0;
            float newY = Random.Range(minY, maxY);
            newY = Mathf.Clamp(newY, lastY - 1.5f, lastY + 1.5f);
            GameObject go = GameObjectPool.Instance.Spawn(HindernissPrefabs[Random.Range(0, HindernissPrefabs.Length)].GetComponent<Hinderniss>().poolName, startPos + Vector3.up * newY, Quaternion.identity);
            //go.transform.localScale = new Vector3(1, Random.Range(0.8f, 2.0f), 1);
            
            hindernisse.Add(go);
        }

	}
}
