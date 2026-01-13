

Public Class Frm_PrvPrevisions

    Private mPrevisio As Previsio
    Private mProveidor As Proveidor
    Private mMgz As Mgz
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mTpas As Tpas
    Private mAllowEvents As Boolean
    Private mMode As Modes = Modes.Add

    Private mLastValidatedObject As Object
    Private mDirtyCell As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Art
        Dsc
        Qty
    End Enum

    Private Enum Modes
        Add
        Edit
        Insert
    End Enum

    Public WriteOnly Property Previsio() As Previsio
        Set(ByVal Value As Previsio)
            If Not Value Is Nothing Then
                mPrevisio = Value
                mProveidor = mPrevisio.proveidor
                TextBoxPrv.Text = mProveidor.Clx
                TextBoxPrv.ContextMenuStrip = root.ContextMenu_Contact(mProveidor)
                With mPrevisio
                    NumericUpDownYea.Value = .Yea
                    NumericUpDownWeek.Value = .Week
                End With
                refresca()
                mAllowEvents = True
            End If
        End Set
    End Property

    Public WriteOnly Property Proveidor() As Proveidor
        Set(ByVal Value As Proveidor)
            mProveidor = Value
            NumericUpDownYea.Value = Today.Year
            NumericUpDownWeek.Value = DatePart(DateInterval.WeekOfYear, Today, FirstDayOfWeek.Monday, Microsoft.VisualBasic.FirstWeekOfYear.FirstFourDays)
            If Not Value Is Nothing Then
                TextBoxPrv.Text = mProveidor.Clx
                mPrevisio = New Previsio(CurrentProveidor, NumericUpDownYea.Value, NumericUpDownWeek.Value)
                LabelWeekTxt.Text = mPrevisio.WeekTxt
                refresca()
            End If
            mAllowEvents = True
        End Set
    End Property



    Private Sub refresca()
        LabelWeekTxt.Text = mPrevisio.WeekTxt
        LoadGrid()
        'LabelYea.Visible = True
        NumericUpDownYea.Visible = True
        'LabelWeek.Visible = True
        NumericUpDownWeek.Visible = True
        DataGridView1.Visible = True
        mMgz = New Mgz(BLL.BLLApp.Mgz.Guid)
    End Sub

    Private Function CurrentProveidor() As Proveidor
        Return mProveidor
    End Function


    Private Function CurrentYea() As Integer
        Return NumericUpDownYea.Value
    End Function


    Private Function CurrentWeek() As Integer
        Return NumericUpDownWeek.Value
    End Function

    Private Sub LoadGrid()
        mDs = New DataSet
        Dim oTb As DataTable = mDs.Tables.Add()
        oTb.Columns.Add("ART", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("DSC", System.Type.GetType("System.String"))
        oTb.Columns.Add("QTY", System.Type.GetType("System.Int32"))
        Dim oItm As LineItm
        Dim oRow As DataRow
        For Each oItm In mPrevisio.Itms
            oRow = oTb.NewRow()
            oRow(Cols.Art) = oItm.Art.Id
            oRow(Cols.Dsc) = oItm.Art.Nom_ESP
            oRow(Cols.Qty) = oItm.Qty
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.Art)
                .Visible = False
            End With
            With .Columns(Cols.Dsc)
                .HeaderText = "Article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 300
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "Quant"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
        End With
    End Sub

    Private Sub NumericUpDownYea_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownYea.ValueChanged
        If mAllowEvents Then
            If SetClean() Then
                mPrevisio = New Previsio(CurrentProveidor, NumericUpDownYea.Value, NumericUpDownWeek.Value)
                refresca()
            End If
        End If
    End Sub

    Private Sub NumericUpDownWeek_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownWeek.ValueChanged
        If mAllowEvents Then
            If SetClean() Then
                mPrevisio = New Previsio(CurrentProveidor, NumericUpDownYea.Value, NumericUpDownWeek.Value)
                refresca()
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "canviar de setmana"
            If DataGridView1.Rows.Count = 0 Then
                .Enabled = False
            Else
                With .DropDownItems
                    .Add("setmana següent", Nothing, New System.EventHandler(AddressOf MoveToNextWeek_Click))
                    .Add("a la setmana...", Nothing, New System.EventHandler(AddressOf MoveToWeek_Click))
                End With
            End If
            oContextMenu.Items.Add(oMenuItem)
        End With

        If DataGridView1.SelectedRows.Count > 0 Then
            oContextMenu.Items.Add("eliminar", My.Resources.del, New System.EventHandler(AddressOf MenuItemDel_Click))
        End If

        Dim oArt As Art = CurrentArt()
        If oArt IsNot Nothing Then
            oContextMenu.Items.Add("-")

            oMenuItem = New ToolStripMenuItem
            With oMenuItem
                .Text = "article..."
                If DataGridView1.SelectedRows.Count = 1 Then
                    Dim oMenu_Art As New Menu_Art(oArt)
                    .DropDownItems.AddRange(oMenu_Art.Range)
                Else
                    .Enabled = False
                End If
            End With
            oContextMenu.Items.Add(oMenuItem)
        End If


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function SelectedItms() As LineItmPncs
        Dim oItms As New LineItmPncs
        Dim oItm As LineItmPnc
        Dim oRow As DataGridViewRow

        For Each oRow In DataGridView1.SelectedRows
            oItm = New LineItmPnc
            With oItm
                .Art = MaxiSrvr.Art.FromNum(mEmp, oRow.Cells(Cols.Art).Value)
                .Qty = oRow.Cells(Cols.Qty).Value
            End With
            oItms.Add(oItm)
        Next
        Return oItms
    End Function

    Private Sub MenuItemDel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSelectedItems As LineItmPncs = Me.SelectedItms
        Dim oItm As LineItm
        Dim i As Integer
        For Each oItm In oSelectedItems
            For i = 0 To mPrevisio.Itms.Count - 1
                If oItm.Art.Id = mPrevisio.Itms(i).Art.Id And oItm.Qty = mPrevisio.Itms(i).Qty Then
                    mPrevisio.Itms.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        SetDirty()
        refresca()
    End Sub

    Private Sub MoveToNextWeek_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim IntWeek As Integer = NumericUpDownWeek.Value + 1
        Dim IntYea As Integer = NumericUpDownYea.Value
        If IntWeek > 53 Then
            IntYea = IntYea + 1
            IntWeek = 1
        End If
        MoveToWeek(IntYea, IntWeek)
    End Sub

    Private Sub MoveToWeek_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sWeek As String = InputBox("moure a any i semana (YYYYWW)", "M+O", Format(CurrentYea(), "0000") & Format(CurrentWeek, "00"))
        If Len(sWeek) <> 6 Then
            MsgBox("format incorrecte!", MsgBoxStyle.Exclamation, "M+O")
        Else
            MoveToWeek(sWeek.Substring(0, 4), sWeek.Substring(4, 2))
        End If

    End Sub

    Private Sub MoveToWeek(ByVal IntYea As Integer, ByVal IntWeek As Integer)
        Dim exs as New List(Of exception)
        If mPrevisio.MoveToWeek(Me.SelectedItms, IntYea, IntWeek, exs) Then
            SetClean()
            MsgBox("previsió moguda a semana " & IntWeek, MsgBoxStyle.Information, "M+O")
            refresca()
        Else
            MsgBox("error al moure la previsió" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oItms As New LineItmPncs
        Dim oItm As LineItmPnc
        For Each oRow In oTb.Rows
            oItm = New LineItmPnc
            oItm.Qty = oRow(Cols.Qty)
            oItm.Art = MaxiSrvr.Art.FromNum(mEmp, oRow(Cols.Art))
            oItms.Add(oItm)
        Next

        mPrevisio.Itms = oItms

        Dim exs as New List(Of exception)
        If mPrevisio.UpdateItms( exs) Then
            RaiseEvent AfterUpdate(sender, e)
            Me.Close()
        Else
            MsgBox("error al desar la previsió" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = True
    End Sub

    Private Function SetClean() As Boolean
        If ButtonOk.Enabled Then
            Dim rc As MsgBoxResult = MsgBox("Guardem els canvis?", MsgBoxStyle.YesNo, "M+O")
            If rc = MsgBoxResult.Ok Then
                Dim exs as New List(Of exception)
                If mPrevisio.UpdateItms( exs) Then
                    ButtonOk.Enabled = False
                Else
                    MsgBox("error al desar la previsió" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If
        End If
        Return True
    End Function

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        mDirtyCell = True
    End Sub


    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Select Case e.ColumnIndex
                Case Cols.Dsc
                    If e.FormattedValue = "" Then
                        mLastValidatedObject = Nothing
                    Else
                        Dim oSku As ProductSku = Finder.FindSku(BLL.BLLApp.Mgz, e.FormattedValue)
                        If oSku Is Nothing Then
                            e.Cancel = True
                        Else
                            mLastValidatedObject = New Art(oSku.Guid)
                        End If

                    End If
                Case Cols.Qty
                    If Not IsNumeric(e.FormattedValue) Then
                        MsgBox("Només s'accepten valors numerics", MsgBoxStyle.Exclamation, "MAT.NET")
                        e.Cancel = True
                    End If
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case Cols.Dsc
                    Dim oArt As Art = CType(mLastValidatedObject, Art)
                    If oArt Is Nothing Then
                        oRow.Cells(Cols.Art).Value = 0
                        oRow.Cells(Cols.Dsc).Value = ""
                    Else
                        oRow.Cells(Cols.Art).Value = oArt.Id
                        oRow.Cells(Cols.Dsc).Value = oArt.Nom_ESP
                    End If
            End Select

            SetDirty()
            mDirtyCell = False
        End If
    End Sub



    Private Sub ZoomArt()
        Dim oArt As Art = CurrentArt()
        If oArt IsNot Nothing Then
            root.ShowArt(oArt)
        End If
    End Sub

    Private Sub TextBoxPrv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxPrv.DoubleClick
        root.ShowContact(mProveidor)
    End Sub

    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Art).Value) Then

                Dim LngId As Integer = oRow.Cells(Cols.Art).Value
                oArt = MaxiSrvr.Art.FromNum(mEmp, LngId)
                If Not oArt.Exists Then
                    oArt = Nothing
                End If
            End If
        End If
        Return oArt
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ZoomArt()
    End Sub

    Private Sub DataGridView1_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow
        SetDirty()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class
