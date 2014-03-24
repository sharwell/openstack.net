namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON request for creating an Import Image task.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/POST_importTask_v2_tasks_Image_Task_Calls.html">Import Task (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskDescriptor : ImageTaskDescriptor<ImportTaskInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskDescriptor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImportTaskDescriptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskDescriptor"/> class
        /// with the specified import path and desired image name.
        /// </summary>
        /// <param name="importFrom">The container and object name of the image in Object Storage.</param>
        /// <param name="imageName">The desired name assigned to the imported image.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="importFrom"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="imageName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="importFrom"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="imageName"/> is empty.</para>
        /// </exception>
        public ImportTaskDescriptor(string importFrom, string imageName)
            : this(new ImportTaskInput(importFrom, imageName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskDescriptor"/> class with
        /// the specified input argument.
        /// </summary>
        /// <remarks>
        /// This method is primarily intended as an extensibility point for customization by
        /// service providers. Users who only need to use the default behavior may call
        /// <see cref="ImportTaskDescriptor(string, string)"/> instead.
        /// </remarks>
        /// <param name="input">An <see cref="ImportTaskInput"/> object describing the image export request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="input"/> is <see langword="null"/>.</exception>
        public ImportTaskDescriptor(ImportTaskInput input)
            : base(ImageTaskType.Import, input)
        {
        }
    }
}
