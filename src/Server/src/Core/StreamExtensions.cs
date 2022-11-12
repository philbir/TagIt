namespace TagIt;

public static class StreamExtensions
{
    public static byte[] ToByteArray(this Stream input)
    {
        if (input is MemoryStream mem)
        {
            return mem.ToArray();
        }

        byte[] buffer = new byte[input.Length];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

}
