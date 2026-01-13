Public Class Xl_ImportacioDocs
    Private _Importacio As DTOImportacio
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Nom
        Eur
    End Enum

    Public Shadows Sub Load(ByRef value As DTOImportacio)
        _Importacio = value
        refresca()
    End Sub

    Private Sub refresca()
        _ControlItems = New ControlItems
        Dim oSrc As DTOImportacioItem.SourceCodes = DTOImportacioItem.SourceCodes.NotSet
        Dim oPartial As New ControlItem(oSrc)
        If _Importacio.Items IsNot Nothing Then
            For Each oItem As DTOImportacioItem In _Importacio.Items.OrderBy(Function(x) DTOImportacioItem.GetConcept(x)).OrderBy(Function(y) y.SrcCod)
                If oItem.SrcCod <> oSrc Then
                    If _ControlItems.Count > 0 Then
                        _ControlItems.Add(oPartial)
                    End If
                    oSrc = oItem.SrcCod
                    oPartial = New ControlItem(oSrc)
                    _ControlItems.Add(New ControlItem())
                End If
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
                oPartial.Eur += oControlItem.Eur
            Next
            If _ControlItems.Count > 0 Then
                _ControlItems.Add(oPartial)
            End If

        End If
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOImportacioItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOImportacioItem = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .DataPropertyName = "Eur"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .ClearSelection()
        End With

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOImportacioItem)
        Dim retval As New List(Of DTOImportacioItem)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem.LinCod = ControlItem.LinCods.item AndAlso oControlItem.Source IsNot Nothing Then
                Dim oMenuRange As New Menu_ImportacioItem(oControlItem.Source)
                AddHandler oMenuRange.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenuRange.Range)
                oContextMenu.Items.Add("-")
            End If
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, "importar document a la remesa " & _Importacio.Id) Then
            ImportaDocFile(oDocFile)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        'Dim oSelectedValue As ImportacioItem = CurrentControlItem.Source
        'Dim oFrm As New Frm_ImportacioItem(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        refresca()
    End Sub


    Private Sub ImportaDocFile(ByVal oDocFile As DTODocFile)
        Dim oFrm As New Frm_ImportacioItem(_Importacio, oDocFile)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridViewDocs_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            '    or none of the above
        ElseIf (e.Data.GetDataPresent(GetType(DTODelivery))) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(List(Of DTODelivery)))) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(DTOCca))) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(DTOCcb))) Then
            e.Effect = DragDropEffects.Copy
            'Dim oCcb As DTOCcb = e.Data.GetData(GetType(DTOCcb))
            'Stop
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Async Sub DataGridViewDocs_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim exs As New List(Of Exception)
        If (e.Data.GetDataPresent(DataFormats.FileDrop) Or e.Data.GetDataPresent("FileGroupDescriptor")) Then
            Dim oDocFiles As List(Of DTODocFile) = Nothing
            Dim oTargetCell As DataGridViewCell = Nothing
            If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
                Dim oDocFile As DTODocFile = oDocFiles.First
                ImportaDocFile(oDocFile)
            Else
                UIHelper.WarnError(exs, "error al importar el document")
            End If
        ElseIf (e.Data.GetDataPresent(GetType(DTODelivery))) Then
            Dim oDelivery As DTODelivery = e.Data.GetData(GetType(DTODelivery))
            If FEB2.Delivery.Load(oDelivery, exs) Then
                Dim oItem = DTOImportacioItem.Factory(_Importacio, DTOImportacioItem.SourceCodes.Alb, oDelivery.Guid)
                With oItem
                    .Amt = Await FEB2.Delivery.Total(exs, oDelivery)
                    .Descripcio = "Albará " & oDelivery.Id & " del " & oDelivery.Fch.ToShortDateString
                End With
                _Importacio.Items.Add(oItem)
                RaiseEvent AfterUpdate(Me, New MatEventArgs(oDelivery))
                refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        ElseIf (e.Data.GetDataPresent(GetType(List(Of DTODelivery)))) Then
            Dim oDeliveries As List(Of DTODelivery) = e.Data.GetData(GetType(List(Of DTODelivery)))
            For Each oDelivery As DTODelivery In oDeliveries
                If FEB2.Delivery.Load(oDelivery, exs) Then
                    Dim oItem = DTOImportacioItem.Factory(_Importacio, DTOImportacioItem.SourceCodes.Alb, oDelivery.Guid)
                    With oItem
                        .Amt = oDelivery.Import()
                        .Descripcio = "Albará " & oDelivery.Id & " del " & oDelivery.Fch.ToShortDateString
                    End With
                    _Importacio.Items.Add(oItem)
                Else
                    UIHelper.WarnError(exs)
                End If
            Next
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oDeliveries))
            refresca()
        ElseIf (e.Data.GetDataPresent(GetType(DTOCcb))) Then
            Dim oCcb As DTOCcb = e.Data.GetData(GetType(DTOCcb))
            Dim oSrcCod As DTOImportacioItem.SourceCodes = DTOImportacioItem.SourceCodes.NotSet
            Dim oAmt As DTOAmt = Nothing
            Select Case oCcb.Cta.Codi
                Case DTOPgcPlan.Ctas.transport_internacional_fletes, DTOPgcPlan.Ctas.transport_internacional_despeses, DTOPgcPlan.Ctas.transport_internacional_aranzels
                    oSrcCod = DTOImportacioItem.SourceCodes.FraTrp
                    oAmt = FraTransportAmt(oCcb.Cca)
                Case Else
                    oSrcCod = DTOImportacioItem.SourceCodes.Fra
                    oAmt = oCcb.Amt
            End Select
            Dim oItem = DTOImportacioItem.Factory(_Importacio, oSrcCod, oCcb.Cca.Guid)
            With oItem
                .Amt = oAmt
                .Descripcio = oCcb.Cca.Concept
                .DocFile = oCcb.Cca.DocFile
                .Tag = oCcb.Cca
            End With
            _Importacio.Items.Add(oItem)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oCcb))
            refresca()
        Else
        End If

    End Sub

    Private Function FraTransportAmt(oCca As DTOCca) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oCcb As DTOCcb In oCca.Items
            Select Case oCcb.Cta.Codi
                Case DTOPgcPlan.Ctas.transport_internacional_fletes, DTOPgcPlan.Ctas.transport_internacional_despeses, DTOPgcPlan.Ctas.transport_internacional_aranzels
                    If oCcb.Dh = DTOCcb.DhEnum.Debe Then
                        retval.Add(oCcb.Amt)
                    Else
                        retval.Substract(oCcb.Amt)
                    End If
            End Select
        Next
        Return retval
    End Function

    Protected Class ControlItem
        Public Property Source As DTOImportacioItem

        Public Property Ico As Image
        Public Property Nom As String
        Property Eur As Decimal

        Property LinCod As LinCods

        Public Enum LinCods
            item
            epigraf
            blank
        End Enum

        Public Sub New(oImportacioItem As DTOImportacioItem)
            MyBase.New()
            _Source = oImportacioItem
            _LinCod = LinCods.item
            With oImportacioItem
                Select Case .SrcCod
                    Case DTOImportacioItem.SourceCodes.Cmr, DTOImportacioItem.SourceCodes.PackingList, DTOImportacioItem.SourceCodes.Proforma
                        _Ico = My.Resources.star
                    Case DTOImportacioItem.SourceCodes.Alb
                        _Ico = My.Resources.star_green
                    Case DTOImportacioItem.SourceCodes.Fra
                        _Ico = My.Resources.star_blue
                    Case DTOImportacioItem.SourceCodes.FraTrp
                        _Ico = My.Resources.star
                    Case Else
                        _Ico = My.Resources.empty
                End Select

                If .Descripcio > "" Then
                    _Nom = .Descripcio
                Else
                    _Nom = DTOImportacioItem.GetConcept(oImportacioItem)
                End If

                If .Amt IsNot Nothing Then
                    _Eur = .Amt.Eur
                End If

            End With
        End Sub

        Public Sub New(oSrcCod As DTOImportacioItem.SourceCodes)
            MyBase.New
            Select Case oSrcCod
                Case DTOImportacioItem.SourceCodes.Alb
                    _Nom = "total albarans mercancia"
                Case DTOImportacioItem.SourceCodes.Fra
                    _Nom = "total factures mercancia"
                Case DTOImportacioItem.SourceCodes.FraTrp
                    _Nom = "total factures transport"
            End Select
            _LinCod = LinCods.epigraf
        End Sub

        Public Sub New()
            MyBase.New
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

