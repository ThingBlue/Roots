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

        private bool isOpen;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Collider2D>().isTrigger = false;
            isOpen = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void interacted(GameObject object)
        {
            Debug.Log(gameObject.name + " was interacted with!");

            isOpen = !isOpen;

            if(isOpen)
            {
                GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                GetComponent<Collider2D>().isTrigger = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<PlayerController>().runes >= requiredRunesForExit)
                {
                    Debug.Log("Player can exit game! Moving on..");
                    Scene currentScene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(nextSceneName);
                    SceneManager.UnloadSceneAsync(currentScene);
                }
            }
        }
    }
}