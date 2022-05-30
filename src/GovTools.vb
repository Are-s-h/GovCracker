Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions



Public Class GovTools

    '### GovTools by Are§h ###
    '### more Infos: www.govcrack.com ###
    '### STRG+M+O / STRG+M+L ###


#Region "Function"
    Public Function MD5StringHash(ByVal strString As String) As String
        Dim MD5 As New MD5CryptoServiceProvider
        Dim Data As Byte()
        Dim Result As Byte()
        Dim Res As String = ""
        Dim Tmp As String = ""
        Data = Encoding.ASCII.GetBytes(strString)
        Result = MD5.ComputeHash(Data)
        For i As Integer = 0 To Result.Length - 1
            Tmp = Hex(Result(i))
            If Len(Tmp) = 1 Then Tmp = "0" & Tmp
            Res += Tmp
        Next
        Return Res
    End Function
#End Region

#Region "Vari + Load + Menu"
    '### Extractor
    Public TextMsg As String 'PDF
    Public MsgTextAll As String
    Public MsgTextENG As String = "Done! The hash were saved in the folder ""#_Hashout"".Do you want to crack the Hash with GovCracker now?" & vbNewLine & vbNewLine & "The following Hash-Types come into consideration with this file format: "
    Public MsgTextGER As String = "Der Passwort-Hash wurde extrahiert und im Ordner ""#_Hashout"" abgespeichert. Möchten Sie ihn jetzt mit GovCracker entschlüsseln?" & vbNewLine & vbNewLine & "Die folgenden Hash-Typen kommen für dieses Dateiformat in Betracht: "
    Public MsgLinux As String
    Public MsgWin As String
    Public PDFtyp As String
    Public JtR As String = Application.StartupPath & "\Packages\JtR\run\"
    Public PP2 As String = ("""" & Application.StartupPath & "\Packages\Py2\App\Python\PP2.exe" & """")
    Public PP3 As String = ("""" & Application.StartupPath & "\Packages\Py3\App\Python\PP3.exe" & """")
    Public Perl As String = ("""" & Application.StartupPath & "\Packages\Perl\perl\bin\perl_hashbull.exe" & """")
    Public process As New Process()

    '### Maskprozessor
    Dim WithEvents BGW As New System.ComponentModel.BackgroundWorker
    Dim WithEvents BGWMask As New System.ComponentModel.BackgroundWorker
    Public CMD1 As String = "/c "
    Public Multiple As String
    Public Occur As String
    Public NumLeng1 As String
    Public NumLeng2 As String
    Public CommandW As String
    Public Charset1 As String
    Public Charset2 As String
    Public Charset3 As String
    Public Charset4 As String
    Public Ending As String = ".txt"
    Public Outputfile As String
    Public DatePub As String
    Public Combi As String
    Public StartP As String
    Public StopP As String
    Public CommandStart As String

    '### Prince
    Private WithEvents _Process2 As Process
    Dim WithEvents BGWPrince As New System.ComponentModel.BackgroundWorker
    Public PermMin As String
    Public PermMax As String
    Public PassLenMin As String
    Public PassLenMax As String
    Public UpperCase As String '= " --case-permute"

    '### Wordlister
    Dim WithEvents BGWWord As New System.ComponentModel.BackgroundWorker
    Public PermX As String
    Public MinLX As String
    Public MaxLX As String
    Public LeetX As String
    Public CapX As String
    Public UpX As String
    Public RuleX As String
    Public WordlisterExit As Boolean
    Public Wordlistpfad As String
    Public WordlisterOut As String

    '### CeWL
    Public CMax As String
    Public Cmin As String
    Public CewlCommand As String

    '### Dup
    Dim WithEvents BGWDup As New System.ComponentModel.BackgroundWorker

    '### Bulk
    Dim WithEvents BGWBulk As New System.ComponentModel.BackgroundWorker
    Dim WithEvents BGWBulk2 As New System.ComponentModel.BackgroundWorker
    Public MinW As String
    Public MaxW As String

    '### Comb und Len
    Dim WithEvents BGWComb As New System.ComponentModel.BackgroundWorker
    Dim WithEvents BGWComb2 As New System.ComponentModel.BackgroundWorker
    Dim WithEvents BGWLen As New System.ComponentModel.BackgroundWorker

    '### Wordlist-Tools
    Private WithEvents _Process3 As Process
    Dim WithEvents BGWWLT As New System.ComponentModel.BackgroundWorker
    Dim WithEvents BGWWLT2 As New System.ComponentModel.BackgroundWorker
    Public PathX As String
    Public Final As String

    Private Sub Tools_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call Start()
        Call StartPara()

        cbMask1.Checked = True
        cbMask3.Checked = True
        tbMaskPara.Select()

        '### Prince 
        '### Standardwerte vorgeben
        tbPrinceMinLen.Text = "1"
        tbPrinceMaxLen.Text = "10"
        tbPrinceMinPerm.Text = "1"
        tbPrinceMaxPerm.Text = "3"
        Call CalculateX()
        btnPrinceSTART.Select()

        '### Wordlister
        '# Standardwerte laden
        Perm.Text = "3"
        MinL.Text = "6"
        MaxL.Text = "17"

        '### Cewl
        '### Voreinstellungen laden
        tbCewlPage.Text = "https://www."
        btnCewlStart.Enabled = False
        tbCewlSpider.Text = "2"
        tbCewlWord.Text = "5"

        '### DupCleaner
        Dim DupDate As String = Format(Now, "yyyyMMdd_HHmmss")
        DupTargettxb.Text = Application.StartupPath & "\#_Wordlists\Wordlist_DUP_" & DupDate & ".txt"

    End Sub

    Private Sub PictureBoxTrash_Click(sender As Object, e As EventArgs) Handles pbTrash.Click
        Call GovClear()
    End Sub

    '### Menü
    Private Sub GovCrackerX()
        process.StartInfo.FileName = "govcracker.exe"
        process.StartInfo.WorkingDirectory = Application.StartupPath
        process.Start()
        Me.Close()
    End Sub

    Private Sub pbENG_Click(sender As Object, e As EventArgs) Handles pbENG.Click
        My.Settings.ENG = True
        My.Settings.GER = False
        My.Settings.Save()
        My.Settings.Reload()
        RefreshWin.Show()
    End Sub
    Private Sub pgGER_Click(sender As Object, e As EventArgs) Handles pbGER.Click
        My.Settings.ENG = False
        My.Settings.GER = True
        My.Settings.Save()
        My.Settings.Reload()
        RefreshWin.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pbGovC.Click
        Dim process2 As New Process()
        process2.StartInfo.FileName = "govcracker.exe"
        process2.StartInfo.WorkingDirectory = Application.StartupPath
        process2.Start()
        Me.Close()
    End Sub

    Private Sub pbGovCracker_Click(sender As Object, e As EventArgs) Handles pbAbout.Click
        About.Show()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        '#### Help als pdf anzeigen
        If My.Settings.ENG = True Then
            Process.Start(System.IO.Path.Combine(Application.StartupPath, "Docs\GovCracker_User_Manual_ENG.pdf"))
        Else
            Process.Start(System.IO.Path.Combine(Application.StartupPath, "Docs\GovCracker_User_Manual_DE.pdf"))
        End If
    End Sub

#End Region

#Region "Extractor"
    '#######################################################################################################################################################################
    '# EXTRACTOR
    '#######################################################################################################################################################################

    Private Sub LinkEx1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkEx1.LinkClicked
        Process.Start("https://hashes.com/en/johntheripper")
    End Sub
    Private Sub Extraction()

        Try
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

    End Sub

    Private Sub cbEx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEx.SelectedIndexChanged

        Try
            'Linux Login-Extraction ################################################################################################################ 
            If cbEx.Text = "Linux Login Password" Then

                If My.Settings.ENG = True Then MsgLinux = "You have to copy the hash from the ""Shadow"" file in the linux directory ""etc/shadow"" into a hash.txt. The hash starts with $6$. You have to delete everything after the last colon (e.g.:18585:0:99999:7:::). Then you can attack it in hash-mode 1800"
                If My.Settings.ENG = False Then MsgLinux = "Bitte kopieren Sie den User-Hash aus der ""Shadow-Datei"" im Linux Verzeichnis ""etc/shadow"" in eine Hash.txt. Der Hash beginnt mit ""$6$"". Alles ab dem letzten Doppelpunkt müssen Sie im Hash entfernen (bspw. :18585:0:99999:7:::). Der Hash-Typ ist 1800"

                Dim result As DialogResult = MsgBox(MsgLinux, MessageBoxButtons.OK)

                If result = DialogResult.OK Then
                    'nothing
                End If
            End If

            'Windows Login-Extraction ################################################################################################################ 

            If cbEx.Text = "Windows Login Password" Then
                Mimikatz.Show()
            End If

            'APFS - Extraction ################################################################################################################ 

            If cbEx.Text = "APFS (Apple MacBooks)" Then
                APFS.Show()
            End If

            'ZIP und RAR ################################################################################################################ 

            If cbEx.Text = "ZIP" OrElse cbEx.Text = "RAR" Then
                '### Cygwin 
                If My.Settings.ENG = True Then
                    MsgBox("Important: Extraction is only possible if the ""Cygwin"" (https://cygwin.com/install.html) software is installed!")
                Else
                    MsgBox("Wichtig: Eine Extraktion ist nur möglich, wenn die Software ""Cygwin"" (https://cygwin.com/install.html) installiert ist!")
                End If
            End If

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub btnExFile_Click(sender As Object, e As EventArgs) Handles btnExFile.Click

        '######## Datei auswählen 
        If My.Settings.ENG = True Then TextMsg = "Do you want to extract only one single PDF-file? Click ""Yes""." & vbNewLine & vbNewLine & "If you want to extract several PDF-files in one folder? Click ""No"""
        If My.Settings.ENG = False Then TextMsg = "Wenn Sie nur eine einzelnen PDF-Hash extrahieren möchten, dann klicken Sie ""Ja""." & vbNewLine & vbNewLine & "Wenn Sie mehrere PDF-Hashes in einem Ordner extrahieren möchten, dann klicken Sie ""Nein""."

        Dim openFileDialog1 As New OpenFileDialog()

        If cbEx.Text = "PDF" Then
            Dim result As DialogResult = MessageBox.Show(TextMsg, "PDF-Extraction", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            ElseIf result = DialogResult.No Then

                Using f As New FolderBrowserDialog()
                    If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        Dim d As New System.IO.DirectoryInfo(f.SelectedPath)
                        tbEx.Text = f.SelectedPath & "\*.pdf"
                    End If
                End Using

            ElseIf result = DialogResult.Yes Then
                openFileDialog1.InitialDirectory = "c:\"
                openFileDialog1.Filter = "All Files (*.*)| *.*"
                openFileDialog1.InitialDirectory = Application.StartupPath

                If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    tbEx.Text = openFileDialog1.FileName
                End If

            End If
            Exit Sub
        End If

        '#############################################################################################

        If cbEx.Text = "iTunes Backups (iPhones)" Then
            If My.Settings.ENG = True Then
                MsgBox("Please select the file: ""manifest.plist"" in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""manifest.plist"" im folgendem Fenster aus.")
            End If
        End If

        '###############################################################################################

        If cbEx.Text = "eCryptfs" Then
            If My.Settings.ENG = True Then
                MsgBox("Please select the file: ""wrapped-passphrase"" from the relevant linux system in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""wrapped-passphrase"" im folgendem Fenster aus.")
            End If
        End If

        openFileDialog1.Filter = "All Files (*.*)| *.*"
        openFileDialog1.InitialDirectory = Application.StartupPath

        If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbEx.Text = openFileDialog1.FileName
        End If

    End Sub

    Private Sub btnEx_Click(sender As Object, e As EventArgs) Handles btnEx.Click

        '### Prüfen ob alle Felder ausgefüllt wurden
        Try
            If tbEx.TextLength = 0 Then
                If My.Settings.ENG = True Then MsgBox("Both fields must be completed.")
                If My.Settings.ENG = False Then MsgBox("Bitte beide Felder ausfüllen.")
                Exit Sub
            End If

            '###ecryptfs-Extraction ### 

            If cbEx.Text = "eCryptfs" Then
                Dim dateECR As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiECR1 As String
                Dim DateiECR2 As String

                DateiECR1 = """" & Application.StartupPath & "\#_Hashout\Hashout_eCryptfs_" & dateECR & ".txt" & """"
                DateiECR2 = ("""" & tbEx.Text & """" & " > " & DateiECR1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP3 & " " & """" & Application.StartupPath & "\Packages\Hashbull_lib\eCryptfs\ecryptfs2john_gov.py" & """" & " " & DateiECR2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(1000)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "12200", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###7zip-Extraction ###

            If cbEx.Text = "7zip" Then
                Dim datezip As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateizip1 As String
                Dim Dateizip2 As String

                Dateizip1 = """" & Application.StartupPath & "\#_Hashout\Hashout_7zip_" & datezip & ".txt" & """"
                Dateizip2 = """" & tbEx.Text & """" & " > " & Dateizip1

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & """" & Application.StartupPath & "\Packages\Hashbull_lib\7z2hashcat\7z2hashcat6414.exe" & """" & " " & Dateizip2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(1000)

                Call Extraction()

                '######## Messagebox mit Weiterleitung zu Form4
                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "11600", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###iTunes-Extraction ### 

            If cbEx.Text = "iTune Backup (iPhone)" Then
                Dim Dateiitunes As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateiitunes1 As String
                Dim Dateiitunes2 As String

                Dateiitunes1 = """" & Application.StartupPath & "\#_Hashout\Hashout_iTunes_" & Dateiitunes & ".txt" & """"
                Dateiitunes2 = """" & tbEx.Text & """" & " > " & Dateiitunes1

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & Perl & " " & """" & JtR & "itunes_backup2john.pl" & """" & " " & Dateiitunes2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(1000)

                Call Extraction()

                '######## Messagebox mit Weiterleitung zu Form4

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "14700 / 14800", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###ZIP-Extraction ### 

            If cbEx.Text = "ZIP" Then
                Dim datezip As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateizip1 As String
                Dim Dateizip2 As String
                Dateizip1 = """" & Application.StartupPath & "\#_Hashout\Hashout_ZIP_" & datezip & ".txt" & """"
                Dateizip2 = ("""" & tbEx.Text & """" & " > " & Dateizip1)

                Dim process2 As New Process()
                process2.StartInfo.FileName = "cmd.exe"
                'process2.StartInfo.Verb = "runas"
                process2.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\JtR\run"
                process2.StartInfo.Arguments = ("/c " & "zip2john.exe " & Dateizip2)
                process2.Start()
                process2.WaitForExit()

                '###Datei einlesen und String nach $ wiedergeben
                Dim fileReader As System.IO.StreamReader
                fileReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\#_Hashout\Hashout_ZIP_" & datezip & ".txt")

                Dim stringReader As String
                stringReader = fileReader.ReadLine()
                Dim Str = stringReader
                Dim strStart As Integer = Str.IndexOf("$") 'Zahl des ersten Treffers
                Dim strEnd As Integer = Str.IndexOf(":", strStart) 'Beginn suche ab ersten Treffer oben
                Dim txt2 = (Str.Substring(strStart, strEnd - strStart)) 'urspr. String, von Zahl Anfang bis Zahl Ende
                fileReader.Close()

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                '### Hashout korrigieren und wieder speichern
                System.IO.File.WriteAllText(Application.StartupPath & "\#_Hashout\Hashout_ZIP_" & datezip & ".txt", txt2)

                '######## Messagebox mit Weiterleitung zu Form4

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "13600 (WinZip), 17200 - 17230 (PKZIP / default is 17210)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            '###RAR-Extraction ### 

            If cbEx.Text = "RAR" Then
                Dim datezip As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateizip1 As String
                Dim Dateizip2 As String
                Dateizip1 = """" & Application.StartupPath & "\#_Hashout\Hashout_RAR_" & datezip & ".txt" & """"
                Dateizip2 = ("""" & tbEx.Text & """" & " > " & Dateizip1)

                Dim process2 As New Process()
                process2.StartInfo.FileName = "cmd.exe"
                'process2.StartInfo.Verb = "runas"
                process2.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\JtR\run"
                process2.StartInfo.Arguments = ("/c " & "rar2john.exe " & Dateizip2)
                process2.Start()
                process2.WaitForExit()

                '### Hash extrahieren zwischen den Doppelpunkten #### https://www.vb-paradise.de/index.php/Thread/102911-vb-net-String-an-bestimmter-Stelle-abschneiden/

                '###Datei einlesen und String nach $ wiedergeben
                Dim fileReader As System.IO.StreamReader
                fileReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\#_Hashout\Hashout_RAR_" & datezip & ".txt")

                Dim stringReader As String
                stringReader = fileReader.ReadLine()
                Dim Str = stringReader
                Dim strStart As Integer = Str.IndexOf("$") 'Zahl des ersten Treffers
                Dim strEnd As Integer = Str.IndexOf(":", strStart) 'Beginn suche ab ersten Treffer oben
                Dim txt2 = (Str.Substring(strStart, strEnd - strStart)) 'urspr. String, von Zahl Anfang bis Zahl Ende
                fileReader.Close()

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                '### Hashout korrigieren und wieder speichern
                System.IO.File.WriteAllText(Application.StartupPath & "\#_Hashout\Hashout_RAR_" & datezip & ".txt", txt2)

                '######## Messagebox mit Weiterleitung zu Form4
                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "12500, 13000 (default is 12500)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            '###PDF-Extraction ###

            If cbEx.Text = "PDF" Then
                Dim datepdf As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateipdf0 As String
                Dim Dateipdf1 As String
                Dim Dateipdf2 As String
                Dim Dateipdf4 As String

                Dateipdf0 = Application.StartupPath & "\#_Hashout\Hashout_PDF_" & datepdf & ".txt"
                Dateipdf1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_PDF_" & datepdf & ".txt" & """")
                Dateipdf2 = ("""" & tbEx.Text & """" & " > " & Dateipdf1)
                Dateipdf4 = Application.StartupPath & "\Packages\Hashbull_lib\PDF\pdf2hashcat.py"

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & Dateipdf4 & """" & " " & Dateipdf2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(1000)

                Call Extraction()

                '### Sprache Msgbox
                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "10400 - 10700 (Default Is 10500)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###Office-Extraction ### 

            If cbEx.Text = "Office (Word, Excel, etc.)" Then
                Dim dateoffice As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateioffice1 As String
                Dim Dateioffice2 As String

                Dateioffice1 = """" & Application.StartupPath & "\#_Hashout\Hashout_Office_" & dateoffice & ".txt" & """"
                Dateioffice2 = ("""" & tbEx.Text & """" & " > " & Dateioffice1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & Application.StartupPath & "\Packages\Hashbull_lib\Office\office2hashcat.py" & """" & " " & Dateioffice2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "9400 - 9820 (Default Is 9600)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###Bitlocker-Extraction ### 

            If cbEx.Text = "Bitlocker" Then
                Try
                    If My.Settings.ENG = True Then MsgBox("The extraction of a Bitlocker-Hash can take some time. A 15 GB USB-Stick can last up to an hour. The progress is displayed in the following CMD window. The window closes automatically at the end of the extraction.")
                    If My.Settings.ENG = False Then MsgBox("Die Extraktion des Bitlocker-Hashes kann einige Zeit in Anspruch nehmen. Die Extraktion eines 15GB USB-Stick dauert ca. eine Stunde. Der Fortschritt wird in dem folgenden CMD-Fenster angezeigt. Das Fenster schließt automatisch am Ende der Extraktion.")

                    '### Hash wird extrahiert
                    Dim datebit As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                    Dim BitlockerOutput As String = """" & Application.StartupPath & "\#_Hashout\BitlockerExtraction_" & datebit & ".txt" & """"
                    System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "@echo off" & vbNewLine & "set LOGFILE=" & BitlockerOutput & vbNewLine & "Call :LOG > %LOGFILE%" & vbNewLine & "Exit /b" & vbNewLine & vbNewLine & ":LOG" & vbNewLine & "call " & """" & JtR & "bitlocker2john.exe" & """" & " -i " & """" & tbEx.Text & """")

                    '### 1 Sek. warten
                    Threading.Thread.Sleep(500)

                    Dim process2 As New Process()
                    process2.StartInfo.FileName = "cmd.exe"
                    'process2.StartInfo.Verb = "runas"
                    process2.StartInfo.WorkingDirectory = Application.StartupPath
                    process2.StartInfo.Arguments = ("/c " & """" & Application.StartupPath & "\Packages\Temp\Extraction.bat" & """")
                    process2.Start()
                    process2.WaitForExit()

                    '### 1 Sek. warten
                    Threading.Thread.Sleep(500)

                    '###Datei einlesen und String nach $ wiedergeben
                    Dim fileReader As System.IO.StreamReader
                    fileReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\#_Hashout\BitlockerExtraction_" & datebit & ".txt")

                    '### Hash extrahieren zwischen den Doppelpunkten #### https://www.vb-paradise.de/index.php/Thread/102911-vb-net-String-an-bestimmter-Stelle-abschneiden/
                    Dim stringReader As String
                    stringReader = fileReader.ReadToEnd()
                    Dim txt = stringReader
                    Dim txt2 = txt.Substring(txt.IndexOf("$1$")) '$1$ suchen
                    Dim txt3 = txt2.Substring(1, 198) 'Ab Index 198 Zeichen des Strings wiedergeben
                    fileReader.Close()

                    '### 1 Sek. warten
                    Threading.Thread.Sleep(500)

                    '### Hashout korrigieren und wieder speichern
                    System.IO.File.WriteAllText(Application.StartupPath & "\#_Hashout\BitlockerExtraction_" & datebit & ".txt", "$bitlocker$" & txt3) 'Hash zusammensetzen

                    '### Übergabe an Hashcrack
                    If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                    If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                    Dim result As DialogResult = MessageBox.Show(MsgTextAll & "Bitlocker: 22100", "GovCracker", MessageBoxButtons.YesNo)
                    If result = DialogResult.Yes Then
                        Call GovCrackerX()
                    ElseIf result = DialogResult.No Then
                        Exit Sub
                    End If

                Catch ex As Exception
                    MsgBox("Extraction Error!")
                End Try
            End If

            '###VeraCrypt-Container-File-Extraction ### 

            If cbEx.Text = "VeraCrypt / TrueCrypt (File)" Then
                Dim datevera As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateivera1 = Application.StartupPath & "\#_Hashout\Hashout_VeraCrypt_" & datevera & ".txt"

                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", """" & Application.StartupPath & "\Packages\DD\dd2.exe" & """" & " if=" & """" & tbEx.Text & """" & " of=" & """" & Application.StartupPath & "\#_Hashout\Hashout_VeraCrypt_" & datevera & ".txt" & """" & " " & "bs=512 count=1")

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "VeraCrypt:  13711 - 13773 (default Is 13721) // TrueCrypt: 6211 - 6233", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            '###VeraCrypt-Partition-Extraction ###

            If cbEx.Text = "VeraCrypt / TrueCrypt (Partition)" Then
                Dim dateVCP As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiVCP1 = Application.StartupPath & "\" & "#_Hashout\Hashout_VeraCrypt_" & dateVCP & ".txt"

                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", """" & Application.StartupPath & "\Packages\DD\dd2.exe" & """" & " if=" & """" & tbEx.Text & """" & " of=" & """" & Application.StartupPath & "\#_Hashout\Hashout_VeraCrypt_" & dateVCP & ".txt" & """" & " " & "count=512 skip=31744 bs=1")

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "13711 - 13773 (default Is 13721)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            '###VeraCrypt-Hidden-Partition-Extraction ###

            If cbEx.Text = "VeraCrypt / TrueCrypt (Hidden Partition)" Then
                Dim dateVCP As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiVCP1 = Application.StartupPath & "\" & "#_Hashout\Hashout_VeraCrypt_" & dateVCP & ".txt"

                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", """" & Application.StartupPath & "\Packages\DD\dd2.exe" & """" & " if=" & """" & tbEx.Text & """" & " of=" & """" & Application.StartupPath & "\#_Hashout\Hashout_VeraCrypt_" & dateVCP & ".txt" & """" & " " & "bs=1 skip=65536 count=512")

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "13711 - 13773 (default Is 13721)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            '###Bitcoin-Extraction ###

            If cbEx.Text = "Bitcoin-Wallet" Then
                Dim dateBTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiBTC1 As String
                Dim DateiBTC2 As String

                DateiBTC1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Bitcoin_" & dateBTC & ".txt" & """")
                DateiBTC2 = ("""" & tbEx.Text & """" & " > " & DateiBTC1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & JtR & "bitcoin2john.py" & """" & " " & DateiBTC2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "11300", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            '###MyEtherWallet / Ethereum-Extraction ### 

            If cbEx.Text = "Ethereum (MyEtherWallet.com / Keystore-File)" Then
                Dim dateBTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiBTC1 As String
                Dim DateiBTC2 As String

                DateiBTC1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Ether_" & dateBTC & ".txt" & """")
                DateiBTC2 = ("""" & tbEx.Text & """" & " > " & DateiBTC1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & JtR & "ethereum2john.py" & """" & " " & DateiBTC2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                '###### Outputfile Filename entfernen

                Threading.Thread.Sleep(500)
                Dim input As String = System.IO.File.ReadAllText(Application.StartupPath & "\#_Hashout\Hashout_Ether_" & dateBTC & ".txt")
                Dim mark = "$" ' Es wird nach dem ersten "$" gesucht

                If input.Contains(mark) Then
                    Dim markPosition = input.IndexOf(mark)
                    Dim result2 = input.Substring(markPosition) ' mit bspw (markposition + 1) wird vor dem zweiten Buchstaben gelöscht
                    Dim schreiben As New IO.StreamWriter(Application.StartupPath & "\#_Hashout\Hashout_Ether_" & dateBTC & ".txt", False) ' True = Inhalt wird angefügt und nicht überschrieben,, bei False wird überschrieben
                    schreiben.WriteLine(result2)
                    schreiben.Close() ' Erst durch .Close() werden Zeilen abschließend gespeichert
                End If

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "15700", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###Litecoin-Extraction ### 

            If cbEx.Text = "Litecoin-Wallet" Then
                Dim dateLTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiLTC1 As String
                Dim DateiLTC2 As String

                DateiLTC1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Litecoin_" & dateLTC & ".txt" & """")
                DateiLTC2 = ("""" & tbEx.Text & """" & " > " & DateiLTC1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & JtR & "bitcoin2john.py" & """" & " " & DateiLTC2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "11300", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###Electrum-Extraction ### 

            If cbEx.Text = "Electrum-Wallet" Then
                Dim dateBTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiBTC1 As String
                Dim DateiBTC2 As String

                DateiBTC1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Electrum_" & dateBTC & ".txt" & """")
                DateiBTC2 = ("""" & tbEx.Text & """" & " > " & DateiBTC1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & JtR & "electrum2john.py" & """" & " " & DateiBTC2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                '###### Outputfile Filename entfernen

                Threading.Thread.Sleep(1000)
                Dim input As String = System.IO.File.ReadAllText(Application.StartupPath & "\#_Hashout\Hashout_Electrum_" & dateBTC & ".txt")
                Dim mark = "$" ' Es wird nach dem ersten "$" gesucht

                If input.Contains(mark) Then
                    Dim markPosition = input.IndexOf(mark)
                    Dim result2 = input.Substring(markPosition) ' mit bspw (markposition + 1) wird vor dem zweiten Buchstaben gelöscht
                    Dim schreiben As New IO.StreamWriter(Application.StartupPath & "\#_Hashout\Hashout_Electrum_" & dateBTC & ".txt", False) ' True = Inhalt wird angefügt und nicht überschrieben,, bei False wird überschrieben
                    schreiben.WriteLine(result2)
                    schreiben.Close() ' Erst durch .Close() werden Zeilen abschließend gespeichert
                End If

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "16600, 21700, 21800 (default is 21700)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If

            End If

            '###LibreOffice-Extraction ###

            If cbEx.Text = "LibreOffice" Then
                Dim dateilibreoffice As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim Dateilibreoffice1 As String
                Dim Dateilibreoffice2 As String

                Dateilibreoffice1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_LibreOffice_" & dateilibreoffice & ".txt" & """")
                Dateilibreoffice2 = ("""" & tbEx.Text & """" & " > " & Dateilibreoffice1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP3 & " " & """" & Application.StartupPath & "\Packages\Hashbull_lib\LibreOffice\libreoffice2john.py" & """" & " " & Dateilibreoffice2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "18400", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            '###LUKS (Linux Unified Key System) Extraction ###

            If cbEx.Text = "LUKS (Linux Unified Key System)" Then
                Dim dateLUKS As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiLUKS1 = Application.StartupPath & "\" & "#_Hashout\Hashout_LUKS_" & dateLUKS & ".txt"

                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", """" & Application.StartupPath & "\Packages\DD\dd2.exe" & """" & " if=" & """" & tbEx.Text & """" & " of=" & """" & Application.StartupPath & "\#_Hashout\Hashout_LUKS_" & dateLUKS & ".txt" & """" & " bs=512 count=4097")

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If My.Settings.ENG = True Then MsgTextAll = MsgTextENG
                If My.Settings.ENG = False Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "14600", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            'nothing
        End Try

    End Sub

#End Region

    '##########################################################################################
    '######## Maskprozessor ###################################################################
    '##########################################################################################

    Sub Aktualiseren()
        If cbMask3.Checked = True Then
            Dim Date1 As String = Format(Now, "yyyyMMdd_HHmmss")
            DatePub = Date1 & Ending & """"
        Else
            DatePub = ""
        End If

        CommandStart = CMD1 & "mp64.exe " & CommandW & Charset1 & Charset2 & Charset3 & Charset4 & NumLeng1 & NumLeng2 & Multiple & Occur & StartP & StopP & Outputfile & DatePub & Combi

    End Sub

    Private Sub btnMaskStart_Click(sender As Object, e As EventArgs) Handles btnMaskStart.Click

        '#########START Button 
        If tbMaskPara.TextLength = 0 Then
            If My.Settings.ENG = True Then MsgBox("No command-parameters were entered.")
            If My.Settings.ENG = False Then MsgBox("Es wurden keine Command-Parameters eingegeben.")
            Exit Sub
        End If

        If cbMask3.Checked Then
            If My.Settings.ENG = True Then MsgBox("You find the wordlist in the GovCracker-Folder ""#_Wordlists"".")
            If My.Settings.ENG = False Then MsgBox("Sie finden die Wordlist im Ordner ""#_Wordlists"".")
        End If

        Radar.Visible = True
        BGWMask.RunWorkerAsync()

    End Sub

    Private Sub BGWMask_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWMask.DoWork

        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\MP"
        process.StartInfo.Arguments = (CommandStart)
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWMask_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWMask.RunWorkerCompleted

        Radar.Visible = False

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        MaskHelp.Show()
    End Sub

    Private Sub tb13Mask_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                Handles tbMask13.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 50 To 57, 8
                ' Zahlen ab 2, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub tb14Mask_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                Handles tbMask14.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 50 To 57, 8
                ' Zahlen ab 2, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                Handles tbMask8.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
            Handles tbMask14.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
            Handles tbMask13.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles tbMask8.TextChanged

        '### Search-Lenght 1
        If tbMask8.TextLength > 0 Then
            NumLeng1 = "-i " & tbMask8.Text
            Call Aktualiseren()
        Else
            NumLeng1 = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles tbMask9.TextChanged

        '### Search-Lenght 2
        If tbMask9.TextLength > 0 Then
            NumLeng2 = ":" & tbMask9.Text & " "
            Call Aktualiseren()
        Else
            NumLeng2 = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
            Handles tbMask9.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles tbMask13.TextChanged

        '#### Multiple Chars
        If tbMask13.TextLength > 0 Then
            Multiple = "-q " & tbMask13.Text & " "
            Call Aktualiseren()
        Else
            Multiple = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles tbMaskPara.TextChanged

        '#### Command Parameters
        If tbMaskPara.TextLength > 0 Then
            CommandW = tbMaskPara.Text & " "
            Call Aktualiseren()
        Else
            CommandW = ""
            Call Aktualiseren()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles tbMask3.TextChanged

        '#### Charset 1
        If tbMask3.TextLength > 0 Then
            Charset1 = "-1 " & tbMask3.Text & " "
            Call Aktualiseren()
        Else
            Charset1 = ""
            Call Aktualiseren()
        End If


    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles tbMask4.TextChanged

        '#### Charset 2
        If tbMask4.TextLength > 0 Then
            Charset2 = "-2 " & tbMask4.Text & " "
            Call Aktualiseren()
        Else
            Charset2 = ""
            Call Aktualiseren()
        End If

    End Sub


    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles tbMask5.TextChanged

        '#### Charset 3
        If tbMask5.TextLength > 0 Then
            Charset3 = "-3 " & tbMask5.Text & " "
            Call Aktualiseren()
        Else
            Charset3 = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles tbMask6.TextChanged

        '#### Charset 4
        If tbMask6.TextLength > 0 Then
            Charset4 = "-4 " & tbMask6.Text & " "
            Call Aktualiseren()
        Else
            Charset4 = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles cbMask1.CheckedChanged

        If cbMask1.Checked = True Then
            Ending = ".txt"
            cbMask2.Checked = False
            Call Aktualiseren()
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles cbMask2.CheckedChanged

        If cbMask2.Checked = True Then
            Ending = ".rule"
            cbMask1.Checked = False
            Call Aktualiseren()
        End If

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles tbMask14.TextChanged
        '#### Occur Chars

        If tbMask14.TextLength > 0 Then
            Occur = "-r " & tbMask14.Text & " "
            Call Aktualiseren()
        Else
            Occur = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles cbMask3.CheckedChanged

        If cbMask3.Checked = True Then
            Outputfile = "-o """ & Application.StartupPath & "\#_Wordlists\" & "MP_Out_"
            cbMask4.Checked = False
            Call Aktualiseren()
        Else
            Outputfile = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles cbMask4.CheckedChanged

        If cbMask4.Checked = True Then
            Combi = "--combinations "
            CMD1 = "/k "
            cbMask3.Checked = False
            Call Aktualiseren()
        Else
            Combi = ""
            CMD1 = "/c "
            Call Aktualiseren()
        End If

    End Sub
    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles tbMask11.TextChanged

        '#### Start position
        If tbMask11.TextLength > 0 Then
            StartP = "-s " & tbMask11.Text & " "
            Call Aktualiseren()
        Else
            StartP = ""
            Call Aktualiseren()
        End If

    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles tbMask12.TextChanged

        '#### Stop position
        If tbMask12.TextLength > 0 Then
            StopP = "-l " & tbMask12.Text & " "
            Call Aktualiseren()
        Else
            StopP = ""
            Call Aktualiseren()
        End If

    End Sub



    '##########################################################################################
    '######## Prince ##########################################################################
    '##########################################################################################

    Private Sub btnPrinceFile_Click(sender As Object, e As EventArgs) Handles btnPrinceFile.Click
        '### Datei laden
        Dim openFileDialog1 As New OpenFileDialog()

        If My.Settings.ENG = True Then
            MsgBox("Select only files that are smaller than 20MB.")
        Else
            MsgBox("Nur Dateien auswählen, die kleiner als 20MB sind.")
        End If

        openFileDialog1.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            ' Get the file name.
            Dim path As String = openFileDialog1.FileName
            Try
                tbPrince.Clear()
                ' Read in text.
                Dim text As String = File.ReadAllText(path)

                tbPrince.Text = text

            Catch ex As Exception
                'nothing
            End Try
        End If
    End Sub

    Private Sub btnPrinceSTART_Click(sender As Object, e As EventArgs) Handles btnPrinceSTART.Click
        '### Alle Felder müssen ausgefüllt sein
        If tbPrinceMinLen.TextLength < 1 Or tbPrinceMaxLen.TextLength < 1 Or tbPrinceMinPerm.TextLength < 1 Or tbPrinceMaxPerm.TextLength < 1 Then

            If My.Settings.ENG = True Then
                MsgBox("Please fill in all fields.")
            Else
                MsgBox("Bitte alle Felder ausfüllen.")
            End If
            Exit Sub

        End If

        '### Check Permutation Parameter
        Dim maxL As Integer = Convert.ToInt32(tbPrinceMaxLen.Text)
        Dim maxP As Integer = Convert.ToInt32(tbPrinceMaxPerm.Text)

        If maxP > maxL Then
            If My.Settings.ENG = True Then
                MsgBox("max. permutation must not be greater than max. password length!")
            Else
                MsgBox("max. Permutation darf nicht größer sein als max. Passwortlänge!")
            End If
            Exit Sub
        End If

        '#### Output schreiben
        Dim save As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(Application.StartupPath & "\Packages\Temp\PRINCE_Input.txt"), IO.FileMode.Create))
        System.Threading.Thread.Sleep(200)

        save.WriteLine(tbPrince.Text)
        save.Close()

        System.Threading.Thread.Sleep(200)

        '### Hinweis
        If My.Settings.ENG = True Then
            MsgBox("You can find the PRINCE-Wordlist in the folder ""#_Wordlists""" & vbNewLine & vbNewLine & "PRINCE starts now! Depending on the size, this process can take a long time and use a lot of hard drive storage. The process is finished when the CMD window disappears.")
        Else
            MsgBox("Sie finden die PRINCE-Wordlist im Ordner ""#_Wordlists""" & vbNewLine & vbNewLine & "PRINCE startet jetzt! Je nach Umfang kann dieser Vorgang sehr lange dauern und viel Speicherplatz benötigen. Der Vorgang ist abgeschlossen, wenn das CMD-Fenster erlischt.")
        End If

        Radar.Visible = True
        BGWPrince.RunWorkerAsync()

    End Sub

    Private Sub BGWPrince_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWPrince.DoWork

        Dim PrinceDate As String = DateTime.Now.ToString("ddMMyy_HHmmss")
        Dim PrinceOut = Application.StartupPath & "\#_Wordlists\PRINCE_" & PrinceDate & ".txt"

        '### Prince starten
        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\PRINCE\"
        process.StartInfo.Arguments = ("/c " & "pp64.exe --pw-min=" & tbPrinceMinLen.Text & " --pw-max=" & tbPrinceMaxLen.Text & " --elem-cnt-min=" & tbPrinceMinPerm.Text & " --elem-cnt-max=" & tbPrinceMaxPerm.Text & UpperCase & " -o " & """" & PrinceOut & """" & " """ & Application.StartupPath & "\Packages\Temp\PRINCE_Input.txt" & """")
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWPrince_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWPrince.RunWorkerCompleted

        Radar.Visible = False
        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Erledigt!")
        End If
        Me.BringToFront()

    End Sub

    Private Sub CheckBoxPrince_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPrince.CheckedChanged
        If CheckBoxPrince.Checked = True Then UpperCase = " --case-permute"
        If CheckBoxPrince.Checked = False Then UpperCase = ""
    End Sub

    Private Sub tbPrinceCalc_Click(sender As Object, e As EventArgs) Handles btnPrinceCalc.Click
        Call CalculateX()
    End Sub

    Private Sub CalculateX()

        Try
            '#### Output schreiben
            Dim save As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(Application.StartupPath & "\Packages\Temp\PRINCE_Input.txt"), IO.FileMode.Create))
            System.Threading.Thread.Sleep(200)
            save.WriteLine(tbPrince.Text)
            save.Close()

            System.Threading.Thread.Sleep(200)

            '### Ergebnis löschen
            RichTextBox1.Clear()
            Me.RichTextBox1.SelectionAlignment = HorizontalAlignment.Center

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .Verb = "runas"
                .FileName = "cmd"
                .Arguments = ("/k " & "pp64.exe --keyspace --pw-min=" & tbPrinceMinLen.Text & " --pw-max=" & tbPrinceMaxLen.Text & " --elem-cnt-min=" & tbPrinceMinPerm.Text & " --elem-cnt-max=" & tbPrinceMaxPerm.Text & UpperCase & " """ & Application.StartupPath & "\Packages\Temp\PRINCE_Input.txt" & """")
                .WorkingDirectory = Application.StartupPath & "\Packages\PRINCE"
                .CreateNoWindow = True
                .UseShellExecute = False
                '.ErrorDialog = True
                .RedirectStandardOutput = True

            End With
            _Process2.Start()
            _Process2.BeginOutputReadLine()

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub _Process2_OutputDataReceived(sender As Object, e As DataReceivedEventArgs) Handles _Process2.OutputDataReceived
        BeginInvoke(Sub() OutputData(e.Data))
    End Sub

    Private Sub OutputData(txt As String)

        RichTextBox1.AppendText(txt) ' & vbNewLine)

        Try

            '### String in große Zahlen umwandeln
            Dim txt3 As Long = Convert.ToInt64(RichTextBox1.Text)
            Dim txt4 As String = txt3.ToString("N0")
            tbPRINCEPass.Text = txt4

            '### MB Wordlist
            Dim LenMin As Integer = Convert.ToInt32(tbPrinceMinLen.Text)
            Dim LenMax As Integer = Convert.ToInt32(tbPrinceMaxLen.Text)
            Dim Summe As Integer = (LenMax - LenMin) / 2 + LenMin
            Dim Berechnung As Integer = Convert.ToInt32(Math.Round(txt3 * Summe / 1024 / 1024 / 10) * 10)
            Dim Berechnung2 As Integer = Berechnung * 30 / 100 + Berechnung
            tbPRINCEMB.Text = Berechnung2.ToString("N0")

        Catch ex As Exception

            If My.Settings.ENG = True Then
                MsgBox("Sorry. Max. permutation is greater than max. password length or the password numbers is greater than 10 quintillion!")
            Else
                MsgBox("Sorry. Die max. Permutation ist größer als max. Passwortlänge oder die Anzahl der möglichen Passwörter ist Größer als 10 Trillionen!")
            End If

        End Try

    End Sub

    Private Sub btnPrinceDefault_Click_1(sender As Object, e As EventArgs) Handles btnPrinceDefault.Click
        Call Defaultx()
    End Sub



    '##########################################################################################
    '######## Wordlister ######################################################################
    '##########################################################################################

    Private Sub btnWordStart_Click(sender As Object, e As EventArgs) Handles btnWordStart.Click

        Try

            If My.Settings.ENG = True Then MsgBox("The wordlist will now be created and saved in the folder ""\#_Wordlists\"". This can take up to 10 minutes.")
            If My.Settings.ENG = False Then MsgBox("Die Wordlist wird jetzt erstellt und im Ordner ""\#_Wordlists\"" abgespeichert. Dies kann bis zu 10 Min. dauern.")

            Radar.Visible = True

            Dim save As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(Application.StartupPath & "\#_Wordlists\Wordlister_Input.txt"), IO.FileMode.Create))

            System.Threading.Thread.Sleep(200)

            '### Subjektive Wordlist
            If tbWord1.Text <> "" Then save.WriteLine(tbWord1.Text)
            If tbWord2.Text <> "" Then save.WriteLine(tbWord2.Text)
            If tbWord3.Text <> "" Then save.WriteLine(tbWord3.Text)
            If tbWord4.Text <> "" Then save.WriteLine(tbWord4.Text)
            If tbWord5.Text <> "" Then save.WriteLine(tbWord5.Text)
            If tbWord6.Text <> "" Then save.WriteLine(tbWord6.Text)
            If tbWord7.Text <> "" Then save.WriteLine(tbWord7.Text)
            If tbWord8.Text <> "" Then save.WriteLine(tbWord8.Text)
            If tbWord9.Text <> "" Then save.WriteLine(tbWord9.Text)
            If tbWord10.Text <> "" Then save.WriteLine(tbWord10.Text)
            If tbWord11.Text <> "" Then save.WriteLine(tbWord11.Text)
            If tbWord12.Text <> "" Then save.WriteLine(tbWord12.Text)
            If tbWord13.Text <> "" Then save.WriteLine(tbWord13.Text)
            If tbWord14.Text <> "" Then save.WriteLine(tbWord14.Text)
            If tbWord15.Text <> "" Then save.WriteLine(tbWord15.Text)
            If tbWord16.Text <> "" Then save.WriteLine(tbWord16.Text)
            If tbWord17.Text <> "" Then save.WriteLine(tbWord17.Text)
            If tbWord18.Text <> "" Then save.WriteLine(tbWord18.Text)
            If tbWord19.Text <> "" Then save.WriteLine(tbWord19.Text)
            If tbWord20.Text <> "" Then save.WriteLine(tbWord20.Text)
            If tbWord21.Text <> "" Then save.WriteLine(tbWord21.Text)
            If tbWord22.Text <> "" Then save.WriteLine(tbWord22.Text)
            If tbWord23.Text <> "" Then save.WriteLine(tbWord23.Text)
            If tbWord24.Text <> "" Then save.WriteLine(tbWord24.Text)
            If tbWord25.Text <> "" Then save.WriteLine(tbWord25.Text)
            If tbWord26.Text <> "" Then save.WriteLine(tbWord26.Text)
            If tbWord27.Text <> "" Then save.WriteLine(tbWord27.Text)
            If tbWord28.Text <> "" Then save.WriteLine(tbWord28.Text)
            If tbWord29.Text <> "" Then save.WriteLine(tbWord29.Text)
            If tbWord30.Text <> "" Then save.WriteLine(tbWord30.Text)
            If tbWord31.Text <> "" Then save.WriteLine(tbWord31.Text)
            If tbWord32.Text <> "" Then save.WriteLine(tbWord32.Text)
            If tbWord33.Text <> "" Then save.WriteLine(tbWord33.Text)
            If tbWord34.Text <> "" Then save.WriteLine(tbWord34.Text)
            If tbWord35.Text <> "" Then save.WriteLine(tbWord35.Text)
            If tbWord36.Text <> "" Then save.WriteLine(tbWord36.Text)
            If tbWord37.Text <> "" Then save.WriteLine(tbWord37.Text)
            If tbWord38.Text <> "" Then save.WriteLine(tbWord38.Text)
            If tbWord39.Text <> "" Then save.WriteLine(tbWord39.Text)
            If tbWord40.Text <> "" Then save.WriteLine(tbWord40.Text)
            If tbWord41.Text <> "" Then save.WriteLine(tbWord41.Text)
            If tbWord42.Text <> "" Then save.WriteLine(tbWord42.Text)
            If tbWord43.Text <> "" Then save.WriteLine(tbWord43.Text)
            If tbWord44.Text <> "" Then save.WriteLine(tbWord44.Text)
            If tbWord45.Text <> "" Then save.WriteLine(tbWord45.Text)
            If tbWordStandard.Text <> "" Then save.WriteLine(tbWordStandard.Text)
            save.Close()

            System.Threading.Thread.Sleep(500)

            '### Target-Words dokumentieren
            Dim DateBatch As String = Format(Now, "yyyy_MM_dd_HH_mm_ss")
            Dim SourceFile, DestinationFile As String
            SourceFile = Application.StartupPath & "\#_Wordlists\Wordlister_Input.txt"   ' Define source file name.
            DestinationFile = Application.StartupPath & "\Logs\" & "Wordlister_Log_" & DateBatch & "_Wordlister.txt"   ' Define target file name.
            FileCopy(SourceFile, DestinationFile)   ' Copy source to target.

            System.Threading.Thread.Sleep(500)

            '# in die Variablen schreiben
            PermX = " --perm " & Perm.Text
            MinLX = " --min " & MinL.Text
            MaxLX = " --max " & MaxL.Text

            If Leet.Checked = True Then
                LeetX = " --leet "
            Else
                LeetX = ""
            End If

            If Cap.Checked = True Then
                CapX = " --cap "
            Else
                CapX = ""
            End If

            If Up.Checked = True Then
                UpX = " --up "
            Else
                UpX = ""
            End If

            BGWWord.RunWorkerAsync()

        Catch ex As Exception
            MsgBox("Out of Memory. There has been an error. Please try it with a smaller mask.")
        End Try

    End Sub

    Private Sub BGWWord_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWWord.DoWork

        System.Threading.Thread.Sleep(500)

        '### Wordlister-Permutation wird gestartet
        Dim WLM As String = PP3 & " wordlister.py --input " & """" & Application.StartupPath & "\#_Wordlists\Wordlister_Input.txt" & """" & PermX & MinLX & MaxLX & LeetX & CapX & UpX
        Dim process2 As New Process()
        process2.StartInfo.FileName = "cmd.exe"
        process2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process2.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Wordlister\"
        process2.StartInfo.Arguments = ("/c call " & WLM)
        process2.Start()
        process2.WaitForExit()

    End Sub

    Private Sub BGWWord_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWWord.RunWorkerCompleted

        System.Threading.Thread.Sleep(3000) 'warten bis Permutation erledigt ist

        '### Output mit Datum versehen
        Dim DateAuto As String = Format(Now, "HHmmss")
        Dim OldWord As String
        Dim NewWord As String
        WordlisterOut = Application.StartupPath & "\#_Wordlists\Wordlister_Output_" & DateAuto & ".txt"

        OldWord = Application.StartupPath & "\#_Wordlists\Wordlister_Output.txt"
        NewWord = WordlisterOut

        System.Threading.Thread.Sleep(250)
        Rename(OldWord, NewWord)

        '### Zusammenführung von Output und Input in eine Datei
        Dim InputTxt As String = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\#_Wordlists\Wordlister_Input.txt")
        System.IO.File.AppendAllText(NewWord, InputTxt)

        Radar.Visible = False
        Me.BringToFront()

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Erledigt!")
        End If

    End Sub

    Private Sub btnWordOpen_Click(sender As Object, e As EventArgs) Handles btnWordOpen.Click

        '### Wordlister Datei laden
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            ' Get the file name.
            Dim path As String = openFileDialog1.FileName
            Try
                tbWordStandard.Clear()
                ' Read in text.
                Dim text As String = File.ReadAllText(path)

                tbWordStandard.Text = text

            Catch ex As Exception
                'nothing
            End Try
        End If

    End Sub


    '##########################################################################################
    '######## CeWL ############################################################################
    '##########################################################################################

    Private Sub C_Aktualisieren()

        '### CeWL ausführen
        Dim Date1 As String = Format(Now, "yyyyMMdd_HHmmss")
        CewlCommand = "ubuntu2004.exe" & " run cewl " & tbCewlPage.Text & " -w CeWL_Wordlist_" & Date1 & ".txt " & " -e  --email_file CeWL_EMails_" & Date1 & ".txt " & CMax & Cmin
        ' tbCewlCommand.Text = command

        Me.btnCewlStart.Enabled = True

    End Sub

    Private Sub btnCewlInstall_Click(sender As Object, e As EventArgs) Handles btnCewlInstall.Click

        '### Bash Datei schreiben // das Clone Verzeichnis existiert bereits
        Dim Command As String = "#!/bin/bash" & vbNewLine & "sudo -s apt-get update && sudo apt-get install cewl libcurl4-gnutls-dev libxml2 libxml2-dev libxslt1-dev ruby-dev"
        Dim Datei As String = Application.StartupPath + "/Packages/CEWL/cewl.sh"

        System.IO.File.WriteAllText(Datei, Command)

        '### Bash starten mit Root Rechten
        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WorkingDirectory = Application.StartupPath & "/Packages/CEWL/"
        process.StartInfo.Arguments = ("/k ubuntu2004.exe run sudo -s bash cewl.sh")
        process.Start()

    End Sub

    Private Sub btnCewlStart_Click(sender As Object, e As EventArgs) Handles btnCewlStart.Click

        '#########START Button führt das Commad Fenster aus
        If My.Settings.ENG = True Then
            MsgBox("The process can take up to an hour for larger websites", MessageBoxButtons.OK)
        Else
            MsgBox("Bei größeren Internetseiten kann dieser Vorgang bis zu einer Stunde dauern!", MessageBoxButtons.OK)
        End If

        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\#_Wordlists"
        process.StartInfo.Arguments = ("/c " & CewlCommand) 'tbCewlCommand.Text)
        process.Start()
        process.WaitForExit()

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub

    Private Sub tbCewlword_TextChanged(sender As Object, e As EventArgs) Handles tbCewlWord.TextChanged
        '### Min. Word-Length

        If tbCewlWord.TextLength > 0 Then
            Cmin = "-m " & tbCewlWord.Text & " "
        Else
            Cmin = ""
        End If

        Call C_Aktualisieren()

    End Sub

    Private Sub tbCewlSpider_TextChanged(sender As Object, e As EventArgs) Handles tbCewlSpider.TextChanged
        '### Depth Spider

        If tbCewlSpider.TextLength > 0 Then
            CMax = "-d " & tbCewlSpider.Text & " "
        Else
            CMax = ""
        End If

        Call C_Aktualisieren()

    End Sub

    Private Sub tbCewlSpider_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbCewlSpider.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select
    End Sub


    Private Sub tbCewlword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbCewlWord.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select
    End Sub

    Private Sub btnCewlLinux_Click(sender As Object, e As EventArgs) Handles btnCewlLinux.Click
        Linux.Show()
    End Sub

    Private Sub tbCewlPage_TextChanged(sender As Object, e As EventArgs) Handles tbCewlPage.TextChanged
        Call C_Aktualisieren()
    End Sub



    '##########################################################################################
    '######## Combinator#######################################################################
    '##########################################################################################

    Private Sub btnCombinator_Click(sender As Object, e As EventArgs) Handles btnCombinator.Click

        Me.Select()
        If My.Settings.ENG = True Then MsgBox("The output file was created in the folder ""#_Wordlist"". The process may take some time for large files.")
        If My.Settings.ENG = False Then MsgBox("Die Ausgabedatei wird nach Fertigstellung in den Ordner ""#_Wordlist"" gespeichert. Bei sehr großen Dateien kann der Vorgang einige Zeit in Anspruch nehmen.")

        Radar.Visible = True

        If File1Txb.TextLength > 1 And File2Txb.TextLength > 1 And File3Txb.TextLength < 1 Then
            BGWComb.RunWorkerAsync()
            Exit Sub
        End If

        If File1Txb.TextLength > 1 And File2Txb.TextLength > 1 And File3Txb.TextLength > 1 Then
            BGWComb2.RunWorkerAsync()
            Exit Sub
        End If

        If File1Txb.TextLength > 1 AndAlso File2Txb.TextLength < 1 AndAlso File3Txb.TextLength < 1 Then

            If My.Settings.ENG = True Then MsgBox("You have to fill in at least two fields", vbSystemModal)
            If My.Settings.ENG = False Then MsgBox("Sie müssen mind. zwei Felder ausfüllen.", vbSystemModal)

        End If

    End Sub
    Private Sub pbCombinator1_Click(sender As Object, e As EventArgs) Handles pbCombinator1.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            File1Txb.Text = openFileDialog.FileName
        End If

    End Sub

    Private Sub pbCombinator2_Click(sender As Object, e As EventArgs) Handles pbCombinator2.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            File2Txb.Text = openFileDialog.FileName
        End If

    End Sub

    Private Sub pbCombinator3_Click(sender As Object, e As EventArgs) Handles pbCombinator3.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            File3Txb.Text = openFileDialog.FileName
        End If

    End Sub

    Private Sub BGWComb_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWComb.DoWork

        Dim Date3 As String = Format(Now, "yyyyMMdd_HHmmss")
        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
        process.StartInfo.Arguments = ("/c combinator.exe """ & File1Txb.Text & """ """ & File2Txb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Combinator2_" & Date3 & ".txt""")
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWComb2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWComb2.DoWork

        Dim Date3 As String = Format(Now, "yyyyMMdd_HHmmss")
        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
        process.StartInfo.Arguments = ("/c combinator3.exe """ & File1Txb.Text & """ """ & File2Txb.Text & """ """ & File3Txb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Combinator3_" & Date3 & ".txt""")
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWComb_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWComb.RunWorkerCompleted

        Radar.Visible = False
        Me.Select()

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

    End Sub

    Private Sub BGWComb2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWComb2.RunWorkerCompleted

        Radar.Visible = False
        Me.Select()

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

    End Sub



    '##########################################################################################
    '######## Len #############################################################################
    '##########################################################################################

    Private Sub pbLen_Click(sender As Object, e As EventArgs) Handles pbLen.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            LenTxb.Text = openFileDialog.FileName
        End If
    End Sub

    Private Sub LenMinTxb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles LenMinTxb.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub LenMaxTxb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles LenMaxTxb.KeyPress

        '#### Nur Zahlen und Backspace 
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen, Backspace 
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select

    End Sub

    Private Sub btnlen_Click(sender As Object, e As EventArgs) Handles btnLen.Click

        If LenTxb.TextLength < 1 Or LenMinTxb.TextLength < 1 Or LenMaxTxb.TextLength < 1 Then
            If My.Settings.ENG = True Then MsgBox("You have to fill in the input fields.", vbSystemModal)
            If My.Settings.ENG = False Then MsgBox("You must fill in the fields.", vbSystemModal)
            Exit Sub
        End If

        If My.Settings.ENG = True Then MsgBox("The output file was created in the folder ""#_Wordlist"". The process may take some time for large files.")
        If My.Settings.ENG = False Then MsgBox("Die Ausgabedatei wird nach Fertigstellung in den Ordner ""#_Wordlist"" gespeichert. Bei sehr großen Dateien kann der Vorgang einige Zeit in Anspruch nehmen.")

        Radar.Visible = True
        BGWLen.RunWorkerAsync()

    End Sub

    Private Sub BGWLen_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWLen.DoWork

        Dim Date3 As String = Format(Now, "yyyyMMdd_HHmmss")
        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
        process.StartInfo.Arguments = ("/c len.exe " & LenMinTxb.Text & " " & LenMaxTxb.Text & " < """ & LenTxb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Len_Wordlist_" & Date3 & ".txt""")
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWLen_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWLen.RunWorkerCompleted

        Radar.Visible = False
        Me.Select()

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

    End Sub

    '##########################################################################################
    '######## DupCleaner ######################################################################
    '##########################################################################################
    Private Sub pbDup1_Click(sender As Object, e As EventArgs) Handles pbDup1.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Duptxb.Text = openFileDialog.FileName
        End If

    End Sub

    Private Sub btnDup_Click(sender As Object, e As EventArgs) Handles btnDup.Click
        Radar.Visible = True
        BGWDup.RunWorkerAsync()
    End Sub

    Private Sub BGWDup_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWDup.DoWork

        Dim process As New Process()
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Py3\App\Python"
        process.StartInfo.Arguments = ("/c " & "PP3.exe " & """" & Application.StartupPath & "\Packages\DupC\DupCleaner.py" & """" & " --inTxt " & """" & Duptxb.Text & """" & " --out " & """" & DupTargettxb.Text & """")
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWDup_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWDup.RunWorkerCompleted
        Radar.Visible = False
        MsgBox("Done!")
        Me.BringToFront()
    End Sub


    '##########################################################################################
    '######## HashGen #########################################################################
    '##########################################################################################
    Private Sub btnHashGen_Click(sender As Object, e As EventArgs) Handles btnHashGen.Click

        'https://geekshangout.com/a-simple-vb-net-application-to-hash-a-string-to-a-md5-hash/
        'Online https://www.webatic.com/md5-convertor

        HashOUT.Text = MD5StringHash(HashIn.Text)

        Dim DateBatch3 As String = Format(Now, "yyyyMMdd_HHmmss")
        Dim Pfadbatch3 As String = (Application.StartupPath & "\#_Hashout\MD5_Example_Hash_" & HashIn.Text & "_" & DateBatch3 & ".txt")
        Dim fs3 As New FileStream(Pfadbatch3, FileMode.Append, FileAccess.Write)
        Dim s3 As New StreamWriter(fs3)
        s3.WriteLine(HashOUT.Text)
        s3.Close()

        If My.Settings.ENG = True Then
            MsgBox("Done! The hash was saved to the folder ""#_Hashout"".")
        Else
            MsgBox("Fertig! Der Hash wurde in den Ordner ""#_Hashout"" gespeichert.")
        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://www.webatic.com/md5-convertor")
    End Sub


    '##########################################################################################
    '######## BULK ############################################################################
    '##########################################################################################
    Private Sub pbBulk1_Click(sender As Object, e As EventArgs) Handles pbBulk1.Click

        '#### Image Datei auswählen
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "All Files (*.*)| *.*"
        openFileDialog1.InitialDirectory = Application.StartupPath

        If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbBulkImage.Text = openFileDialog1.FileName
        End If

    End Sub


    Private Sub btnbulkstart_Click(sender As Object, e As EventArgs) Handles btnBulkStart.Click

        If cbBulk1.Checked = True Then
            Radar.Visible = True
            BGWBulk.RunWorkerAsync()
        End If

        If cbBulk2.Checked = True Then
            Radar.Visible = True
            BGWBulk.RunWorkerAsync()
        End If

    End Sub

    Private Sub BGWBulk_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWBulk.DoWork

        '#### Check1 = nur Wordlist
        Dim dateBulk As String = DateTime.Now.ToString("ddMMyy_HHmmss")
        Dim Bulkin As String = """" & tbBulkImage.Text & """"
        Dim Bulkout As String = """" & Application.StartupPath & "\#_Wordlists\Wordlist_BulkExtractor" & "_" & dateBulk & ".txt" & """"

        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\BuEx"
        process.StartInfo.Arguments = ("/k bulk_extractor64.exe " & MinW & MaxW & "-E wordlist -o " & Bulkout & " " & Bulkin)
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWBulk2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWBulk2.DoWork

        '#### Check2 = Alles extrahieren
        Dim dateBulk As String = DateTime.Now.ToString("ddMMyy_HHmmss")
        Dim Bulkin As String = """" & tbBulkImage.Text & """"
        Dim Bulkout As String = """" & Application.StartupPath & "\#_Wordlists\Wordlist_BulkExtractor" & "_" & dateBulk & ".txt" & """"

        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\BuEx"
        process.StartInfo.Arguments = ("/k bulk_extractor64.exe " & MinW & MaxW & "-e base16 -e facebook -e outlook -e sceadan -e wordlist -e xor -o " & Bulkout & " " & Bulkin)
        process.Start()
        process.WaitForExit()

    End Sub


    Private Sub BGWBulk_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWBulk.RunWorkerCompleted

        Radar.Visible = False

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub

    Private Sub BGWBulk2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWBulk2.RunWorkerCompleted

        Radar.Visible = False

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub

    Private Sub tbbulkmin_TextChanged(sender As Object, e As EventArgs) Handles tbBulkmin.TextChanged

        '#### Wortlänge (min)
        If tbBulkmin.Text.Length > 0 Then
            MinW = " -S word_min=" & tbBulkmin.Text
        Else
            MinW = ""
        End If

    End Sub

    Private Sub tbbulkmax_TextChanged(sender As Object, e As EventArgs) Handles tbBulkmax.TextChanged

        '#### Wortlänge (max)
        If tbBulkmax.Text.Length > 0 Then
            MaxW = " -S word_max=" & tbBulkmax.Text & " "
        Else
            MaxW = ""
        End If

    End Sub

    Private Sub cbbulk1_CheckedChanged(sender As Object, e As EventArgs) Handles cbBulk1.CheckedChanged

        '#### Nur eine Checkbox aktiv
        If cbBulk1.Checked Then
            cbBulk2.Checked = False
        End If

    End Sub

    Private Sub cbbulk2_CheckedChanged(sender As Object, e As EventArgs) Handles cbBulk2.CheckedChanged

        '#### Nur eine Checkbox aktiv
        If cbBulk2.Checked Then
            cbBulk1.Checked = False
        End If

    End Sub

    Private Sub tBbulkmin_KeyPress(
      ByVal sender As Object,
      ByVal e As System.Windows.Forms.KeyPressEventArgs) _
      Handles tbBulkmin.KeyPress

        '#### Nur Zahlen und Backspace bei Wörterlänge zulassen
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8 ', 32
                ' Zahlen, Backspace und Space zulassen
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select
    End Sub


    Private Sub tbBulkmax_KeyPress(
    ByVal sender As Object,
    ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles tbBulkmax.KeyPress

        '#### Nur Zahlen und Backspace bei Wörterlänge zulassen
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8 ', 32
                ' Zahlen, Backspace und Space zulassen
            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select
    End Sub


    '##########################################################################################
    '######## Wordlist-Tools ##################################################################
    '##########################################################################################
    Private Sub btnWordMask_Click(sender As Object, e As EventArgs) Handles btnWordMask.Click

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog.FilterIndex = 1
        openFileDialog.RestoreDirectory = True

        If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            PathX = openFileDialog.FileName
        End If

        If My.Settings.ENG = True Then
            MsgBox("The maskfile is stored in the ""\#_Wordlists"" directory.")
        Else
            MsgBox("Die Masken-Datei wird in das Verzeichnis ""\#_Wordlists"" gespeichert.")
        End If

        Radar.Visible = True
        BGWWLT.RunWorkerAsync()

    End Sub

    Private Sub BGWWLT_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWWLT.DoWork

        Dim DateNow As String = Format(Now, "HH_mm_ss")
        Dim command As String = ("/c " & """" & PP3 & " " & """" & Application.StartupPath & "\Packages\Analyser\Wordlist2Mask.py" & """" & " -in " & """" & PathX & """" & " -out " & """" & Application.StartupPath & "\#_Wordlists\Wordlist2Mask_" & DateNow & ".hcmask" & """")
        Dim process As New Process()

        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        process.StartInfo.WorkingDirectory = Application.StartupPath
        process.StartInfo.CreateNoWindow = True
        process.StartInfo.Arguments = (command)
        process.Start()
        process.WaitForExit()

    End Sub

    Private Sub BGWWLT_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWWLT.RunWorkerCompleted

        Radar.Visible = False

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub


    '### Analyser ###
    Private Sub btnWordAnalyser_Click(sender As Object, e As EventArgs) Handles btnWordAnalyser.Click

        ListBoxWTL.Items.Clear()
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ' Get the file name.
            PathX = openFileDialog1.FileName
        End If

        Radar.Visible = True

        Dim command As String = ("/k " & """" & PP3 & " " & """" & Application.StartupPath & "\Packages\Analyser\Analyser2.py" & """" & " -in " & """" & PathX & """")

        _Process3 = New Process With {.EnableRaisingEvents = True}
        With _Process3.StartInfo

            '.Verb = "runas"
            .FileName = "cmd.exe"
            .WindowStyle = ProcessWindowStyle.Minimized
            .Arguments = (command)
            .WorkingDirectory = Application.StartupPath & "\Packages\Analyser"
            .CreateNoWindow = True
            .UseShellExecute = False
            .ErrorDialog = True
            .RedirectStandardOutput = True

        End With
        _Process3.Start()
        _Process3.BeginOutputReadLine()

    End Sub

    Private Sub _Process3_OutputDataReceived(sender As Object, e As DataReceivedEventArgs) Handles _Process3.OutputDataReceived
        BeginInvoke(Sub() OutputDataWord(e.Data))
    End Sub

    Private Sub OutputDataWord(txt As String)

        ListBoxWTL.Items.Add(txt & vbNewLine)

        If (txt.Contains("= _")) Then
            Call SortListbox()
        End If

    End Sub


    Sub SortListbox()

        Dim DateNow As String = Format(Now, "HH_mm_ss")
        ' box prüfen: sortieren sinnvoll ?

        If ListBoxWTL Is Nothing OrElse ListBoxWTL.Items.Count < 2 Then Return
        ' 2 arrays anlegen
        Dim count As Integer = ListBoxWTL.Items.Count
        Dim items(count - 1) As Object       ' enthält die ganzen strings
        Dim numbers(count - 1) As Integer    ' enthält nur die extrahierten Zahlen
        ' items kopieren
        ListBoxWTL.Items.CopyTo(items, 0)
        ' Zahlenarray
        For i = 0 To count - 1
            'Dim m As Match = Regex.Match(CStr(items(i)), "(\d+)")  ' extrahiert die erste beliebige Zahl
            Dim m As Match = Regex.Match(CStr(items(i)), "^(\d+)")  ' extrahiert nur Zahlen die am Anfang stehen
            If m.Success Then numbers(i) = CInt(m.Groups(1).Value) Else numbers(i) = 0
        Next
        ' beide arrays sortieren: numbers führt
        Array.Sort(numbers, items)

        ' Liste neu befüllen
        ListBoxWTL.Items.Clear()
        ListBoxWTL.Items.Add("Wordlist-Analyse:")
        ListBoxWTL.Items.Add("Count / Chars")
        ListBoxWTL.Items.Add("")
        ListBoxWTL.Items.AddRange(items)

        Radar.Visible = False

    End Sub

    Private Sub btnWordAnalyserEx_Click(sender As Object, e As EventArgs) Handles btnWordAnalyserEx.Click

        Dim DateNow As String = Format(Now, "HH_mm_ss")

        If My.Settings.ENG = True Then
            MsgBox("The Wordlist-Analyse is stored in the ""\#_Wordlists"" directory.")
        Else
            MsgBox("Die Wordlist-Analyse wird in das Verzeichnis ""\#_Wordlists"" gespeichert.")
        End If

        Dim text As String = ""
        For Each Litem As String In ListBoxWTL.Items
            text &= vbCrLf & Litem
        Next
        IO.File.WriteAllText(Application.StartupPath & "\#_Wordlists\Wordlist_Analyse_" & DateNow & ".txt", text)

    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles pbWordScanner.Click

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbWordScanner2.Text = openFileDialog.FileName
        End If

    End Sub

    Private Sub btnWordScanner_Click(sender As Object, e As EventArgs) Handles btnWordScanner.Click
        '### Starten BGW2
        Radar.Visible = True
        BGWWLT2.RunWorkerAsync()
    End Sub

    Private Sub BGWWLT2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWWLT2.DoWork

        Dim DateNow As String = Format(Now, "HH_mm_ss")
        Dim command As String = ("/c " & "findstr.exe /I /N " & tbWordScanner.Text & " " & """" & tbWordScanner2.Text & """" & " > " & """" & Application.StartupPath & "\#_Wordlists\WordlistScanner_" & DateNow & ".txt" & """")
        Dim commandLineNo As String = ("/c " & "findstr.exe /I " & tbWordScanner.Text & " " & """" & tbWordScanner2.Text & """" & " > " & """" & Application.StartupPath & "\#_Wordlists\WordlistScanner_" & DateNow & ".txt" & """")

        If cbWordScanner.Checked = True Then

            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            process.StartInfo.WorkingDirectory = Application.StartupPath & "\#_Wordlists"
            process.StartInfo.CreateNoWindow = True
            process.StartInfo.Arguments = (command)
            process.Start()
            process.WaitForExit()

        Else

            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            process.StartInfo.WorkingDirectory = Application.StartupPath & "\#_Wordlists"
            process.StartInfo.CreateNoWindow = True
            process.StartInfo.Arguments = (commandLineNo)
            process.Start()
            process.WaitForExit()

        End If

    End Sub

    Private Sub BGWWLT2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWWLT2.RunWorkerCompleted

        Radar.Visible = False

        If My.Settings.ENG = True Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub


    '##########################################################################################
    '######## CUPP ############################################################################
    '##########################################################################################
    Private Sub btnCupp_Click(sender As Object, e As EventArgs) Handles btnCupp.Click

        '#### Start Button CUPP
        Dim dateoffice As String = DateTime.Now.ToString("ddMMyy_HHmmss")
        Dim Dateioffice1 As String

        Dim sAppPath As String
        sAppPath = Application.StartupPath
        Dateioffice1 = "#_Wordlists\CUPP_" & dateoffice & ".txt"

        '### CUPP im Verzeichnis "#_Wordlists ausführen
        If My.Settings.ENG = True Then

            Select Case MessageBox.Show("You can find the CUPP-Wordlist in the folder ""#_Wordlists"". Start CUPP?", "CUPP", MessageBoxButtons.YesNo)
                Case Windows.Forms.DialogResult.Yes

                    Dim process As New Process()
                    process.StartInfo.FileName = "cmd.exe"
                    process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\CUPP\"
                    process.StartInfo.Arguments = ("call /c " & PP3 & " cupp_hashbull.py -i")
                    process.Start()
                    process.WaitForExit()

                    If My.Settings.ENG = True Then
                        MsgBox("Done!")
                    Else
                        MsgBox("Fertig!")
                    End If

                    Me.BringToFront()

                Case Windows.Forms.DialogResult.No
                    Exit Sub
            End Select

        Else

            Select Case MessageBox.Show("Sie finden die CUPP-Wordlist im Ordner ""#_Wordlists"". CUPP starten?", "CUPP", MessageBoxButtons.YesNo)
                Case Windows.Forms.DialogResult.Yes

                    Dim process As New Process()
                    process.StartInfo.FileName = "cmd.exe"
                    process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\CUPP\"
                    process.StartInfo.Arguments = ("call /c " & PP3 & " cupp_hashbull_GER.py -i")
                    process.Start()
                    process.WaitForExit()

                    If My.Settings.ENG = True Then
                        MsgBox("Done!")
                    Else
                        MsgBox("Fertig!")
                    End If

                    Me.BringToFront()

                Case Windows.Forms.DialogResult.No
                    Exit Sub
            End Select
        End If

    End Sub


End Class
