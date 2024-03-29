﻿using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersMgr : MonoBehaviour
{
    public static ParametersMgr instance;

    private Dictionary<string, string> parameters = new Dictionary<string, string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        string csv = File.ReadAllText(Application.dataPath + "/gameplay_Parameters.csv");
        Debug.Log(csv);
        parameters = SplitCSV(csv);

        Debug.Log("PARAMETERS :");
        Debug.Log(PrintParameters());
    }

    private string PrintParameters()
    {
        string result = "";
        foreach (KeyValuePair<string, string> keyval in parameters)
        {
            result += keyval.Key + " => " + keyval.Value;
            result += '\n';
        }
        return result;
    }

    private Dictionary<string, string> SplitCSV(string csv)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        string[] lines = csv.Split("\n"[0]);
        int length = lines.Count();
        foreach (string str in lines)
        {
            string[] line = str.Split(';');
            // Debug.Log(line.Count());
            if (line.Count() == 2)
            {
                result.Add(line[0], line[1]);
            }
        }
        return result;
    }

    public float GetParameterFloat(string parameterName)
    {
        if (parameters.ContainsKey(parameterName))
        {
            string str = parameters[parameterName];
            float result;
            bool parsingSuccessful = float.TryParse(str, out result);
            if (parsingSuccessful)
            {
                // Debug.Log(parameterName + " has been called!");
                return result;
            }
            else
            {
                Debug.LogError("Parsing Error!");
                return 0.0f;
            }
        }
        else
        {
            Debug.LogError("No parameters matching this name!");
            return 0.0f;
        }
    }

    public int GetParameterInt(string parameterName)
    {
        if (parameters.ContainsKey(parameterName))
        {
            string str = parameters[parameterName];
            int result;
            bool parsingSuccessful = int.TryParse(str, out result);
            if (parsingSuccessful)
            {
                Debug.Log(parameterName + " has been called!");
                return result;
            }
            else
            {
                Debug.LogError("Parsing Error!");
                return 0;
            }
        }
        else
        {
            Debug.LogError("No parameters matching this name!");
            return 0;
        }
    }

    public bool GetParameterBool(string parameterName)
    {
        if (parameters.ContainsKey(parameterName))
        {
            string str = parameters[parameterName];
            bool result;
            bool parsingSuccessful = bool.TryParse(str, out result);
            if (parsingSuccessful)
            {
                // Debug.Log(parameterName + " has been called!");
                return result;
            }
            else
            {
                Debug.LogError("Parsing Error!");
                return false;
            }
        }
        else
        {
            Debug.LogError("No parameters matching this name!");
            return false;
        }
    }
}
