Public Class Xl_ProductDownloads
    Inherits DataGridView

    Private _Values As List(Of DTOProductDownload)
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onFileDropped(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Src
        Fch
        Ico
        Features
        Nom
        Lang
        LogCount
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductDownload), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOProductDownload) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOProductDownload In FilteredValues()
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        MyBase.CurrentCell = MyBase.FirstDisplayedCell

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOProductDownload)
        Dim retval As List(Of DTOProductDownload)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.DocFile.Nom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOProductDownload
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductDownload = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Src)
            .DataPropertyName = "Src"
            .HeaderText = "tipus"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Features)
            .DataPropertyName = "Features"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .DataPropertyName = "Nom"
            .HeaderText = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Lang)
            .DataPropertyName = "Lang"
            .HeaderText = "Idioma"
            .Width = 45
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.LogCount)
            .DataPropertyName = "LogCount"
            .HeaderText = "log"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOProductDownload)
        Dim retval As New List(Of DTOProductDownload)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_ProductDownload As New Menu_ProductDownload(SelectedItems.First)
            AddHandler oMenu_ProductDownload.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ProductDownload.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTOProductDownload = CurrentControlItem.Source
        Dim oFrm As New Frm_ProductDownload(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            Dim oProductDownload As DTOProductDownload = oControlItem.Source
            If oProductDownload.Obsoleto Then
                oRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray
            End If
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

#Region "DragDrop"
    Private Sub DataGridView1_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragOver

        If (e.Data.GetDataPresent("FileDrop") Or e.Data.GetDataPresent("FileGroupDescriptor")) Then
            Static RowIndex As Integer
            Dim clientPoint As Point = MyBase.PointToClient(New Point(e.X, e.Y)) ' The mouse locations are relative to the screen, so they must be converted to client coordinates.
            Dim iDestinationRowIndex As Integer = MyBase.HitTest(clientPoint.X, clientPoint.Y).RowIndex
            'Dim hit As DataGridView.HitTestInfo = MyBase.HitTest(e.X, e.Y)
            If iDestinationRowIndex <> RowIndex Then
                e.Effect = DragDropEffects.Move

                If (iDestinationRowIndex >= 0) Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(iDestinationRowIndex)
                    oRow.DividerHeight = 2
                End If
                If RowIndex >= 0 And MyBase.Rows.Count > RowIndex Then
                    MyBase.Rows(RowIndex).DividerHeight = 0.5
                End If
                RowIndex = iDestinationRowIndex
            End If
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent("FileDrop")) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = MyBase.HitTest(e.X, e.Y)
            If (hit.RowIndex >= 0) Then
                Dim oRow As DataGridViewRow = MyBase.Rows(hit.RowIndex)
                If oRow IsNot Nothing Then
                    MyBase.DoDragDrop(oRow, DragDropEffects.Move)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
        'Dim oMediaObjects As New MediaObjects
        'Dim exs As New List(Of Exception)
        'Dim oTargetCell As DataGridViewCell = Nothing
        'Dim oDocFiles As List(Of DTODocFile) = Nothing
        'If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
        ' RaiseEvent onFileDropped(Me, New MatEventArgs(oDocFiles.First))
        ' Else
        ' MsgBox("error al importar fitxer" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
        ' End If

    End Sub

#End Region

    Protected Class ControlItem
        Property Source As DTOProductDownload

        Property Src As String
        Property Fch As Date
        Property Ico As Image
        Property Features As String
        Property Nom As String
        Property Lang As String
        Property LogCount As Integer

        Public Sub New(oProductDownload As DTOProductDownload)
            MyBase.New()
            _Source = oProductDownload

            With oProductDownload
                _Src = .Src.ToString.Replace("_", " ")
                _Fch = .DocFile.fch
                _Ico = IconHelper.GetIconFromMimeCod(.DocFile.mime)
                _Features = DTODocFile.Features(.DocFile, True)
                _Nom = .DocFile.Nom
                If .Lang IsNot Nothing Then
                    _Lang = .Lang.Tag
                End If
                _LogCount = .DocFile.LogCount
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


