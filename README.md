#WinDevices
##A higher level wrapper for [Win32_PnPEntity](https://msdn.microsoft.com/en-us/library/aa394353%28v=vs.85%29.aspx) AKA plug and play drivers.

##Properties
| type | Property | Returns |
| -----|----------------|---------|
|  uint16 |  Availability | Availability and status of the device. See [return values](#availability-return-values) |
|  string |  Caption |  Short description of the object |
|  string |  ClassGuid |  Globally unique identifier (GUID) of this Plug and Play device |
|  string |  CompatibleID[] | Not implemented in this library as of writing.  |
|  uint32 |  ConfigManagerErrorCode | Win32 Configuration Manager error code. See [return values](#configmanager-error-codes)  |
|  boolean | ConfigManagerUserConfig |  Not implemented in this library as of writing. |
|  string |  CreationClassName |  Name of the first concrete class to appear in the inheritance chain used in the creation of an instance. When used with the other key properties of the class, the property allows all instances of this class and its subclasses to be uniquely identified. |
|  string |  Description |  Description of the object. |
|  string |  DeviceID |  Identifier of the Plug and Play device.  |
|  boolean | ErrorCleared | If TRUE, the error reported in LastErrorCode is now cleared. |
|  string |  ErrorDescription |  More information about the error recorded in LastErrorCode, and information about any corrective actions that may be taken.  |
|  string |  HardwareID[] | Not implemented in this library as of writing. |
|  datetime | InstallDate |  Not implemented in this library as of writing. |
|  uint32 |  LastErrorCode |  Last error code reported by the logical device. |
|  string |  Manufacturer | Name of the manufacturer of the Plug and Play device.  |
|  string |  Name | Label by which the object is known. When subclassed, the property can be overridden to be a key property.  |
|  string |  PNPClass |  Not implemented in this library as of writing. |
|  string |  PNPDeviceID | Windows Plug and Play device identifier of the logical device.  |
|  uint16 |  PowerManagementCapabilities[] | Not implemented in Win32_PnPEntity.  |
|  boolean | PowerManagementSupported |  Not implemented in Win32_PnPEntity |
|  boolean | Present | Not implemented in this library as of writing.  |
|  string |  Service | Name of the service that supports this Plug and Play device  |
|  string |  Status |  Current status of the object. See [possible return values](#status-return-values)|
|  uint16 |  StatusInfo | State of the logical device. If this property does not apply to the logical device, the value 5 (Not Applicable) should be used. See [return values](#statusinfo-return-values) |
|  string |  SystemCreationClassName | Value of the scoping computer's CreationClassName property.   |
|  string |  SystemName | Name of the scoping system  |
|  string |  FriendlyName | FriendlyName of the device.  |

##Methods

| Returns | Name | Description | Parameters |
|---------|------|-------------| -----------|
| void | Refresh | Rechecks what plug and play devices are connected | N/A |
| List<Device> | GetDevices | Returns a list of all connected devices | N/A |
| bool | IsDeviceNameConnected | Returns true if a device matching that name is connected | name: string to match against FriendlyName. exact: bool indicating if it must be equal or just contained in the FriendlyName |
| bool | IsDeviceIDConnected | Returns true if a device matching that DeviceID is connected | DeviceID: string to match against the DeviceID |
| Device | GetDeviceByName | Returns an instance of the device if name matches a friendly name. Default is `null` | name: string to match against FriendlyName. exact: bool indicating if it must be equal or just contained in the FriendlyName |
| Device | GetDeviceByID | Returns an instance of the device if there is a DeviceID match. Default is `null` | | DeviceID: string to match against the DeviceID |


##Notes
* `FriendlyName` isn't always populated. And `Name` isn't always useful. So I am not sure which to rely on
* There are a few different types of device IDs. Still not sure which is best to use


##Resources
###Availability Return Values
enum in WinDevices.AccessibilityStatus

| Value | Name | Meaning |
|-------|------|---------|
| 0x1 | OTHER | Other |
| 0x2 | UNKNOWN | Unknown |
| 0x3 | RUNNING | Running or Full Power |
| 0x4 | WARNING | Warning |
| 0x5 | IN_TEST | In Test |
| 0x6 | NA | Not Applicable |
| 0x7 | OFF | Power Off |
| 0x8 | OFFLINE | Off Line |
| 0x9 | OFFDUTY | Off Duty |
| 0xA | DEGRADED | Degraded |
| 0xB | NOT_INSTALLED | Not installed |
| 0xC | INSTALLERROR | Install Error |
| 0xD | POWERSAVE_UNKOWN | Power Save - Unknown. The device is known to be in a power save mode, but its exact status is unknown. |
| 0xE | POWERSAVELOW | Power Save - Low Power Mode. The device is in a power save state but still functioning, and may exhibit degraded performance. |
| 0xF | POWERSAVE_STANDBY | Power Save - Standby. The device is not functioning, but could be brought to full power quickly. |
| 0x10 | POWERCYCLE | Power Cycle |
| 0x11 | POWERSAVE_WARN | Power Save - Warning. The device is in a warning state, though also in a power save mode. |

###ConfigManager Error Codes
enum in WinDevices.AccessibilityStatus

| Value | Name | Meaning |
|-------|------|---------|
| 0 (0x0) | OK | Device is working properly. |
| 1 (0x1) | BAD_CONFIG | Device is not configured correctly. |
| 2 (0x2) | BAD_DRIVER | Windows cannot load the driver for this device. |
| 3 (0x3) | CORRUPTED_DRIVER | Driver for this device might be corrupted, or the system may be low on memory or other resources. |
| 4 (0x4) | DEVICE_NOT_WORKING | Device is not working properly. One of its drivers or the registry might be corrupted. |
| 5 (0x5) | WINDOWS_CANT_MANAGE | Driver for the device requires a resource that Windows cannot manage. |
| 6 (0x6) | BOOT_CONFIG_CONFLICT | Boot configuration for the device conflicts with other devices. |
| 7 (0x7) | CANNOT_FILTER | Cannot filter. |
| 8 (0x8) | MISSING_DRIVER | Driver loader for the device is missing. |
| 9 (0x9) | FIRMWARE_NOT_REPORT_RESX | Device is not working properly.The controlling firmware is incorrectly reporting the resources for the device. |
| 10 (0xA) | DEVICE_CANT_START | Device cannot start. |
| 11 (0xB) | DEVICE_FAILED | Device failed. |
| 12 (0xC) | NEED_MORE_RESOURCES | Device cannot find enough free resources to use. |
| 13 (0xD) | WINDOWS_CANT_VERIFY_RESOURCES | Windows cannot verify the device's resources. |
| 14 (0xE) | PC_NEEDS_RESTART | Device cannot work properly until the computer is restarted. |
| 15 (0xF) | DEVICE_NOT_WORKING_BAD_ENUM | Device is not working properly due to a possible re-enumeration problem. |
| 16 (0x10) | CANT_IDENTIFY_REQUESTED_RESX | Windows cannot identify all of the resources that the device uses. |
| 17 (0x11) | WANTS_UNKOWN_RESOURCE | Device is requesting an unknown resource type. |
| 18 (0x12) | RESINTALL_DRIVERS | Device drivers must be reinstalled. |
| 19 (0x13) | VD_LOADER_FAILURE | Failure using the VxD loader. |
| 20 (0x14) | POSSIBLE_REGISTRY_CORRUPTION | Registry might be corrupted. |
| 21 (0x15) | SYSTEM_FAILURE_REMOVING_DEV | System failure. If changing the device driver is ineffective, see the hardware documentation. Windows is removing the device. |
| 22 (0x16) | DEVICE_DISABLED | Device is disabled. |
| 23 (0x17) | SYSTEM_FAILURE | System failure. If changing the device driver is ineffective, see the hardware documentation. |
| 24 (0x18) | DEVICE_NOT_PRESENT | Device is not present, not working properly, or does not have all of its drivers installed. |
| 25 (0x19) | SETTING_UP_DEVICE | Windows is still setting up the device. |
| 26 (0x1A) | STILL_SETTING_UP_DEVICE | Windows is still setting up the device. |
| 27 (0x1B) | DEVICE_LACKS_LOG_CONFIG | Device does not have valid log configuration. |
| 28 (0x1C) | MISSING_DEVICE_DRIVER | Device drivers are not installed. |
| 29 (0x1D) | FIRMWARE_DIDNT_PROVIDE_RESX | Device is disabled. The device firmware did not provide the required resources. |
| 30 (0x1E) | IRQ_ALREADY_IN_USE | Device is using an IRQ resource that another device is using. |
| 31 (0x1F) | DEVICE_NOTWORKING_PROPERLY | Device is not working properly. Windows cannot load the required device drivers. |

###Status Return Values
string with a maximum length of 10. See more on the msdn [page](https://msdn.microsoft.com/en-us/library/aa394353%28v=vs.85%29.aspx)

The possible values are:
```
	"OK"
	"Error"
	"Degraded"
	"Unknown"
	"Pred Fail"
	"Starting"
	"Stopping"
	"Service"
	"Stressed"
	"NonRecover"
	"No Contact"
	"Lost Comm"
```

###StatusInfo Return Values
enum in WinDevices.StatusInfoValue

| Value | Name | Meaning |
|-------|------|---------|
| 0x1 | OTHER | Other |
| 0x2 | UNKNOWN | Unknown |
| 0x3 | ENABLED | Enabled |
| 0x4 | DISABLED | Disabled |
| 0x5 | NA | N/A |