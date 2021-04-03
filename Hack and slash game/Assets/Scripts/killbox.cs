using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killbox : MonoBehaviour
{
    public LayerMask playerLayer;
    //public Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.ToString() + "killbox activated");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
