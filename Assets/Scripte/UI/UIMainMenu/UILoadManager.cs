using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UILoadManager  : MonoBehaviour {
    
    private static UILoadManager Instance;

    [SerializeField] private string _sceneToLoad;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _animationTime;
    private void Awake() {
        if( Instance !=null && Instance!= this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        StaticEvent.OnLevelLoading+= StaticEventOnOnLevelLoading;
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1) {
        _canvasGroup.DOFade(0, _animationTime);
        EventSystem.current.enabled = true;
    }

    private void StaticEventOnOnLevelLoading(object sender, string e) {
        _sceneToLoad = e;
        _canvasGroup.DOFade(1, _animationTime);
        Invoke("LoadScene", _animationTime);
    }

    private void LoadScene() {
        SceneManager.LoadScene(_sceneToLoad);
    }
}