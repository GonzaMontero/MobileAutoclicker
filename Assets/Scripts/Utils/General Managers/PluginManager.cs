using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Utils;
using UnityEngine;

public class PluginManager : MonoBehaviourSingleton<PluginManager>
{
    const string MessagePluginPackageName = "com.montero2024.ml";
    const string MessagePluginClassName = MessagePluginPackageName + ".MessageDisplay";

    const string LogPluginPackageName = "com.montero2024.lv";
    const string LogPluginClassName = LogPluginPackageName + ".LoggerDisplay";

#if UNITY_ANDROID || PLATFORM_ANDROID
    AndroidJavaClass MessagePluginClass;
    AndroidJavaObject MessagePluginInstance;

    AndroidJavaClass LogPluginClass;
    AndroidJavaObject LogPluginInstance;
#endif

    //Unity Events
    public override void Awake()
    {
        base.Awake();

#if UNITY_EDITOR
        Destroy(this);
        return;
#endif
#if UNITY_ANDROID || PLATFORM_ANDROID
        AndroidJavaClass unityClass =
            new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity =
            unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        MessagePluginInstance.Call("Set", activity);

        MessagePluginClass = new AndroidJavaClass(MessagePluginClassName);
        MessagePluginInstance = MessagePluginClass.CallStatic<AndroidJavaObject>("getInstance");

        LogPluginClass = new AndroidJavaClass(LogPluginClassName);
        LogPluginInstance = LogPluginClass.CallStatic<AndroidJavaObject>("getInstance");
#endif

        Application.logMessageReceived += HandleUnityLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleUnityLog;
    }

    //Methods
    public void RegisterTimeLog()
    {
        Debug.Log("Unity - Send Log: " + Time.time);
    }

    public void RegisterLog(string log)
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        MessagePluginInstance.Call("SendLog", log);
#endif
    }

    public string GetLogs()
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        if (MessagePluginInstance != null)
            return MessagePluginInstance.Call<string>("GetLogs");
        else
            return "Plugin not found";
#endif
        return "Plugin not available";
    }

    public void ReadLogs()
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        MessagePluginInstance.Call("ReadLogs");
        GetLogs();
#endif
    }

    public void ClearLogs()
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        MessagePluginInstance.Call("ClearLogs");
#endif
    }

    //Event Receivers
    void HandleUnityLog(string logString, string stacktrace, LogType type)
    {
        RegisterLog(logString);
    }
}