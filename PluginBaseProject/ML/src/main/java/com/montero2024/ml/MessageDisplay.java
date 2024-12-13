package com.montero2024.ml;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;

public class MessageDisplay {

    static MessageDisplay instance = new MessageDisplay();
    public static MessageDisplay getInstance() {return instance; }

    Activity mainActivity;

    AlertDialog dialog;
    AlertDialog.Builder dialogBuilder;

    private static final String LOGTAG = "logs";

    private MessageDisplay() {
    }

    public void Set(Activity activity)
    {
        mainActivity = activity;
    }

    public void SetBasicMessage(String message)
    {
        dialogBuilder = new AlertDialog.Builder(mainActivity);

        dialogBuilder.setMessage(message);

        dialog = dialogBuilder.create();

        dialog.show();
    }

    public void SetPositiveNegativeMessage(String message, String positiveConfirm, String negativeConfirm)
    {
        dialogBuilder.setTitle(message);

        dialogBuilder.setPositiveButton(positiveConfirm, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {

            }
        });

        dialogBuilder.setNegativeButton(negativeConfirm, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {

            }
        });
    }
}