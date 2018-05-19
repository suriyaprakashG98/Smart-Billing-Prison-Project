Imports System.Data.SqlClient
Imports System.IO
Public Class Insert
    Private Sub Browse(sender As Object, e As EventArgs) Handles Button6.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif"
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(opf.FileName)
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim comm As New SqlCommand
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        con.Open()
        comm = New SqlCommand("insert into items(name,price,img) values (@name,@price,@img)", con)
        Dim ms As New MemoryStream
        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
        comm.Parameters.Add("@name", SqlDbType.VarChar).Value = TextBox3.Text
        comm.Parameters.Add("@price", SqlDbType.VarChar).Value = TextBox4.Text
        comm.Parameters.Add("@img", SqlDbType.Image).Value = ms.ToArray()
        If comm.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Done !!!")
        Else
            MessageBox.Show("Not Done !!!")
        End If
        TextBox3.Clear()
        TextBox4.Clear()
        PictureBox1.Image = Nothing



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub Insert_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub
End Class