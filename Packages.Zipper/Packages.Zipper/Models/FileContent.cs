using System.Runtime.Serialization;

namespace Packages.Zipper.Models
{
    [DataContract]
    public class FileContent
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string Extension { get; set; }

        [DataMember]
        public byte[] Content { get; set; }
    }
}
