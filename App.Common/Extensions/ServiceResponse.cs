namespace App.Common.Extensions
{
    public class ServiceResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public TypeMessage MessageType { get; set; }
        public bool Success { get; set; }
        public T Result { get; set; }
    }
}
