Public Class RefreshWin
    Private Sub Refresh_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Visible = False
        GovTools.Close()
        GovTools.Show()
        Me.Close()

    End Sub
End Class