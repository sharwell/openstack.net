using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class Container
    {
        /// <summary>
        /// Gets the name of the container.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets the number of objects in the container.
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        /// <summary>
        /// Gets the total space utilized by the objects in this container.
        /// </summary>
        [DataMember]
        public long Bytes { get; set; }

        //internal IObjectStorageProvider CloudFilesProvider { get; set; }

        //public void AddHeader(string name)
        //{
        //    CloudFilesProvider.Add
        //}
    }
}
