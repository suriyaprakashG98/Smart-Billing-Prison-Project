Imports iTextSharp
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Data.Odbc
Imports System.IO
Imports System.Data.SqlClient

Public Class exportpdf
    Dim con As New SqlConnection("Data Source=SURYAMADHAN;Initial Catalog=billing;Integrated Security=True")
    Dim com As New SqlCommand
    Dim ds As New DataTable("table")
    Dim dataTable As New DataTable("MyDataTable")
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName = "" Then
            MsgBox("Enter Filename to create PDF")
            Exit Sub
        Else
            ExportDataToPDFTable()
            MsgBox("PDF Created Successfully")

        End If
    End Sub
    'GetDataTable() function Is used to read Data's from datgridview and creates a DataTable.
    Private Function GetDataTable() As DataTable
        'Create another DataColumn Name
        Dim dataColumn_1 As New DataColumn(DataGridView1.Columns(0).HeaderText.ToString(), GetType(String))
        dataTable.Columns.Add(dataColumn_1)
        Dim dataColumn_2 As New DataColumn(DataGridView1.Columns(1).HeaderText.ToString(), GetType(String))
        dataTable.Columns.Add(dataColumn_2)
        Dim dataColumn_3 As New DataColumn(DataGridView1.Columns(2).HeaderText.ToString(), GetType(String))
        dataTable.Columns.Add(dataColumn_3)
        Dim dataColumn_4 As New DataColumn(DataGridView1.Columns(3).HeaderText.ToString(), GetType(String))
        dataTable.Columns.Add(dataColumn_4)
        Dim dataColumn_5 As New DataColumn(DataGridView1.Columns(4).HeaderText.ToString(), GetType(String))
        dataTable.Columns.Add(dataColumn_5)
        Dim dataColumn_6 As New DataColumn(DataGridView1.Columns(5).HeaderText.ToString(), GetType(String))
        dataTable.Columns.Add(dataColumn_6)
        Dim dataColumn_7 As New DataColumn(DataGridView1.Columns(6).HeaderText.ToString(), GetType(String))
        dataTable.Columns.Add(dataColumn_7)
        'Now Add some row to newly created dataTable
        ' MsgBox(dataColumn_1.ToString)
        Dim dataRow As DataRow
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            dataRow = dataTable.NewRow()
            ' MsgBox(DataGridView1.Rows(1).Cells(0).Value.ToString())
            ' Important you have create New row
            dataRow(DataGridView1.Columns(0).HeaderText.ToString()) = DataGridView1.Rows(i).Cells(0).Value
            dataRow(DataGridView1.Columns(1).HeaderText.ToString()) = DataGridView1.Rows(i).Cells(1).Value
            dataRow(DataGridView1.Columns(2).HeaderText.ToString()) = DataGridView1.Rows(i).Cells(2).Value
            dataRow(DataGridView1.Columns(3).HeaderText.ToString()) = DataGridView1.Rows(i).Cells(3).Value
            dataRow(DataGridView1.Columns(4).HeaderText.ToString()) = DataGridView1.Rows(i).Cells(4).Value
            dataRow(DataGridView1.Columns(5).HeaderText.ToString()) = DataGridView1.Rows(i).Cells(5).Value
            dataRow(DataGridView1.Columns(6).HeaderText.ToString()) = DataGridView1.Rows(i).Cells(6).Value
            dataTable.Rows.Add(dataRow)
        Next
        dataTable.AcceptChanges()
        Return dataTable
    End Function

    Private Sub ExportDataToPDFTable()
        Dim paragraph As New Paragraph
        Dim doc As New Document(iTextSharp.text.PageSize.A4, 40, 40, 40, 10)
        SaveFileDialog1.InitialDirectory = "E:\BillingSoftware\Dept"
        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(SaveFileDialog1.FileName + ".pdf", FileMode.Create))
        doc.Open()
        Dim font12BoldRed As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12.0F, iTextSharp.text.Font.UNDERLINE Or iTextSharp.text.Font.BOLDITALIC, BaseColor.RED)
        Dim font12Bold As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12.0F, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
        Dim font12Normal As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12.0F, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

        Dim p1 As New Phrase
        p1 = New Phrase(New Chunk("BILLED ITEMS", font12BoldRed))
        doc.Add(p1)

        'Create instance of the pdf table and set the number of column in that table
        Dim PdfTable As New PdfPTable(7)
        PdfTable.TotalWidth = 600.0F
        'fix the absolute width of the table
        PdfTable.LockedWidth = True
        'relative col widths in proportions - 1,4,1,1 and 1
        Dim widths As Single() = New Single() {1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 2.0F, 1.0F}
        PdfTable.SetWidths(widths)
        PdfTable.HorizontalAlignment = 1 ' 0 --> Left, 1 --> Center, 2 --> Right
        PdfTable.SpacingBefore = 2.0F

        'pdfCell Decleration
        Dim PdfPCell As PdfPCell = Nothing

        PdfPCell = New PdfPCell(New Phrase(New Chunk("Billno", font12Bold)))
        PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        PdfTable.AddCell(PdfPCell)
        'Assigning values to each cell as phrases
        PdfPCell = New PdfPCell(New Phrase(New Chunk("Sno", font12Bold)))
        'Alignment of phrase in the pdfcell
        PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        'Add pdfcell in pdftable
        PdfTable.AddCell(PdfPCell)
        PdfPCell = New PdfPCell(New Phrase(New Chunk("Itemname", font12Bold)))
        PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        PdfTable.AddCell(PdfPCell)
        PdfPCell = New PdfPCell(New Phrase(New Chunk("Quantity", font12Bold)))
        PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        PdfTable.AddCell(PdfPCell)
        PdfPCell = New PdfPCell(New Phrase(New Chunk("Itemprice", font12Bold)))
        PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        PdfTable.AddCell(PdfPCell)
        PdfPCell = New PdfPCell(New Phrase(New Chunk("Totalprice", font12Bold)))
        PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        PdfTable.AddCell(PdfPCell)
        PdfPCell = New PdfPCell(New Phrase(New Chunk("Datetime", font12Bold)))
        PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        PdfTable.AddCell(PdfPCell)


        Dim dt As DataTable = GetDataTable()
        If dt IsNot Nothing Then
            'Now add the data from datatable to pdf table
            For rows As Integer = 0 To dt.Rows.Count - 1
                For column As Integer = 0 To dt.Columns.Count - 1
                    PdfPCell = New PdfPCell(New Phrase(dt.Rows(rows)(column).ToString(), font12Normal))

                    PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    ' If column = 0 Or column = 6 Then
                    'PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    'Else
                    'PdfPCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'End If
                    PdfTable.AddCell(PdfPCell)
                Next
            Next
            'Adding pdftable to the pdfdocument
            doc.Add(PdfTable)
        End If
        doc.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        com = New SqlCommand("select * from items_billed", con)
        Dim adp As New SqlDataAdapter(com)
        adp.Fill(ds)
        DataGridView1.DataSource = ds
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
