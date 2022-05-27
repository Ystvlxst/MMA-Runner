using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsInit : MonoBehaviour
{

    public static GameAnalyticsInit Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameAnalytics.Initialize();
    }

    public void OnLevelComlete(string level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Start level: " + level);
    }
}
