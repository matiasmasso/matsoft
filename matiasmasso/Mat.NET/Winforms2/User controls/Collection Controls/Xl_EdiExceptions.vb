Public Class Xl_EdiExceptions
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTO.Integracions.Edi.Exception)
    Private _PropertiesSet As Boolean

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        FileTag
        Fch
        DocNum
        Msg
    End Enum

    Public Shadows Sub Load(values As List(Of DTO.Integracions.Edi.Exception))
        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTO.Integracions.Edi.Exception) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTO.Integracions.Edi.Exception In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTO.Integracions.Edi.Exception)
        Dim retval As List(Of DTO.Integracions.Edi.Exception)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Msg.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function

    Public WriteOnly Property Filter As String
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

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FileTag)
            .HeaderText = "Edi"
            .DataPropertyName = "FileTag"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
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

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.DocNum)
            .HeaderText = "Numero"
            .DataPropertyName = "DocNum"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Msg)
            .HeaderText = "Errors detectats"
            .DataPropertyName = "Msg"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTO.Integracions.Edi.Exception)
        Dim retval As New List(Of DTO.Integracions.Edi.Exception)
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
            Dim oEdiException As DTO.Integracions.Edi.Exception = oControlItem.Source
            Select Case oEdiException.FileTag
                Case DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008
                    Select Case CType(oEdiException.Cod, DTO.Integracions.Edi.Invrpt.ExceptionCods)
                        Case Integracions.Edi.Invrpt.ExceptionCods.reportingNotFound
                            'Dim oFrm As New Frm_ContactSearch
                            'AddHandler oFrm.itemSelected, AddressOf onContactSelected
                            'oFrm.Show()
                    End Select
            End Select
            oContextMenu.Items.Add("torna a processar", Nothing, AddressOf ReProcesaInvRpt)
            oContextMenu.Items.Add("veure fitxer Edi", Nothing, AddressOf Do_ShowEdiFile)
            oContextMenu.Items.Add("copiar doc num", Nothing, AddressOf CopyDocNum)
            'AddHandler oMenu_EDiversaException.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(Await oMenu_EDiversaException.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub ReProcesaInvRpt(sender As Object, e As EventArgs)
        Await ReProcesaInvRpt()
    End Sub

    Private Sub Do_ShowEdiFile()
        Dim oControlItem = CurrentControlItem()
        Dim oEdiException As DTO.Integracions.Edi.Exception = oControlItem.Source
        Dim oEdiFile As New DTOEdiversaFile(oEdiException.FileGuid)
        Dim oFrm As New Frm_EdiversaFile(oEdiFile)
        oFrm.Show()
    End Sub

    Private Sub CopyDocNum()
        Dim oControlItem = CurrentControlItem()
        Dim oEdiException As DTO.Integracions.Edi.Exception = oControlItem.Source
        UIHelper.CopyToClipboard(oEdiException.DocNum)
    End Sub

    Private Async Function ReProcesaInvRpt() As Task
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(True))
        Dim oControlItem = CurrentControlItem()
        Dim oEdiException As DTO.Integracions.Edi.Exception = oControlItem.Source
        Dim oInvRpt As New DTO.Integracions.Edi.Invrpt()
        oInvRpt.Guid = oEdiException.DocGuid
        Dim oEdiversaFile = Await FEB.EdiversaFile.FromResultGuid(exs, oEdiException.DocGuid)
        If exs.Count = 0 Then
            If oEdiversaFile Is Nothing Then
                UIHelper.WarnError("No s'ha trobat el fitxer Edi original")
            Else
                If Await FEB.InvRpt.Delete(exs, oInvRpt) Then
                    If Await FEB.EdiversaFile.Procesa(exs, oEdiversaFile) Then
                        MyBase.RefreshRequest(Me, EventArgs.Empty)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
    Private Sub onContactSelected(sender As Object, e As MatEventArgs)
        Dim oContact As DTOContact = e.Argument
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oEdiException As DTO.Integracions.Edi.Exception = oControlItem.Source
        Select Case oEdiException.FileTag
            Case DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008
                Select Case CType(oEdiException.Cod, DTO.Integracions.Edi.Invrpt.ExceptionCods)
                    Case Integracions.Edi.Invrpt.ExceptionCods.reportingNotFound
                        'Dim oInvRpt = Await FEB.InvRpts.Find
                        'AddHandler oFrm.itemSelected, AddressOf onContactSelected
                        'oFrm.Show()
                End Select
        End Select


    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTO.Integracions.Edi.Exception = CurrentControlItem.Source
            'Dim oFrm As New Frm_EDiversaException(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub



    Protected Shadows Class ControlItem
        Property Source As DTO.Integracions.Edi.Exception

        Property FileTag As String
        Property Fch As Date
        Property DocNum As String
        Property Msg As String

        Public Sub New(value As DTO.Integracions.Edi.Exception)
            MyBase.New()
            _Source = value
            With value
                Select Case .FileTag
                    Case DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008
                        _FileTag = "Inventari"
                    Case Else
                        _FileTag = .FileTag.ToString()
                End Select
                _Fch = .Fch
                _DocNum = .DocNum
                _Msg = .Msg
            End With
        End Sub

    End Class

    Protected Shadows Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

