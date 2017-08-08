Public Class deadForm
    Private Sub quitter_Click(sender As Object, e As EventArgs) Handles quitter.Click
        Me.Hide()
        quit = True
    End Sub

    Private Sub tryAgain_Click(sender As Object, e As EventArgs) Handles tryAgain.Click
        Me.Hide()
    End Sub
End Class