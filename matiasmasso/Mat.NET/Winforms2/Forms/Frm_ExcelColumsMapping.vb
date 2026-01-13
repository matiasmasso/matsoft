Public Class Frm_ExcelColumsMapping
    Private _Sheet As MatHelper.Excel.Sheet
    Private _Fields As List(Of String)
    Private _Allowevents As Boolean
    Public Property Cancel As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)



    Public Sub New(sFields As String(), sFilename As String)
        MyBase.New
        InitializeComponent()

        Dim exs As New List(Of Exception)
        Dim oBook As MatHelper.Excel.Book = MatHelper.Excel.ClosedXml.Read(exs, sFilename)
        If exs.Count = 0 Then
            _Sheet = oBook.Sheets.First
            _Sheet.ColumnHeadersOnFirstRow = CheckBoxFirstRowHeaders.Checked
            _Fields = sFields.ToList
            Xl_ExcelColumnsMapping1.Load(_Fields, _Sheet)
            Xl_ExcelSheet1.Load(Xl_ExcelColumnsMapping1.Sheet, _Fields)
            ' Xl_ExcelSheet1.Load(_Sheet, _Fields)
        Else
            UIHelper.WarnError(exs)
            Me.Cancel = True
        End If
        _Allowevents = True
    End Sub

    Private Sub Xl_ExcelColumnsMapping1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ExcelColumnsMapping1.AfterUpdate
        'If _Allowevents Then
        Dim oSheet As MatHelper.Excel.Sheet = e.Argument
        Xl_ExcelSheet1.Load(oSheet, _Fields)
        'End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oSheet = Xl_ExcelColumnsMapping1.Sheet
        oSheet.columnHeadersOnFirstRow = CheckBoxFirstRowHeaders.Checked

        RaiseEvent AfterUpdate(Me, New MatEventArgs(oSheet))
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class