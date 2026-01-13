Public Class Xl_IncidenciesOld
    Private _Incidencies As HashSet(Of Models.IncidenciesModel.Item)
    Private _Customers As HashSet(Of DTOGuidNom.Compact)
    Private _Customer As DTOGuidNom.Compact
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Fch
        PdfIco
        ImgIco
        VideoIco
        clx
        prodNom
        FchClose
        Description
        UsrLastEdited
        FchLastEdited
    End Enum

    Public Shadows Sub Load(oIncidencies As HashSet(Of Models.IncidenciesModel.Item), oCustomers As HashSet(Of DTOGuidNom.Compact), oCustomer As DTOGuidNom.Compact, oCatalog As Models.CatalogModel)
        _Incidencies = oIncidencies
        _Customers = oCustomers
        _Customer = oCustomer
        _ControlItems = New ControlItems
        For Each oItem In _Incidencies
            Dim oControlItem As New ControlItem(oItem, oCustomers, oCatalog)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOIncidencia
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval = oControlItem.Incidencia
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
            .AllowDrop = False
            .ReadOnly = True

            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .DataPropertyName = "Fch"
                .HeaderText = "data"
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End With

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.PdfIco), DataGridViewImageColumn)
                .DataPropertyName = "PdfIco"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.ImgIco), DataGridViewImageColumn)
                .DataPropertyName = "ImgIco"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.VideoIco), DataGridViewImageColumn)
                .DataPropertyName = "VideoIco"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.clx)
                .DataPropertyName = "Clx"
                If _Customer Is Nothing Then
                    .Visible = True
                    .HeaderText = "client"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Else
                    .Visible = False
                End If
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.prodNom)
                .DataPropertyName = "prodNom"
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchClose)
                .DataPropertyName = "FchClose"
                .Visible = False
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Description)
                .DataPropertyName = "Description"
                .HeaderText = "observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.UsrLastEdited)
                .DataPropertyName = "UsrLastEdited"
                .HeaderText = "operador"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchLastEdited)
                .DataPropertyName = "FchLastEdited"
                .HeaderText = "control"
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End With
        End With

        DataGridView1.ClearSelection()
        _AllowEvents = True
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem.FchClose = Nothing Then
            oRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 153)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
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

    Private Function SelectedItems() As List(Of DTOIncidencia)
        Dim retval As New List(Of DTOIncidencia)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Incidencia)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Incidencia)
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
            Dim oMenu_Template_Object As New Menu_Incidencia(SelectedItems.First)
            AddHandler oMenu_Template_Object.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Template_Object.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        Dim oIncidencia = DTOIncidencia.Factory(DTOIncidencia.ContactTypes.Professional, DTOIncidencia.Srcs.Producte)
        Dim oFrm As New Frm_Incidencia(oIncidencia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOIncidencia = CurrentControlItem.Incidencia
        Dim oFrm As New Frm_Incidencia(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub OldRefreshRequest()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
            MsgBox("no hi han incidencies registrades!", MsgBoxStyle.Exclamation)
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Models.IncidenciesModel.Item

        Property Id As String
        Property Fch As Date
        Property PdfIco As Image
        Property ImgIco As Image
        Property VideoIco As Image
        Property clx As String
        Property prodNom As String
        Property FchClose As Date
        Property Description As String
        Property UsrLastEdited As String
        Property FchLastEdited As Nullable(Of Date)


        Public Sub New(oIncidencia As Models.IncidenciesModel.Item, oCustomers As HashSet(Of DTOGuidNom.Compact), oCatalog As Models.CatalogModel)
            MyBase.New()
            _Source = oIncidencia
            With oIncidencia
                _Id = If(String.IsNullOrEmpty(.Asin), .Num, .Asin)
                _Fch = .Fch
                _PdfIco = IIf(.HasTicket, My.Resources.pdf, Nothing)
                _ImgIco = IIf(.HasImg, My.Resources.img_16, Nothing)
                _VideoIco = IIf(.HasVideo, My.Resources.video16, Nothing)

                If Not .CustomerGuid.Equals(Guid.Empty) Then
                    _clx = oCustomers.FirstOrDefault(Function(x) x.Guid.Equals(.CustomerGuid)).Nom
                End If
                If Not .ProductGuid.Equals(Guid.Empty) Then
                    _prodNom = oCatalog.ProductFullNom(.ProductGuid)
                End If
                _FchClose = .FchClose
                _Description = .Obs
                _UsrLastEdited = .UserNom
                If .FchLastEdited <> Nothing Then
                    _FchLastEdited = .FchLastEdited
                End If
            End With
        End Sub

        Public Function Incidencia() As DTOIncidencia
            Dim retval As New DTOIncidencia(_Source.Guid)
            With retval
                .Num = _Source.Num
                .Asin = _Source.Asin
            End With
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

