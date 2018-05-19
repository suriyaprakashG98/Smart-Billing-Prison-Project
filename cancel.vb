Imports System.Data.SqlClient
Imports System.IO

Public Class cancel
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim comm As New SqlCommand
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        If TextBox3.Text <> "" Then
            con.Open()
            comm = New SqlCommand("delete from items_billed where billno =" & TextBox3.Text & " ", con)
            If comm.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Done !!!")
            Else
                MessageBox.Show("Done !!!")
            End If
        Else
            MessageBox.Show("enter the billno")
        End If
        TextBox3.Clear()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class