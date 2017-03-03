//-----------------------------------------------------------------------
// <copyright file="WakeComputer.cs">
//     Copyright
// </copyright>
//-----------------------------------------------------------------------

namespace Poesoft
{
    using System.Management.Automation;
    using System.Net;

    /// <summary>
    /// Implements the Wake-Computer PowerShell Cmdlet
    /// </summary>
    [Cmdlet("Wake", "Computer")]
    public class WakeComputer : PSCmdlet
    {
        /// <summary>
        /// The IP Address to address the magic packet to
        /// </summary>
        private IPAddress _ipAddress = IPAddress.Broadcast;

        /// <summary>
        /// Gets or sets the MAC address of the machine to wake
        /// </summary>
        [Parameter(ParameterSetName = "Default", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the IP address to address the magic packet to
        /// </summary>
        [Parameter(ParameterSetName = "Default", Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string IpAddress
        {
            get { return this._ipAddress.ToString(); }

            set { IPAddress.TryParse(value, out this._ipAddress); }
        }

        /// <summary>
        /// The main method for a cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            var macAddress = Poesoft.MacAddress.Parse(this.MacAddress);
            WakeOnLan.Send(macAddress, this._ipAddress);
        }
    }
}
