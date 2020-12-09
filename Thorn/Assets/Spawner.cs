using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private int waveNumber = 1;
    private int wavesCompleted = 0;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] float rate;
    [SerializeField] float waveDelay;

    [SerializeField] GameObject player;
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject mage;
    [SerializeField] GameObject ogre;

    [SerializeField] GameObject waveText;
    [SerializeField] GameObject restartText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnWaves");
        restartText.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        waveText.GetComponent<Text>().text = "Waves Completed: " + wavesCompleted;
        if (player.GetComponent<Player>().dead)
        {
            waveText.GetComponent<Text>().text = "You survived " + wavesCompleted + " waves";
            restartText.GetComponent<Text>().enabled = true;
        }
        if (player.GetComponent<Player>().dead && Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject e in enemies)
            {
                if (e.name.Substring(0, 8) == "Skeleton")
                {
                    e.GetComponent<Skeleton>().isQuitting = true;
                }
                else if (e.name.Substring(0, 4) == "Mage")
                {
                    e.GetComponent<Mage>().isQuitting = true;
                }
                else if (e.name.Substring(0, 4) == "Ogre")
                {
                    e.GetComponent<Ogre>().isQuitting = true;
                }
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject skele = Instantiate(skeleton, new Vector3(1, 10, -1), Quaternion.identity);
            skele.GetComponent<Skeleton>().player = this.player;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject cast = Instantiate(mage, new Vector3(5, 10, -1), Quaternion.identity);
            cast.GetComponent<Mage>().player = this.player;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject og = Instantiate(ogre, new Vector3(9, 10, -1), Quaternion.identity);
            og.GetComponent<Ogre>().player = this.player;
        }

    }

    IEnumerator SpawnWaves()
    {
        while (!player.GetComponent<Player>().dead)
        {
            float currDelay = waveDelay;
            float currRate = rate;
            float enemies = 2 + (wavesCompleted / 4);
            float bossEnemies = 1 + (wavesCompleted / 4);
            while (enemies != 0)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Vector3 pos = spawnPoints[spawnIndex].transform.position;
                pos.z = -1;


                if (waveNumber == 1)
                {
                    GameObject enemy = Instantiate(skeleton, pos, Quaternion.identity);
                    enemy.GetComponent<Skeleton>().player = this.player;
                    enemies--;
                }
                else if (waveNumber == 2)
                {
                    int rand = Random.Range(0, 2);
                    if (rand == 0)
                    {
                        GameObject enemy = Instantiate(skeleton, pos, Quaternion.identity);
                        enemy.GetComponent<Skeleton>().player = this.player;
                    }
                    else
                    {
                        GameObject enemy = Instantiate(mage, pos, Quaternion.identity);
                        enemy.GetComponent<Mage>().player = this.player;
                    }

                    enemies--;
                }
                else if (waveNumber == 3)
                {
                    GameObject enemy = Instantiate(mage, pos, Quaternion.identity);
                    enemy.GetComponent<Mage>().player = this.player;
                    enemies--;
                }
                else if (waveNumber == 4)
                {
                    if (bossEnemies != 0)
                    {
                        GameObject enemy = Instantiate(ogre, pos, Quaternion.identity);
                        enemy.GetComponent<Ogre>().player = this.player;
                        bossEnemies--;
                    }
                    else
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            GameObject enemy = Instantiate(skeleton, pos, Quaternion.identity);
                            enemy.GetComponent<Skeleton>().player = this.player;
                        }
                        else
                        {
                            GameObject enemy = Instantiate(mage, pos, Quaternion.identity);
                            enemy.GetComponent<Mage>().player = this.player;
                        }

                        enemies--;
                    }

                }



                while (currRate > 0)
                {
                    currRate -= Time.deltaTime;
                    yield return null;
                }
                currRate = rate;
                yield return null;
            }
            while (GameObject.FindWithTag("Enemy") != null)
            {
                yield return null;
            }
            wavesCompleted++;
            while (currDelay > 0)
            {
                currDelay -= Time.deltaTime;
                yield return null;
            }

            waveNumber++;
            if (waveNumber > 4)
            {
                waveNumber = 1;
            }




            yield return null;
        }

    }
}
