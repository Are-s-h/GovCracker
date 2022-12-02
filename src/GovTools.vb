Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Environment
Imports System.ComponentModel

Public Class GovTools

    '### GovTools by Are§h ###
    '### More Infos: www.govcracker.com ###

#Region "Function"

    ' #### INI - Deklaration der Speichern-Funktion
    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (
ByVal lpApplicationName As String,
ByVal lpKeyName As String,
ByVal lpString As String,
ByVal lpFileName As String) _
As Integer

    '### MD5 Hash
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

    Public KillPID As Integer

    '### Settings
    Public myGER As String
    Public myENG As String
    Public PID As Integer
    Public PIDPrince As Integer

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
    Public PDFMulti As Boolean

    '### Maskprozessor
    Dim WithEvents BGW As New System.ComponentModel.BackgroundWorker
    Dim WithEvents BGWMask As New System.ComponentModel.BackgroundWorker
    Public CMD1 As String = "/c "
    Public CMD2 As String = "/k"
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
    Public CommandCalc As String
    Public parts() As String

    '### Prince
    Private WithEvents _Process2 As Process
    Dim WithEvents BGWPrince As New System.ComponentModel.BackgroundWorker
    Public Counter As Integer
    Public PermMin As String
    Public PermMax As String
    Public PassLenMin As String
    Public PassLenMax As String
    Public UpperCase As String '= " --case-permute"
    Public Berechnung As Double
    Public Potenz As Double

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
    Public PathRules As String = ""

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
    Dim WithEvents BGWRule As New System.ComponentModel.BackgroundWorker
    Public PathX As String
    Public Final As String
    Public DateNowTools As String = Format(Now, "HH_mm_ss")

    Private Sub Tools_Load(sender As Object, e As EventArgs) Handles Me.Load

        '### INI laden
        Dim sb As StringBuilder
        sb = New StringBuilder(1024)

        INI.Lesen("GovTools", "myGER", "", sb, sb.Capacity, Application.StartupPath & "\Packages\INI\SettingsGT.ini")
        myGER = sb.ToString
        INI.Lesen("GovTools", "myENG", "", sb, sb.Capacity, Application.StartupPath & "\Packages\INI\SettingsGT.ini")
        myENG = sb.ToString

        Call Start()
        Call StartPara()

        cbMask1.Checked = True
        cbMask3.Checked = True
        tbMaskPara.Select()

        '### Wordlister / Standardwerte laden
        Perm.Text = "3"
        MinL.Text = "1"
        MaxL.Text = "50"

        '### Cewl
        '### Voreinstellungen laden
        tbCewlPage.Text = "https://www."
        btnCewlStart.Enabled = False
        tbCewlSpider.Text = "2"
        tbCewlWord.Text = "5"

        '### DupCleaner
        Dim DupDate As String = Format(Now, "yyyyMMdd_HHmmss")
        DupTargettxb.Text = Application.StartupPath & "\#_Wordlists\Wordlist_DUP_" & DupDate & ".txt"

        '### Tabcontrols verstecken
        TabControl1.SuspendLayout()
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.ItemSize = New Size(0, 1)
        TabControl1.Appearance = TabAppearance.Buttons
        TabControl1.ResumeLayout()

        '## Extractor Button selektieren
        btnExtractor.BackColor = Color.Gray

        '## Alles links ausrichten
        Call Select_TXB()

        '## Prince Werte zuweisen. Addhandler erst danach, damit bei der ersten Zuweisung Textchange nicht ausgelöst wird.
        tbPrinceMinLen.Text = "1"
        tbPrinceMaxLen.Text = "25"
        tbPrinceMinPerm.Text = "1"
        tbPrinceMaxPerm.Text = "3"
        tbPRINCEPass.Text = "2.502.200"
        tbPRINCEMB.Text = "13"
        btnPrinceSTART.Select()

    End Sub

    Public Sub Select_TXB()

        '# Txb rechts ausrichten
        tbEx.Select(tbEx.Text.Length, 0)
        File1Txb.Select(File1Txb.Text.Length, 0)
        File2Txb.Select(File2Txb.Text.Length, 0)
        File3Txb.Select(File3Txb.Text.Length, 0)
        LenTxb.Select(LenTxb.Text.Length, 0)
        tbWordScanner2.Select(tbWordScanner2.Text.Length, 0)
        Duptxb.Select(Duptxb.Text.Length, 0)
        DupTargettxb.Select(DupTargettxb.Text.Length, 0)
        tbBulkImage.Select(tbBulkImage.Text.Length, 0)

    End Sub

    Public Sub GovCrackerX()

        Try

            Dim process1 As New Process
            For Each process1 In System.Diagnostics.Process.GetProcessesByName("GovCracker")
                If myENG = "Yes" Then
                    MsgBox("GovCracker are already running.")
                Else
                    MsgBox("GovCracker ist bereits gestartet.")
                End If
                Exit Sub
            Next

            '### zu GovCrack wechseln
            Dim process2 As New Process()
            process2.StartInfo.FileName = "govcracker.exe"
            process2.StartInfo.WorkingDirectory = Application.StartupPath
            process2.Start()

            Me.WindowState = FormWindowState.Minimized

        Catch ex As Exception
        End Try

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

                If myENG = "Yes" Then MsgLinux = "You have to copy the hash from the ""Shadow"" file in the linux directory ""etc/shadow"" into a hash.txt. The hash starts with $6$. You have to delete everything after the last colon (e.g.:18585:0:99999:7:::). Then you can attack it in hash-mode 1800"
                If myENG = "No" Then MsgLinux = "Bitte kopieren Sie den User-Hash aus der ""Shadow-Datei"" im Linux Verzeichnis ""etc/shadow"" in eine Hash.txt. Der Hash beginnt mit ""$6$"". Alles ab dem letzten Doppelpunkt müssen Sie im Hash entfernen (bspw. :18585:0:99999:7:::). Der Hash-Typ ist 1800"

                Dim result As DialogResult = MessageBox.Show(MsgLinux, "GovTools", MessageBoxButtons.OK)

                If result = DialogResult.OK Then
                    'nothing
                End If
            End If

            'Windows Login-Extraction ################################################################################################################ 

            If cbEx.Text = "Windows Login Password" Then
                NTLM.Show()
            End If

            'APFS - Extraction ################################################################################################################ 

            If cbEx.Text = "APFS (Apple MacBooks)" Then
                APFS.Show()
            End If

            'ZIP und RAR ################################################################################################################ 

            If cbEx.Text = "ZIP" OrElse cbEx.Text = "RAR" Then
                '### Cygwin 
                If myENG = "Yes" Then
                    MsgBox("Important: Extraction is only possible if the ""Cygwin"" (https://cygwin.com/install.html) software is installed!")
                Else
                    MsgBox("Wichtig: Eine Extraktion ist nur möglich, wenn die Software ""Cygwin"" (https://cygwin.com/install.html) installiert ist!")
                End If
            End If

            'ZIP und RAR ################################################################################################################ 

            If cbEx.Text = "MetaMask-Wallet" Then
                MetaMask.Show()
            End If

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub btnExFile_Click(sender As Object, e As EventArgs) Handles btnExFile.Click

        '######## Datei auswählen 
        If myENG = "Yes" Then TextMsg = "Do you want to extract only one single PDF-file? Click ""Yes""." & vbNewLine & vbNewLine & "If you want to extract several PDF-files in one folder? Click ""No"""
        If myENG = "No" Then TextMsg = "Wenn Sie nur eine einzelnen PDF-Hash extrahieren möchten, dann klicken Sie ""Ja""." & vbNewLine & vbNewLine & "Wenn Sie mehrere PDF-Hashes in einem Ordner extrahieren möchten, dann klicken Sie ""Nein""."

        Dim openFileDialog1 As New OpenFileDialog()

        If cbEx.Text = "PDF" Then
            Dim result As DialogResult = MessageBox.Show(TextMsg, "PDF-Extraction", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                Me.BringToFront()
                Exit Sub
            ElseIf result = DialogResult.No Then

                Using f As New FolderBrowserDialog()
                    If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        Dim d As New System.IO.DirectoryInfo(f.SelectedPath)
                        tbEx.Text = f.SelectedPath & "\*.pdf"
                        PDFMulti = True
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
            Me.BringToFront()
            Exit Sub

        End If

        '#############################################################################################

        If cbEx.Text = "iTunes Backup (iPhone)" Then
            If myENG = "Yes" Then
                MsgBox("Please select the file: ""manifest.plist"" in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""manifest.plist"" im folgendem Fenster aus.")
            End If
        End If

        '#############################################################################################

        If cbEx.Text = "Bitcoin-Wallet" Then
            If myENG = "Yes" Then
                MsgBox("Please select the file: ""wallet.dat"" in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""wallet.dat"" im folgendem Fenster aus.")
            End If
        End If

        '#############################################################################################

        If cbEx.Text = "Dogecoin-Wallet" Then
            If myENG = "Yes" Then
                MsgBox("Please select the file: ""wallet.dat"" in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""wallet.dat"" im folgendem Fenster aus.")
            End If
        End If

        '#############################################################################################

        If cbEx.Text = "Litecoin-Wallet" Then
            If myENG = "Yes" Then
                MsgBox("Please select the file: ""wallet.dat"" in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""wallet.dat"" im folgendem Fenster aus.")
            End If
        End If

        '#############################################################################################


        If cbEx.Text = "MultiBit-Wallet (classic)" Then
            If myENG = "Yes" Then
                MsgBox("Please select the Backup-Key-File (*.key) from the folder: ""AppData\Roaming\MultiBit\multibit-data\key-backup\"" or the Wallet-file (*.wallet) from the folder: ""\AppData\Roaming\MultiBit\multibit-data\wallet-backup"".")
            Else
                MsgBox("Bitte wählen Sie das Backup-Key-File (*.key) aus dem Ordner: ""AppData\Roaming\MultiBit\multibit-data\key-backup\"" oder das Wallet-file (*.wallet) aus dem Ordner: ""\AppData\Roaming\MultiBit\multibit-data\wallet-backup"".aus.")
            End If
        End If

        '#############################################################################################

        If cbEx.Text = "Electrum-Wallet" Then
            If myENG = "Yes" Then
                MsgBox("Please select the file: ""default_wallet"" in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""default_wallet"" im folgendem Fenster aus.")
            End If
        End If

        '#############################################################################################

        If cbEx.Text = "Ethereum (MyEtherWallet.com / Keystore-File)" Then
            If myENG = "Yes" Then
                MsgBox("Please select the keystore-file: ""UTC-- ..."" in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Keystore-Datei: ""UTC-- ..."" im folgendem Fenster aus.")
            End If
        End If

        '###############################################################################################

        If cbEx.Text = "eCryptfs" Then
            If myENG = "Yes" Then
                MsgBox("Please select the file: ""wrapped-passphrase"" from the relevant linux system in the next field.")
            Else
                MsgBox("Bitte wählen Sie die Datei: ""wrapped-passphrase"" im folgendem Fenster aus.")
            End If
        End If

        '###############################################################################################

        If cbEx.Text = "Exodus-Wallet" Then

            If myENG = "Yes" Then
                MsgBox("Please select the ""seed.seco"" file in the ""...\AppData\Roaming\Exodus\"" folder.")
            Else
                MsgBox("Wählen Sie bitte im Ordner ""...\AppData\Roaming\Exodus\"" die Datei ""seed.seco"" aus.")
            End If

            Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)

            openFileDialog1.Filter = "All Files (*.*)| *.*"
            openFileDialog1.InitialDirectory = appData & "\Exodus\exodus.wallet\" 'Application.StartupPath

            If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                tbEx.Text = openFileDialog1.FileName
            End If

            Me.BringToFront()
            Exit Sub

        End If

        '###############################################################################################

        If cbEx.Text = "Mozilla-Firefox (Master Password)" Then

            If myENG = "Yes" Then
                MsgBox("Please select the ""key3.db"" or ""key4.db"" file in the ""...\AppData\Roaming\Mozilla\Firefox\Profiles\"" folder.")
            Else
                MsgBox("Wählen Sie bitte im Ordner ""...\AppData\Roaming\Mozilla\Firefox\Profiles\"" die Datei ""key3.db"" oder ""key4.db"" aus.")
            End If

            Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)

            openFileDialog1.Filter = "All Files (*.*)| *.*"
            openFileDialog1.InitialDirectory = appData & "\Mozilla\Firefox\Profiles\" 'Application.StartupPath

            If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                tbEx.Text = openFileDialog1.FileName
            End If

            Me.BringToFront()
            Exit Sub

        End If

        '########################################################################################

        '### Standard

        openFileDialog1.Filter = "All Files (*.*)| *.*"
        openFileDialog1.InitialDirectory = Application.StartupPath

        If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbEx.Text = openFileDialog1.FileName
        End If

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub

    Private Sub btnEx_Click(sender As Object, e As EventArgs) Handles btnEx.Click

        '### Prüfen ob alle Felder ausgefüllt wurden
        Try
            If cbEx.Text.Contains("#") Or cbEx.Text = "" Then
                If myENG = "Yes" Then MsgBox("Both fields must be completed.")
                If myENG = "No" Then MsgBox("Bitte beide Felder ausfüllen.")
                Exit Sub

            Else

                If tbEx.TextLength = 0 Then
                    If myENG = "Yes" Then MsgBox("Both fields must be completed.")
                    If myENG = "No" Then MsgBox("Bitte beide Felder ausfüllen.")
                    Exit Sub

                End If
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "12200", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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
                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "11600", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If

                'Kill 7z Extraktor
                Dim process2 As New Process()
                For Each process2 In System.Diagnostics.Process.GetProcessesByName("7z2hashcat6414")
                    process2.Kill()
                Next

            End If

            '###iTunes-Extraction ### 

            If cbEx.Text = "iTunes Backup (iPhone)" Then
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "14700 / 14800", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "13600 (WinZip), 17200 - 17230 (PKZIP / default is 17210)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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
                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "12500, 13000, 23700, 23800 (default is 12500)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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
                Dim DateipdfM0 As String
                Dim DateipdfM1 As String
                Dim DateipdfM2 As String

                Dateipdf0 = Application.StartupPath & "\#_Hashout\Hashout_PDF_" & datepdf & ".txt"
                Dateipdf1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_PDF_" & datepdf & ".txt" & """")
                Dateipdf2 = ("""" & tbEx.Text & """" & " > " & Dateipdf1)
                Dateipdf4 = Application.StartupPath & "\Packages\Hashbull_lib\PDF\pdf2hashcat.py"

                DateipdfM0 = Application.StartupPath & "\#_Hashout\Hashout_PDF_" & datepdf & "+Filenames.txt" 'Bei Multi werden zwei Dateien erzeugt
                DateipdfM1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_PDF_" & datepdf & "+Filenames.txt" & """")
                DateipdfM2 = ("""" & tbEx.Text & """" & " > " & DateipdfM1)

                If PDFMulti = False Then

                    '### Nur ein Hash wird extrahiert / Python-Skript
                    System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & Dateipdf4 & """" & " " & Dateipdf2)
                    '### 1 Sek. warten
                    Threading.Thread.Sleep(1000)
                    Call Extraction()

                Else

                    '### Mehrere Hashes extrahieren / Perl Skript
                    System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & Perl & " " & """" & Application.StartupPath & "\Packages\JtR\run\pdf2john.pl" & """" & " " & DateipdfM2)
                    '### 1 Sek. warten

                    PDFMulti = False
                    Threading.Thread.Sleep(1000)

                    Call Extraction()

                    Threading.Thread.Sleep(1000)
                    '###Datei einlesen und String nach $ wiedergeben

                    For Each line In IO.File.ReadAllLines(DateipdfM0)
                        Dim Zeile() As String = line.Split(New String() {":$"}, StringSplitOptions.RemoveEmptyEntries)
                        System.IO.File.AppendAllText(Application.StartupPath & "\#_Hashout\Hashout_PDF_" & datepdf & ".txt", "$" & Zeile(1) & vbNewLine)
                    Next

                End If

                '### Sprache Msgbox
                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "10400 - 10700 (Default Is 10500)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "9400 - 9820 (Default Is 9600)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If

            End If

            '###Bitlocker-Extraction ### 

            If cbEx.Text = "Bitlocker" Then
                Try
                    If myENG = "Yes" Then MsgBox("The extraction of a Bitlocker-Hash can take some time. A 15 GB USB-Stick can last up to an hour. The progress is displayed in the following CMD window. The window closes automatically at the end of the extraction.")
                    If myENG = "No" Then MsgBox("Die Extraktion des Bitlocker-Hashes kann einige Zeit In Anspruch nehmen. Die Extraktion eines 15GB USB-Stick dauert ca. eine Stunde. Der Fortschritt wird In dem folgenden CMD-Fenster angezeigt. Das Fenster schließt automatisch am Ende der Extraktion.")

                    '### Hash wird extrahiert
                    Dim datebit As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                    Dim BitlockerOutput As String = """" & Application.StartupPath & "\#_Hashout\BitlockerExtraction_" & datebit & ".txt" & """"
                    System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "@echo off" & vbNewLine & "Set LOGFILE=" & BitlockerOutput & vbNewLine & "Call :LOG > %LOGFILE%" & vbNewLine & "Exit /b" & vbNewLine & vbNewLine & ":LOG" & vbNewLine & "call " & """" & JtR & "bitlocker2john.exe" & """" & " -i " & """" & tbEx.Text & """")

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
                    If myENG = "Yes" Then MsgTextAll = MsgTextENG
                    If myENG = "No" Then MsgTextAll = MsgTextGER

                    Dim result As DialogResult = MessageBox.Show(MsgTextAll & "Bitlocker: 22100", "GovCracker", MessageBoxButtons.YesNo)
                    If result = DialogResult.Yes Then
                        Call GovCrackerX()
                    ElseIf result = DialogResult.No Then
                        Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "VeraCrypt:  13711 - 13773 (default Is 13721) // TrueCrypt: 6211 - 6233", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "13711 - 13773 (default Is 13721)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "13711 - 13773 (default Is 13721)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "11300", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If
            End If

            '###Dogecoin-Extraction ###

            If cbEx.Text = "Dogecoin-Wallet" Then
                Dim dateBTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiBTC1 As String
                Dim DateiBTC2 As String

                DateiBTC1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Dogecoin_" & dateBTC & ".txt" & """")
                DateiBTC2 = ("""" & tbEx.Text & """" & " > " & DateiBTC1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & JtR & "bitcoin2john.py" & """" & " " & DateiBTC2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "11300", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If
            End If

            '###Multibit-Extraction ###

            If cbEx.Text = "MultiBit-Wallet (classic)" Then
                Dim dateBTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiBTC1 As String
                Dim DateiBTC2 As String

                DateiBTC1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Multibit_" & dateBTC & ".txt" & """")
                DateiBTC2 = ("""" & tbEx.Text & """" & " > " & DateiBTC1)

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & JtR & "multibit2john.py" & """" & " " & DateiBTC2)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                '###### Outputfile Filename entfernen

                Threading.Thread.Sleep(1000)
                Dim input As String = System.IO.File.ReadAllText(Application.StartupPath & "\#_Hashout\Hashout_Multibit_" & dateBTC & ".txt")
                Dim mark = "$" ' Es wird nach dem ersten "$" gesucht

                If input.Contains(mark) Then
                    Dim markPosition = input.IndexOf(mark)
                    Dim result2 = input.Substring(markPosition) ' mit bspw (markposition + 1) wird vor dem zweiten Buchstaben gelöscht
                    Dim schreiben As New IO.StreamWriter(Application.StartupPath & "\#_Hashout\Hashout_Multibit_" & dateBTC & ".txt", False) ' True = Inhalt wird angefügt und nicht überschrieben,, bei False wird überschrieben
                    schreiben.WriteLine(result2)
                    schreiben.Close() ' Erst durch .Close() werden Zeilen abschließend gespeichert
                End If


                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "22500 oder 27700", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "15700", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "11300", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "16600, 21700, 21800 (default is 21700)", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If

            End If

            '###LibreOffice-Extraction ###

            If cbEx.Text = "LibreOffice / OpenOffice" Then
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

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "18400", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If
            End If

            '###Open Office-Extraction ###

            'If cbEx.Text = "OpenOffice" Then
            '    Dim dateilibreoffice As String = DateTime.Now.ToString("ddMMyy_HHmmss")
            '    Dim Dateilibreoffice1 As String
            '    Dim Dateilibreoffice2 As String

            '    Dateilibreoffice1 = ("""" & Application.StartupPath & "\#_Hashout\Hashout_OpenOffice_" & dateilibreoffice & ".txt" & """")
            '    Dateilibreoffice2 = ("""" & tbEx.Text & """" & " > " & Dateilibreoffice1)

            '    '### Hash wird extrahiert
            '    System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP2 & " " & """" & Application.StartupPath & "\Packages\Hashbull_lib\LibreOffice\odf2hashcat.py" & """" & " " & Dateilibreoffice2)

            '    '### 1 Sek. warten
            '    Threading.Thread.Sleep(500)

            '    Call Extraction()

            '    If myENG = "Yes" Then MsgTextAll = MsgTextENG
            '    If myENG = "No" Then MsgTextAll = MsgTextGER

            '    Dim result As DialogResult = MessageBox.Show(MsgTextAll & "18400, 18600", "GovCracker", MessageBoxButtons.YesNo)
            '    If result = DialogResult.Yes Then
            '        Call GovCrackerX()
            '    ElseIf result = DialogResult.No Then
            '        Me.BringToFront()
            '        Exit Sub
            '    End If
            'End If


            '###LUKS (Linux Unified Key System) Extraction ###

            If cbEx.Text = "LUKS (Linux Unified Key System)" Then
                Dim dateLUKS As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiLUKS1 = Application.StartupPath & "\" & "#_Hashout\Hashout_LUKS_" & dateLUKS & ".txt"

                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", """" & Application.StartupPath & "\Packages\DD\dd2.exe" & """" & " if=" & """" & tbEx.Text & """" & " of=" & """" & Application.StartupPath & "\#_Hashout\Hashout_LUKS_" & dateLUKS & ".txt" & """" & " bs=512 count=4097")

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result As DialogResult = MessageBox.Show(MsgTextAll & "14600", "GovCracker", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If
            End If

            '###Exodus-Extraction ### 

            If cbEx.Text = "Exodus-Wallet" Then

                Dim dateBTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiBTC1 As String = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Exodus_" & dateBTC & ".txt" & """")

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP3 & " " & """" & Application.StartupPath & "\Packages\Hashbull_lib\Exo\" & "exodus2hashcat.py" & """" & " " & """" & tbEx.Text & """" & " > " & DateiBTC1)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result2 As DialogResult = MessageBox.Show(MsgTextAll & "28200", "GovCracker", MessageBoxButtons.YesNo)
                If result2 = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result2 = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If

            End If

            '###Firefox-Extraction ### 

            If cbEx.Text = "Mozilla-Firefox (Master Password)" Then

                Dim dateBTC As String = DateTime.Now.ToString("ddMMyy_HHmmss")
                Dim DateiBTC1 As String = ("""" & Application.StartupPath & "\#_Hashout\Hashout_Firefox_" & dateBTC & ".txt" & """")

                '### Hash wird extrahiert
                System.IO.File.WriteAllText(Application.StartupPath & "\Packages\Temp\Extraction.bat", "call " & PP3 & " " & """" & Application.StartupPath & "\Packages\Hashbull_lib\Moz\" & "mozilla2hashcat.py" & """" & " " & """" & tbEx.Text & """" & " > " & DateiBTC1)

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                Call Extraction()

                '### 1 Sek. warten
                Threading.Thread.Sleep(500)

                '### Datei auslesen
                Dim txt As String = System.IO.File.ReadAllText(Application.StartupPath & "\#_Hashout\Hashout_Firefox_" & dateBTC & ".txt")
                If txt.Contains("No Primary") Then

                    If myENG = "Yes" Then
                        MsgBox("No Primary Password is set in Firefox.")
                    Else
                        MsgBox("Es ist kein primäres Kennwort in Firefox festgelegt.")
                    End If

                    Me.BringToFront()
                    Exit Sub

                End If

                '### GovCracker Message
                If myENG = "Yes" Then MsgTextAll = MsgTextENG
                If myENG = "No" Then MsgTextAll = MsgTextGER

                Dim result2 As DialogResult = MessageBox.Show(MsgTextAll & "26000, 26100", "GovCracker", MessageBoxButtons.YesNo)
                If result2 = DialogResult.Yes Then
                    Call GovCrackerX()
                ElseIf result2 = DialogResult.No Then
                    Me.BringToFront()
                    Exit Sub
                End If

            End If

            '### Zum Schluss Txb leeren

            Me.BringToFront()

        Catch ex As Exception
            If myENG = "Yes" Then
                MsgBox("An error has occurred. This can have several reasons. Please make sure that there are no spaces in the file name and path.")
            Else
                MsgBox("Es ist ein Fehler aufgetreten. Dies kann mehrere Gründe haben. Bitte achten Sie darauf, dass keine Leerzeichen im Dateinamen oder Pfad vorhanden sind.")
            End If
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
        CommandCalc = CMD2 & "mp64.exe " & CommandW & Charset1 & Charset2 & Charset3 & Charset4 & NumLeng1 & NumLeng2 & Multiple & Occur & StartP & StopP & Outputfile & DatePub & Combi

    End Sub

    Private Sub btnMaskStart_Click(sender As Object, e As EventArgs) Handles btnMaskStart.Click

        '#########START Button 
        Try

            If tbMaskPara.TextLength = 0 Then
                If myENG = "Yes" Then MsgBox("No command-parameters were entered.")
                If myENG = "No" Then MsgBox("Es wurden keine Command-Parameters eingegeben.")
                Exit Sub
            End If

            If cbMask3.Checked Then
                If myENG = "Yes" Then MsgBox("You find the wordlist in the GovCracker-Folder ""#_Wordlists"".")
                If myENG = "No" Then MsgBox("Sie finden die Wordlist im Ordner ""#_Wordlists"".")
            End If

            If cbMask4.Checked = True Then

                'nur kalkulieren
                Dim process As New Process()
                process.StartInfo.FileName = "cmd.exe"
                process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\MP"
                process.StartInfo.Arguments = (CommandCalc)
                process.Start()
                Exit Sub

            End If

            Radar.Visible = True
            pbRadar.Visible = True
            BGWMask.RunWorkerAsync()

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub BGWMask_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWMask.DoWork
        Try

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .FileName = "cmd"
                .Arguments = (CommandStart)
                .WorkingDirectory = Application.StartupPath & "\Packages\MP"
                .CreateNoWindow = True
                .UseShellExecute = False

            End With
            _Process2.Start()
            PID = _Process2.Id
            _Process2.WaitForExit()

        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWMask_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWMask.RunWorkerCompleted

        Radar.Visible = False
        pbRadar.Visible = False

        If myENG = "Yes" Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub

    Private Sub btnStopMP_Click(sender As Object, e As EventArgs) Handles btnStopMP.Click

        Try
            'Kill Process
            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(PID)
            aProcess.Kill()

            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("mp64")
                process2.Kill()
            Next

        Catch ex As Exception
        End Try

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

        If myENG = "Yes" Then
            MsgBox("Select only files that are smaller than 1 MB.")
        Else
            MsgBox("Nur Dateien auswählen, die kleiner als 1 MB sind.")
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

        Try

            '### Alle Felder müssen ausgefüllt sein
            If tbPrinceMinLen.TextLength < 1 Or tbPrinceMaxLen.TextLength < 1 Or tbPrinceMinPerm.TextLength < 1 Or tbPrinceMaxPerm.TextLength < 1 Then

                If myENG = "Yes" Then
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
                If myENG = "Yes" Then
                    MsgBox("max. permutation must not be greater than max. password length!")
                Else
                    MsgBox("max. Permutation darf nicht größer sein als max. Passwortlänge!")
                End If
                Exit Sub
            End If

            '#### Output schreiben
            Dim save As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(Application.StartupPath & "\Packages\Temp\PRINCE_Input.txt"), IO.FileMode.Create))
            'System.Threading.Thread.Sleep(200)

            save.WriteLine(tbPrince.Text)
            save.Close()

            System.Threading.Thread.Sleep(500)

            '### Hinweis
            If myENG = "Yes" Then
                MsgBox("You can find the PRINCE-Wordlist in the folder ""#_Wordlists""" & vbNewLine & vbNewLine & "PRINCE starts now! Depending on the size, this process can take a long time and use a lot of hard drive storage.")
            Else
                MsgBox("Sie finden die PRINCE-Wordlist im Ordner ""#_Wordlists""" & vbNewLine & vbNewLine & "PRINCE startet jetzt! Je nach Umfang kann dieser Vorgang einige Zeit dauern und viel Speicherplatz benötigen.")
            End If

            Radar.Visible = True
            pbRadar.Visible = True
            BGWPrince.RunWorkerAsync()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub BGWPrince_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWPrince.DoWork
        Try

            Dim PrinceDate As String = DateTime.Now.ToString("ddMMyy_HHmmss")
            Dim PrinceOut = Application.StartupPath & "\#_Wordlists\PRINCE_" & PrinceDate & ".txt"

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .FileName = "cmd"
                .WorkingDirectory = Application.StartupPath & "\Packages\PRINCE\"
                .Arguments = ("/c " & "pp64.exe --pw-min=" & tbPrinceMinLen.Text & " --pw-max=" & tbPrinceMaxLen.Text & " --elem-cnt-min=" & tbPrinceMinPerm.Text & " --elem-cnt-max=" & tbPrinceMaxPerm.Text & UpperCase & " -o " & """" & PrinceOut & """" & " """ & Application.StartupPath & "\Packages\Temp\PRINCE_Input.txt" & """")
                .CreateNoWindow = True
                .UseShellExecute = False

            End With
            _Process2.Start()
            PIDPrince = _Process2.Id
            _Process2.WaitForExit()

        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWPrince_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWPrince.RunWorkerCompleted

        Try

            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(PIDPrince)
            aProcess.Kill()

            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("pp64")
                process2.Kill()
            Next

        Catch ex As Exception
            'nothing
        End Try

        Radar.Visible = False
        pbRadar.Visible = False
        If myENG = "Yes" Then
            MsgBox("Done! The wordlist was saved to the ""\#_Wordlist"" folder.")
        Else
            MsgBox("Erledigt! Die Wordlist wurde in den Ordner ""\#_Wordlist"" gespeichert.")
        End If
        Me.BringToFront()

    End Sub

    Private Sub btnPrinceSTOP_Click(sender As Object, e As EventArgs) Handles btnPrinceSTOP.Click

        Try
            'Kill Process
            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(PIDPrince)
            aProcess.Kill()

            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("pp64")
                process2.Kill()
            Next

        Catch ex As Exception
        End Try

    End Sub

    Private Sub CheckBoxPrince_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPrince.CheckedChanged
        If CheckBoxPrince.Checked = True Then UpperCase = " --case-permute"
        If CheckBoxPrince.Checked = False Then UpperCase = ""
    End Sub

    Private Sub btnPrinceDefault_Click_1(sender As Object, e As EventArgs) Handles btnPrinceDefault.Click
        Call DefaultX()
    End Sub

    Private Sub tbPrinceMaxLen_TextChanged(sender As Object, e As EventArgs) ' Handles tbPrinceMaxLen.TextChanged

        Try
            Dim zahl As Integer = CType(tbPrinceMaxLen.Text, Integer)

            If zahl > 25 Then
                If myENG = "Yes" Then
                    MsgBox("The maximum length is ""25"".")
                Else
                    MsgBox("Die max. Länge beträgt ""25"".")
                End If
                tbPrinceMaxLen.Text = "25"
            End If

        Catch ex As Exception
            'Nothing
        End Try
    End Sub

    Private Sub btnCalc_Click(sender As Object, e As EventArgs) Handles btnCalc.Click

        Try

            tbPRINCEPass.Clear()
            tbPRINCEMB.Clear()

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .Verb = "runas"
                .FileName = "cmd"
                .Arguments = ("/c " & "pp64.exe --keyspace --pw-min=" & tbPrinceMinLen.Text & " --pw-max=" & tbPrinceMaxLen.Text & " --elem-cnt-min=" & tbPrinceMinPerm.Text & " --elem-cnt-max=" & tbPrinceMaxPerm.Text & UpperCase & " """ & Application.StartupPath & "\Packages\Temp\PRINCE_Input.txt" & """")
                .WorkingDirectory = Application.StartupPath & "\Packages\PRINCE"
                .CreateNoWindow = True
                .UseShellExecute = False
                .ErrorDialog = True
                .RedirectStandardOutput = True
                .RedirectStandardError = True
                .RedirectStandardInput = True

            End With
            _Process2.Start()
            _Process2.BeginOutputReadLine()
            _Process2.BeginErrorReadLine()
            KillPID = _Process2.Id

        Catch ex As Exception
        End Try

    End Sub

    Private Sub _Process2_OutputDataReceived(sender As Object, e As DataReceivedEventArgs) Handles _Process2.OutputDataReceived
        Try
            BeginInvoke(Sub() OutputData(e.Data))
        Catch ex As Exception
        End Try

    End Sub

    Private Sub OutputData(txt As String)
        Try
            'Sonst Fehler beim Abbruch von Auto
            If txt Is Nothing Then Exit Sub

            If tbPrinceMaxPerm.Text <> "" And tbPrinceMinPerm.Text <> "" And tbPrinceMinLen.Text <> "" And tbPrinceMaxLen.Text <> "" And tbPrince.Text <> "" Then

                Dim txt2 As Integer = CType(txt, Integer)
                Dim txt3 As String
                txt3 = String.Format("{0:0,0}", txt2)

                tbPRINCEPass.AppendText(txt3 & vbNewLine)
                tbPRINCEPass.Select(tbPRINCEPass.TextLength, 0)
                tbPRINCEPass.ScrollToCaret()

            End If

            Call KillPrince

        Catch ex As Exception
        End Try
    End Sub

    Private Sub KillPrince()

        Try
            '# pp64 sofort wieder per PID beenden
            If KillPID > 1 Then
                Dim Process0 As System.Diagnostics.Process
                Process0 = System.Diagnostics.Process.GetProcessById(KillPID)
                Process0.Kill()
            End If

            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("pp64")
                process2.Kill()
            Next

        Catch ex As Exception
        End Try

    End Sub

    Private Sub tbPRINCEPass_TextChanged(sender As Object, e As EventArgs) Handles tbPRINCEPass.TextChanged
        Call MB()
    End Sub

    Private Sub MB()

        Try
            tbPRINCEMB.Clear()

            '# Alle Zeichen zählen
            Counter = 0
            For Each zeile As String In tbPrince.Lines
                Counter = Counter + zeile.Length
            Next

            Dim maxP As Integer
            If tbPrinceMaxPerm.Text <> "" Then
                maxP = CInt(tbPrinceMaxPerm.Text)
            End If

            Dim Anzahlzeilen As Double = CInt(tbPrince.Lines.Length)
            Dim Zeichenanzahl As Double = Counter
            Dim Durchschnitt As Double = Zeichenanzahl / Anzahlzeilen 'Durchschnittliche Anzahl der Zeichen

            Dim Combis As Long
            If tbPRINCEPass.Text <> "" Then
                Combis = CInt(tbPRINCEPass.Text)
            End If

            Berechnung = (Combis * maxP * Durchschnitt) / 1048576 '(Potenz / 4000000 * 100) '(Potenz * Durchschnitt * maxP / 1048576 / (3 / 2)) 'Anpassung zur Wertfindung

            tbPRINCEMB.Text = String.Format("{0:0,0}", Berechnung)
            'Dim Zahl As Double = CType(tbPRINCEMB.Text, Double)
            'If Zahl < 10 Then Zahl.TryParse(tbPRINCEMB.Text, Zahl)

            Threading.Thread.Sleep(250)

            Call KillPrince()

        Catch ex As Exception
        End Try

    End Sub




    '##########################################################################################
    '######## Wordlister ######################################################################
    '##########################################################################################

    Private Sub btnWordStart_Click(sender As Object, e As EventArgs) Handles btnWordStart.Click


        Try

            If myENG = "Yes" Then MsgBox("The wordlist will now be created and saved in the folder ""\#_Wordlists\"". This can take up to 10 minutes.")
            If myENG = "No" Then MsgBox("Die Wordlist wird jetzt erstellt und im Ordner ""\#_Wordlists\"" abgespeichert. Dies kann bis zu 10 Min. dauern.")

            Radar.Visible = True
            pbRadar.Visible = True

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

            '### Vor- und Nachnamen zwei und drei Stellen
            If tbWord1.TextLength > 2 Then save.WriteLine(tbWord1.Text.Substring(0, 2))
            If tbWord2.TextLength > 2 Then save.WriteLine(tbWord2.Text.Substring(0, 2))
            If tbWord6.TextLength > 2 Then save.WriteLine(tbWord6.Text.Substring(0, 2))
            If tbWord7.TextLength > 2 Then save.WriteLine(tbWord7.Text.Substring(0, 2))
            If tbWord11.TextLength > 2 Then save.WriteLine(tbWord11.Text.Substring(0, 2))
            If tbWord12.TextLength > 2 Then save.WriteLine(tbWord12.Text.Substring(0, 2))
            If tbWord16.TextLength > 2 Then save.WriteLine(tbWord16.Text.Substring(0, 2))
            If tbWord17.TextLength > 2 Then save.WriteLine(tbWord17.Text.Substring(0, 2))
            If tbWord21.TextLength > 2 Then save.WriteLine(tbWord21.Text.Substring(0, 2))
            If tbWord22.TextLength > 2 Then save.WriteLine(tbWord22.Text.Substring(0, 2))
            If tbWord26.TextLength > 2 Then save.WriteLine(tbWord26.Text.Substring(0, 2))
            If tbWord27.TextLength > 2 Then save.WriteLine(tbWord27.Text.Substring(0, 2))
            If tbWord31.TextLength > 2 Then save.WriteLine(tbWord31.Text.Substring(0, 2))
            If tbWord32.TextLength > 2 Then save.WriteLine(tbWord32.Text.Substring(0, 2))

            If tbWord1.TextLength > 3 Then save.WriteLine(tbWord1.Text.Substring(0, 3))
            If tbWord2.TextLength > 3 Then save.WriteLine(tbWord2.Text.Substring(0, 3))
            If tbWord6.TextLength > 3 Then save.WriteLine(tbWord6.Text.Substring(0, 3))
            If tbWord7.TextLength > 3 Then save.WriteLine(tbWord7.Text.Substring(0, 3))
            If tbWord11.TextLength > 3 Then save.WriteLine(tbWord11.Text.Substring(0, 3))
            If tbWord12.TextLength > 3 Then save.WriteLine(tbWord12.Text.Substring(0, 3))
            If tbWord16.TextLength > 3 Then save.WriteLine(tbWord16.Text.Substring(0, 3))
            If tbWord17.TextLength > 3 Then save.WriteLine(tbWord17.Text.Substring(0, 3))
            If tbWord21.TextLength > 3 Then save.WriteLine(tbWord21.Text.Substring(0, 3))
            If tbWord22.TextLength > 3 Then save.WriteLine(tbWord22.Text.Substring(0, 3))
            If tbWord26.TextLength > 3 Then save.WriteLine(tbWord26.Text.Substring(0, 3))
            If tbWord27.TextLength > 3 Then save.WriteLine(tbWord27.Text.Substring(0, 3))
            If tbWord31.TextLength > 3 Then save.WriteLine(tbWord31.Text.Substring(0, 3))
            If tbWord32.TextLength > 3 Then save.WriteLine(tbWord32.Text.Substring(0, 3))

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
        Try
            System.Threading.Thread.Sleep(500)

            '### Wordlister-Permutation wird gestartet
            Dim WLM As String = PP3 & " wordlister.py --input " & """" & Application.StartupPath & "\#_Wordlists\Wordlister_Input.txt" & """" & PermX & MinLX & MaxLX & LeetX & CapX & UpX
            Dim process2 As New Process()

            'process2.StartInfo.FileName = "cmd.exe"
            'process2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            'process2.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Wordlister\"
            'process2.StartInfo.Arguments = ("/c call " & WLM)
            'process2.Start()
            'process2.WaitForExit()

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .FileName = "cmd"
                .Arguments = ("/c call " & WLM)
                .WorkingDirectory = Application.StartupPath & "\Packages\Wordlister\"
                .CreateNoWindow = True
                .UseShellExecute = False

            End With
            _Process2.Start()
            PID = _Process2.Id
            _Process2.WaitForExit()

        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWWord_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWWord.RunWorkerCompleted

        Try

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
            pbRadar.Visible = False
            Me.BringToFront()

            If myENG = "Yes" Then
                MsgBox("Done!")
            Else
                MsgBox("Erledigt!")
            End If

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub btnWordStop_Click(sender As Object, e As EventArgs) Handles btnWordStop.Click

        Try

            'Kill Process
            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(PID)
            aProcess.Kill()

            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("pp3")
                process2.Kill()
            Next

        Catch ex As Exception
            'nothing
        End Try

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

    Private Sub btnWordDefault_Click(sender As Object, e As EventArgs) Handles btnWordDefault.Click
        tbWordStandard.Clear()
        Call WordlisterX()
    End Sub


    '##########################################################################################
    '######## CeWL ############################################################################
    '##########################################################################################

    Private Sub C_Aktualisieren()
        Try


            '### CeWL ausführen
            Dim Date1 As String = Format(Now, "yyyyMMdd_HHmmss")
            CewlCommand = "ubuntu2004.exe" & " run cewl " & tbCewlPage.Text & " -w CeWL_Wordlist_" & Date1 & ".txt " & " -e  --email_file CeWL_EMails_" & Date1 & ".txt " & CMax & Cmin
            ' tbCewlCommand.Text = command

            Me.btnCewlStart.Enabled = True

        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnCewlInstall_Click(sender As Object, e As EventArgs) Handles btnCewlInstall.Click
        Try
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
        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub btnCewlStart_Click(sender As Object, e As EventArgs) Handles btnCewlStart.Click
        Try


            '#########START Button führt das Commad Fenster aus
            If myENG = "Yes" Then

                MessageBox.Show("The process can take up to an hour for larger websites", "GovTools", MessageBoxButtons.OK)
            Else
                MessageBox.Show("Bei größeren Internetseiten kann dieser Vorgang bis zu einer Stunde dauern!", "GovTools", MessageBoxButtons.OK)
            End If

            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"
            process.StartInfo.WorkingDirectory = Application.StartupPath & "\#_Wordlists"
            process.StartInfo.Arguments = ("/c " & CewlCommand) 'tbCewlCommand.Text)
            process.Start()
            process.WaitForExit()

            If myENG = "Yes" Then
                MsgBox("Done!")
            Else
                MsgBox("Fertig!")
            End If

            Me.BringToFront()

        Catch ex As Exception
            'nothing
        End Try
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
        If myENG = "Yes" Then MsgBox("The output file was created in the folder ""#_Wordlist"". The process may take some time for large files.")
        If myENG = "No" Then MsgBox("Die Ausgabedatei wird nach Fertigstellung in den Ordner ""#_Wordlist"" gespeichert. Bei sehr großen Dateien kann der Vorgang einige Zeit in Anspruch nehmen.")

        Radar.Visible = True
        pbRadar.Visible = True

        If File1Txb.TextLength > 1 And File2Txb.TextLength > 1 And File3Txb.TextLength < 1 Then
            BGWComb.RunWorkerAsync()
            Exit Sub
        End If

        If File1Txb.TextLength > 1 And File2Txb.TextLength > 1 And File3Txb.TextLength > 1 Then
            BGWComb2.RunWorkerAsync()
            Exit Sub
        End If

        If File1Txb.TextLength > 1 AndAlso File2Txb.TextLength < 1 AndAlso File3Txb.TextLength < 1 Then

            If myENG = "Yes" Then MsgBox("You have to fill in at least two fields", vbSystemModal)
            If myENG = "No" Then MsgBox("Sie müssen mind. zwei Felder ausfüllen.", vbSystemModal)

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

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub

    Private Sub pbCombinator2_Click(sender As Object, e As EventArgs) Handles pbCombinator2.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            File2Txb.Text = openFileDialog.FileName
        End If

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub

    Private Sub pbCombinator3_Click(sender As Object, e As EventArgs) Handles pbCombinator3.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            File3Txb.Text = openFileDialog.FileName
        End If

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub

    Private Sub BGWComb_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWComb.DoWork
        Try
            Dim Date3 As String = Format(Now, "yyyyMMdd_HHmmss")
            Dim process As New Process()

            'process.StartInfo.FileName = "cmd.exe"
            'process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            'process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
            'process.StartInfo.Arguments = ("/c combinator.exe """ & File1Txb.Text & """ """ & File2Txb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Combinator2_" & Date3 & ".txt""")
            'process.Start()
            'process.WaitForExit()


            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .FileName = "cmd"
                .Arguments = ("/c combinator.exe """ & File1Txb.Text & """ """ & File2Txb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Combinator2_" & Date3 & ".txt""")
                .WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
                .CreateNoWindow = True
                .UseShellExecute = False

            End With
            _Process2.Start()
            PID = _Process2.Id
            _Process2.WaitForExit()

        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWComb2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWComb2.DoWork
        Try
            Dim Date3 As String = Format(Now, "yyyyMMdd_HHmmss")
            Dim process As New Process()

            'process.StartInfo.FileName = "cmd.exe"
            'process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            'process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
            'process.StartInfo.Arguments = ("/c combinator3.exe """ & File1Txb.Text & """ """ & File2Txb.Text & """ """ & File3Txb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Combinator3_" & Date3 & ".txt""")
            'process.Start()
            'process.WaitForExit()

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .FileName = "cmd"
                .Arguments = ("/c combinator3.exe """ & File1Txb.Text & """ """ & File2Txb.Text & """ """ & File3Txb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Combinator3_" & Date3 & ".txt""")
                .WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
                .CreateNoWindow = True
                .UseShellExecute = False

            End With
            _Process2.Start()
            PID = _Process2.Id
            _Process2.WaitForExit()

        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWComb_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWComb.RunWorkerCompleted

        Radar.Visible = False
        pbRadar.Visible = False
        Me.Select()

        If myENG = "Yes" Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

    End Sub

    Private Sub BGWComb2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWComb2.RunWorkerCompleted

        Radar.Visible = False
        pbRadar.Visible = False
        Me.Select()

        If myENG = "Yes" Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

    End Sub


    Private Sub btnComSTOP_Click(sender As Object, e As EventArgs) Handles btnComSTOP.Click

        Try

            'Kill Process
            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(PID)
            aProcess.Kill()


            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("combinator")
                process2.Kill()
            Next

        Catch ex As Exception
            'nothing
        End Try

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

        '## Alles links ausrichten
        Call Select_TXB()

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
            If myENG = "Yes" Then MsgBox("You have to fill in the input fields.", vbSystemModal)
            If myENG = "No" Then MsgBox("You must fill in the fields.", vbSystemModal)
            Exit Sub
        End If

        If myENG = "Yes" Then MsgBox("The output file was created in the folder ""#_Wordlist"". The process may take some time for large files.")
        If myENG = "No" Then MsgBox("Die Ausgabedatei wird nach Fertigstellung in den Ordner ""#_Wordlist"" gespeichert. Bei sehr großen Dateien kann der Vorgang einige Zeit in Anspruch nehmen.")

        Radar.Visible = True
        pbRadar.Visible = True
        BGWLen.RunWorkerAsync()

    End Sub

    Private Sub BGWLen_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWLen.DoWork

        Try

            Dim Date3 As String = Format(Now, "yyyyMMdd_HHmmss")
            Dim process As New Process()
            'process.StartInfo.FileName = "cmd.exe"
            'process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            'process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
            'process.StartInfo.Arguments = ("/c len.exe " & LenMinTxb.Text & " " & LenMaxTxb.Text & " < """ & LenTxb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Len_Wordlist_" & Date3 & ".txt""")
            'process.Start()
            'process.WaitForExit()

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .FileName = "cmd"
                .Arguments = ("/c len.exe " & LenMinTxb.Text & " " & LenMaxTxb.Text & " < """ & LenTxb.Text & """ > """ & Application.StartupPath & "\#_Wordlists\Len_Wordlist_" & Date3 & ".txt""")
                .WorkingDirectory = Application.StartupPath & "\Packages\Hashbull_lib\HCUtils"
                .CreateNoWindow = True
                .UseShellExecute = False

            End With
            _Process2.Start()
            PID = _Process2.Id
            _Process2.WaitForExit()

        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWLen_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWLen.RunWorkerCompleted

        Radar.Visible = False
        pbRadar.Visible = False
        Me.Select()

        If myENG = "Yes" Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

    End Sub

    Private Sub bntLenSTOP_Click(sender As Object, e As EventArgs) Handles bntLenSTOP.Click

        Try

            'Kill Process
            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(PID)
            aProcess.Kill()

            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("len")
                process2.Kill()
            Next

        Catch ex As Exception
            'nothing
        End Try

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

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub

    Private Sub pbDup2_Click(sender As Object, e As EventArgs) Handles pbDup2.Click

        '#### Auswahl und in TexBox einfügen 
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            DupTargettxb.Text = openFileDialog.FileName
        End If

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub


    Private Sub btnDup_Click(sender As Object, e As EventArgs) Handles btnDup.Click

        If Duptxb.Text = "" Then
            If myENG = "Yes" Then
                MsgBox("Please select a file.")
            Else
                MsgBox("Bitte wählen Sie eine Datei aus.")
            End If
            Exit Sub
        End If

        Radar.Visible = True
        pbRadar.Visible = True
        BGWDup.RunWorkerAsync()
    End Sub

    Private Sub BGWDup_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWDup.DoWork
        Try

            Dim process As New Process()

            _Process2 = New Process With {.EnableRaisingEvents = True}
            With _Process2.StartInfo

                .FileName = "cmd"
                .Arguments = ("/c " & "PP3.exe " & """" & Application.StartupPath & "\Packages\DupC\DupCleaner.py" & """" & " --inTxt " & """" & Duptxb.Text & """" & " --out " & """" & DupTargettxb.Text & """")
                .WorkingDirectory = Application.StartupPath & "\Packages\Py3\App\Python"
                .CreateNoWindow = True
                .UseShellExecute = False

            End With
            _Process2.Start()
            PID = _Process2.Id
            _Process2.WaitForExit()

        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWDup_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWDup.RunWorkerCompleted
        Radar.Visible = False
        pbRadar.Visible = False
        MsgBox("Done!")
        Me.BringToFront()
    End Sub

    Private Sub btnDupSTOP_Click(sender As Object, e As EventArgs) Handles btnDupSTOP.Click

        Try

            'Kill Process
            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(PID)
            aProcess.Kill()

            Dim process2 As New Process()
            For Each process2 In System.Diagnostics.Process.GetProcessesByName("pp3")
                process2.Kill()
            Next

        Catch ex As Exception
            'nothing
        End Try

    End Sub


    '##########################################################################################
    '######## HashGen #########################################################################
    '##########################################################################################
    Private Sub btnHashGen_Click(sender As Object, e As EventArgs) Handles btnHashGen.Click

        Try
            'https://geekshangout.com/a-simple-vb-net-application-to-hash-a-string-to-a-md5-hash/
            'Online https://www.webatic.com/md5-convertor

            HashOUT.Text = MD5StringHash(HashIn.Text)

            Dim DateBatch3 As String = Format(Now, "yyyyMMdd_HHmmss")
            Dim Pfadbatch3 As String = (Application.StartupPath & "\#_Hashout\MD5_Example_Hash_" & HashIn.Text & "_" & DateBatch3 & ".txt")
            Dim fs3 As New FileStream(Pfadbatch3, FileMode.Append, FileAccess.Write)
            Dim s3 As New StreamWriter(fs3)
            s3.WriteLine(HashOUT.Text)
            s3.Close()

            If myENG = "Yes" Then
                MsgBox("Done! The hash was saved to the folder ""#_Hashout"".")
            Else
                MsgBox("Fertig! Der Hash wurde in den Ordner ""#_Hashout"" gespeichert.")
            End If
        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
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

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub


    Private Sub btnbulkstart_Click(sender As Object, e As EventArgs) Handles btnBulkStart.Click

        If cbBulk1.Checked = True Then
            BGWBulk.RunWorkerAsync()
        End If

        If cbBulk2.Checked = True Then
            BGWBulk2.RunWorkerAsync()
        End If

    End Sub

    Private Sub BGWBulk_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWBulk.DoWork
        Try
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
        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWBulk2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWBulk2.DoWork
        Try
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
        Catch ex As Exception
            'nothing
        End Try
    End Sub


    Private Sub BGWBulk_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWBulk.RunWorkerCompleted

        If myENG = "Yes" Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub

    Private Sub BGWBulk2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWBulk2.RunWorkerCompleted

        If myENG = "Yes" Then
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

        Try

            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            openFileDialog.FilterIndex = 1
            openFileDialog.RestoreDirectory = True

            If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                PathX = openFileDialog.FileName

                If myENG = "Yes" Then
                    MsgBox("The maskfile is stored in the ""\#_Wordlists"" directory.")
                Else
                    MsgBox("Die Masken-Datei wird in das Verzeichnis ""\#_Wordlists"" gespeichert.")
                End If

                Radar.Visible = True
                pbRadar.Visible = True
                BGWWLT.RunWorkerAsync()

            Else
                Exit Sub
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub BGWWLT_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWWLT.DoWork

        Try
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
        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub BGWWLT_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWWLT.RunWorkerCompleted

        Radar.Visible = False
        pbRadar.Visible = False

        If myENG = "Yes" Then
            MsgBox("Done!")
        Else
            MsgBox("Fertig!")
        End If

        Me.BringToFront()

    End Sub


    '### Analyser ###
    Private Sub btnWordAnalyser_Click(sender As Object, e As EventArgs) Handles btnWordAnalyser.Click
        Try
            ListBoxWTL.Items.Clear()
            btnWordAnalyserEx.Enabled = True
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.InitialDirectory = Application.StartupPath & "\#_Wordlists"
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            openFileDialog1.FilterIndex = 1
            openFileDialog1.RestoreDirectory = True

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                ' Get the file name.
                PathX = openFileDialog1.FileName
            Else
                Exit Sub
            End If


            Radar.Visible = True
            pbRadar.Visible = True

            Dim command As String = ("/k " & """" & PP3 & " " & """" & Application.StartupPath & "\Packages\Analyser\GovAnalyser.py" & """" & " -in " & """" & PathX & """")

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

        Catch ex As Exception
            'nothing
        End Try

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
        pbRadar.Visible = False

    End Sub

    Private Sub btnWordAnalyserEx_Click(sender As Object, e As EventArgs) Handles btnWordAnalyserEx.Click
        Try
            Dim DateNow As String = Format(Now, "HH_mm_ss")

            If myENG = "Yes" Then
                MsgBox("The Wordlist-Analyse is saved in the ""\#_Wordlists"" directory.")
            Else
                MsgBox("Die Wordlist-Analyse wird in das Verzeichnis ""\#_Wordlists"" gespeichert.")
            End If

            Dim text As String = ""
            For Each Litem As String In ListBoxWTL.Items
                text &= vbCrLf & Litem
            Next
            IO.File.WriteAllText(Application.StartupPath & "\#_Wordlists\Wordlist_Analyse_" & DateNow & ".txt", text)
        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles pbWordScanner.Click

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
        openFileDialog.Filter = "All Files (*.*)| *.*"

        If openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbWordScanner2.Text = openFileDialog.FileName
        End If

        '## Alles links ausrichten
        Call Select_TXB()

    End Sub

    Private Sub btnWordScanner_Click(sender As Object, e As EventArgs) Handles btnWordScanner.Click

        Try

            '### Starten BGW2

            If tbWordScanner.Text = "" Or tbWordScanner2.Text = "" Then
                If myENG = "Yes" Then
                    MsgBox("Please fill in both fields.")
                Else
                    MsgBox("Bitte füllen Sie beide Felder aus.")
                End If
                Exit Sub
            End If

            Radar.Visible = True
            pbRadar.Visible = True

            ListBoxWTL.Items.Clear()

            BGWWLT2.RunWorkerAsync()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub BGWWLT2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWWLT2.DoWork
        Try

            DateNowTools = Format(Now, "HH_mm_ss")
            Dim command As String = ("/c " & "findstr.exe /I /N /OFFLINE " & tbWordScanner.Text & " " & """" & tbWordScanner2.Text & """" & " > " & """" & Application.StartupPath & "\#_Wordlists\WordlistScanner_" & DateNowTools & ".txt" & """")
            Dim commandLineNo As String = ("/c " & "findstr.exe /I /OFFLINE " & tbWordScanner.Text & " " & """" & tbWordScanner2.Text & """" & " > " & """" & Application.StartupPath & "\#_Wordlists\WordlistScanner_" & DateNowTools & ".txt" & """")

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


        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub BGWWLT2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWWLT2.RunWorkerCompleted

        Radar.Visible = False
        pbRadar.Visible = False


        Dim PathTools As String = (Application.StartupPath & "\#_Wordlists\WordlistScanner_" & DateNowTools & ".txt")

        '# Kein Ergbnis abfangen
        If My.Computer.FileSystem.GetFileInfo(PathTools).Length = 0 Then
            If myENG = "Yes" Then
                MsgBox("The keyword was not found.")
            Else
                MsgBox("Der Suchbegriff wurde nicht gefunden.")
            End If
            Me.BringToFront()
            Exit Sub
        End If

        '# Datei unter 120kb werden in die Listbox geladen
        If My.Computer.FileSystem.GetFileInfo(PathTools).Length < 120000 Then
            For Each line In IO.File.ReadLines(PathTools)
                ListBoxWTL.Items.Add(line)
            Next
        End If

        If myENG = "Yes" Then
            MsgBox("Done! The result is in the folder ""#_Wordlists"".")
        Else
            MsgBox("Fertig! Das Ergebnis liegt im Ordner ""#_Wordlists"".")
        End If

        Me.BringToFront()

    End Sub


    '##########################################################################################
    '######## CUPP ############################################################################
    '##########################################################################################
    Private Sub btnCupp_Click(sender As Object, e As EventArgs) Handles btnCupp.Click
        Try

            '#### Start Button CUPP
            Dim dateoffice As String = DateTime.Now.ToString("ddMMyy_HHmmss")
            Dim Dateioffice1 As String

            Dim sAppPath As String
            sAppPath = Application.StartupPath
            Dateioffice1 = "#_Wordlists\CUPP_" & dateoffice & ".txt"

            '### CUPP im Verzeichnis "#_Wordlists ausführen
            If myENG = "Yes" Then

                Select Case MessageBox.Show("You can find the CUPP-Wordlist in the folder ""#_Wordlists"". Start CUPP?", "CUPP", MessageBoxButtons.YesNo)
                    Case Windows.Forms.DialogResult.Yes

                        Dim process As New Process()
                        process.StartInfo.FileName = "cmd.exe"
                        process.StartInfo.WorkingDirectory = Application.StartupPath & "\Packages\CUPP\"
                        process.StartInfo.Arguments = ("call /c " & PP3 & " cupp_hashbull.py -i")
                        process.Start()
                        process.WaitForExit()

                        If myENG = "Yes" Then
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

                        If myENG = "Yes" Then
                            MsgBox("Done!")
                        Else
                            MsgBox("Fertig!")
                        End If

                        Me.BringToFront()

                    Case Windows.Forms.DialogResult.No
                        Exit Sub
                End Select
            End If

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub GovTools_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        Try

            Dim process3 As New Process()
            For Each process3 In System.Diagnostics.Process.GetProcessesByName("pp64.exe")
                process3.Kill()
            Next

            Dim process4 As New Process()
            For Each process4 In System.Diagnostics.Process.GetProcessesByName("mp64.exe")
                process4.Kill()
            Next

        Catch ex As Exception
            'nothing
        End Try

    End Sub

    Private Sub ClearTool_Click(sender As Object, e As EventArgs) Handles ClearTool.Click
        Call GovClear()
    End Sub

    Private Sub GovCrackerTool_Click(sender As Object, e As EventArgs) Handles GovCrackerTool.Click
        Call GovCrackerX()
    End Sub

    Private Sub HelpTool_Click(sender As Object, e As EventArgs) Handles HelpTool.Click
        '#### Help als pdf anzeigen
        If myENG = "Yes" Then
            Process.Start(System.IO.Path.Combine(Application.StartupPath, "Docs\GovCracker_User_Manual_ENG.pdf"))
        Else
            Process.Start(System.IO.Path.Combine(Application.StartupPath, "Docs\GovCracker_User_Manual_DE.pdf"))
        End If
    End Sub

    Private Sub Language_Click(sender As Object, e As EventArgs) Handles LanguageTool.Click
        If myENG = "No" Then

            myENG = "Yes"
            myGER = "No"

            WritePrivateProfileString("GovTools", "myGER", myGER, Application.StartupPath & "\Packages\INI\SettingsGT.ini")
            WritePrivateProfileString("GovTools", "myENG", myENG, Application.StartupPath & "\Packages\INI\SettingsGT.ini")

            MsgBox("The language will be changed.")

        Else

            myENG = "No"
            myGER = "Yes"

            WritePrivateProfileString("GovTools", "myGER", myGER, Application.StartupPath & "\Packages\INI\SettingsGT.ini")
            WritePrivateProfileString("GovTools", "myENG", myENG, Application.StartupPath & "\Packages\INI\SettingsGT.ini")

            MsgBox("Die Sprache wird geändert.")

        End If

        RefreshWin.Show()
    End Sub

    Private Sub AutoTool_Click(sender As Object, e As EventArgs) Handles AboutTool.Click
        About.Show()
    End Sub


    Private Sub WordlistTool_Click(sender As Object, e As EventArgs) Handles WordlistTool.Click
        System.Diagnostics.Process.Start("explorer", Application.StartupPath & "\#_Wordlists")
    End Sub


    Private Sub HashoutTool_Click(sender As Object, e As EventArgs) Handles HashoutTool.Click
        System.Diagnostics.Process.Start("explorer", Application.StartupPath & "\#_Hashout")
    End Sub

    Private Sub CrackoutTool_Click(sender As Object, e As EventArgs) Handles CrackoutTool.Click
        System.Diagnostics.Process.Start("explorer", Application.StartupPath & "\#_Crackout")
    End Sub

    Private Sub btnCeWL_Click(sender As Object, e As EventArgs) Handles btnCeWL.Click
        Call Farben()
        btnCeWL.BackColor = Color.Gray
        TabControl1.SelectedIndex = 5
    End Sub

    Private Sub btnCombiLen_Click(sender As Object, e As EventArgs) Handles btnCombiLen.Click
        Call Farben()
        btnCombiLen.BackColor = Color.Gray
        TabControl1.SelectedIndex = 6
    End Sub

    Private Sub btnTools_Click(sender As Object, e As EventArgs) Handles btnTools.Click
        Call Farben()
        btnTools.BackColor = Color.Gray
        TabControl1.SelectedIndex = 7
    End Sub

    Private Sub btnDupC_Click(sender As Object, e As EventArgs) Handles btnDupC.Click
        Call Farben()
        btnDupC.BackColor = Color.Gray
        TabControl1.SelectedIndex = 8
    End Sub

    Private Sub btnBulk_Click(sender As Object, e As EventArgs) Handles btnBulk.Click
        Call Farben()
        btnBulk.BackColor = Color.Gray
        TabControl1.SelectedIndex = 9
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnExtractor.Click
        Call Farben()
        btnExtractor.BackColor = Color.Gray
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btnMaskP.Click
        Call Farben()
        btnMaskP.BackColor = Color.Gray
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnCupp1.Click
        Call Farben()
        btnCupp1.BackColor = Color.Gray
        TabControl1.SelectedIndex = 3
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnPrince.Click
        Call Farben()
        btnPrince.BackColor = Color.Gray
        TabControl1.SelectedIndex = 2
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnWordl.Click
        Call Farben()
        btnWordl.BackColor = Color.Gray
        TabControl1.SelectedIndex = 4
    End Sub

    Private Sub Farben()
        btnExtractor.BackColor = Color.FromArgb(36, 49, 60)
        btnMaskP.BackColor = Color.FromArgb(36, 49, 60)
        btnCombiLen.BackColor = Color.FromArgb(36, 49, 60)
        btnTools.BackColor = Color.FromArgb(36, 49, 60)
        btnDupC.BackColor = Color.FromArgb(36, 49, 60)
        btnBulk.BackColor = Color.FromArgb(36, 49, 60)
        btnCupp1.BackColor = Color.FromArgb(36, 49, 60)
        btnCeWL.BackColor = Color.FromArgb(36, 49, 60)
        btnPrince.BackColor = Color.FromArgb(36, 49, 60)
        btnWordl.BackColor = Color.FromArgb(36, 49, 60)
        btnTools.BackColor = Color.FromArgb(36, 49, 60)
    End Sub

    Private Sub btnWordRules_Click(sender As Object, e As EventArgs) Handles btnWordRules.Click

        Try

            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.InitialDirectory = Application.StartupPath & "\#_Wordlists"
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            openFileDialog.FilterIndex = 1
            openFileDialog.RestoreDirectory = True

            If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                PathRules = openFileDialog.FileName
            Else
                Exit Sub
            End If

            If myENG = "Yes" Then
                MsgBox("The file is stored in the folder: ""\#_Rules"".")
            Else
                MsgBox("Die Datei wird in das Verzeichnis ""\#_Rules"" gespeichert.")
            End If

            Radar.Visible = True
            pbRadar.Visible = True

            BGWRule.RunWorkerAsync()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub cbBackRule_CheckedChanged(sender As Object, e As EventArgs) Handles cbBackRule.CheckedChanged
        If cbBackRule.Checked = True Then
            cbFrontRule.Checked = False
        End If

        If cbBackRule.Checked = False Then
            cbFrontRule.Checked = True
        End If
    End Sub

    Private Sub cbFrontRule_CheckedChanged(sender As Object, e As EventArgs) Handles cbFrontRule.CheckedChanged
        If cbFrontRule.Checked = True Then
            cbBackRule.Checked = False
        End If

        If cbFrontRule.Checked = False Then
            cbBackRule.Checked = True
        End If

    End Sub

    Private Sub BGWRule_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWRule.DoWork

        Dim DateRule As String = DateTime.Now.ToString("ddMMyy_HHmmss")
        Dim encoding As Encoding = Encoding.GetEncoding("ISO-8859-1")

        Try
            If cbBackRule.Checked = True Then

                System.IO.File.AppendAllText(Application.StartupPath & "\Rules\Rule_back_" & DateRule & ".rule", ":" & vbNewLine, encoding)

                For Each line In IO.File.ReadAllLines(PathRules, encoding)

                    Dim Rule As String = "$"
                    Dim finalString As String
                    For i As Long = 0 To line.Length - 1
                        Rule = Rule & line.Chars(i) & "$"
                        finalString = Rule.Substring(0, Rule.Length - 1) & vbNewLine
                    Next

                    '### Rule schreiben
                    System.IO.File.AppendAllText(Application.StartupPath & "\Rules\Rule_back_" & DateRule & ".rule", finalString, encoding)

                Next
            End If

            If cbFrontRule.Checked = True Then

                System.IO.File.AppendAllText(Application.StartupPath & "\Rules\Rule_front_" & DateRule & ".rule", ":" & vbNewLine, encoding)

                For Each line In IO.File.ReadAllLines(PathRules, Encoding)

                    Dim Rule As String = "^"
                    Dim finalString As String
                    Dim RevString As String

                    For i As Long = 0 To line.Length - 1
                        RevString = StrReverse(line)
                        Rule = Rule & "^" & RevString.Chars(i)

                        finalString = Rule.Substring(1) & vbNewLine
                    Next

                    '### Rule schreiben
                    System.IO.File.AppendAllText(Application.StartupPath & "\Rules\Rule_front_" & DateRule & ".rule", finalString, encoding)

                Next
            End If

        Catch ex As Exception
        End Try


    End Sub

    Private Sub BGWRule_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGWRule.RunWorkerCompleted

        Try

            Radar.Visible = False
            pbRadar.Visible = False

            If myENG = "Yes" Then
                MsgBox("Done!")
            Else
                MsgBox("Erledigt!")
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub pbLogo_Click(sender As Object, e As EventArgs) Handles pbLogo.Click
        Process.Start("https://www.govcracker.com")
    End Sub
End Class
