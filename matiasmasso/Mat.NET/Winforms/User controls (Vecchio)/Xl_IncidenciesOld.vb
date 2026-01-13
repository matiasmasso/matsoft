Public Class Xl_IncidenciesOld
    Private _Query As DTOIncidenciaQuery
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

    Public Shadows Sub Load(oQuery As DTOIncidenciaQuery)
        _Query = oQuery
        _ControlItems = New ControlItems
        For Each oItem As DTOIncidencia In oQuery.result
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOIncidencia
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOIncidencia = oControlItem.Source
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
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
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
                If _Query.Customer Is Nothing Then
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
        Dim oSelectedValue As DTOIncidencia = CurrentControlItem.Source
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
        Property Source As DTOIncidencia

        Property Id As Integer
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


        Public Sub New(oIncidencia As DTOIncidencia)
            MyBase.New()
            _Source = oIncidencia
            With oIncidencia
                _Id = .num
                _Fch = .fch
                _PdfIco = IIf(.existTickets, My.Resources.pdf, Nothing)
                _ImgIco = IIf(.existImages, My.Resources.img_16, Nothing)
                _VideoIco = IIf(.existVideos, My.Resources.video16, Nothing)
                If .customer IsNot Nothing Then
                    _clx = .customer.nom
                End If
                If .product IsNot Nothing Then
                    _prodNom = .product.FullNom()
                End If
                _FchClose = .fchClose
                _Description = .description
                If .UsrLog.usrLastEdited IsNot Nothing Then
                    _UsrLastEdited = DTOUser.NicknameOrElse(.UsrLog.usrLastEdited)
                    _FchLastEdited = .UsrLog.fchLastEdited
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

