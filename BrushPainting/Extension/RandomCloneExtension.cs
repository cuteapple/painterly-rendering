using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BrushPainting
{
    public static class RandomCloneExtension
    {
        public static Random Clone(this Random random)
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                bformatter.Serialize(stream, random);
                stream.Seek(0, SeekOrigin.Begin);
                return bformatter.Deserialize(stream) as Random;
            }
        }
    }
}
