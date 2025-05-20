Imports System.Runtime.InteropServices

Namespace Utilities.Network
    Public Class NetworkDrive
        Public Enum ResourceScope
            RESOURCE_CONNECTED = 1
            RESOURCE_GLOBALNET
            RESOURCE_REMEMBERED
            RESOURCE_RECENT
            RESOURCE_CONTEXT
        End Enum

        Public Enum ResourceType
            RESOURCETYPE_ANY
            RESOURCETYPE_DISK
            RESOURCETYPE_PRINT
            RESOURCETYPE_RESERVED
        End Enum

        Public Enum ResourceUsage
            RESOURCEUSAGE_CONNECTABLE = &H1
            RESOURCEUSAGE_CONTAINER = &H2
            RESOURCEUSAGE_NOLOCALDEVICE = &H4
            RESOURCEUSAGE_SIBLING = &H8
            RESOURCEUSAGE_ATTACHED = &H10
            RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE Or RESOURCEUSAGE_CONTAINER Or RESOURCEUSAGE_ATTACHED)
        End Enum

        Public Enum ResourceDisplayType
            RESOURCEDISPLAYTYPE_GENERIC
            RESOURCEDISPLAYTYPE_DOMAIN
            RESOURCEDISPLAYTYPE_SERVER
            RESOURCEDISPLAYTYPE_SHARE
            RESOURCEDISPLAYTYPE_FILE
            RESOURCEDISPLAYTYPE_GROUP
            RESOURCEDISPLAYTYPE_NETWORK
            RESOURCEDISPLAYTYPE_ROOT
            RESOURCEDISPLAYTYPE_SHAREADMIN
            RESOURCEDISPLAYTYPE_DIRECTORY
            RESOURCEDISPLAYTYPE_TREE
            RESOURCEDISPLAYTYPE_NDSCONTAINER
        End Enum

        <StructLayout(LayoutKind.Sequential)> _
        Private Class NETRESOURCE
            Public dwScope As ResourceScope = 0
            Public dwType As ResourceType = 0
            Public dwDisplayType As ResourceDisplayType = 0
            Public dwUsage As ResourceUsage = 0
            Public lpLocalName As String = Nothing
            Public lpRemoteName As String = Nothing
            Public lpComment As String = Nothing
            Public lpProvider As String = Nothing
        End Class

        <DllImport("mpr.dll")> _
        Private Shared Function WNetAddConnection2(lpNetResource As NETRESOURCE, lpPassword As String, lpUsername As String, dwFlags As Integer) As Integer
        End Function

        Public Function MapNetworkDrive(unc As String, drive As String, user As String, password As String) As Integer
            Dim myNetResource As New NETRESOURCE()
            myNetResource.lpLocalName = drive
            myNetResource.lpRemoteName = unc
            myNetResource.lpProvider = Nothing
            Dim result As Integer = WNetAddConnection2(myNetResource, password, user, 0)
            Return result
        End Function
    End Class
End Namespace


