using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    static string path = @"C:\Users\Public\ignore";

    public Animator transition;
    private float WaitTime = 1f;
    public void PlayGame()
    {
        StartCoroutine(LoadLevel("Tutorial"));
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OptionsMenu()
    {
        GameObject.Find("Canvas").transform.Find("MainMenu").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Options_menu").gameObject.SetActive(true);
    }
    private IEnumerator LoadLevel(string levelname)
    {
        transition.SetTrigger("Transition");
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(levelname);
    }
    public void LoadGame()
    {
        if (!File.Exists(path))
        {
            Debug.Log("No File Found");
            return;
        }

        using (StreamReader file = new StreamReader(path))
        {
            string value = file.ReadLine();
           
            SceneManager.LoadScene(Decrypt(value));
        }
    }
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "Kurvaneatspesniekas";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}
