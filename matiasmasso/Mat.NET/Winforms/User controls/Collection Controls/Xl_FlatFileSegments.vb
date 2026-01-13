Public Class Xl_FlatFileSegments
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As FlatFile

    Private _AllowEvents As Boolean


    Public Shadows Sub Load(oFlatFile As MatHelperStd.FlatFile)
        _Value = oFlatFile

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        MyBase.Rows.Clear()
        MyBase.Columns.Clear()
        For i As Integer = 1 To _Value.Segments.First.Fields.Count
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(i - 1)
                .HeaderText = String.Format("F{0:00}", i)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        Next

        For Each oSegment In _Value.Segments
            Dim fieldValues = oSegment.Fields.Select(Function(x) x.Value).ToList()
            MyBase.Rows.Add(fieldValues.ToArray)
        Next

        _AllowEvents = True
    End Sub


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


    End Sub



End Class


