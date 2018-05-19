Public Class User_login

    Private Sub User_login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox10.Select()
    End Sub

    Private Sub Login(sender As Object, e As EventArgs) Handles Button9.Click
        If TextBox10.Text.Trim = "" And TextBox9.Text.Trim = "" Then
            MsgBox("Enter The Details")
        Else
            Form1.Show()
            Me.Close()

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged

    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            Button9.Select()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class