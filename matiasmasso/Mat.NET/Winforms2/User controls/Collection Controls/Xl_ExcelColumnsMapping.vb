Public Class Xl_ExcelColumnsMapping
    Inherits DataGridView
    Private _Sheet As MatHelper.Excel.Sheet
    Private _Fields As List(Of String)
    Private _results As List(Of String)


    Private _allowEvents As Boolean

    Private Enum Cols
        Header
        Result
    End Enum

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Function Load(sFields As List(Of String), oSheet As MatHelper.Excel.Sheet) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Try
            _Sheet = oSheet
            _Fields = sFields
            _results = Results()
            SetProperties()

            Dim oCombo As DataGridViewComboBoxColumn = MyBase.Columns(Cols.Result)

            For i = 0 To _Fields.Count - 1
                Dim row() As String = {
                _Fields(i),
               GetResult(i)
            }
                MyBase.Rows.Add(row)
            Next
            _allowEvents = True
            'RaiseEvent AfterUpdate(Me, New MatEventArgs(Sheet))
            retval.Succeed("carregats {0} camps correctament", _Fields.Count)

        Catch ex As Exception
            retval.Fail(ex, "Xl_ExcelColumnsMapping.Load: Error al carregar les columnes de l'Excel")
        End Try
        Return retval
    End Function

    Private Function GetResult(idx As Integer) As String
        Dim retval As String = ""
        If idx < _results.Count Then
            If _Fields(idx) = "Ean" Then
                retval = _results.FirstOrDefault(Function(x) x.ToLower.Contains("ean"))
            ElseIf _Fields(idx) = "Quantitat" Then
                retval = _results.FirstOrDefault(Function(x) x.ToLower.Contains("cantidad"))
            ElseIf _Fields(idx) = "Linia" Then
                retval = _results.FirstOrDefault(Function(x) x.ToLower.Contains("linea"))
            End If
            If String.IsNullOrEmpty(retval) Then
                retval = _results(idx)
            End If
        End If
        Return retval
    End Function

    Public ReadOnly Property Sheet As MatHelper.Excel.Sheet
        Get
            Dim retval As New MatHelper.Excel.Sheet
            Dim oCombo As DataGridViewComboBoxColumn = MyBase.Columns(Cols.Result)
            For Each oSrcRow As MatHelper.Excel.Row In _Sheet.Rows
                Dim oRow As MatHelper.Excel.Row = retval.AddRow()
                For j = 0 To _Fields.Count - 1
                    Dim iSrcColumn As Integer = ResultColumn(j)
                    Dim sSrcValue As String = ""
                    If iSrcColumn >= 0 And iSrcColumn < oSrcRow.Cells.Count Then
                        Try
                            sSrcValue = oSrcRow.Cells(iSrcColumn).Content
                        Catch ex As Exception

                        End Try
                    End If
                    oRow.AddCell(sSrcValue)
                Next
            Next
            Return retval
        End Get
    End Property

    Function Results() As List(Of String)
        Dim retval As New List(Of String)
        Dim ASCII As Integer = 65
        For Each oCell As MatHelper.Excel.Cell In _Sheet.Rows(0).Cells
            Dim sColId As String = "?"
            Try
                sColId = Chr(ASCII)
            Catch ex As Exception
                Stop
            End Try
            retval.Add(sColId & ": " & oCell.Content)
            ASCII += 1
        Next
        retval.Add("(ignorar)")
        Return retval
    End Function

    Function ResultColumn(iField As Integer) As Integer
        Dim sValue As String = MyBase.Rows(iField).Cells(Cols.Result).Value
        Dim retval As Integer = _results.IndexOf(sValue)
        Return retval
    End Function

    Private Sub SetProperties()
        'MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Header)
            .HeaderText = "Camp"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewComboBoxColumn)
        With DirectCast(MyBase.Columns(Cols.Result), DataGridViewComboBoxColumn)
            .HeaderText = "Fitxer"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            For Each sResult As String In _results
                .Items.Add(sResult)
            Next

        End With

    End Sub


    Private Sub Xl_ExcelColumnsMapping_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles Me.CurrentCellDirtyStateChanged
        If MyBase.IsCurrentCellDirty Then
            MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub Xl_ExcelColumnsMapping_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValueChanged
        If _allowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Sheet))
        End If
    End Sub
End Class
