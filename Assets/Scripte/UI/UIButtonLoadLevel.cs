using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonLoadLevel : MonoBehaviour
{
    
    [SerializeField] private string _levelName;
    
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
        
    }

    private void LoadLevel() {
        SceneManager.LoadScene(_levelName);
    }
}
