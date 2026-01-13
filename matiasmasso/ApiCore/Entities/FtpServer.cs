using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Partner ftp server with whom we usually connect to interchange documents
/// </summary>
public partial class FtpServer
{
    /// <summary>
    /// Ftp server owner; foreign key for CliGral table 
    /// </summary>
    public Guid Owner { get; set; }

    /// <summary>
    /// Server name
    /// </summary>
    public string Servername { get; set; } = null!;

    /// <summary>
    /// User name to log in the server
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Password to log in the server
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Ftp port
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// True if secure sockets layer connection
    /// </summary>
    public bool? Ssl { get; set; }

    /// <summary>
    /// True if connexion should be stablished under passive mode
    /// </summary>
    public bool? PassiveMode { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    public virtual CliGral OwnerNavigation { get; set; } = null!;
}
