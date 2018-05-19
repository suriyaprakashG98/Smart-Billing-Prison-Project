Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable
Imports System.Data
Imports System.IO.Directory
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Public Class Form1
    Dim a As Integer
    Dim total As Integer = 0
    Dim ds As New DataSet()
    Dim con As SqlConnection
    Dim com As SqlCommand
    Dim grid As New DataTable()
    Dim table1 As New DataTable("table")
    Dim adp As SqlDataAdapter
    Dim reader As SqlDataReader
    Dim s1 As String
    Dim s2 As Integer
    Dim flow As New FlowLayoutPanel
    Dim sn As Integer = 1
    Private Sub FrmLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'BillingSoftwareDataSet.bill' table. You can move, or remove it, as needed.
        Dim com As New SqlCommand
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        con.Open()
        com = New SqlCommand("select max(billno) from items_billed", con)
        com.ExecuteNonQuery()
        Dim adp As New SqlDataAdapter(com)
        adp.Fill(table1)
        TextBox10.ReadOnly = True
        TextBox10.Text = table1.Rows(0)(0).ToString + 1
        con.Close()
        PrintDocument1.PrinterSettings.PrinterName = "POS-58-Series"
        GroupBox3.Visible = False
        PrintPreviewControl1.Document = PrintDocument1
        TextBox10.ReadOnly = True
        TextBox9.ReadOnly = True
        If User_login.TextBox10.Text <> "" Then
            TextBox9.Text = User_login.TextBox10.Text
            User_login.TextBox10.Clear()
        ElseIf Admin_login.TextBox1.Text <> "" Then
            If Admin_login.TextBox1.Text = "admin" Or Admin_login.TextBox1.Text = "ADMIN" Then
                TextBox9.Text = Admin_login.TextBox1.Text
                Admin_login.TextBox1.Clear()
                Admin_login.TextBox2.Clear()
            End If
        End If
        TextBox11.ReadOnly = True
        TextBox11.Tag = DefaultFont
        'TextBox10.Text = CStr(a)


        Dim comm As New SqlCommand
        ' Dim con As New SqlConnection("Data Source=KAVIN\SQLEXPRESS;Initial Catalog=BillingSoftware;Integrated Security=True")
        Try
            con.Open()
            comm = New SqlCommand("select * From items", con)
            reader = comm.ExecuteReader
            FlowLayoutPanel1.Controls.Clear()

            Dim str As String = ""

            While reader.Read
                Dim pan As New Panel
                Dim pic As New PictureBox
            Dim lab As New Label
            pan.Size = New Size(180, 145)
                pic.Size = New Size(180, 145)
                lab.Size = New Size(180, 25)
                pic.SizeMode = ImageLayout.Stretch
                pic.BorderStyle = BorderStyle.FixedSingle
            lab.BorderStyle = BorderStyle.FixedSingle
            lab.TextAlign = ContentAlignment.MiddleCenter
                lab.Text = reader.Item("name")
                pic.Name = reader.Item("name")
                lab.Name = reader.Item("name")
                pan.Name = reader.Item("name")
                pan.Controls.Add(pic)
                pan.Controls.Add(lab)
                lab.Dock = DockStyle.Bottom
                lab.BringToFront()
                ToolTip1.SetToolTip(pic, reader.Item("name"))
                ComboBox1.Items.Add(reader.Item("name"))
                Dim im() As Byte
            im = reader.Item("img")
            Dim m As New MemoryStream(im)
                pic.Image = Image.FromStream(m)
                pan.Tag = pic.Image
                AddHandler pic.Click, AddressOf Retrieve
                AddHandler lab.Click, AddressOf Retrieve
                FlowLayoutPanel1.Controls.Add(pan)
            End While
            con.Close()
        Catch ex As SqlException
            MessageBox.Show("ex.message")
        Finally
            con.Dispose()
        End Try

    End Sub

    Public Sub Retrieve(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim comm As New SqlCommand
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        Dim var As String
        Dim nme As String = ""

        If sender.GetType.ToString.Contains("ComboBox") Then
            var = ComboBox1.Text
        Else
            var = sender.name

        End If
        con.Open()
        comm = New SqlCommand("select * From items where name='" & var & "'", con)
        Dim adp As New SqlDataAdapter(comm)
        adp.Fill(grid)
        reader = comm.ExecuteReader
        While reader.Read
            TextBox7.Text = reader.Item("price")
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
    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub PrintPageHandler(ByVal sender As Object, ByVal args As Printing.PrintPageEventArgs)

        Dim lvwItem As ListViewItem
        Dim lvwSubItem As ListViewItem.ListViewSubItem
        Dim xPos As Integer = 0
        Dim yPos As Integer = 0

        ' Counter for display purposes
        Dim listviewcount As Integer = 1

        ' Loop through our listview items
        For Each lvwItem In ListView1.Items
            xPos = 0

            ' Print the count
            ' Debug.Print(listviewcount)

            ' Print the subitems of this particular ListViewItem
            For Each lvwSubItem In lvwItem.SubItems
                xPos += 100
                yPos = 100 + (listviewcount * 15)
                args.Graphics.DrawString(lvwSubItem.Text(),
                    New Font("Arial", 10, FontStyle.Bold), Brushes.Black, xPos, yPos)
            Next

            ' Increment the count (for display purposes)
            listviewcount += 1
        Next

    End Sub

    Public Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '        FlowLayoutPanel1.BringToFront()
        If ComboBox1.Text <> "" And TextBox8.Text <> "" And TextBox7.Text <> "" Then
            Dim itm As New ListViewItem(sn)
            itm.SubItems.Add(ComboBox1.Text)
            itm.SubItems.Add(TextBox8.Text)
            itm.SubItems.Add(TextBox7.Text)
            If TextBox8.Text >= 0 Then
                total = total + (TextBox7.Text * TextBox8.Text)
                itm.SubItems.Add(TextBox7.Text * TextBox8.Text)
                ListView1.Items.Add(itm)
                TextBox11.Text = total
                sn += 1
            Else
                MessageBox.Show("Invalid quantity")
            End If
        Else
            MsgBox("Enter all values.")
        End If
        ComboBox1.ResetText()
        TextBox8.Clear()
        TextBox7.Clear()
    End Sub
    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            Button7.Select()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim btn As New Button
        btn.BackColor = Color.Black
        btn.Size = New Size(100, 25)
        FlowLayoutPanel1.Controls.Add(btn)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextBox6.Text = DateAndTime.Now
        '   BringToFront()

    End Sub
    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ListView1.Items.Clear()
        total = 0
        sn = 1
        TextBox11.Clear()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        exportpdf.Show()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' Login.Show()
        Me.Close()
    End Sub
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim receipt As Font = New Drawing.Font("Times new roman", 10)
        Dim receipt1 As Font = New Drawing.Font("Times new roman", 11, FontStyle.Bold)
        Dim receipt2 As Font = New Drawing.Font("Times new roman", 13, FontStyle.Bold)
        Dim receipt3 As Font = New Drawing.Font("Times new roman", 10, FontStyle.Bold)
        Dim listviewcount As Integer = 1
        e.Graphics.DrawString("Tamilnadu Prison Department", receipt3, Brushes.Black, 5, 10)
        e.Graphics.DrawString("BillNo : " & TextBox10.Text & "", receipt, Brushes.Black, 5, 40)
        e.Graphics.DrawString("Date : " & TextBox6.Text & "", receipt, Brushes.Black, 5, 60)
        e.Graphics.DrawString("Cashier : " & TextBox9.Text & "", receipt, Brushes.Black, 5, 80)
        e.Graphics.DrawString("---------------------------------------", receipt, Brushes.Black, 5, 90)
        e.Graphics.DrawString("Sno", receipt, Brushes.Black, 5, 100)
        e.Graphics.DrawString("Items", receipt, Brushes.Black, 29, 100)
        e.Graphics.DrawString("Qty", receipt, Brushes.Black, 112, 100)
        e.Graphics.DrawString("Price", receipt, Brushes.Black, 140, 100)
        e.Graphics.DrawString("---------------------------------------", receipt, Brushes.Black, 5, 110)
        Dim i = 120
        For n = 0 To ListView1.Items.Count - 1
            e.Graphics.DrawString(ListView1.Items(n).SubItems(0).Text, receipt, Brushes.Black, 7, i)
            e.Graphics.DrawString(ListView1.Items(n).SubItems(1).Text, receipt, Brushes.Black, 29, i)
            e.Graphics.DrawString(ListView1.Items(n).SubItems(2).Text, receipt, Brushes.Black, 114, i)
            e.Graphics.DrawString(ListView1.Items(n).SubItems(4).Text, receipt, Brushes.Black, 140, i)
            i += 20
        Next
        i -= 10
        e.Graphics.DrawString("---------------------------------------", receipt, Brushes.Black, 5, i)
        e.Graphics.DrawString("Total : Rs." & TextBox11.Text & "", receipt2, Brushes.Black, 5, i + 20)
        e.Graphics.DrawString("---------------------------------------", receipt, Brushes.Black, 5, i + 40)
        e.Graphics.DrawString("Thank You!!!!!!", receipt1, Brushes.Black, 40, i + 60)

    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        ' PrintDocument1.Print()
        PrintPreviewControl1.Document = Nothing
        Dim comm As New SqlCommand
        Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
        con.Open()
        For x As Integer = 0 To ListView1.Items.Count - 1
            ListView1.Items(x).Selected = True
            For Each item As ListViewItem In ListView1.SelectedItems
                Dim str As String = "insert into items_billed(billno,sno,name,quantity,price,itemprice,datetime) values(" & TextBox10.Text & "," & item.Text & ",'" & item.SubItems(1).Text & "'," & item.SubItems(2).Text & "," & item.SubItems(3).Text & "," & item.SubItems(4).Text & ",'" & TextBox6.Text & "')"
                com = New SqlCommand(str, con)
                com.ExecuteNonQuery()

            Next
        Next
        Dim printControl = New Printing.StandardPrintController
        PrintDocument1.PrintController = printControl
        Try
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()
        total = 0
        a = TextBox10.Text + 1
        TextBox10.Text = CStr(a)
        TextBox11.Clear()
        GroupBox3.Hide()
        ListView1.Items.Clear()
        sn = 1
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        GroupBox3.Visible = False
        GroupBox3.Refresh()
        PrintPreviewControl1.Refresh()
        TextBox11.Clear()
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim j As ListViewItem
        If Not ListView1.SelectedItems.Count = 0 Then
            j = ListView1.SelectedItems(0)
            total = total - ListView1.Items(ListView1.FocusedItem.Index).SubItems(4).Text
            TextBox11.Text = total
            j.Remove()
            ListView_refresh()
            sn -= 1
        Else
            MsgBox("No items selected")
        End If
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        'MsgBox("hi")
        'flow.Dispose()

        GroupBox3.Visible = True
        GroupBox3.BringToFront()
        PrintPreviewControl1.Document = PrintDocument1
        'PrintDocument1 .PrinterSettings .PrinterName =" "
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'prisonerprofile.Show()
        Dim box = New prisonerprofile
        box.Show()


    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Function ListView_refresh()
        Dim i = 0
        For Each itm In ListView1.Items
            i += 1
            itm.text = i
        Next
    End Function

    Private Sub PrintPreviewControl1_Click(sender As Object, e As EventArgs) Handles PrintPreviewControl1.Click

    End Sub

    Private Sub ComboBox1_DoubleClick(sender As Object, e As EventArgs) Handles ComboBox1.DoubleClick

    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        GroupBox2.Controls.Add(flow)
        flow.Bounds = FlowLayoutPanel1.Bounds
        flow.Controls.Clear()
        flow.BringToFront()
        flow.AutoScroll = True
        Dim ctrl As New Control
        For Each cntrl As Control In FlowLayoutPanel1.Controls
            If cntrl.Name.ToString.Contains(ComboBox1.Text) Then
                Dim pan As New Panel
                Dim pic As New PictureBox
                Dim lab As New Label
                pan.Size = New Size(180, 145)
                pic.Size = New Size(180, 145)
                lab.Size = New Size(180, 25)
                pic.SizeMode = ImageLayout.Stretch
                pic.BorderStyle = BorderStyle.FixedSingle
                lab.BorderStyle = BorderStyle.FixedSingle
                lab.TextAlign = ContentAlignment.MiddleCenter
                lab.Text = cntrl.Name
                pic.Name = cntrl.Name
                lab.Name = cntrl.Name
                pan.Tag = cntrl.Name
                pan.Controls.Add(pic)
                pan.Controls.Add(lab)
                lab.Dock = DockStyle.Bottom
                lab.BringToFront()
                ToolTip1.SetToolTip(pic, cntrl.Name)
                pic.Image = cntrl.Tag
                AddHandler pic.Click, AddressOf Retrieve
                AddHandler lab.Click, AddressOf Retrieve
                flow.Controls.Add(pan)
            End If
        Next
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        For Each txt In sender.text
            If Not IsNumeric(txt) Then
                TextBox8.Text = TextBox8.Text.Remove(TextBox8.Text.IndexOf(txt), 1)
            End If
        Next

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class
