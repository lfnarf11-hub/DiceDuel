using System.Collections;
using Given.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Given.Manager
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;
    
        private Material _targetMaterial;
        private bool _isLoading;

        public static LoadingManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            gameObject.SetActive(false);

            _targetMaterial = GetComponentInChildren<Image>().material;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private void OnDisable()
        {
            if(Instance == this)
                _targetMaterial.SetFloat(StaticUtility.TimeID, 0);
        }


        public void LoadLevelById(int id)
        {
            if (_isLoading) return;
            gameObject.SetActive(true);
            StartCoroutine(OnTransitionBegin(id));
        }

        private IEnumerator OnTransitionBegin(int id)
        {
        
            _isLoading = true;
        
            float t = 0;
            while (t < transitionDuration)
            {
                t += Time.deltaTime;
                float p = t/ transitionDuration;
                _targetMaterial.SetFloat(StaticUtility.TimeID, p);
                yield return null;
            }

            //Load new, unload old
            AsyncOperation op = SceneManager.LoadSceneAsync(id);

            //Operation cannot be null.
            op!.completed += _ =>
            {
                StartCoroutine(OnTransitionEnd());
            };
        
        }

        private IEnumerator OnTransitionEnd()
        {
            float t = 0;
            //Load level.
            while (t < transitionDuration)
            {
                t += Time.deltaTime;
                float p = t/ transitionDuration;
                _targetMaterial.SetFloat(StaticUtility.TimeID, 1-p); // inverse
                yield return null;
            }

            _isLoading = false;
            gameObject.SetActive(false);

        }




    }
}
