using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public enum Site
{
    a,
    b,
    c,
    d,
    e,
    f,
    g,
    h,
    i,
    j

}

public class HugeDataTest : MonoBehaviour {



    List<UserData> myDatas = new List<UserData>();

    int myDatasCount = 10000;

    float preTime = 0;

    public ParticleSystemCurveMode mode;



    private void Start()
    {
        StartCoroutine(InitMyDatas());


    }


    IEnumerator InitMyDatas()
    {
        Debug.Log(Time.time - preTime);

        preTime = Time.time;

        yield return null;

        for (int i = 0; i < myDatasCount; i++)
        {
            byte a = (byte)UnityEngine.Random.Range(0, 256);
            byte b = (byte)UnityEngine.Random.Range(0, 256);
            byte c = (byte)UnityEngine.Random.Range(0, 256);
            byte d = (byte)UnityEngine.Random.Range(0, 256);
            byte e = (byte)UnityEngine.Random.Range(0, 256);
            byte f = (byte)UnityEngine.Random.Range(0, 256);

            myDatas.Add(new UserData(a, b, c, d, e, f));
        }

        yield return null;

        UserData calData = CalcurateData();

        Debug.Log(string.Format("{0}/{1}/{2}/{3}", calData.age, calData.gender, calData.politicality, calData.activity));

        SaveUserData();
        //myDatas.Clear();
        //StartCoroutine(InitMyDatas());
    }

    UserData CalcurateData()
    {
        ulong totalA = 0;
        ulong totalB = 0;
        ulong totalC = 0;
        ulong totalD = 0;

        for (int j = 0; j < myDatasCount; j++)
        {
            totalA += myDatas[j].age; //10 : 80
            totalB += myDatas[j].gender; // -100 : 100
            totalC += myDatas[j].politicality; // -100 : 100
            totalD += myDatas[j].activity; // 0 : 100
        }

        Debug.Log(string.Format("{0}/{1}/{2}/{3}", totalA, totalB, totalC, totalD));

        byte averageA = (byte)(totalA / (ulong)myDatas.Count);
        byte averageB = (byte)(totalB / (ulong)myDatas.Count);
        byte averageC = (byte)(totalC / (ulong)myDatas.Count);
        byte averageD = (byte)(totalD / (ulong)myDatas.Count);

        return new UserData(averageA,averageB,averageC,averageD);
    }

    void SaveUserData()
    {


        Debug.Log(Application.dataPath);

        StreamWriter sWriter = new StreamWriter(Application.dataPath + "/data.bin");
        BinaryFormatter bin = new BinaryFormatter();
        bin.Serialize(sWriter.BaseStream, myDatas);
        sWriter.Close();


    }

}


[System.Serializable]
public class UserData
{
    public byte age;
    public byte gender;
    public byte politicality;
    public byte activity;
    public byte variability;
    public byte susceptibility;

    public Site userSite;

    public UserData(byte ma = 1, byte mb = 1, byte mc = 1, byte md = 1, byte me = 1, byte mf = 1)
    {
        age = ma; gender = mb; politicality = mc; activity = md;
        variability = me; susceptibility = mf;
     }

}
