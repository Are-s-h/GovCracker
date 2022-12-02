<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NTLM
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NTLM))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnLive = New System.Windows.Forms.Button()
        Me.bntSecretSTART = New System.Windows.Forms.Button()
        Me.rtbSecretGER = New System.Windows.Forms.RichTextBox()
        Me.btnSystem = New System.Windows.Forms.Button()
        Me.tbSYSTEM = New System.Windows.Forms.TextBox()
        Me.rtbSecretENG = New System.Windows.Forms.RichTextBox()
        Me.gbOnline = New System.Windows.Forms.GroupBox()
        Me.gbOffline = New System.Windows.Forms.GroupBox()
        Me.gbOnline.SuspendLayout()
        Me.gbOffline.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnLive
        '
        Me.btnLive.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnLive.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLive.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnLive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLive.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnLive.Location = New System.Drawing.Point(16, 28)
        Me.btnLive.Name = "btnLive"
        Me.btnLive.Size = New System.Drawing.Size(111, 23)
        Me.btnLive.TabIndex = 0
        Me.btnLive.Text = "Live-Mode"
        Me.btnLive.UseVisualStyleBackColor = False
        '
        'bntSecretSTART
        '
        Me.bntSecretSTART.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.bntSecretSTART.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bntSecretSTART.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.bntSecretSTART.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bntSecretSTART.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.bntSecretSTART.Location = New System.Drawing.Point(409, 28)
        Me.bntSecretSTART.Name = "bntSecretSTART"
        Me.bntSecretSTART.Size = New System.Drawing.Size(73, 23)
        Me.bntSecretSTART.TabIndex = 13
        Me.bntSecretSTART.Text = "START"
        Me.bntSecretSTART.UseVisualStyleBackColor = False
        '
        'rtbSecretGER
        '
        Me.rtbSecretGER.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.rtbSecretGER.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbSecretGER.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbSecretGER.Location = New System.Drawing.Point(12, 21)
        Me.rtbSecretGER.Name = "rtbSecretGER"
        Me.rtbSecretGER.Size = New System.Drawing.Size(642, 321)
        Me.rtbSecretGER.TabIndex = 5
        Me.rtbSecretGER.Text = resources.GetString("rtbSecretGER.Text")
        '
        'btnSystem
        '
        Me.btnSystem.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnSystem.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSystem.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSystem.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSystem.Location = New System.Drawing.Point(16, 28)
        Me.btnSystem.Name = "btnSystem"
        Me.btnSystem.Size = New System.Drawing.Size(103, 23)
        Me.btnSystem.TabIndex = 10
        Me.btnSystem.Text = "Folder-Path"
        Me.btnSystem.UseVisualStyleBackColor = False
        '
        'tbSYSTEM
        '
        Me.tbSYSTEM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSYSTEM.Location = New System.Drawing.Point(125, 28)
        Me.tbSYSTEM.Multiline = True
        Me.tbSYSTEM.Name = "tbSYSTEM"
        Me.tbSYSTEM.Size = New System.Drawing.Size(278, 23)
        Me.tbSYSTEM.TabIndex = 7
        '
        'rtbSecretENG
        '
        Me.rtbSecretENG.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.rtbSecretENG.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbSecretENG.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbSecretENG.Location = New System.Drawing.Point(12, 21)
        Me.rtbSecretENG.Name = "rtbSecretENG"
        Me.rtbSecretENG.Size = New System.Drawing.Size(645, 293)
        Me.rtbSecretENG.TabIndex = 6
        Me.rtbSecretENG.Text = resources.GetString("rtbSecretENG.Text")
        '
        'gbOnline
        '
        Me.gbOnline.Controls.Add(Me.btnLive)
        Me.gbOnline.Location = New System.Drawing.Point(12, 348)
        Me.gbOnline.Name = "gbOnline"
        Me.gbOnline.Size = New System.Drawing.Size(142, 69)
        Me.gbOnline.TabIndex = 0
        Me.gbOnline.TabStop = False
        Me.gbOnline.Text = "Live-Mode"
        '
        'gbOffline
        '
        Me.gbOffline.Controls.Add(Me.btnSystem)
        Me.gbOffline.Controls.Add(Me.tbSYSTEM)
        Me.gbOffline.Controls.Add(Me.bntSecretSTART)
        Me.gbOffline.Location = New System.Drawing.Point(160, 348)
        Me.gbOffline.Name = "gbOffline"
        Me.gbOffline.Size = New System.Drawing.Size(497, 69)
        Me.gbOffline.TabIndex = 16
        Me.gbOffline.TabStop = False
        Me.gbOffline.Text = "Offline-Mode"
        '
        'NTLM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(666, 431)
        Me.Controls.Add(Me.gbOffline)
        Me.Controls.Add(Me.gbOnline)
        Me.Controls.Add(Me.rtbSecretGER)
        Me.Controls.Add(Me.rtbSecretENG)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "NTLM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Secretsdump"
        Me.gbOnline.ResumeLayout(False)
        Me.gbOffline.ResumeLayout(False)
        Me.gbOffline.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents bntSecretSTART As Button
    Friend WithEvents btnSystem As Button
    Friend WithEvents tbSYSTEM As TextBox
    Friend WithEvents rtbSecretGER As RichTextBox
    Friend WithEvents rtbSecretENG As RichTextBox
    Friend WithEvents btnLive As Button
    Friend WithEvents gbOnline As GroupBox
    Friend WithEvents gbOffline As GroupBox
End Class
