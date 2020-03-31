using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StageLock : MonoBehaviour
{

    public GameObject[] LockButtons;

    private int m_ClearedStage = 0;

    [SerializeField]
    private int CurrentStageNum;

    private string L_key;
    private string L_value;
    string StageClearData;

    void Awake()
    {
        Init();
    }

    private void Start()
    {
        if (LockButtons != null)
        {
            for (int a = 0; a < LockButtons.Length; a++)
            {

                if (a < m_ClearedStage)  // 3 => true , 0 => false
                    LockButtons[a].SetActive(false);
                else
                    LockButtons[a].SetActive(true);


            }
        }
    }
    public void AddCheatNum()
    {
        LoadLockedInfo();
        m_ClearedStage++;
        SaveLockedInfo();
    }
    
    public void AddClearStageInfo()
    {
        LoadLockedInfo();
        if(m_ClearedStage<CurrentStageNum)
            m_ClearedStage = CurrentStageNum;
        SaveLockedInfo();
    }

    public void ResetClearedStage()
    {
        LoadLockedInfo();
        m_ClearedStage = 0;
        SaveLockedInfo();
    }

    public void OnApplicationFocus(bool value)
    {
        //Debug.Log("OnApplicationFocus() : " + value);
        if (value)
        {
            LoadLockedInfo();

        }
        else
        {
            SaveLockedInfo();
        }
    }

    public void OnApplicationQuit()
    {
        SaveLockedInfo();
    }

    public void Init()
    {
        string SecretValue = "Secret_Cleared";
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(SecretValue));

        L_key = "Lock_key";
        L_value = "Encrypt_UnlockedStage";

        SetString(L_key, L_value, secret);
        StageClearData = GetString(L_key, secret);

        LoadLockedInfo();
    }

    public bool LoadLockedInfo()
    {
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey(StageClearData))
            {
                m_ClearedStage = PlayerPrefs.GetInt(StageClearData);

            }
            else
            {
                m_ClearedStage = 0;
            }

            
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadLockedInfo Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool SaveLockedInfo()
    {
        //Debug.Log("SaveHeartInfo");
        bool result = false;
        try
        {
            PlayerPrefs.SetInt(StageClearData, m_ClearedStage);
            PlayerPrefs.Save();
            Debug.Log("Saved Unlock stage Amount : " + m_ClearedStage);
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Save Failed (" + e.Message + ")");
        }
        return result;
    }


    // 암호화
    public static void SetString(string _key, string _value, byte[] _secret)
    {
        // Hide '_key' string.  
        MD5 md5Hash = MD5.Create();
        byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Encrypt '_value' into a byte array  
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_value);

        // Eecrypt '_value' with 3DES.  
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = _secret;
        des.Mode = CipherMode.ECB;
        ICryptoTransform xform = des.CreateEncryptor();
        byte[] encrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // Convert encrypted array into a readable string.  
        string encryptedString = System.Convert.ToBase64String(encrypted);

        // Set the ( key, encrypted value ) pair in regular PlayerPrefs.  
        PlayerPrefs.SetString(hashKey, encryptedString);

        Debug.Log("SetString hashKey: " + hashKey + " Encrypted Data: " + encryptedString);
    }

    public static string GetString(string _key, byte[] _secret)
    {
        // Hide '_key' string.  
        MD5 md5Hash = MD5.Create();
        byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Retrieve encrypted '_value' and Base64 decode it.  
        string _value = PlayerPrefs.GetString(hashKey);
        byte[] bytes = System.Convert.FromBase64String(_value);

        // Decrypt '_value' with 3DES.  
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = _secret;
        des.Mode = CipherMode.ECB;
        ICryptoTransform xform = des.CreateDecryptor();
        byte[] decrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // decrypte_value as a proper string.  
        string decryptedString = System.Text.Encoding.UTF8.GetString(decrypted);

        Debug.Log("GetString hashKey: " + hashKey + " GetData: " + _value + " Decrypted Data: " + decryptedString);
        return decryptedString;
    }

}
