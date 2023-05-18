using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHelper {
    private Sprite face;
    private Dialog dialog;

    void Start()
    {
        // Prepare the dialog
        dialog = new Dialog();
        dialog.speakerFace = face; // Set the path to your speaker face sprite


    }

    public void DoSampleDialog() {
        DialogManager.instance.StartDialog(dialog);
    }
}
