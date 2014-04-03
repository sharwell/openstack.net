namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using ExtensibleJsonObject = net.openstack.Core.Domain.ExtensibleJsonObject;

    /// <summary>
    /// Provides predefined property names which may be used as the <see cref="ImageUpdateOperation.Path"/>
    /// property for an <see cref="ImageUpdateOperation"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class PredefinedImageProperties
    {
        /// <summary>
        /// The <see cref="Image.Name"/> property.
        /// </summary>
        public static readonly string Name = "name";

        /// <summary>
        /// The <see cref="Image.Tags"/> property.
        /// </summary>
        public static readonly string Tags = "tags";

        /// <summary>
        /// The common name of the operating system distribution. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        /// <remarks>
        /// The following table shows the allowed values for this property.
        /// <list type="table">
        ///  <listheader>
        ///   <description>Value</description>
        ///   <description>Description</description>
        ///  </listheader>
        ///  <item>
        ///   <description>arch</description>
        ///   <description>Arch Linux</description>
        ///  </item>
        ///  <item>
        ///   <description>centos</description>
        ///   <description>Community Enterprise Operating System</description>
        ///  </item>
        ///  <item>
        ///   <description>debian</description>
        ///   <description>Debian</description>
        ///  </item>
        ///  <item>
        ///   <description>fedora</description>
        ///   <description>Fedora</description>
        ///  </item>
        ///  <item>
        ///   <description>freebsd</description>
        ///   <description>FreeBSD</description>
        ///  </item>
        ///  <item>
        ///   <description>gentoo</description>
        ///   <description>Gentoo Linux</description>
        ///  </item>
        ///  <item>
        ///   <description>mandrake</description>
        ///   <description>Mandrakelinux (MandrakeSoft)</description>
        ///  </item>
        ///  <item>
        ///   <description>mes</description>
        ///   <description>Mandriva Enterprise Server</description>
        ///  </item>
        ///  <item>
        ///   <description>msdos</description>
        ///   <description>Microsoft Disk Operating System</description>
        ///  </item>
        ///  <item>
        ///   <description>netbsd</description>
        ///   <description>NetBSD</description>
        ///  </item>
        ///  <item>
        ///   <description>netware</description>
        ///   <description>Novell NetWare</description>
        ///  </item>
        ///  <item>
        ///   <description>openbsd</description>
        ///   <description>OpenBSD</description>
        ///  </item>
        ///  <item>
        ///   <description>opensolaris</description>
        ///   <description>OpenSolaris</description>
        ///  </item>
        ///  <item>
        ///   <description>opensuse</description>
        ///   <description>openSUSE</description>
        ///  </item>
        ///  <item>
        ///   <description>rhel</description>
        ///   <description>Red Hat Enterprise Linux</description>
        ///  </item>
        ///  <item>
        ///   <description>sled</description>
        ///   <description>SUSE Linux Enterprise Desktop</description>
        ///  </item>
        ///  <item>
        ///   <description>ubuntu</description>
        ///   <description>Ubuntu</description>
        ///  </item>
        ///  <item>
        ///   <description>windows</description>
        ///   <description>Microsoft Windows</description>
        ///  </item>
        /// </list>
        /// </remarks>
        public static readonly string OsDistro = "os_distro";

        /// <summary>
        /// The operating system version as specified by the distributor. The value
        /// of this property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string OsVersion = "os_version";

        /// <summary>
        /// <placeholder>placeholder</placeholder>. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string Protected = "protected";

        /// <summary>
        /// <placeholder>placeholder</placeholder>. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string ContainerFormat = "container_format";

        /// <summary>
        /// <placeholder>placeholder</placeholder>. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string DiskFormat = "disk_format";

        /// <summary>
        /// <placeholder>placeholder</placeholder>. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string MinDisk = "min_disk";

        /// <summary>
        /// <placeholder>placeholder</placeholder>. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string MinRam = "min_ram";

        /// <summary>
        /// <placeholder>placeholder</placeholder>. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string RamdiskId = "ramdisk_id";

        /// <summary>
        /// <placeholder>placeholder</placeholder>. The value of this
        /// property, if specified by the Image Service, is specified in the
        /// <see cref="ExtensibleJsonObject.ExtensionData"/> dictionary.
        /// </summary>
        public static readonly string KernelId = "kernel_id";
    }
}
