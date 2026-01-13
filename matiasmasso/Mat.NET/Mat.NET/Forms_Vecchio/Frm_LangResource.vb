

Public Class Frm_LangResource
    Private mLangResource As LangResource
    Private mAllowEvents As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        LangId
        LangNom
        Flag
        Text
    End Enum

    Public WriteOnly Property LangResource() As LangResource
        Set(ByVal value As LangResource)
            mLangResource = value
            If mLangResource.Guid = Nothing Then
                Me.Text = "NOU RECURS DE TEXTE"
                SetContextMenu()
            Else
                Me.Text = "RECURS DE TEXTE #" & mLangResource.Guid.ToString
                refresca()
            End If
            mAllowEvents = True
        End Set
    End Property

    Private Sub refresca()
        TextBoxClau.Text = mLangResource.Clau
        TextBoxEsp.Text = mLangResource.Text
        LoadGrid()
    End Sub


    Private Sub LoadGrid()
        mAllowEvents = False

        Dim SQL As String = "SELECT LANGID, TAG, FLAG, TEXT " _
        & "FROM LANG_TEXT INNER JOIN maxisrvr.dbo.LANG ON LANG_TEXT.LANGID=maxisrvr.dbo.LANG.ID " _
        & "WHERE LANG_TEXT.GUID=@GUID AND LANG_TEXT.SRC=@SRC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@GUID", mLangResource.Guid.ToString, "@SRC", mLangResource.Src)

        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.5
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            With .Columns(Cols.LangId)
                .Visible = False
            End With
            With .Columns(Cols.LangNom)
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Flag)
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Text)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With

        SetContextMenu()
    End Sub


    Private Function CurrentLangText() As LangText
        Dim oLangText As LangText = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim oLang As DTOLang
        If oRow IsNot Nothing Then
            oLang = New DTOLang(CType(oRow.Cells(Cols.LangId).Value, DTOLang.Ids))
            oLangText = New LangText(oLang, oRow.Cells(Cols.Text).Value)
        End If
        Return oLangText
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.LangNom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        refresca()

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

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oResource As LangText = CurrentLangText()
        If oResource IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom))
        End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oLang As New DTOLang(DTOLang.Ids.NotSet)
        Dim oFrm As New Frm_LangText
        AddHandler oFrm.AfterUpdate, AddressOf OnItemAdded
        With oFrm
            .LangResource = mLangResource
            .LangText = New LangText(oLang, "")
            .Show()
        End With
    End Sub

    Private Sub OnItemAdded(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oLangText As LangText = sender
        With mLangResource
            .Clau = TextBoxClau.Text
            .Text = TextBoxEsp.Text
            .LangTexts = GetItmsFromGrid()
            .LangTexts.Add(oLangText)
            .Update()
        End With
        RefreshRequest(sender, e)
        ButtonOk.Enabled = False
        RaiseEvent AfterUpdate(mLangResource, New System.EventArgs)
    End Sub

    Private Sub OnItemChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oLangText As LangText = sender
        mLangResource.UpdateLangText(oLangText)
        RefreshRequest(sender, e)
        ButtonOk.Enabled = False
        RaiseEvent AfterUpdate(mLangResource, New System.EventArgs)
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_LangText
        AddHandler oFrm.AfterUpdate, AddressOf OnItemChanged
        With oFrm
            .LangResource = mLangResource
            .LangText = CurrentLangText()
            .Show()
        End With
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxClau.TextChanged, _
     TextBoxEsp.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mLangResource
            .Clau = TextBoxClau.Text
            .Text = TextBoxEsp.Text
            .LangTexts = GetItmsFromGrid()
            .Update()
        End With
        RaiseEvent AfterUpdate(mLangResource, New System.EventArgs)
        Me.Close()
    End Sub

    Private Function GetItmsFromGrid() As LangTexts
        Dim oItms As New LangTexts
        Dim oItm As LangText
        Dim oLang As DTOLang
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            oLang = New DTOLang(CType(oRow.Cells(Cols.LangId).Value, DTOLang.Ids))
            oItm = New LangText(oLang, oRow.Cells(Cols.Text).Value)
            oItms.Add(oItm)
        Next
        Return oItms
    End Function
End Class