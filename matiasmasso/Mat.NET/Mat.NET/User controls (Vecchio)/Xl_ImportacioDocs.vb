Public Class Xl_ImportacioDocs
    Private _Importacio As Importacio
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Nom
    End Enum

    Public Shadows Sub Load(ByRef value As Importacio)
        _Importacio = value
        refresca()
    End Sub

    Private Sub refresca()
        _ControlItems = New ControlItems
        For Each oItem As ImportacioItem In _Importacio.Items
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As ImportacioItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As ImportacioItem = oControlItem.Source
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
            With CType(.Columns(Cols.ico), DataGridViewImageColumn)
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

    Private Function SelectedItems() As ImportacioItems
        Dim retval As New ImportacioItems
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
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenuRange As New Menu_ImportacioItem(oControlItem.Source)
            AddHandler oMenuRange.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenuRange.Range)
            oContextMenu.Items.Add("-")
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
        _Importacio = ImportacioLoader.Find(_Importacio.Guid)
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
        ElseIf (e.Data.GetDataPresent(GetType(Alb))) Then
            Dim oAlb As Alb = e.Data.GetData(GetType(Alb))
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(Cca))) Then
            Dim oCca As Cca = e.Data.GetData(GetType(Cca))
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(Ccb))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridViewDocs_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        If (e.Data.GetDataPresent(DataFormats.FileDrop) Or e.Data.GetDataPresent("FileGroupDescriptor")) Then
            Dim oDocFiles As List(Of DTODocFile) = Nothing
            Dim oTargetCell As DataGridViewCell = Nothing
            Dim exs as New List(Of exception)
            If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
                Dim oDocFile As DTODocFile = oDocFiles.First
                ImportaDocFile(oDocFile)
            Else
                UIHelper.WarnError( exs, "error al importar el document")
            End If
        ElseIf (e.Data.GetDataPresent(GetType(Alb))) Then
            Dim oAlb As Alb = e.Data.GetData(GetType(Alb))
        ElseIf (e.Data.GetDataPresent(GetType(Cca))) Then
            Dim oCca As Cca = e.Data.GetData(GetType(Cca))
        ElseIf (e.Data.GetDataPresent(GetType(Ccb))) Then
            Dim oCcb As Ccb = e.Data.GetData(GetType(Ccb))
            Dim oSrcCod As ImportacioItem.SourceCodes = ImportacioItem.SourceCodes.Fra
            Select Case oCcb.Cta.Cod
                Case DTOPgcPlan.Ctas.transport_internacional_fletes, DTOPgcPlan.Ctas.transport_internacional_despeses, DTOPgcPlan.Ctas.transport_internacional_aranzels
                    oSrcCod = ImportacioItem.SourceCodes.FraTrp
            End Select
            Dim oItem As New ImportacioItem(_Importacio, oSrcCod, oCcb.Cca.Guid)
            With oItem
                .Amt = oCcb.Amt
                .Descripcio = oCcb.Cca.Txt
                .DocFile = oCcb.Cca.DocFile
            End With
            _Importacio.Items.Add(oItem)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oCcb))
            refresca()
        Else
        End If

    End Sub

    Protected Class ControlItem
        Public Property Source As ImportacioItem

        Public Property Ico As Image
        Public Property Nom As String

        Public Sub New(oImportacioItem As ImportacioItem)
            MyBase.New()
            _Source = oImportacioItem
            With oImportacioItem
                Select Case .SrcCod
                    Case ImportacioItem.SourceCodes.Cmr
                        _Ico = My.Resources.star
                    Case ImportacioItem.SourceCodes.Alb
                        _Ico = My.Resources.star_green
                    Case ImportacioItem.SourceCodes.Fra
                        _Ico = My.Resources.star_blue
                    Case ImportacioItem.SourceCodes.FraTrp
                        _Ico = My.Resources.star
                    Case Else
                        _Ico = My.Resources.empty
                End Select

                If .Descripcio > "" Then
                    _Nom = .Descripcio
                Else
                    _Nom = .Text
                End If
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

