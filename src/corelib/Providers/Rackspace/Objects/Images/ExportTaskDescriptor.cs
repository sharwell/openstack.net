namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON request for creating an Export Image task.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/POST_exportTask_v2_tasks_Image_Task_Calls.html">Export Task (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ExportTaskDescriptor : ImageTaskDescriptor<ExportTaskInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskDescriptor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExportTaskDescriptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskDescriptor"/> class for
        /// the specified image ID and target container where the image will be saved.
        /// </summary>
        /// <param name="imageId">The ID of the image to export. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="receivingContainer">The name of the container where the image will be saved.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="receivingContainer"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="receivingContainer"/> is empty.</exception>
        public ExportTaskDescriptor(ImageId imageId, string receivingContainer)
            : this(new ExportTaskInput(imageId, receivingContainer))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskDescriptor"/> class with
        /// the specified input argument.
        /// </summary>
        /// <remarks>
        /// This method is primarily intended as an extensibility point for customization by
        /// service providers. Users who only need to use the default behavior may call
        /// <see cref="ExportTaskDescriptor(ImageId, string)"/> instead.
        /// </remarks>
        /// <param name="input">An <see cref="ExportTaskInput"/> object describing the image export request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="input"/> is <see langword="null"/>.</exception>
        public ExportTaskDescriptor(ExportTaskInput input)
            : base(ImageTaskType.Export, input)
        {
        }
    }
}
