using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[ExcelAsset(LogOnImport = true, ExcelName = "DataGame")]
public class DataGame : ScriptableObject
{
 
    public List<PlayerLevel> PlayerLevel;
    public List<Tool> Tool;
  
}
