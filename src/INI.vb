Imports System.Runtime.InteropServices
Imports System.Text

Public Class INI
    <DllImport("kernel32", EntryPoint:="GetPrivateProfileString")>
    Shared Function Lesen(
    ByVal Sektion As String, ByVal Key As String, ByVal StandartVal As String,
    ByVal Result As StringBuilder, ByVal Size As Int32, ByVal Dateiname As String) As Int32

    End Function
End Class