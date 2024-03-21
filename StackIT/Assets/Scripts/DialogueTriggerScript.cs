using System;
using UnityEngine;
using UnityEngine.UI;

namespace LMNT
{
    public class DialogueTriggerScript : MonoBehaviour
    {
        private LMNTSpeech speech;
        private Coroutine speechCoroutine;
        public GameObject triggerGameObject; // Reference to the game object that will trigger the speech

        void Start()
        {
            speech = GetComponent<LMNTSpeech>();
            StartCoroutine(speech.Prefetch());
        }

        void Update()
        {
            // Check if the triggerGameObject is active
            if (triggerGameObject.activeSelf)
            {
                // If it's active and the coroutine is not running, start the coroutine
                if (speechCoroutine == null)
                {
                    speechCoroutine = StartCoroutine(speech.Talk());
                }
            }
            else
            {
                // If the triggerGameObject is inactive and the coroutine is running, stop the coroutine
                if (speechCoroutine != null)
                {
                    StopCoroutine(speechCoroutine);
                    speechCoroutine = null;
                }
            }
        }
    }
}