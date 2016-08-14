using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ap.Base
{
    /// <summary>
    /// 文件表格
    /// </summary>
    public class FileTable : IDisposable
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string[] Columns
        {
            get
            {
                return m_Columns;
            }
        }
        private string[] m_Columns = null;

        /// <summary>
        /// 行
        /// </summary>
        public Dictionary<int, string[]> Rows
        {
            get
            {
                return m_Rows;
            }
        }
        private Dictionary<int, string[]> m_Rows = null;

        public FileTable()
        {

        }

        public FileTable(string path)
        {
            Open(path);
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="path"></param>
        public bool Open(string path)
        {
            if (File.Exists(path) == false)
                return false;

            if (m_Rows == null)
                m_Rows = new Dictionary<int, string[]>();

            int lineIndex = 0;

            using (StreamReader sr = new StreamReader(File.Open(path, FileMode.Open)))
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    Debugger.Log(string.Format("Error in FileTable : {0}", path));
                }
                else
                {
                    string[] splits = line.Split('\t');
                    m_Columns = splits;

                    while (sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        splits = line.Split('\t');
                        if (splits.Length != m_Columns.Length)
                        {
                            Debugger.Log(string.Format("Error in FileTable : {0}:{1}", path, lineIndex));
                            break;
                        }
                        else
                        {
                            m_Rows.Add(lineIndex, splits);
                            lineIndex++;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 获取某一行的数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string[] GetRow(int index)
        {
            return m_Rows[index];
        }

        public void Dispose()
        {
            if (m_Columns != null)
            {
                m_Columns = null;
            }
            if (m_Rows != null)
            {
                m_Rows.Clear();
                m_Rows = null;
            }
        }
    }
}
