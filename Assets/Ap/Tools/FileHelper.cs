﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.IO;
using System.Security.Cryptography;

namespace Ap.Tools
{
    /// <summary>
    /// 文件帮助
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 创建获取替换现有的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        public static void CreateOrReplace(string path, byte[] bytes)
        {
            if (File.Exists(path))
                File.Delete(path);
            using (FileStream fs = File.Create(path))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }



        /// <summary>
        /// 从streaimingassets里面拷贝内容到指定目录
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static IEnumerator CopyFromStreamingAssets(string from, string to)
        {
            string copyPath = Application.streamingAssetsPath + from;
            WWW www = new WWW(copyPath);

            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {

                using (FileStream fs = File.Open(to, FileMode.OpenOrCreate))
                {
                    fs.Write(www.bytes, 0, www.bytes.Length);
                }
            }
            else
            {

            }

            yield return null;
        }

        /// <summary>
        /// 从Resouces拷贝文本内容
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void CopyTextFromResources(string from, string to)
        {
            TextAsset ta = (TextAsset)Resources.Load(from);
            if (ta != null)
            {
                using (FileStream fs = File.Open(to, FileMode.OpenOrCreate))
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(ta.text);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        /// 获取文件md5
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileMD5(string path)
        {
            if (File.Exists(path) == false)
                return "";
            using (FileStream file = new FileStream(path, System.IO.FileMode.Open))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 从路径里面获取文件名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            path = path.Replace('\\', '/');
            string name = path.Substring(path.LastIndexOf('/'));
            return name;
        }

        /// <summary>
        /// 读取文本内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadText(string path)
        {
            if (File.Exists(path) == false)
                return "";
            string text = "";
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                text = Encoding.ASCII.GetString(bytes);
            }
            return text;
        }

        /// <summary>
        /// 写入文本(追加)
        /// 注意 ,如果文件不存在会先创建
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        public static void WriteText(string path, string text)
        {
            if (File.Exists(path) == false)
                File.Create(path);
            using (FileStream fs = File.Open(path, FileMode.Append))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
