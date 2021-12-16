Public Class Ex3
    Dim num As Double
    Dim cont As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        num = Int(Rnd() * 10) + 1
        Label1.Text = num
        cont = cont + 1
        If num = 7 Then
            Label2.Text = "Acertaste!"
            Label3.Text = cont
            cont = 0
        Else
            Label2.Text = "Erraste...Tenta outra vez"
        End If
    End Sub
End Class