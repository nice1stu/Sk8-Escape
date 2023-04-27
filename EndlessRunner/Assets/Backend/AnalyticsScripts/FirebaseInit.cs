using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var app = FirebaseApp.DefaultInstance;
        });
    }
}
