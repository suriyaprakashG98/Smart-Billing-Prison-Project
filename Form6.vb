Public Class Form6
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Insert.Close()
        Updateitems.Close()
        cancel.Close()
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Updateitems.Close()
        cancel.Close()
        exportpdf.Close()


        Insert.TopLevel = False
        Me.GroupBox2.Controls.Add(Insert)
        Insert.Show()

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Admin_login.TextBox1.Text


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Insert.Close()
        cancel.Close()
        exportpdf.Close()
        Updateitems.TopLevel = False
        Me.GroupBox2.Controls.Add(Updateitems)
        Updateitems.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Insert.Close()
        Updateitems.Close()
        exportpdf.Close()
        cancel.TopLevel = False
        Me.GroupBox2.Controls.Add(cancel)
        cancel.Show()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Admin_login.TextBox1.Clear()
        Admin_login.TextBox2.Clear()

        Me.Close()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Insert.Close()
        Updateitems.Close()
        cancel.Close()
        exportpdf.TopLevel = False
        Me.GroupBox2.Controls.Add(exportpdf)
        exportpdf.Show()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        Insert.Close()
        Updateitems.Close()
        cancel.Close()
        exportpdf.Close()


    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class