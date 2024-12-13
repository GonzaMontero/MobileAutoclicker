package com.montero2024.lv;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Environment;
import android.util.Log;

import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Arrays;

public class LoggerDisplay {
    public static final String LOGTAG = "LOG: ";

    static LoggerDisplay instance = new LoggerDisplay();
    public static LoggerDisplay getInstance() {return instance; }

    Activity mainActivity;

    AlertDialog logsDialog;
    AlertDialog.Builder logsDialogBuilder;

    String filePath;
    String currentLogs = "";

    public LoggerDisplay()
    {
        Log.i(LOGTAG, "Logger Initialized");
    }

    public void SetActivity(Activity activity)
    {
        Log.i(LOGTAG, "Setting Activity");
        mainActivity = activity;
        currentLogs = "Plugin Started";
        filePath = mainActivity.getExternalFilesDir(Environment.DIRECTORY_DOWNLOADS) + "/logs.txt";

        logsDialogBuilder = new AlertDialog.Builder(mainActivity);

        Log.i(LOGTAG, "Dialog Created");

        logsDialogBuilder.setTitle("This will delete all previous logs. Confirm?");
        logsDialogBuilder.setPositiveButton("Confirm", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                ConfirmClearLogs();
            }
        });
        logsDialogBuilder.setNegativeButton("Deny", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });

        logsDialog = logsDialogBuilder.create();
    }

    public void SendLog(String logMsg)
    {
        Log.i(LOGTAG,logMsg);
        logMsg = "\n" + logMsg;
        currentLogs += logMsg;
        SaveLogs(logMsg);
    }

    void SaveLogs(String msg)
    {
        try {
            FileWriter writer = new FileWriter(filePath, true);
            writer.write(msg);
            writer.close();
            Log.i(LOGTAG, "Logs saved to file");
        } catch (IOException e) {
            Log.e(LOGTAG, "Logs file save FAILED: " + e.getMessage());
            currentLogs += "\n ERROR SAVING FILE: " + e.getMessage();
        }
    }

    public String GetLogs()
    {
        return currentLogs;
    }

    public void ReadLogs()
    {
        try{
            FileReader reader = new FileReader(filePath);
            currentLogs = "";
            boolean hasTextLeft = true;
            while(hasTextLeft)
            {
                int c = reader.read();
                if(c < 0) hasTextLeft = false;
                else currentLogs += (char)c;
            }
            reader.close();
        } catch (IOException e) {
            Log.e(LOGTAG, "Logs file read FAILED: " + e.getMessage());
            currentLogs += "\n ERROR READING FILE: " + e.getMessage();
        }
    }

    public void ClearLogs()
    {
        logsDialog.show();
    }
    
    void ConfirmClearLogs()
    {
        try {
            File file = new File(filePath);

            if(!file.exists()) return;

            file.delete();
            Log.i(LOGTAG, "Logs file cleared");
            currentLogs = "";
        } catch (SecurityException e) {
            Log.e(LOGTAG, "Logs file clear FAILED: " + e.getMessage());
            currentLogs += "\n ERROR DELETING FILE: " +  e.getMessage();
        }
    }
}
