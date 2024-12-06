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
       
        MessagePluginClass = new AndroidJavaClass(MessagePluginClassName);
        MessagePluginInstance = MessagePluginClass.CallStatic<AndroidJavaObject>("getInstance");

        LogPluginClass = new AndroidJavaClass(LogPluginClassName);
        LogPluginInstance = LogPluginClass.CallStatic<AndroidJavaObject>("getInstance");

        MessagePluginInstance.Call("Set", activity);
        LogPluginInstance.Call("SetActivity", activity);
#endif

        Application.logMessageReceived += HandleUnityLog;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

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
        LogPluginInstance.Call("SendLog", log);
#endif
    }

    public string GetLogs()
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        if (LogPluginInstance != null)
            return LogPluginInstance.Call<string>("GetLogs");
        else
            return "Plugin not found";
#endif
        return "Plugin not available";
    }

    public void ReadLogs()
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        LogPluginInstance.Call("ReadLogs");
        GetLogs();
#endif
    }

    public void GenerateLogs()
    {
        LogPluginInstance.Call("ReadLogs");
        Debug.Log("ReadLogsSuccess");
        string s = GetLogs();
        Debug.Log("GetLogsSuccess");
        MessagePluginInstance.Call("SetBasicMessage", s);
        Debug.Log("SetMessageSuccess");
    }

    public void ClearLogs()
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        LogPluginInstance.Call("ClearLogs");
#endif
    }

    //Event Receivers
    void HandleUnityLog(string logString, string stacktrace, LogType type)
    {
        RegisterLog(logString);
    }
}