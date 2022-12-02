Public Class MetaMask

    Private Sub MetaMask_Load(sender As Object, e As EventArgs) Handles Me.Load

        If GovTools.myENG = "Yes" Then
            tcMetaENG.Visible = True
            tcMetaGER.Visible = False
        Else
            tcMetaENG.Visible = False
            tcMetaGER.Visible = True
        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://metamask.zendesk.com/hc/en-us/articles/360018766351-How-to-recover-your-Secret-Recovery-Phrase")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://metamask.zendesk.com/hc/en-us/articles/360018766351-How-to-recover-your-Secret-Recovery-Phrase")
    End Sub

    Private Sub btnMeta1ger_Click(sender As Object, e As EventArgs) Handles btnMeta1ger.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta1ger.Text)
    End Sub

    Private Sub btnMeta2ger_Click(sender As Object, e As EventArgs) Handles btnMeta2ger.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta2ger.Text)
    End Sub

    Private Sub btnMeta3ger_Click(sender As Object, e As EventArgs) Handles btnMeta3ger.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta3ger.Text)
    End Sub

    Private Sub btnMeta4ger_Click(sender As Object, e As EventArgs) Handles btnMeta4ger.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta4ger.Text)
    End Sub

    Private Sub btnMeta1_Click(sender As Object, e As EventArgs) Handles btnMeta1.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta1.Text)
    End Sub

    Private Sub btnMeta2_Click(sender As Object, e As EventArgs) Handles btnMeta2.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta2.Text)
    End Sub

    Private Sub btnMeta3_Click(sender As Object, e As EventArgs) Handles btnMeta3.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta3.Text)
    End Sub

    Private Sub btnMeta4_Click(sender As Object, e As EventArgs) Handles btnMeta4.Click
        '### CMD in Zwischenablage
        Clipboard.Clear()
        Clipboard.SetText(txbMeta4.Text)
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://metamask.github.io/vault-decryptor/")
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Process.Start("https://metamask.github.io/vault-decryptor/")
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Process.Start("https://metamask.github.io/vault-decryptor/")
    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        Process.Start("https://metamask.github.io/vault-decryptor/")
    End Sub
End Class