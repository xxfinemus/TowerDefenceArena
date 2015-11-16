using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;

public class dbscript : MonoBehaviour {

    private static SqliteConnection dbconn;
    void Start () {
        string connection;
        string filePath = Application.persistentDataPath + "/" + "towerdefDB.s3db";
        //filen skal hentes et andet sted fra hvis man kører android
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!File.Exists(filePath))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "towerdefDB.s3db");
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filePath, loadDB.bytes);
            }
        }
        connection = "URI=file:" + filePath;
        dbconn = new SqliteConnection(connection);
    }
    public static object[,] GetTopScores()
    {
        dbconn.Open();
        object[,] array = new object[10,3];
        string sql = "select * from highscore order by score desc limit 10";
        SqliteCommand cmd = new SqliteCommand(sql, dbconn);
        SqliteDataReader reader = cmd.ExecuteReader();
        int i = 0;
        
        while (reader.Read())
        {
            array[i, 0] = reader["name"];
            array[i, 1] = reader["score"];
            array[i, 2] = reader["wave"];
            i++;
        }
        dbconn.Close();
        return array;
    }
    public static void InsertNewScore(string name, int score, int wave)
    {
        dbconn.Open();
        if (name == "")
        {
            name = "noname";
        }
        string sql = string.Format("insert into highscore (name, score, wave) values ( '{0}', {1} , {2})" , name, score, wave);

        SqliteCommand cmd = new SqliteCommand(sql, dbconn);
        cmd.ExecuteNonQuery();
        dbconn.Close();
    }
}
