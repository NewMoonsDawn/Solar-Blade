using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnTimer = 5f;
    public float spawnNumber = 1f;

    public GameObject meelee;


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Spawn());
    }
    

    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(spawnTimer);

        for (int i =0;i<spawnNumber;i++)
        {
            float x = Random.Range(-18f, 18f);
            float y = Random.Range(-8f, 8f);

            Vector2 position = new Vector2(x, y);

            GameObject enemy = Instantiate(meelee, position, Quaternion.identity);
            enemy.GetComponent<Rigidbody2D>().WakeUp();
            new WaitForSeconds(spawnTimer);
        }
        

        
    }
}
