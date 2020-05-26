using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GoldManagement : MonoBehaviour
{
    public Text goldAmount = null;

    private int m_GoldAmout = 0;
    //public bool isGoldBelowZero = false;
    private const int MAX_GOLD = 999999;

    public int ClearReward = 50;
    public int BuyGuideLine = 300;
    public int BuyMultiShot = 1000;
    public int ExtraGold = 500;
    public int Continue = 0;

    private string g_key;
    private string g_value;
    string GoldData;





    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }
    private void Update()
    {
        //if (m_GoldAmout < 300)
        //    isGoldBelowZero = true;
        //else
        //    isGoldBelowZero = false;
    }
    public void OnApplicationFocus(bool value)
    {
        //Debug.Log("OnApplicationFocus() : " + value);
        if (value)
        {
            LoadGoldInfo();

        }
        else
        {
            SaveGoldInfo();
        }
    }

    public void OnApplicationQuit()
    {
        SaveGoldInfo();
    }
    public void ResetGold()
    {
        LoadGoldInfo();
        m_GoldAmout = 0;
        if (goldAmount != null)
            goldAmount.text = string.Format("{0} G", m_GoldAmout);
        SaveGoldInfo();
    }

    //스테이지 클리어 보상
    public void StageClearRewardGold()
    {
        LoadGoldInfo();
        m_GoldAmout += ClearReward;
        SaveGoldInfo();

    }
    //가이드선 선택 구매후 플레이시 호출
    public void UseGoldForGuideLine()
    {
        if(m_GoldAmout >300)
        {
            LoadGoldInfo();
            m_GoldAmout -= BuyGuideLine;
            SaveGoldInfo();

        }
    }
    public bool IsGoldMoreThan300()
    {
        if (m_GoldAmout > 300)
            return true;
        else
            return false;
    }

    public bool IsGoldMoreThan1000()
    {
        if (m_GoldAmout > 1000)
            return true;
        else
            return false;
    }

    public void UseGoldForMultiShot()
    {
        if (m_GoldAmout > 1000)
        {
            LoadGoldInfo();
            m_GoldAmout -= BuyMultiShot;
            SaveGoldInfo();

        }
    }

    public bool UseGoldForContinue()
    {
        if (m_GoldAmout > Continue)
        {
            LoadGoldInfo();
            m_GoldAmout -= Continue;
            SaveGoldInfo();
            return true;
        }
        else
            return false;
    }

    //골드 다른곳에서 얻을때
    public void GetExtraGold()
    {
        LoadGoldInfo();
        m_GoldAmout += ExtraGold;
        SaveGoldInfo();

        if (goldAmount != null)
            goldAmount.text = string.Format("{0} G", m_GoldAmout);

    }

    public void Init()
    {
        string SecretValue = "Secret_Gold";

        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(SecretValue));


        g_key = "Gold_key";
        g_value = "Encrypt_GoldAmount";

        SetString(g_key, g_value, secret);

        GoldData = GetString(g_key, secret);

        LoadGoldInfo();
        
    }

    public bool LoadGoldInfo()
    {
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey(GoldData))
            {
                m_GoldAmout = PlayerPrefs.GetInt(GoldData);

            }
            else
            {
                m_GoldAmout = 0;
            }

            if(goldAmount!=null)
                goldAmount.text = string.Format("{0} G", m_GoldAmout);

            //Debug.Log("Loaded HeartAmount : " + m_HeartAmount);
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadGoldInfo Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool SaveGoldInfo()
    {
        //Debug.Log("SaveHeartInfo");
        bool result = false;
        try
        {
            PlayerPrefs.SetInt(GoldData, m_GoldAmout);
            PlayerPrefs.Save();
            Debug.Log("Saved GoldtAmount : " + m_GoldAmout);
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("SaveGoldInfo Failed (" + e.Message + ")");
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
        string encryptedString = Convert.ToBase64String(encrypted);

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
        byte[] bytes = Convert.FromBase64String(_value);

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
