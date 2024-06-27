using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathObstacleManager : MonoBehaviour
{
    public GameObject active_objects;
    //public GameObject bushes;
    //public GameObject snails;

    public Vector2 x_range = new Vector2(-5.65f, 5.65f);
    public float spawn_time = 1f;
    public float speed = 10f;

    public List<GameObject> obstacle_list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //obstacle_list.Add(bushes);
        //obstacle_list.Add(snails);
        StartCoroutine(SpawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform active_obstacle in active_objects.transform)
        {
            active_obstacle.transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            int random = Random.Range(0, obstacle_list.Count);
            GameObject obj = obstacle_list[random];
            PushToActive(obj.transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(spawn_time);
        }
    }

    public void PushToActive(GameObject obstacle)
    {
        obstacle.GetComponent<ObstacleScript>().Begin();
        StartCoroutine(ReturnObstacles(obstacle, obstacle.transform.parent.gameObject));
        obstacle.transform.parent = active_objects.transform;
        float random_x = Random.Range(x_range.x, x_range.y);
        int random_flip = Random.Range(0, 2);
        if (random_flip == 0)
        {
            obstacle.transform.eulerAngles = new Vector3(obstacle.transform.eulerAngles.x, 0, obstacle.transform.eulerAngles.z);
        }
        else
        {
            obstacle.transform.eulerAngles = new Vector3(obstacle.transform.eulerAngles.x, 180, obstacle.transform.eulerAngles.z);
        }
        obstacle.transform.position = new Vector3(random_x, obstacle.transform.position.y, 57.14762f);
    }

    IEnumerator ReturnObstacles(GameObject obstacle, GameObject previous_parent)
    {
        yield return new WaitForSeconds(8);
        obstacle.transform.parent = previous_parent.transform;
        obstacle.GetComponent<ObstacleScript>().End();
        yield return null;
    }

    GameObject FindRootObjectByName(string name)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        GameObject[] rootObjects = activeScene.GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }
}
