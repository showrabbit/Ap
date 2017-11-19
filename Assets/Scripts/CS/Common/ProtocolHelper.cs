using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ProtocolHelper
{
    public static byte[] ObjectToBytes<T>(T instance)
    {
        try
        {
            byte[] array;

            if (instance == null)
            {
                array = new byte[0];
            }
            else
            {
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    Serializer.Serialize<T>(memoryStream, instance);

                    array = new byte[memoryStream.Length];

                    memoryStream.Position = 0L;

                    memoryStream.Read(array, 0, array.Length);
                }
            }
            return array;
        }

        catch (System.Exception)
        {

            return new byte[0];
        }

    }

    public static T BytesToObject<T>(byte[] bytesData, int offset, int length)
    {
        if (bytesData.Length == 0)
        {
            return default(T);
        }
        try
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                memoryStream.Write(bytesData, 0, bytesData.Length);

                memoryStream.Position = 0L;

                T result = Serializer.Deserialize<T>(memoryStream);

                return result;
            }
        }
        catch (System.Exception ex)
        {
            return default(T);
        }

    }
}

