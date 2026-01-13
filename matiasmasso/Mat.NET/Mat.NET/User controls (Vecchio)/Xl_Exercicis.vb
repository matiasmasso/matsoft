<System.ComponentModel.DefaultBindingProperty("DataSource")>
Public Class Xl_Exercicis
    Private _DataSource As Exercicis
    Private _DefaultExercici As Exercici
    Private _AllowEvents As Boolean

    Public Event AfterSelect(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Year
    End Enum

    Public Shadows Sub Load(oDataSource As Exercicis, Optional oDefaultExercici As Exercici = Nothing)
        _DataSource = oDataSource

        If oDefaultExercici Is Nothing And _DataSource.Count > 0 Then
            oDefaultExercici = _DataSource(0)
        End If
        _DefaultExercici = oDefaultExercici

        LoadGrid()
    End Sub

    Public ReadOnly Property DataSource As Exercicis
        Get
            Return _DataSource
        End Get
    End Property

    Public Function SelectedItem() As Exercici
        Dim retval As Exercici = Nothing
        Dim oControlItem As ControlItem = CurrentItem()
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        'Dim oMenuItem As ToolStripMenuItem = _IncludeObsoletsMenuItem
        'oContextMenu.Items.Add(oMenuItem)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow Is Nothing Then
            If DataGridView1.Rows.Count > 0 Then
                oRow = DataGridView1.Rows(0)
                DataGridView1.CurrentCell = oRow.Cells(Cols.Year)
            End If
        Else
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function



    Private Sub LoadGrid()
        _AllowEvents = False

        Dim oSelectedItem As ControlItem = Nothing
        Dim oControlItems As New ControlItems
        For Each oExercici As Exercici In _DataSource
            Dim oControlItem As New ControlItem(oExercici)
            If oExercici.Equals(_DefaultExercici) Then oSelectedItem = oControlItem
            oControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .RowHeadersVisible = False
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False
            .Columns.Clear()
            .DataSource = oControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.year)
                .DataPropertyName = "Year"
                .HeaderText = "exercici"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            If oSelectedItem IsNot Nothing Then
                Dim idx As Integer = oControlItems.IndexOf(oSelectedItem)
                .CurrentCell = .Rows(idx).Cells(Cols.Year)
            End If
        End With

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentItem()
            Dim oExercici As Exercici = oControlItem.Source
            Dim oArgs As New MatEventArgs(oExercici)
            RaiseEvent AfterSelect(Me, oArgs)
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As Exercici
        Public Property Year As Integer

        Public Sub New(oExercici As Exercici)
            MyBase.New()
            _Source = oExercici
            With oExercici
                _Year = .Yea
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class
