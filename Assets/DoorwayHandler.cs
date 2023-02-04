using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Roots;

namespace Roots 
{
    public class DoorwayHandler : MonoBehaviour
    {
        public int requiredRunesForExit;
        public string nextSceneName;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void interacted()
        {
            Debug.Log(gameObject.name + " was interacted with!");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<PlayerController>().runes >= requiredRunesForExit)
                {
                    Debug.Log("Player can exit game! Quitting..");
                    Scene currentScene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(nextSceneName);
                    SceneManager.UnloadSceneAsync(currentScene);
                }
                else
                {
                    Debug.Log("Not Enough Runes!");
                }
            }
        }
    }
}