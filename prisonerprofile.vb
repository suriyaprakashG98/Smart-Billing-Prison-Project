Imports System.IO
Imports System.Data.SqlClient
Imports System.Data

Public Class prisonerprofile
    Dim id As String
    Dim rfid As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        TextBox5.Clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        GroupBox1.Refresh()
    End Sub

    Private Sub prisonerprofile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SerialPort1.PortName = "COM7"
        SerialPort1.BaudRate = 9600
        SerialPort1.DataBits = 8
        SerialPort1.Encoding = System.Text.Encoding.Default
        TextBox5.Select()


    End Sub
    Private Sub prisonerprofile_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            SerialPort1.Open()
            rfid = SerialPort1.ReadLine()
            rfid.Replace(":", "")
            SerialPort1.Close()
            TextBox5.Text = rfid
            TextBox4.Text = Form1.TextBox11.Text
            Try
                Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
                Dim reader As SqlDataReader
                con.Open()
                'Dim name1 As String = rfid
                Dim cmd As New SqlCommand("select * from prisoner_detail where id='" & TextBox5.Text.Trim & "'", con)
                reader = cmd.ExecuteReader
                While reader.Read
                    TextBox1.Text = reader.Item("number")
                    TextBox2.Text = reader.Item("name")
                    TextBox3.Text = reader.Item("bal")
                End While
                con.Close()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        Dim remaining As Integer
        con.Open()
        If CInt(TextBox3.Text) >= CInt(TextBox4.Text) Then
            remaining = TextBox3.Text - TextBox4.Text
            Dim cmd As New SqlCommand("update prisoner_detail set bal=" & remaining & " where id='" & TextBox5.Text.Trim & "'", con)
            cmd.ExecuteNonQuery()
            Dim cmd1 As New SqlCommand("select bal from prisoner_detail where id='" & TextBox5.Text.Trim & "'", con)
            Dim reader As SqlDataReader
            reader = cmd1.ExecuteReader
            Dim balance As String = " "
            While reader.Read
                balance = reader.Item("bal")
            End While
            MsgBox("Done!!!" & vbCrLf & "Remaining Balance = Rs." & balance & "")
        Else
            MsgBox("No enough money" & vbCrLf & "Balance = Rs." & TextBox3.Text & "")
        End If
        con.Close()
        TextBox5.Clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        Form1.ListView1.Items.Clear()
        Form1.TextBox11.Clear()
        'con.Dispose()
        Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class