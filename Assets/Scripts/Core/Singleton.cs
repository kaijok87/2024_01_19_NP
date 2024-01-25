using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static bool shutdownCheck = false;

    static T instance;
    public static T Instance 
    {
        get 
        {
            if (shutdownCheck) 
            {
                Debug.Log($"{typeof(T).Name} 싱글톤은 삭제중입니다.");
                return null;
            }
            if (Instance == null ) 
            {
                if (FindObjectOfType<T>(true) == null)
                {
                    GameObject singletonGameObject = new GameObject($"{typeof(T).Name}_Singleton");
                    instance = singletonGameObject.AddComponent<T>();
                    DontDestroyOnLoad(singletonGameObject);
                }
                else 
                {

                    Debug.Log($"인스턴스는 없는데 {typeof(T).Name}_Singleton 을 만들수없다.");
                    Debug.Log($"instance [ {instance} ]" );
                }
            }
            return Instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else 
        {
            if (instance != this) //맴버에 등록된값이 내자신이 아닌경우는 싱글톤패턴에 어긋남으로 
            {
                Debug.Log($"{instance.name} 초기화 하는데 이미 존재하는경우가 있다고한다? 씬이동이나 비동기를 의심해야되.");
                Destroy(this.gameObject);//삭제
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeScene;
    }

    private void OnApplicationQuit()
    {
        shutdownCheck = true;
    }
    
    private void ChangeScene(Scene scene, LoadSceneMode loadMode)
    {
        SceneChangeDataSetting(scene,loadMode);
    }
    
    protected virtual void SceneChangeDataSetting(Scene scene, LoadSceneMode loadMode)
    {

    }
}
