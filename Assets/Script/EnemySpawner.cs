using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private List<GameObject> enemyPrefabs;

    [SerializeField]
    private bool isTop;

    [SerializeField]
    private float spawningTime;
    private float timer;

    [SerializeField]
    private Transform targetTransform;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 )
        {
            Spawning();
            timer = spawningTime;

        }
    }

    private void Spawning()
    {
        int index =Random.Range( 0, enemyPrefabs.Count );
        GameObject go = Instantiate(enemyPrefabs[index], transform.position,Quaternion.identity);
        go.transform.parent = this.transform;
        go.GetComponent<Enemy>().Init(targetTransform,isTop);
        enemies.Add(go);
    }
}
