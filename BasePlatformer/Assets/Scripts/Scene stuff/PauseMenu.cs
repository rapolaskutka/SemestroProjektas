using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    static string path = @"C:\Users\Public\ignore";

    public static bool Paused = false;
    public GameObject UI;
    public class GUI_Extra
    {
        private static List<GameObject> GUI_Elements = new List<GameObject>();

        public GUI_Extra()
        {

        }
        public static void Add_Gui(GameObject gui)
        {
            GUI_Elements.Add(gui);
        }
        public static void Remove_Gui_All()
        {
            foreach(GameObject gui in GUI_Elements)
            {
                Destroy(gui);
            }
            GUI_Elements.Clear();
        }
    }
    private void Start()
    {
      UI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GUI_Extra.Remove_Gui_All();
            if (Paused) Resume();
            else Pause();
        }
    }
    public void Resume()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    void Pause()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OptionsMenu()
    {
        SceneManager.LoadScene("Best");
    }

    public void SaveGame()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("wat");

        }

        using (StreamWriter file = new StreamWriter(path))
        {
            string value = SceneManager.GetActiveScene().name;
            file.WriteLine(Encrypt(value));
        }
       

    }
    public static string Encrypt(string clearText)
    {
        string EncryptionKey = "Kurvaneatspesniekas";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

}
