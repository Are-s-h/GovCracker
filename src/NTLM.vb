Imports System.ComponentModel

Public Class NTLM
    Public Function Right(ByVal sText As String,
  ByVal nLen As Integer) As String

        If nLen > sText.Length Then nLen = sText.Length
        Return (sText.Substring(sText.Length - nLen))

    End Function

    Public PP2 As String = ("""" & Application.StartupPath & "\Packages\Py2\App\Python\PP2.exe" & """" & " ")
    Public MsgTextAll As String
    Public MsgTextENG As String = "Done! The hash were saved in the folder ""#_Hashout"".Do you want to crack the hash with GovCracker now?" & vbNewLine & vbNewLine & "The following Hash-Types come into consideration with this file format: "
    Public MsgTextGER As String = "Der Passwort-Hash wurde extrahiert und im Ordner ""#_Hashout"" abgespeichert. Möchten Sie ihn jetzt mit GovCracker entschlüsseln?" & vbNewLine & vbNewLine & "Die folgenden Hash-Typen kommen für dieses Dateiformat in Betracht: "
    Public tbSystemX As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        '### Sprache umschalten
        If GovTools.myENG = "Yes" Then
            rtbSecretGER.Visible = False
            rtbSecretENG.Visible = True
        Else
            rtbSecretENG.Visible = False
            rtbSecretGER.Visible = True
        End If

    End Sub

    Private Sub Mimikatz_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        GovTools.BringToFront()
        GovTools.Select()
    End Sub

    Private Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnSystem.Click

        '### Folder auswählen
        Using f As New FolderBrowserDialog()
            If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim d As New System.IO.DirectoryInfo(f.SelectedPath)
                tbSYSTEM.Text = f.SelectedPath & "\"
                tbSystemX = tbSYSTEM.Text
            End If
        End Using

    End Sub

    Private Sub bntSecretSTART_Click(sender As Object, e As EventArgs) Handles bntSecretSTART.Click

        If tbSYSTEM.TextLength = 0 Then
            If GovTools.myENG = "Yes" Then MsgBox("Please fill out all fields")
            If GovTools.myENG = "No" Then MsgBox("Please fill out all fields")
            Exit Sub
        End If

        Call Extractor()

    End Sub

    Private Sub Extractor()

        Dim dateX As String = DateTime.Now.ToString("ddMMyy_HHmmss")
        Dim Datei1 As String
        Datei1 = """" & Application.StartupPath & "\#_Hashout\Hashout_WinLogin_1000_raw_" & dateX & ".txt" & """"

        '### Batch schreiben
        System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & """" & Application.StartupPath & "\Packages\SD\secretsdump.py" & """" & " -sam " & """" & tbSystemX & "SAM" & """" & " -system " & """" & tbSystemX & "SYSTEM" & """" & " -security " & """" & tbSystemX & "SECURITY" & """" & " local" & " > " & Datei1)

        '### 1 Sek. warten
        Threading.Thread.Sleep(1000)

        Try
            '### Batch ausführen
            Dim process2 As New Process()
            process2.StartInfo.FileName = "cmd.exe"
            process2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            'process2.StartInfo.Verb = "runas"
            process2.StartInfo.WorkingDirectory = Application.StartupPath
            process2.StartInfo.Arguments = ("/c " & """" & Application.StartupPath & "\Packages\Temp\Extraction.bat" & """")
            process2.Start()
            process2.WaitForExit()
        Catch ex As Exception
            MsgBox("Extraction Error!")
        End Try

        Try
            '### Hash aufbereiten
            Dim auswahlListe As New List(Of String)
            For Each zeile As String In IO.File.ReadAllLines(Application.StartupPath & "\#_Hashout\Hashout_WinLogin_1000_raw_" & dateX & ".txt")

                If zeile.Contains(":::") Then

                    Dim txt5 As String = zeile.Replace(":", "")
                    Dim txt6 As String = Right(txt5, 32)
                    Dim txt7 As String = txt6.Replace("31d6cfe0d16ae931b73c59d7e0c089c0", "")
                    auswahlListe.Add(txt7)

                End If

            Next

            '### Ergebnis in eine Liste schreiben
            Dim txt8 As String = String.Join(vbNewLine, auswahlListe.ToArray)

            '### Warten
            Threading.Thread.Sleep(500)

            '### Hashout korrigieren und wieder speichern
            System.IO.File.WriteAllText(Application.StartupPath & "\#_Hashout\Hashout_WinLogin_1000_edit_" & dateX & ".txt", txt8)

        Catch ex As Exception
        End Try

        Me.BringToFront()

        '######## Messagebox mit Weiterleitung zu Form4
        If GovTools.myENG = "Yes" Then MsgTextAll = MsgTextENG
        If GovTools.myENG = "No" Then MsgTextAll = MsgTextGER

        Dim result As DialogResult = MessageBox.Show(MsgTextAll & "1000", "GovCracker", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Call GovTools.GovCrackerX()
        ElseIf result = DialogResult.No Then
            Me.BringToFront()
            Exit Sub
        End If

    End Sub

    Private Sub btnLive_Click(sender As Object, e As EventArgs) Handles btnLive.Click

        Try
            '### SAM, etc. speichern
            Dim process2 As New Process()
            process2.StartInfo.FileName = "cmd.exe"
            process2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            process2.StartInfo.Verb = "runas"
            process2.StartInfo.WorkingDirectory = Application.StartupPath
            process2.StartInfo.Arguments = ("/c " & "reg.exe save hklm\sam " & """" & Application.StartupPath & "\Packages\Temp\SAM" & """" & " /y & reg.exe save hklm\system " & """" & Application.StartupPath & "\Packages\Temp\SYSTEM" & """" & " /y & reg.exe save hklm\security " & """" & Application.StartupPath & "\Packages\Temp\SECURITY" & """" & " /y")
            process2.Start()
            process2.WaitForExit()
        Catch ex As Exception
            MsgBox("Extraction Error!")
        End Try

        Me.BringToFront()

        '### Pfadvorgabe
        tbSystemX = Application.StartupPath & "\Packages\Temp\"
        Call Extractor()

        Me.BringToFront()

    End Sub
End Class