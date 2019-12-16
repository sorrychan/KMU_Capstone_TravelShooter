using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HeartRechargeManagement : MonoBehaviour
{
    #region Heart
    //화면에 표시하기 위한 UI변수
    public Text heartRechargeTimer = null;
    public Text heartAmountLabel = null;


    private int m_HeartAmount = 0; //보유 하트 개수
    public bool isHeartBelowZero = false;
    private DateTime m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
    private const int MAX_HEART = 20; //하트 최대값
    public int HeartRechargeInterval = 30;// 하트 충전 간격(단위:초)
    private Coroutine m_RechargeTimerCoroutine = null;
    private int m_RechargeRemainTime = 0;
    #endregion


    //암호화 및 데이터 저장을 위한 값들
    private string key;
    private string _value;
    string ret;

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        if (m_HeartAmount < 3)
        {
            if (m_HeartAmount < 0)
                m_HeartAmount = 0;
            isHeartBelowZero = true;
        }
        else
        {
            isHeartBelowZero = false;
        }
    }
    //게임 초기화, 중간 이탈, 중간 복귀 시 실행되는 함수
    public void OnApplicationFocus(bool value)
    {
        //Debug.Log("OnApplicationFocus() : " + value);
        if (value)
        {
            LoadHeartInfo();
            LoadAppQuitTime();
            SetRechargeScheduler();
        }
        else
        {
            SaveHeartInfo();
            SaveAppQuitTime();
        }
    }
    //값 초기화
    public void ResetPlayerPrefabData()
    {
        m_RechargeRemainTime = 60;
        heartRechargeTimer.text = string.Format("{0} s", m_RechargeRemainTime);
        m_HeartAmount = 18;
        heartAmountLabel.text = string.Format("{0}", m_HeartAmount.ToString());
    }
    //게임 종료 시 실행되는 함수
    public void OnApplicationQuit()
    {
        //Debug.Log("GoodsRechargeTester: OnApplicationQuit()");
        SaveHeartInfo();
        SaveAppQuitTime();
    }

    //버튼 이벤트에 이 함수를 연동한다.
    public void OnClickUseHeart()
    {
        //Debug.Log("OnClickUseHeart");
        UseHeart();

    }

    public void Init()
    {
        Time.timeScale = 1;
        m_HeartAmount = 0;
        m_RechargeRemainTime = 0;
        m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
        //Debug.Log("heartRechargeTimer : " + m_RechargeRemainTime + "s");
        heartRechargeTimer.text = string.Format("{0} s", m_RechargeRemainTime);

        string SecretValue = "SecretValueKey";

        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(SecretValue));

        // Game progress ( key, value ) pair.  
         key = "Heart_Key";
         _value = "Encrypt_HeartAmount";

        // Insert ( key, value ) pair.  
        SetString(key, _value, secret);

        // Retrieve ( key, value ) pair.  
         ret = GetString(key, secret);

        // Output.  
        //Debug.Log("Sec Value: " + SecretValue);
        //Debug.Log(key + " : " + ret);

        LoadHeartInfo();
        LoadAppQuitTime();
        SetRechargeScheduler();

    }
    public bool LoadHeartInfo()
    {

       
        //Debug.Log("LoadHeartInfo");

        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey(ret))
            {
                m_HeartAmount = PlayerPrefs.GetInt(ret);

            }
            else
            {
                m_HeartAmount = MAX_HEART;
            }
            heartAmountLabel.text = m_HeartAmount.ToString();
            //Debug.Log("Loaded HeartAmount : " + m_HeartAmount);
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadHeartInfo Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool SaveHeartInfo()
    {
        //Debug.Log("SaveHeartInfo");
        bool result = false;
        try
        {
            PlayerPrefs.SetInt(ret, m_HeartAmount);
            PlayerPrefs.Save();
            //Debug.Log("Saved HeartAmount : " + m_HeartAmount);
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("SaveHeartInfo Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool LoadAppQuitTime()
    {
        //Debug.Log("LoadAppQuitTime");
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey("AppQuitTime"))
            {
                //Debug.Log("PlayerPrefs has key : AppQuitTime");
                var appQuitTime = string.Empty;
                appQuitTime = PlayerPrefs.GetString("AppQuitTime");
                m_AppQuitTime = DateTime.FromBinary(Convert.ToInt64(appQuitTime));
            }
            //Debug.Log(string.Format("Loaded AppQuitTime : {0}", m_AppQuitTime.ToString()));
            //appQuitTimeLabel.text = string.Format("AppQuitTime : {0}", m_AppQuitTime.ToString());
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadAppQuitTime Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool SaveAppQuitTime()
    {
        //Debug.Log("SaveAppQuitTime");
        bool result = false;
        try
        {
            var appQuitTime = DateTime.Now.ToLocalTime().ToBinary().ToString();
            PlayerPrefs.SetString("AppQuitTime", appQuitTime);
            PlayerPrefs.Save();
            //Debug.Log("Saved AppQuitTime : " + DateTime.Now.ToLocalTime().ToString());
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("SaveAppQuitTime Failed (" + e.Message + ")");
        }
        return result;
    }
    public void SetRechargeScheduler(Action onFinish = null)
    {
        if (m_RechargeTimerCoroutine != null)
        {
            StopCoroutine(m_RechargeTimerCoroutine);
        }
        var timeDifferenceInSec = (int)((DateTime.Now.ToLocalTime() - m_AppQuitTime).TotalSeconds);
        //Debug.Log("TimeDifference In Sec :" + timeDifferenceInSec + "s");
        var heartToAdd = timeDifferenceInSec / HeartRechargeInterval;
        //Debug.Log("Heart to add : " + heartToAdd);
        var remainTime = timeDifferenceInSec % HeartRechargeInterval;
        //Debug.Log("RemainTime : " + remainTime);
        m_HeartAmount += heartToAdd;
        if (m_HeartAmount >= MAX_HEART)
        {
            m_HeartAmount = MAX_HEART;
        }
        else
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(remainTime, onFinish));
        }
        heartAmountLabel.text = string.Format("{0}", m_HeartAmount.ToString());
        //Debug.Log("HeartAmount : " + m_HeartAmount);
    }
    public void UseHeart(Action onFinish = null)
    {
      
        if (m_HeartAmount < 3)
        {
            heartAmountLabel.text = string.Format("{0}", m_HeartAmount.ToString());
            //isHeartBelowZero = true;
            return;
        }
        //else
        //    isHeartBelowZero = false;

        m_HeartAmount-=3;
        SaveHeartInfo();
        SaveAppQuitTime();
        heartAmountLabel.text = string.Format("{0}", m_HeartAmount.ToString());
        if (m_RechargeTimerCoroutine == null)
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(HeartRechargeInterval));
        }
        if (onFinish != null)
        {

            onFinish();
        }
        
    }
    private IEnumerator DoRechargeTimer(int remainTime, Action onFinish = null)
    {
        //Debug.Log("DoRechargeTimer");
        if (remainTime <= 0)
        {
            m_RechargeRemainTime = HeartRechargeInterval;
        }
        else
        {
            m_RechargeRemainTime = remainTime;
        }
        //Debug.Log("heartRechargeTimer : " + m_RechargeRemainTime + "s");
        heartRechargeTimer.text = string.Format("{0} s", m_RechargeRemainTime);

        while (m_RechargeRemainTime > 0)
        {
            //Debug.Log("heartRechargeTimer : " + m_RechargeRemainTime + "s");
            heartRechargeTimer.text = string.Format("{0} s", m_RechargeRemainTime);
            m_RechargeRemainTime -= 1;
            yield return new WaitForSeconds(1f);
        }
        m_HeartAmount++;
        if (m_HeartAmount >= MAX_HEART)
        {
            m_HeartAmount = MAX_HEART;
            m_RechargeRemainTime = 0;
            heartRechargeTimer.text = string.Format("{0} s", m_RechargeRemainTime);
            //Debug.Log("HeartAmount reached max amount");
            m_RechargeTimerCoroutine = null;
        }
        else
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(HeartRechargeInterval, onFinish));
        }
        heartAmountLabel.text = string.Format("{0}", m_HeartAmount.ToString());
        //Debug.Log("HeartAmount : " + m_HeartAmount);
    }


    // 암호화, 현재는 HeartAmount만
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

