using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// CSVファイルを読み込んでScriptableObjectを生成・更新する
#if UNITY_EDITOR
public class CsvToScriptableObject : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            //　IndexOfの引数は"/(読み込ませたいファイル名)"
            if (str.IndexOf("/Resources/CardDataBase.csv") != -1)
            {
                Debug.Log("CardDataBase.csvが読み込まれた!!!");
                //　strに格納されたパスから読み込む
                TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
                string assetfile = str.Replace(".csv", ".asset");
                CardDataBase cdb = AssetDatabase.LoadAssetAtPath<CardDataBase>(assetfile);
                if (cdb == null)
                {
                    cdb = ScriptableObject.CreateInstance("CardDataBase") as CardDataBase;
                    AssetDatabase.CreateAsset(cdb, assetfile);
                }

                cdb.datas = CSVSerializer.Deserialize<CardData>(textasset.text);
                EditorUtility.SetDirty(cdb);
                AssetDatabase.SaveAssets();
            }
            else if(str.IndexOf("/Resources/EnemyDataBase.csv") != -1)
            {
                Debug.Log("EnemyDataBaseが読み込まれた!!!");
                //　strに格納されたパスから読み込む
                TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
                string assetfile = str.Replace(".csv", ".asset");
                EnemyDataBase edb = AssetDatabase.LoadAssetAtPath<EnemyDataBase>(assetfile);
                if (edb == null)
                {
                    edb = ScriptableObject.CreateInstance("EnemyDataBase") as EnemyDataBase;
                    AssetDatabase.CreateAsset(edb, assetfile);
                }

                edb.datas = CSVSerializer.Deserialize<EnemyData>(textasset.text);
                EditorUtility.SetDirty(edb);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif