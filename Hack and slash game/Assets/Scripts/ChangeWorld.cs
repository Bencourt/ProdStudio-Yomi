using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeWorld : MonoBehaviour
{

    public LayerMask playerLayer;
    public Transform portalTransform;
    public Scene realWorldScene;
    public BoxCollider cube;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(portalTransform.position + ", " + new Vector3(10, 20, 1) + ", " + portalTransform.rotation + ", ");
        //SceneManager.LoadScene(sceneName: "SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.CheckBox(this.transform.position, new Vector3(10,0),new Quaternion(0,0,0,0),playerLayer)
        if (Physics.CheckBox(portalTransform.position, new Vector3(9, 20, 1), portalTransform.rotation, playerLayer))
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                Debug.Log("Changing scene");
                SceneManager.LoadScene(sceneName: "UnderworldIntro");
            }
            else if (SceneManager.GetActiveScene().name == "UnderworldIntro")
            {
                Debug.Log("Changing scene");
                SceneManager.LoadScene(sceneName: "SampleScene");
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        
        //Gizmos.DrawCube(portalTransform.position, new Vector3(15, 20, 1));
    }
}
