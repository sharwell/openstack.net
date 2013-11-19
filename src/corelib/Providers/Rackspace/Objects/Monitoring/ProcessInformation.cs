namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ProcessInformation
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("pid")]
        private int? _pid;

        [JsonProperty("exe_name")]
        private string _exeName;

        [JsonProperty("exe_root")]
        private string _exeRoot;

        [JsonProperty("exe_cwd")]
        private string _exeCwd;

        [JsonProperty("cred_group")]
        private string _credGroup;

        [JsonProperty("cred_user")]
        private string _credUser;

        [JsonProperty("memory_size")]
        private long? _memorySize;

        [JsonProperty("memory_resident")]
        private long? _memoryResident;

        [JsonProperty("memory_page_faults")]
        private long? _memoryPageFaults;

        [JsonProperty("memory_minor_faults")]
        private long? _memoryMinorFaults;

        [JsonProperty("memory_major_faults")]
        private long? _memoryMajorFaults;

        [JsonProperty("memory_share")]
        private long? _memoryShare;

        [JsonProperty("state_name")]
        private string _stateName;

        [JsonProperty("state_threads")]
        private int? _stateThreads;

        [JsonProperty("state_ppid")]
        private int? _stateParentPid;

        [JsonProperty("state_priority")]
        private int? _statePriority;

        [JsonProperty("state_nice")]
        private int? _stateNice;

        [JsonProperty("time_start_time")]
        private long? _timeStartTime;

        [JsonProperty("time_sys")]
        private long? _timeSys;

        [JsonProperty("time_total")]
        private long? _timeTotal;

        [JsonProperty("time_user")]
        private long? _timeUser;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInformation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ProcessInformation()
        {
        }

        /// <summary>
        /// Gets the process ID.
        /// </summary>
        public int? ProcessId
        {
            get
            {
                return _pid;
            }
        }

        /// <summary>
        /// Gets the path to the executable.
        /// </summary>
        public string ExecutableName
        {
            get
            {
                return _exeName;
            }
        }

        /// <summary>
        /// Gets the root namespace of the process.
        /// </summary>
        public string ExecutableRoot
        {
            get
            {
                return _exeRoot;
            }
        }

        /// <summary>
        /// Gets the current working directory of the process.
        /// </summary>
        public string CurrentDirectory
        {
            get
            {
                return _exeCwd;
            }
        }

        /// <summary>
        /// Gets the group of the user who owns the process.
        /// </summary>
        public string OwnerGroup
        {
            get
            {
                return _credGroup;
            }
        }

        /// <summary>
        /// Gets the user who owns the process.
        /// </summary>
        public string OwnerUser
        {
            get
            {
                return _credUser;
            }
        }

        /// <summary>
        /// Gets the total address space of the process, in bytes.
        /// </summary>
        public long? MemorySize
        {
            get
            {
                return _memorySize;
            }
        }

        /// <summary>
        /// Gets the total resident memory of the process, in bytes.
        /// </summary>
        public long? MemoryResident
        {
            get
            {
                return _memoryResident;
            }
        }

        /// <summary>
        /// Gets the total number of faults.
        /// </summary>
        public long? MemoryPageFaults
        {
            get
            {
                return _memoryPageFaults;
            }
        }

        /// <summary>
        /// Gets the total number of minor page faults.
        /// </summary>
        /// <remarks>
        /// Minor faults generally do not involve disk latency.
        /// </remarks>
        public long? MemoryMinorFaults
        {
            get
            {
                return _memoryMinorFaults;
            }
        }

        /// <summary>
        /// Gets the total number of major page faults.
        /// </summary>
        /// <remarks>
        /// Major faults generally involve disk latency.
        /// </remarks>
        public long? MemoryMajorFaults
        {
            get
            {
                return _memoryMajorFaults;
            }
        }

        /// <summary>
        /// Gets the total resident memory that is shared with other processes, in bytes.
        /// </summary>
        public long? MemoryShare
        {
            get
            {
                return _memoryShare;
            }
        }

        /// <summary>
        /// Gets the name of the executable.
        /// </summary>
        public string Name
        {
            get
            {
                return _stateName;
            }
        }

        /// <summary>
        /// Gets the number of threads the process owns.
        /// </summary>
        public int? ThreadCount
        {
            get
            {
                return _stateThreads;
            }
        }

        /// <summary>
        /// Gets the parent process ID.
        /// </summary>
        public int? ParentProcessId
        {
            get
            {
                return _stateParentPid;
            }
        }

        /// <summary>
        /// Gets the priority of the process.
        /// </summary>
        /// <remarks>
        /// Higher numbers indicate lower priority.
        /// </remarks>
        public int? StatePriority
        {
            get
            {
                return _statePriority;
            }
        }

        /// <summary>
        /// Gets the nice value set on the process.
        /// </summary>
        /// <remarks>
        /// Higher numbers indicate lower priority.
        /// </remarks>
        /// <value>
        /// The nice value set on the process, or <c>null</c> if nice is not set on the process.
        /// </value>
        public int? StateNice
        {
            get
            {
                return _stateNice;
            }
        }

        /// <summary>
        /// Gets the start time of the process.
        /// </summary>
        public DateTimeOffset? StartTime
        {
            get
            {
                return DateTimeOffsetExtensions.ToDateTimeOffset(_timeStartTime);
            }
        }

        /// <summary>
        /// Gets the total time spent executing system calls.
        /// </summary>
        public TimeSpan? SystemTime
        {
            get
            {
                if (_timeSys == null)
                    return null;

                return TimeSpan.FromMilliseconds(_timeSys.Value);
            }
        }

        /// <summary>
        /// Gets the total time spent executing user code.
        /// </summary>
        public TimeSpan? UserTime
        {
            get
            {
                if (_timeUser == null)
                    return null;

                return TimeSpan.FromMilliseconds(_timeUser.Value);
            }
        }

        /// <summary>
        /// Gets the total execution time of the process.
        /// </summary>
        public TimeSpan? TotalTime
        {
            get
            {
                if (_timeTotal == null)
                    return null;

                return TimeSpan.FromMilliseconds(_timeTotal.Value);
            }
        }
    }
}
