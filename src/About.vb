Imports System.ComponentModel

Public Class About
    Private Sub About_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        GovTools.BringToFront()
    End Sub
End Class