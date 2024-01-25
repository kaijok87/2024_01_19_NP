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
                Debug.Log($"{typeof(T).Name} �̱����� �������Դϴ�.");
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

                    Debug.Log($"�ν��Ͻ��� ���µ� {typeof(T).Name}_Singleton �� ���������.");
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
            if (instance != this) //�ɹ��� ��ϵȰ��� ���ڽ��� �ƴѰ��� �̱������Ͽ� ��߳����� 
            {
                Debug.Log($"{instance.name} �ʱ�ȭ �ϴµ� �̹� �����ϴ°�찡 �ִٰ��Ѵ�? ���̵��̳� �񵿱⸦ �ǽ��ؾߵ�.");
                Destroy(this.gameObject);//����
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
