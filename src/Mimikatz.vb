Imports System.ComponentModel

Public Class Mimikatz

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim process As New Process()
        Process.Start("https://github.com/gentilkiwi/mimikatz")

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        If My.Settings.ENG = True Then
            RichTextBox1.Visible = False
            RichTextBox2.Visible = True
        Else
            RichTextBox2.Visible = False
            RichTextBox1.Visible = True
        End If

        If My.Settings.ENG = False Then Button1.Text = "Mimikatz Website"

    End Sub

    Private Sub Mimikatz_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        GovTools.BringToFront()
        GovTools.Select()
    End Sub
End Class