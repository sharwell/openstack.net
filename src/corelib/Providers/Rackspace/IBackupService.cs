namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core.Domain;
    using net.openstack.Providers.Rackspace.Objects.Backup;

    public interface IBackupService
    {
        #region Agent Operations

        Task<Agent> GetAgentDetailsAsync(MachineAgentId machineAgentId, CancellationToken cancellationToken);

        Task<Agent> GetAgentDetailsAsync(ServerId serverId, CancellationToken cancellationToken);

        Task SetAgentEnabledAsync(MachineAgentId machineAgentId, bool enabled, CancellationToken cancellationToken);

        Task EnableEncryptionAsync(MachineAgentId machineAgentId, string encryptedPasswordHex, CancellationToken cancellationToken);

        Task RemoveAgentAsync(MachineAgentId machineAgentId, CancellationToken cancellationToken);

        Task MigrateVaultAsync(MachineAgentId sourceMachineAgentId, MachineAgentId destinationMachineAgentId, CancellationToken cancellationToken);

        Task UpdateAgentBackupBehaviorAsync(MachineAgentId machineAgentId, AgentBackupBehavior behavior, CancellationToken cancellationToken);

        #endregion Agent Operations

        #region User Operations

        Task<Agent[]> ListAgentsAsync(CancellationToken cancellationToken);

        Task WakeAgentsAsync(CancellationToken cancellationToken);

        #endregion User Operations

        #region Backup Configuration Operations

        Task<BackupConfiguration> CreateBackupConfigurationAsync(NewBackupConfigurationDescriptor configuration, CancellationToken cancellationToken);

        Task UpdateBackupConfigurationAsync(BackupConfigurationId backupConfigurationId, UpdateBackupConfigurationDescriptor configuration, CancellationToken cancellationToken);

        Task<BackupConfiguration> GetBackupConfigurationAsync(BackupConfigurationId backupConfigurationId, CancellationToken cancellationToken);

        Task<BackupConfiguration[]> ListBackupConfigurationsAsync(CancellationToken cancellationToken);

        Task<BackupConfiguration[]> ListBackupConfigurationsAsync(MachineAgentId machineAgentId, CancellationToken cancellationToken);

        Task<BackupConfiguration> SetBackupConfigurationEnabledAsync(BackupConfigurationId backupConfigurationId, bool enabled, CancellationToken cancellationToken);

        Task RemoveBackupConfigurationAsync(BackupConfigurationId backupConfigurationId, CancellationToken cancellationToken);

        #endregion Backup Configuration Operations

        #region Backup Operations

        Task<BackupId> StartBackupAsync(BackupConfigurationId backupConfigurationId, CancellationToken cancellationToken);

        Task StopBackupAsync(BackupId backupId, CancellationToken cancellationToken);

        Task<Backup> GetBackupAsync(BackupId backupId, CancellationToken cancellationToken);

        Task<Backup[]> ListCompletedBackupsAsync(BackupConfigurationId backupConfigurationId, CancellationToken cancellationToken);

        Task<BackupReport> GetBackupReportAsync(BackupId backupId, CancellationToken cancellationToken);

        #endregion Backup Operations

        #region Restore Configuration Operations

        Task<RestoreConfiguration> CreateRestoreConfigurationAsync(NewRestoreConfigurationDescriptor configuration, CancellationToken cancellationToken);

        Task UpdateRestoreConfigurationAsync(RestoreConfigurationId restoreConfigurationId, UpdateRestoreConfigurationDescriptor configuration, CancellationToken cancellationToken);

        Task IncludeFilesInRestoreConfigurationAsync();

        Task ExcludeFilesInRestoreConfigurationAsync();

        Task ListIncludedFilesAsync();

        Task ListExcludedFilesAsync();

        Task RemoveRestoreConfigurationAsync();

        #endregion Restore Configuration Operations

        #region Restore Operations

        Task ListBackupsForRestoreAsync();

        Task StartRestoreAsync();

        Task StopRestoreAsync();

        Task GetRestoreDetailsAsync();

        Task GetRestoreReportAsync();

        #endregion Restore Operations

        #region Activity Operations

        Task ListActivityAsync(CancellationToken cancellationToken);

        Task ListActivityAsync(AgentId agentId, CancellationToken cancellationToken);

        #endregion Activity Operations
    }
}

#if false
   {
      [
           {
          "ID": 134692,
          "Type": "Backup",
          "ParentId": 6265,
          "DisplayName": "Backup1",
          "IsBackupConfigurationDeleted": false,
          "SourceMachineAgentId": 5,
          "SourceMachineName": "BALAJIMBP",
          "DestinationMachineAgentId": 0,
          "DestinationMachineName": "",
          "CurrentState": "Completed",
          "TimeOfActivity": "\/Date(1357230189000)\/"
              },
              {
          "ID": 134693,
          "Type": "Backup",
          "ParentId": 6265,
          "DisplayName": "Backup1",
          "IsBackupConfigurationDeleted": false,
          "SourceMachineAgentId": 5,
          "SourceMachineName": "BALAJIMBP",
          "DestinationMachineAgentId": 0,
          "DestinationMachineName": "",
          "CurrentState": "Completed",
          "TimeOfActivity": "\/Date(1357230189000)\/"
              }
       ]
    }
#endif
