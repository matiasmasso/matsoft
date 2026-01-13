

Public Class Frm_Youtube
    Private mYouTube As DTOYouTubeMovie
    Private mAllowEvents As Boolean = False
    Private mCreateQRUrlChanged As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
        QR
    End Enum

    Private Enum Cols
        Tpa
        Stp
        Art
        Text
    End Enum

    Private Enum ColsQR
        Campaign
        Fch
        Txt
        Img
    End Enum


    Public Sub New(ByVal oYouTube As DTOYouTubeMovie)
        MyBase.New()
        Me.InitializeComponent()
        mYouTube = oYouTube
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mYouTube
            TextBoxYoutubeId.Text = .YoutubeId
            TextBoxNom.Text = .Nom
            TextBoxDsc.Text = .Dsc
            'TextBoxQR_url.Text = BLL.BLLYouTubeMovie.QR_Movie_url(mYouTube, True)
            LoadGridProducts(.Products)

            If Not .IsNew Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub LoadGridProducts(ByVal oProducts As List(Of DTOProduct))
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("TPA", System.Type.GetType("System.Int32"))
            .Add("STP", System.Type.GetType("System.Int32"))
            .Add("ART", System.Type.GetType("System.Int32"))
            .Add("TXT", System.Type.GetType("System.String"))
        End With

        Dim oRow As DataRow
        For Each oProduct As DTOProduct In oProducts
            oRow = oTb.NewRow
            Select Case oProduct.SourceCod
                Case Product.ValueTypes.Art
                    'Dim oArt As Art = CType(oProduct.Value, Art)
                    'oRow(Cols.Art) = oArt.Id
                Case Product.ValueTypes.Stp
                    'Dim oStp As Stp = CType(oProduct.Value, Stp)
                    'oRow(Cols.Tpa) = oStp.Tpa.Id
                    'oRow(Cols.Stp) = oStp.Id
                Case Product.ValueTypes.Tpa
                    'Dim oTpa As Tpa = CType(oProduct.Value, Tpa)
                    'oRow(Cols.Tpa) = oTpa.Id
            End Select
            'oRow(Cols.Text) = oProduct.Text
            oTb.Rows.Add(oRow)
        Next

        With DataGridViewProducts
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = True
            With .Columns(Cols.Tpa)
                .Visible = False
            End With
            With .Columns(Cols.Stp)
                .Visible = False
            End With
            With .Columns(Cols.Art)
                .Visible = False
            End With
            With .Columns(Cols.Text)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

    End Sub

    Private Function GetProductsFromGrid() As Products
        Dim oProducts As New Products
        Dim oProduct As Product = Nothing
        Dim iTpa As Integer
        Dim iStp As Integer
        Dim iArt As Integer
        For Each oRow As DataGridViewRow In DataGridViewProducts.Rows
            iTpa = IIf(IsDBNull(oRow.Cells(Cols.Tpa).Value), 0, oRow.Cells(Cols.Tpa).Value)
            iStp = IIf(IsDBNull(oRow.Cells(Cols.Stp).Value), 0, oRow.Cells(Cols.Stp).Value)
            iArt = IIf(IsDBNull(oRow.Cells(Cols.Art).Value), 0, oRow.Cells(Cols.Art).Value)
            oProduct = New Product(BLL.BLLApp.Emp, iTpa, iStp, iArt)
            oProducts.Add(oProduct)
        Next
        Return oProducts
    End Function

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxYoutubeId.TextChanged, _
        TextBoxNom.TextChanged, _
        TextBoxDsc.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub DataGridViewProducts_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridViewProducts.UserDeletedRow
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_LookupProduct1.AfterUpdate
        ButtonAddProduct.Enabled = True
    End Sub

    Private Sub ButtonAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click
        Dim oProducts As Products = GetProductsFromGrid()
        oProducts.Add(Xl_LookupProduct1.Product)
        'LoadGridProducts(oProducts)
        Xl_LookupProduct1.Clear()
        ButtonAddProduct.Enabled = False
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mYouTube
            .YoutubeId = TextBoxYoutubeId.Text
            .Nom = TextBoxNom.Text
            .Dsc = TextBoxDsc.Text
            '.Products = GetProductsFromGrid()
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLYouTubeMovie.Update(mYouTube, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(mYouTube))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If


    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If BLL.BLLYouTubeMovie.Delete(mYouTube, exs) Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
            Me.Close()
        End If
    End Sub

    Private Sub TextBoxCreateQRUrl_TextChanged(sender As Object, e As System.EventArgs)
        If mAllowEvents Then
            mCreateQRUrlChanged = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.General
            Case Tabs.QR
                Static QRdone As Boolean
                If Not QRdone Then
                    mAllowEvents = False
                    LoadGridQR()
                    SetContextMenuQR()
                    mAllowEvents = True
                    QRdone = True
                End If
        End Select
    End Sub

    Private Sub LoadGridQR()
        Dim SQL As String = "SELECT Guid, Fch, Nom FROM QrCampaign WHERE YouTubeGuid=@YouTubeGuid"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@YouTubeGuid", mYouTube.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oColPdfIco As DataColumn = oTb.Columns.Add("QR", System.Type.GetType("System.Byte[]"))
        oColPdfIco.SetOrdinal(ColsQR.Img)

        For Each oRow As DataRow In oTb.Rows
            Dim oGuid As Guid = GuidHelper.GetGuid(oRow("Guid"))
            Dim oCampaign As New QrCampaign(oGuid)
            Dim oThumbnail = oCampaign.QR.Thumbnail(48)
            oRow(ColsQR.Img) = maxisrvr.GetByteArrayFromImg(oThumbnail)
        Next

        With DataGridViewQR
            .RowTemplate.Height = 48
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsQR.Campaign)
                .Visible = False
            End With

            With .Columns(ColsQR.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            End With

            With .Columns(ColsQR.Txt)
                .HeaderText = "campanya"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            End With

            With .Columns(ColsQR.Img)
                .HeaderText = ""
                .Width = 48
            End With

        End With

        DataGridViewQR.ClearSelection()
    End Sub

    Private Function CurrentCampaign() As QrCampaign
        Dim RetVal As QrCampaign = Nothing
        Dim oRow As DataGridViewRow = DataGridViewQR.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(ColsQR.Campaign).Value)
            RetVal = New QrCampaign(oGuid)
        End If
        Return RetVal
    End Function

    Private Sub SetContextMenuQR()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCampaign As QrCampaign = CurrentCampaign()

        If oCampaign IsNot Nothing Then
            Dim oMenu As New Menu_QrCampaign(oCampaign)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequestQr
            oContextMenu.Items.AddRange(oMenu.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNewQr)

        DataGridViewQR.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNewQr()
        Dim oCampaign As New QrCampaign()
        oCampaign.YouTube = mYouTube

        Dim oFrm As New Frm_QrCampaign(oCampaign)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestQr
        oFrm.Show()
    End Sub

    Private Sub RefreshRequestQr(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsQR.Txt
        Dim oGrid As DataGridView = DataGridViewQR

        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridQR()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridViewQR_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewQR.CellFormatting
        Select Case e.ColumnIndex
            Case ColsQR.Txt
                Dim oRow As DataGridViewRow = DataGridViewQR.Rows(e.RowIndex)
                Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(ColsQR.Campaign).Value)
                Dim oCampaign As New QrCampaign(oGuid)
                Dim sb As New System.Text.StringBuilder
                sb.AppendLine(oCampaign.YouTube.Nom)
                sb.AppendLine(e.Value)
                sb.AppendLine(oCampaign.LogsCount.ToString & " descargas")
                e.Value = sb.ToString
                e.CellStyle.WrapMode = DataGridViewTriState.True
        End Select
    End Sub

    Private Sub DataGridViewQR_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridViewQR.SelectionChanged
        If mAllowEvents Then
            SetContextMenuQR()
        End If
    End Sub
End Class