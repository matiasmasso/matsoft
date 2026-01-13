Public Class Xl_ImportPrevisio

    Inherits _Xl_ReadOnlyDatagridview

    Private _Previsions As List(Of DTOImportPrevisio)
    Private _Validacions As List(Of DTOImportValidacio)
    Private _DefaultValue As DTOImportPrevisio
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _LastSkuEntered As DTOProductSku

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToImportPrevisio(sender As Object, e As MatEventArgs)
    Public Event RequestToImportValidation(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event onPurchaseOrderItemUpdateRequest(sender As Object, e As MatEventArgs)

    Property IsDirty As Boolean

    ' Public Event RequestToShowCurrentCell(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Ref
        Txt
        Qty
    End Enum

    Public ReadOnly Property Values As List(Of DTOImportPrevisio)
        Get
            Dim retval As New List(Of DTOImportPrevisio)
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public Shadows Sub Load(Previsions As List(Of DTOImportPrevisio), Optional oDefaultValue As DTOImportPrevisio = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Previsions = Previsions
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOImportPrevisio) = FilteredValues()
        _ControlItems = New ControlItems
        Dim oControlItem As ControlItem = Nothing
        For Each oItem As DTOImportPrevisio In oFilteredValues
            oControlItem = New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOImportPrevisio)
        Dim retval As List(Of DTOImportPrevisio)
        If _Filter = "" Then
            retval = _Previsions
        Else
            retval = _Previsions.FindAll(Function(x) x.SkuRef.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Previsions IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOImportPrevisio
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOImportPrevisio = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        With MyBase.RowTemplate
            .Height = MyBase.Font.Height * 1.3
            '.DefaultCellStyle.BackColor = Color.Transparent (es transparenten els tabs de sota)
        End With

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.BackgroundColor = Color.White
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.RowHeadersWidth = 25
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False
        MyBase.AllowUserToAddRows = True
        MyBase.AllowUserToDeleteRows = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ref)
            .HeaderText = "Ref."
            .DataPropertyName = "Ref"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Unitats"
            .DataPropertyName = "Qty"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0;-#,###0;#"
        End With

        MyBase.CurrentCell = Me(0, 0)
        MyBase.BeginEdit(True)
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

    Private Function SelectedItems() As List(Of DTOImportPrevisio)
        Dim retval As New List(Of DTOImportPrevisio)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                If oControlItem.LinCod = ControlItem.LinCods.Item Then
                    retval.Add(oControlItem.Source)
                End If
            End If
        Next

        If retval.Count = 0 Then
            If CurrentControlItem.LinCod = ControlItem.LinCods.Item Then
                retval.Add(CurrentControlItem.Source)
            End If
        End If
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
            Dim oItems As List(Of DTOImportPrevisio) = SelectedItems()
            If oItems.Count > 0 Then
                Dim oMenu_ImportPrevisioItem As New Menu_ImportPrevisio(oItems.First)
                AddHandler oMenu_ImportPrevisioItem.AfterUpdate, AddressOf Refreshrequest
                AddHandler oMenu_ImportPrevisioItem.onPurchaseOrderItemUpdateRequest, AddressOf Do_PurchaseOrderItemUpdateRequest
                oContextMenu.Items.AddRange(oMenu_ImportPrevisioItem.Range)
                oContextMenu.Items.Add("-")

            End If

            Dim msg = "eliminar la linia seleccionada"
            If oItems.Count > 1 Then
                msg = String.Format("eliminar les {0} linies seleccionades", oItems.Count)
            End If
            oContextMenu.Items.Add(msg, Nothing, AddressOf Do_DeleteLine)

        End If

        oContextMenu.Items.Add("importar XML validació magatzem", Nothing, AddressOf Do_ImportValidation)
        oContextMenu.Items.Add("exportar XML avís camión al magatzem (totes les linies)", Nothing, AddressOf Do_ExportXMLAll)
        oContextMenu.Items.Add("exportar XML avís camión al magatzem (nomes linies seleccionades)", Nothing, AddressOf Do_ExportXMLSelected)
        oContextMenu.Items.Add("-")
        oContextMenu.Items.Add("importar Excel Previsió", Nothing, AddressOf Do_ImportExcel)
        oContextMenu.Items.Add("exportar Excel Previsió", Nothing, AddressOf Do_ExportExcel)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf Refreshrequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ImportExcel()
        RaiseEvent RequestToImportPrevisio(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ImportValidation()
        RaiseEvent RequestToImportValidation(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ExportExcel()
        Dim oSheet As New MatHelper.Excel.Sheet
        For Each item As DTOImportPrevisio In _Previsions
            Dim oRow = oSheet.AddRow
            oRow.AddCell(item.SkuRef)
            oRow.AddCell(item.SkuNom)
            oRow.AddCell(item.Qty)
            oRow.AddCell(item.NumComandaProveidor)
        Next
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_DeleteLine()
        For Each oRow In MyBase.SelectedRows
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            _ControlItems.Remove(oControlitem)
        Next
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ExportXMLAll()
        Do_ExportXML(_Previsions)
    End Sub

    Private Async Sub Do_ExportXMLSelected()
        Dim exs As New List(Of Exception)
        Dim oImportacio = _Previsions.First.Importacio
        oImportacio = Await FEB.ImportPrevisions.Load(exs, oImportacio)
        If exs.Count = 0 Then
            Dim oPrevisions As New List(Of DTOImportPrevisio)
            For Each oRow As DataGridViewRow In MyBase.SelectedRows
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim item As DTOImportPrevisio = oControlItem.Source
                oPrevisions.Insert(0, item)
            Next
            Do_ExportXML(oPrevisions)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ExportXML(oPrevisions As List(Of DTOImportPrevisio))
        Dim exs As New List(Of Exception)
        'Dim oImportacio As New DTOImportacio(_Previsions.First.Importacio.Guid)
        Dim oImportacio = _Previsions.First.importacio
        'If FEB.Importacio.Load(exs, oImportacio) Then
        Dim XmlSrc As String = FEB.ImportPrevisions.AvisCamionXml(exs, oPrevisions)
        Dim sFilename As String = FileSystemHelper.PathToTmp & String.Format("ArribadaCamion {0}.xml", oImportacio.FormattedId())
        FileSystemHelper.SaveTextToFile(XmlSrc, sFilename, exs)
        If exs.Count = 0 Then
            Dim sTo As String = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.EmailTransmisioVivace, exs)
            If exs.Count = 0 Then
                If sTo.IndexOf(";") >= 0 Then
                    sTo = sTo.Substring(0, sTo.IndexOf(";"))
                End If

                Dim oMailMessage = DTOMailMessage.Factory(sTo)
                With oMailMessage
                    .cc = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.AvisArribadaCamion)
                    .Subject = String.Format("Avis a Vivace arribada remesa {0} {1}", oImportacio.FormattedId(), oImportacio.Proveidor.FullNom)
                    .Body = "Adjuntem fitxer amb la mercancia prevista"
                    .AddAttachment(sFilename)
                End With

                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
        'Else
        'UIHelper.WarnError(exs)
        'End If
    End Sub

    Private Sub Do_PurchaseOrderItemUpdateRequest(sender As Object, e As MatEventArgs)
        RaiseEvent onPurchaseOrderItemUpdateRequest(Me, e)
    End Sub

    Private Shadows Sub Refreshrequest(sender As Object, e As System.EventArgs)
        MyBase.RefreshRequest(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOImportPrevisio = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    'Dim oFrm As New Frm_ImportPrevisio(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Private Async Sub Xl_ImportPrevisio_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValidated
        Dim exs As New List(Of Exception)
        Select Case e.ColumnIndex
            Case Cols.Qty
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Source Is Nothing Then oControlItem.Source = New DTOImportPrevisio
                Dim value As DTOImportPrevisio = oControlItem.Source
                value.Qty = oControlItem.Qty

            Case Cols.Ref, Cols.Txt
                If _LastSkuEntered IsNot Nothing Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    If oControlItem IsNot Nothing Then
                        With oControlItem
                            .Ref = _LastSkuEntered.id
                            .Txt = _LastSkuEntered.nomLlarg.Tradueix(Current.Session.Lang)
                        End With
                        If oControlItem.Source Is Nothing Then oControlItem.Source = New DTOImportPrevisio
                        Dim value As DTOImportPrevisio = oControlItem.Source
                        value.Sku = _LastSkuEntered
                        value.SkuRef = oControlItem.Ref
                        value.SkuNom = oControlItem.Txt

                        If value.Sku.Obsoleto Then
                            oControlItem.Validation = ControlItem.Validations.Obsolet
                        Else
                            Dim oPncs = Await FEB.PurchaseOrderItems.Pending(exs, GlobalVariables.Emp, value.Sku, DTOPurchaseOrder.Codis.Proveidor)
                            If exs.Count = 0 Then
                                If oPncs.Count = 0 Then
                                    oControlItem.Validation = ControlItem.Validations.NotOrdered
                                End If
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        End If
                    End If
                    _LastSkuEntered = Nothing
                End If
        End Select
        _IsDirty = True
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_ImportPrevisio_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles Me.CellValidating
        Dim exs As New List(Of Exception)
        Dim oRow As DataGridViewRow = Me.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case e.ColumnIndex
            Case Cols.Ref
                Dim procesa As Boolean = oControlItem Is Nothing And e.FormattedValue > ""
                If oControlItem IsNot Nothing Then
                    procesa = oControlItem.Ref <> e.FormattedValue
                End If
                If procesa Then
                    _LastSkuEntered = Finder.FindSkuSync(exs, Current.Session.Emp, e.FormattedValue, Current.Session.Emp.Mgz)
                    If _LastSkuEntered Is Nothing Then
                        e.Cancel = True
                    End If
                End If
            Case Cols.Txt
                Dim procesa As Boolean = oControlItem Is Nothing And e.FormattedValue > ""
                If oControlItem IsNot Nothing Then
                    procesa = oControlItem.Txt <> e.FormattedValue
                End If
                If procesa Then
                    _LastSkuEntered = Finder.FindSkuSync(exs, Current.Session.Emp, e.FormattedValue, Current.Session.Emp.Mgz)
                    If _LastSkuEntered Is Nothing Then
                        UIHelper.WarnError("no s'ha trobat cap producte per " & e.FormattedValue)
                        e.Cancel = True
                    End If
                End If
        End Select
    End Sub

    Private Sub Xl_ImportPrevisio_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            Select Case oControlItem.Validation
                Case ControlItem.Validations.Obsolet
                    oRow.DefaultCellStyle.BackColor = Color.LightGray
                Case ControlItem.Validations.NotOrdered
                    oRow.DefaultCellStyle.BackColor = Color.LightSalmon
            End Select
        End If
    End Sub


    Private Sub Xl_ImportPrevisio_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                Select Case oControlItem.Validation
                    Case ControlItem.Validations.Obsolet
                        e.ToolTipText = "aquest article está obsolet"
                    Case ControlItem.Validations.NotOrdered
                        e.ToolTipText = "no hi han comandes pendents de proveidor d'aquest article"
                End Select
            End If

        End If
    End Sub

    Protected Class ControlItem
        Property Source As Object

        Property LinCod As LinCods
        Property Validated As Boolean
        Property Ref As String
        Property Txt As String
        Property Qty As Integer
        Property Cfm As Integer
        Property Validation As Validations

        Public Enum LinCods
            Blank
            Order
            Item
        End Enum

        Public Enum Validations
            Success
            Obsolet
            NotOrdered
        End Enum

        Public Sub New()
            MyBase.New()
            _Source = Nothing
            _LinCod = LinCods.Blank
        End Sub

        Public Sub New(value As DTOPurchaseOrder)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Order
                _Ref = .NumComandaProveidor
                _Txt = ("Confirmació de comanda " & .NumComandaProveidor)
                _Validated = Validated
            End With
        End Sub

        Public Sub New(value As DTOImportPrevisio)
            MyBase.New()
            _Source = value

            With value
                _LinCod = LinCods.Item
                If .Sku IsNot Nothing Then
                    _Ref = .Sku.Id
                    If .Sku.Obsoleto Then
                        _Validation = Validations.Obsolet
                    ElseIf .Sku.Proveidors = 0 Then
                        _Validation = Validations.NotOrdered
                    End If
                End If
                _Txt = .SkuNom
                _Qty = .Qty
                _Validated = .Errors.Count = 0
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

