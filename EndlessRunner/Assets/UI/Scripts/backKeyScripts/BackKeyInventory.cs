using UnityEngine;
using UnityEngine.SceneManagement;

public class BackKeyInventory : MonoBehaviour
{
    public ItemDetailPopup itemPopUp;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (itemPopUp.gameObject.activeInHierarchy)
            {
                itemPopUp.gameObject.SetActive(false);
            }
            else
            {
                SceneManager.LoadScene("StartMenu");
            }
        }
    }
}
