namespace Gepard.Core.Responses
{
    public class ByteResponse
    {
        public byte[] ArrayBytes { get; set; }
        public bool ConnectionAlive { get; set; }

        public ByteResponse(byte[] arrayBytes, bool conntectionAlive)
        {
            ArrayBytes = arrayBytes;
            ConnectionAlive = conntectionAlive;
        }
    }
}
