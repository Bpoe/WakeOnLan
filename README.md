# WakeOnLan
This project implements a Wake On LAN C# library and PowerShell cmdlet.

Sample usage:  
PS> Import-Module WakeOnLanCmdlet.dll  
PS> Wake-Computer -MacAddress "00-01-02-aa-ab-af"

This example would send a Wake On LAN magic packet to the machine with a MAC address of 00-01-02-aa-ab-af that is on the same subnet as the machine running the cmdlet.

Another sample:  
PS> Import-Module WakeOnLanCmdlet.dll  
PS> Wake-Computer -MacAddress "00-01-02-aa-ab-af" -IPAddress "10.0.1.255"

This example would send a Wake On LAN magic packet to the machine with a MAC address of 00-01-02-aa-ab-af that is on the subnet with a broadcast address of 10.0.1.255. This would be the case for a machine with an IP address of 10.0.0.50 and a subnet mask of 255.255.254.0.
