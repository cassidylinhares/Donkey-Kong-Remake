Public Class winForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        quit = True
        Me.Hide()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub clock2_Click(sender As Object, e As EventArgs) Handles clock2.Click
        clock2.Text = finalTime
    End Sub
End Class