Imports System.Data.SqlClient
Imports System.IO
Public Class Updateitems
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim comm As New SqlCommand
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        con.Open()
        If ComboBox1.Text <> "" And TextBox6.Text <> "" Then
            comm = New SqlCommand("update items set price = " & TextBox6.Text & " where name = '" & ComboBox1.Text & "'", con)
            comm.ExecuteNonQuery()
            MsgBox("Done ")
        Else
            MsgBox("Enter the details properly")
        End If
        ComboBox1.ResetText()
        TextBox6.Clear()

    End Sub

    Private Sub Updateitems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        Dim com As SqlCommand
        Dim reader As SqlDataReader

        Try
            con.Open()
            com = New SqlCommand("select name from items", con)
            reader = com.ExecuteReader
            Dim nme As String = ""
            While reader.Read
                ComboBox1.Items.Add(reader.Item("name"))
            End While
        Catch ex As Exception
            MsgBox(ex)
        End Try
        con.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub retrieve(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim comm As New SqlCommand
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        Dim var As String
        Dim nme As String = ""
        Dim reader As SqlDataReader

        If sender.GetType.ToString.Contains("ComboBox") Then
            var = ComboBox1.Text
        Else
            var = sender.name
        End If
        con.Open()
        comm = New SqlCommand("select * From items where name='" & var & "'", con)
        reader = comm.ExecuteReader
        While reader.Read
            TextBox6.Text = reader.Item("price")
            ComboBox1.ResetText()
            nme = reader.Item("name")
        End While
        ComboBox1.Text = nme
        'If DataGridView1.RowCount > 0 Then
        'For i As Integer = 0 To DataGridView1.RowCount - 1
        'total = total + DataGridView1.Rows(i).Cells(3).Value
        'Next
        'End If
        con.Close()
    End Sub
End Class