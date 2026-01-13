Public Class Xl_ElCorteInglesAlineamientoDisponibilidadLogs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
    Private _ControlItems As ControlItems

    Private _AllowEvents As Boolean

    Private Enum Cols
        Fch
    End Enum

    Public Shadows Sub Load(values As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad), Optional oDefaultValue As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad = oControlItem.Source
            Return retval
        End Get
    End Property

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


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm:ss"
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

    Private Function SelectedItems() As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        Dim retval As New List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
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
            Dim oMenu As New Menu_ElCorteInglesAlineamientoDisponibilidad(oControlItem.Source)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)
            'oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad = CurrentControlItem.Source
            Dim oFrm As New Frm_AlineamientoDeStocks(oSelectedValue)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Async Sub Do_Savefile()
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "Desar Fitxer de Alineacion de disponibilidad de El Corte Ingles"
            .FileName = "STOCK.TXT"
            .Filter = "fitxers de text|*.txt|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(True))

                Dim oLog = Await FEB.ElCorteIngles.AlineamientoDeDisponibilidad(exs, CurrentControlItem.Source.Guid, Current.Session.User)
                MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(False))
                If exs.Count = 0 Then
                    If Not MatHelperStd.FileSystemHelper.SaveTextToFile(oLog.Text, .FileName, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End With
    End Sub


    Protected Class ControlItem
        Property Source As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad

        Property Fch As Nullable(Of Date)

        Public Sub New(value As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
            MyBase.New()
            _Source = value
            With value
                _Fch = .Fch
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


