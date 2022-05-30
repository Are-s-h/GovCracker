Imports System.ComponentModel

Public Class Linux
    Private Sub Linux_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Settings.ENG = False Then

            Button1.Text = "Linux aktivieren"

            Label7.Text = "1) Updaten Sie Windows 10 auf den neuesten Stand." & vbNewLine &
                          "2) Gehen Sie in den Windows Store und installieren Sie ""Ubuntu 20.04 LTS"". Es ist kostenlos." & vbNewLine &
                          "3) Jetzt müssen Sie Linux noch aktivieren. Drücken Sie den Button." & vbNewLine &
                          "4) Im Windows-Menü unter ""Start"" finden Sie nun den Eintrag Ubuntu 20.04." & vbNewLine &
                          "5) Starten Sie Ubuntu 20.04 und vergeben Sie einen Username und Passwort."


        End If

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Dim process As New Process()
            process.StartInfo.FileName = "powershell.exe"
            process.StartInfo.Verb = "runas"
            'process.StartInfo.WorkingDirectory = "c:\windows\system32"
            'process.StartInfo.Arguments = ("-noexit dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart")
            process.StartInfo.Arguments = ("-noexit Enable-WindowsOptionalFeature -O -F  Microsoft-Windows-Subsystem-Linux")
            process.Start()

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub Linux_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        If Not APFS.IsDisposed AndAlso APFS.Visible Then
            '### Wenn APFS geöffnet ist, dann APFS in Vordergrund
            APFS.BringToFront()
            APFS.Select()
        Else
            GovTools.BringToFront()
            GovTools.Select()
        End If



    End Sub
End Class