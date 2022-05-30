Imports System.ComponentModel

Public Class MaskHelp
    Private Sub MaskHelp_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        GovTools.BringToFront()
    End Sub
End Class