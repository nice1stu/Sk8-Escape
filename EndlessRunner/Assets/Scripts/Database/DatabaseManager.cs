using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the root reference location of the database
        DatabaseReference dataReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    
}
