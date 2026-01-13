
Imports System.Data.SqlClient

Public Class Xl_Rols_Allowed
    Private _Controlitems As New ControlItems

    Private mAllowEvents As Boolean

    Private Enum Cols
        Chk
        Nom
    End Enum

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Shadows Sub Load(allRols As List(Of DTORol), selectedValues As List(Of DTORol))
        _Controlitems = New ControlItems
        For Each oRol In allRols
            Dim oControlitem As New ControlItem(oRol)
            oControlitem.Checked = selectedValues.Any(Function(x) x.Equals(oControlitem.Source))
        Next

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If



        'oRow.Cells(Cols.Chk).Value = False
        mAllowEvents = True
    End Sub

    Public ReadOnly Property Rols As List(Of DTORol)
        Get
            Dim retVal As New List(Of DTORol)
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If oRow.Cells(Cols.Chk).Value Then
                    Dim RolId As DTORol.Ids = CType(oRow.Cells(Cols.Id).Value, DTORol.Ids)
                    Dim oRol As New DTORol(RolId)
                    retVal.Add(oRol)
                End If
            Next
            Return retVal
        End Get
    End Property



    Private Sub LoadGrid(allRols As List(Of DTORol))
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .DataSource = allRols
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .ReadOnly = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .Visible = False
            End With
            .Columns.Add(New DataGridViewCheckBoxColumn)
            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
        End With
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Chk

                    RaiseEvent AfterUpdate(sender, e)
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTORol

        Property Checked As Boolean
        Property Nom As String

        Public Sub New(value As DTORol)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom.Tradueix(BLLSession.Current.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class
End Class
