using UnityEngine;

namespace Core
{
    public class SingletonsSceneManger : MonoBehaviour
    {
        [SerializeField] private string gameScene = "LevelTest";

        private void Start()
        {
            SceneLoader.LoadAdditiveScene(gameScene).completed += OnMenuLoaded;
            AudioManager.GetInstance().PlayAudio("BGM");
        }
        
        private void OnMenuLoaded(AsyncOperation obj)
        { 
            SceneLoader.SetActiveScene(gameScene);
            SceneLoader.UnloadScene("Singletons");
            obj.completed -= OnMenuLoaded;
        }
    }
}
