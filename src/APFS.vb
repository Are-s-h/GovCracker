
Imports System.ComponentModel
Imports System.IO

Public Class APFS

    Public Property OpenFileDialog As Object


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        '#### Image Auswahl und in TexBox3 einfügen 

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox3.Text = openFileDialog.FileName
        Else
            Exit Sub
        End If

        '### Bash Datei schreiben // das Clone Verzeichnis existiert bereits

        Dim Command As String = "#!/bin/bash" & vbNewLine & "sudo -s apt update && sudo apt install fuse libfuse3-dev bzip2 libbz2-dev cmake g++ git libattr1-dev zlib1g-dev && cd apfs2hashcat && cd build && ./apfs-dump-quick /mnt/" & """" & TextBox3.Text & """" & " log.txt"
        Dim Datei As String = Application.StartupPath + "/Packages/APFS/apfs.sh"

        System.IO.File.WriteAllText(Datei, Command)

        '### Bash starten mit Root Rechten

        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WorkingDirectory = Application.StartupPath & "/Packages/APFS/"
        process.StartInfo.Arguments = ("/k ubuntu2004.exe run sudo -s bash apfs.sh")
        process.Start()


    End Sub


    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

        '### Der Filestring wird um den Doppelpunkt und \ gegen / geändert

        Dim username As String = TextBox3.Text
        username = String.Format("{0}{1}", username.Substring(0, 2).ToLower, username.Substring(2))
        TextBox3.Text = username.Replace("\", "/").Replace(":", "")

    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        '#### Help Button

        Process.Start("https://github.com/Banaanhangwagen/apfs2hashcat")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Linux.Show()

    End Sub

    Private Sub APFS_Load(sender As Object, e As EventArgs) Handles Me.Load

        If My.Settings.ENG = False Then

            GroupBox1.Text = "Image-Datei erstellen"
            GroupBox2.Text = "Hash extrahieren"
            GroupBox3.Text = "Linux-Subsystem aktivieren"
            Button1.Text = "Ubuntu 20.04 installieren"
            Button3.Text = "Öffnen"
            Button7.Text = "Hilfe"
            Label1.Text = "Bitte wählen Sie die Image-Datei aus. Am Ende der Extraktion werden Ihnen die Hashes angezeigt." & vbNewLine &
                          "Es ist möglich, dass mehrere Hashes extrahiert werden, da das System mehrere UUIDs enthalten kann." & vbNewLine &
                          "Normalerweise ist der erste Hash der richtige (der lokale Open-Directory-Benutzer)." & vbNewLine &
                          "Kopieren Sie den Hash in eine Hash.txt. Hashcat-Typ-Modes sind 18300."
            Label2.Text = "Wenn Sie apfs2hashcat erstmalig verwenden, dann müssen Sie Linux für Windows (Ubuntu 20.04.) installieren." & vbNewLine &
                          "Für weitere Schritte drücken Sie bitte den Button:"
            Label4.Text = "Image-Datei öffnen:"
            Label7.Text = "Das Ziel von apfs2hashcat ist es den Hash aus einem verschlüsselten MacBook-Image zu extrahieren. Die Filevault" & vbNewLine &
                          "Verschlüsselung kann Hashcat im Hash-Typ 18300 entschlüsseln." & vbNewLine & vbNewLine &
                          "Booten Sie das MacBook mit bspw. mit Digital Collector, Paladin, Caine, Kali, etc." & vbNewLine &
                          "Erstellen Sie ein Raw-Image (.dmg oder .001) der gesamten Apple-Festplatte (ohne Datei-Splitting). Kopieren Sie" & vbNewLine &
                          "das Image bitte auf eine interne Festplatte, bspw. C:\ oder D:\ des GovCracker-PC (nicht externe Festplatte)." & vbNewLine & vbNewLine

        End If


    End Sub

    Private Sub APFS_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        GovTools.BringToFront()
        GovTools.Select()

    End Sub
End Class