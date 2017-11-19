using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using LuaInterface;

namespace Ap.Net
{
    public class ByteBuffer
    {
        MemoryStream m_Stream = null;
        BinaryWriter m_Writer = null;


        public BinaryReader Reader
        {
            get
            {
                return m_Reader;
            }

        }
        BinaryReader m_Reader = null;

        public ByteBuffer()
        {
            m_Stream = new MemoryStream();
            m_Writer = new BinaryWriter(m_Stream);
            m_Reader = new BinaryReader(m_Stream);
        }

        public ByteBuffer(byte[] data)
        {
            if (data != null)
            {
                m_Stream = new MemoryStream(data);
                m_Reader = new BinaryReader(m_Stream);
            }
            else
            {
                m_Stream = new MemoryStream();
                m_Writer = new BinaryWriter(m_Stream);
            }
        }

        public void Close()
        {
            if (m_Writer != null) m_Writer.Close();
            if (m_Reader != null) m_Reader.Close();

            m_Stream.Close();
            m_Writer = null;
            m_Reader = null;
            m_Stream = null;
        }

        public void WriteByte(byte v)
        {
            m_Writer.Write(v);
        }

        public void WriteInt(int v)
        {
            m_Writer.Write((int)v);
        }

        public void WriteShort(ushort v)
        {
            m_Writer.Write((ushort)v);
        }

        public void WriteLong(long v)
        {
            m_Writer.Write((long)v);
        }

        public void WriteFloat(float v)
        {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            m_Writer.Write(BitConverter.ToSingle(temp, 0));
        }

        public void WriteDouble(double v)
        {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            m_Writer.Write(BitConverter.ToDouble(temp, 0));
        }

        public void WriteString(string v)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(v);
            m_Writer.Write((ushort)bytes.Length);
            m_Writer.Write(bytes);
        }

        public void WriteBytes(byte[] v)
        {
            //writer.Write((int)v.Length);
            m_Writer.Write(v);
        }

        public void WriteBuffer(LuaByteBuffer strBuffer)
        {
            WriteBytes(strBuffer.buffer);
        }

        public byte ReadByte()
        {
            return m_Reader.ReadByte();
        }

        public int ReadInt()
        {
            return (int)m_Reader.ReadInt32();
        }

        public ushort ReadShort()
        {
            return (ushort)m_Reader.ReadInt16();
        }

        public long ReadLong()
        {
            return (long)m_Reader.ReadInt64();
        }

        public float ReadFloat()
        {
            byte[] temp = BitConverter.GetBytes(m_Reader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }

        public double ReadDouble()
        {
            byte[] temp = BitConverter.GetBytes(m_Reader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }

        public string ReadString()
        {
            ushort len = ReadShort();
            byte[] buffer = new byte[len];
            buffer = m_Reader.ReadBytes(len);
            return Encoding.UTF8.GetString(buffer);
        }

        public byte[] ReadBytes(int len)
        {
            //int len = ReadInt();
            return m_Reader.ReadBytes(len);
        }

        public byte[] ReadBytesToEnd()
        {
            int len = (int)(m_Reader.BaseStream.Length - m_Reader.BaseStream.Position);
            return ReadBytes(len);
        }

        public LuaByteBuffer ReadBuffer(int len)
        {
            byte[] bytes = ReadBytes(len);
            return new LuaByteBuffer(bytes);
        }

        public byte[] ToBytes()
        {
            m_Writer.Flush();
            return m_Stream.ToArray();
        }

        public void Flush()
        {
            m_Writer.Flush();
        }
    }
}