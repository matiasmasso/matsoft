Public Class Frm_Stp_Arts
    Private mStp As Stp
    Private mAllowEvents As Boolean
    Private mDirtyCells As Boolean
    Private mInheritVals As Boolean
    Private mExpanded As Boolean

    Private Enum Cols
        Id
        NomCurt
        RefProveidor
        NomProveidor
        NomClient
        Claus
        Ean
        KgNet
        KgBrut
        m3
        InnerPack
        OuterPack
    End Enum

    Public Sub New(oStp As Stp)
        MyBase.New()
        Me.InitializeComponent()
        mStp = oStp
    End Sub

    Private Sub Frm_Stp_Arts_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        mInheritVals = mStp.Dsc_PropagateToChildren
        mExpanded = Not mInheritVals
        Me.Text = mStp.Tpa.Nom & "/" & mStp.Nom
        Refresca()
    End Sub

    Private Sub Refresca()
        mAllowEvents = False
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ART,ORD,REF,REFPRV,MYD,'' AS CLLS,CBAR,KGNET,KG,M3,INNERPACK,OUTERPACK " _
        & "FROM ART WHERE Category=@Category " _
        & "ORDER BY ORD"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Category", mStp.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oRow As DataRow
        Dim oArt As Art
        For Each oRow In oTb.Rows
            oArt = MaxiSrvr.Art.FromNum(BLLApp.Emp, oRow(Cols.Id))
            oRow(Cols.Claus) = maxisrvr.GetSplitCharSeparatedStringFromArrayList(oArt.Keys)
            If oArt.Hereda Then
                oRow(Cols.KgBrut) = oArt.Dimensions.KgBrut
                oRow(Cols.KgNet) = oArt.Dimensions.KgNet
                oRow(Cols.m3) = oArt.Dimensions.M3
                oRow(Cols.InnerPack) = oArt.Dimensions.InnerPack
                oRow(Cols.OuterPack) = oArt.Dimensions.OuterPack
            End If
        Next
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            '.SelectionMode = DataGridViewSelectionMode.CellSelect
            .DataSource = oTb
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = False
            .AllowDrop = True
            .BackgroundColor = Color.White

            With .Columns(Cols.Id)
                .Visible = False
                .DefaultCellStyle.DataSourceNullValue = 0
            End With
            With CType(.Columns(Cols.NomCurt), DataGridViewTextBoxColumn)
                .HeaderText = "nom curt"
                .Width = 70
                .MaxInputLength = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With CType(.Columns(Cols.RefProveidor), DataGridViewTextBoxColumn)
                .HeaderText = "ref proveidor"
                .Width = 70
                .MaxInputLength = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With CType(.Columns(Cols.NomProveidor), DataGridViewTextBoxColumn)
                .HeaderText = "nom proveidor"
                .Width = IIf(mExpanded, 70, 250)
                .MaxInputLength = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With CType(.Columns(Cols.NomClient), DataGridViewTextBoxColumn)
                .HeaderText = "nom llarg"
                .MaxInputLength = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Claus)
                .HeaderText = "claus"
                .Width = IIf(mExpanded, 70, 250)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With CType(.Columns(Cols.Ean), DataGridViewTextBoxColumn)
                .HeaderText = "codi de barres"
                .Width = 88
                .MaxInputLength = 13
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            If mExpanded Then
                With .Columns(Cols.KgNet)
                    .HeaderText = "pes net"
                    .Width = 50
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#0.0 Kg;-#0.0 Kg;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .ReadOnly = mInheritVals
                    If mInheritVals Then .DefaultCellStyle.BackColor = Color.LightGray
                End With
                With .Columns(Cols.KgBrut)
                    .HeaderText = "pes brut"
                    .Width = 50
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#0.0 Kg;-#0.0 Kg;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .ReadOnly = mInheritVals
                    If mInheritVals Then .DefaultCellStyle.BackColor = Color.LightGray
                End With
                With .Columns(Cols.m3)
                    .HeaderText = "volum"
                    .Width = 60
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#0.000 m3;-#0.000 m3;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .ReadOnly = mInheritVals
                    If mInheritVals Then .DefaultCellStyle.BackColor = Color.LightGray
                End With
                With .Columns(Cols.InnerPack)
                    .HeaderText = "pack"
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .ReadOnly = mInheritVals
                    If mInheritVals Then .DefaultCellStyle.BackColor = Color.LightGray
                End With
                With .Columns(Cols.OuterPack)
                    .HeaderText = "palet"
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .ReadOnly = mInheritVals
                    If mInheritVals Then .DefaultCellStyle.BackColor = Color.LightGray
                End With
            End If
            .Columns(Cols.KgNet).Visible = mExpanded
            .Columns(Cols.KgBrut).Visible = mExpanded
            .Columns(Cols.m3).Visible = mExpanded
            .Columns(Cols.InnerPack).Visible = mExpanded
            .Columns(Cols.OuterPack).Visible = mExpanded
        End With
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then
            mDirtyCells = True
        End If
    End Sub

    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Id).Value) Then
                Dim ArtId As Integer = oRow.Cells(Cols.Id).Value
                oArt = MaxiSrvr.Art.FromNum(BLLApp.Emp, ArtId)
            End If
        End If
        Return oArt
    End Function

    Private Sub RefreshRequest()
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()
        LoadGrid()
        DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

        If i > DataGridView1.Rows.Count - 1 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
        Else
            DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oArt As Art = CurrentArt()

        If oArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(oArt)
            AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oArt As Art = CurrentArt()
        If oArt IsNot Nothing Then
            If oArt.Id <> 0 Then
                Dim oFrm As New Frm_Art(oArt)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            End If
        End If
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        'conversió de coma decimal
        'ho crida DataGridView1_EditingControlShowing
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.KgNet, Cols.KgBrut, Cols.m3
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub


    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing

        'fa que funcioni KeyPress per DataGridViews
        '(per convertir la coma decimal)
        If TypeOf e.Control Is TextBox Then
            Dim oControl As TextBox = CType(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf DataGridView1_KeyPress
        End If
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        If mAllowEvents AndAlso mDirtyCells Then
            UpdateRow(e.RowIndex)
            mDirtyCells = False
        End If
    End Sub

    Private Sub DataGridView1_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.RowValidating

    End Sub

    Private Sub UpdateRow(ByVal iRowIndex As Integer)
        Dim oRow As DataGridViewRow = DataGridView1.Rows(iRowIndex)
        If IsDBNull(oRow.Cells(Cols.Id).Value) Then oRow.Cells(Cols.Id).Value = 0
        Dim ArtId As Integer = oRow.Cells(Cols.Id).Value
        Dim oArt As Art = MaxiSrvr.Art.FromNum(BLLApp.Emp, ArtId)
        If oArt Is Nothing Then
            oArt = New Art(mStp)
            If mStp.Dsc_PropagateToChildren Then
                oArt.Hereda = True
            End If
        Else
            oArt.SetItm()
        End If
        With oArt

            If Not IsDBNull(oRow.Cells(Cols.NomCurt).Value) Then

                .NomCurt = oRow.Cells(Cols.NomCurt).Value
                If IsDBNull(oRow.Cells(Cols.RefProveidor).Value) Then
                    .CodPrv = ""
                Else
                    .CodPrv = oRow.Cells(Cols.RefProveidor).Value
                End If
                If IsDBNull(oRow.Cells(Cols.NomProveidor).Value) Then
                    .NomPrv = ""
                Else
                    .NomPrv = oRow.Cells(Cols.NomProveidor).Value
                End If
                If Not IsDBNull(oRow.Cells(Cols.NomClient).Value) Then
                    .Nom_ESP = oRow.Cells(Cols.NomClient).Value
                End If
                If Not IsDBNull(oRow.Cells(Cols.Claus).Value) Then
                    .Keys = New ArrayList
                    For Each sKey As String In MaxiSrvr.GetArrayListFromSplitCharSeparatedString(oRow.Cells(Cols.Claus).Value)
                        .Keys.Add(sKey)
                    Next
                End If
                If Not IsDBNull(oRow.Cells(Cols.Ean).Value) Then
                    .Cbar = BLLEan13.FromDigits(oRow.Cells(Cols.Ean).Value.ToString)
                End If
                If mInheritVals Then
                    .Hereda = True
                Else
                    If Not IsDBNull(oRow.Cells(Cols.KgNet).Value) Then
                        .Dimensions.KgNet = oRow.Cells(Cols.KgNet).Value
                    End If
                    If Not IsDBNull(oRow.Cells(Cols.KgBrut).Value) Then
                        .Dimensions.KgBrut = oRow.Cells(Cols.KgBrut).Value
                    End If
                    If Not IsDBNull(oRow.Cells(Cols.m3).Value) Then
                        .Dimensions.M3 = oRow.Cells(Cols.m3).Value
                    End If
                    If Not IsDBNull(oRow.Cells(Cols.InnerPack).Value) Then
                        .Dimensions.InnerPack = oRow.Cells(Cols.InnerPack).Value
                    End If
                    '.ForzarInnerPack = oRow.Cells(Cols.Forzar).Value
                    If Not IsDBNull(oRow.Cells(Cols.OuterPack).Value) Then
                        .Dimensions.OuterPack = oRow.Cells(Cols.OuterPack).Value
                    End If
                End If

                Dim exs as New List(Of exception)
                If .Update( exs) Then
                    oRow.Cells(Cols.Id).Value = .Id
                Else
                    MsgBox("error al desar l'article" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If
        End With
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripButtonExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExpand.Click
        mExpanded = Not mExpanded
        LoadGrid()
    End Sub

    Private Sub DataGridView1_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If e.Data.GetDataPresent(GetType(List(Of DTOPriceListItem_Supplier))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        If e.Data.GetDataPresent(GetType(List(Of DTOPriceListItem_Supplier))) Then
            MsgBox("operació obsoleta. fer afegir article al cataleg per el menu de la linea de la tarifa de proveidor", MsgBoxStyle.Exclamation)
        End If
    End Sub
End Class