

Public Class Frm_PortsCondicions

    Private mPortsConds As PortsCondicions
    Private _PortsCondicio As DTOPortsCondicio

    Private mCur As DTOCur = BLLApp.Cur
    Private mLang As DTOLang = BLL.BLLApp.Lang
    Private mDs As DataSet
    Private mAllowEvents As Boolean

    Public Sub New(value As DTOPortsCondicio)
        MyBase.New
        InitializeComponent()
        mLang = BLLSession.Current.Lang
        _PortsCondicio = value
        TextBoxNom.Text = _PortsCondicio.Nom
        'TextBoxText.Text = _PortsCondicio.ToString(mLang)

    End Sub

    Public WriteOnly Property PortsCondicions() As PortsCondicions
        Set(ByVal value As PortsCondicions)
            If value IsNot Nothing Then
                mPortsConds = value
                refresca()
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Sub refresca()
        With mPortsConds
            TextBoxNom.Text = .Nom
            TextBoxText.Text = .ToString(mLang)
            Select Case .Cod
                Case MaxiSrvr.PortsCondicions.Cods.PortsDeguts
                    RadioButtonPortsDeguts.Checked = True
                Case MaxiSrvr.PortsCondicions.Cods.PortsPagats, MaxiSrvr.PortsCondicions.Cods.CarrecEnFactura
                    RadioButtonPortsPagats.Checked = True
            End Select

            Xl_AmtPdcMin.Amt = BLLApp.GetAmt(.PdcMinVal, mCur.Tag, .PdcMinVal)
            If .PdcMinVal > 0 Then
                CheckBoxPerImport.Checked = True
            Else
                GroupBoxPdcMinVal.Visible = False
            End If

            TextBoxUnitsQty.Text = .UnitsQty
            Xl_AmtUnitsMinPreu.Amt = BLLApp.GetAmt(.UnitsMinPreu, mCur.Tag, .UnitsMinPreu)
            If .UnitsQty > 0 Or .UnitsMinPreu > 0 Then
                CheckBoxPerQty.Checked = True
            Else
                GroupBoxPcs.Visible = False
            End If

            Xl_AmtForfait.Amt = BLLApp.GetAmt(.Fee, mCur.Tag, .Fee)
            If .Fee > 0 Then
                CheckBoxForfait.Checked = True
            Else
                GroupBoxForfait.Visible = False
            End If
        End With
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mPortsConds
            .Nom = TextBoxNom.Text
            .PdcMinVal = 0
            .UnitsQty = 0
            .UnitsMinPreu = 0
            .Fee = 0

            If RadioButtonPortsDeguts.Checked Then
                .Cod = MaxiSrvr.PortsCondicions.Cods.PortsDeguts
            ElseIf RadioButtonPortsPagats.Checked Then
                .Cod = MaxiSrvr.PortsCondicions.Cods.PortsPagats
            End If

            If CheckBoxPerImport.Checked Then
                .PdcMinVal = Xl_AmtPdcMin.Amt.Val
            End If

            If CheckBoxPerQty.Checked Then
                .UnitsQty = TextBoxUnitsQty.Text
                .UnitsMinPreu = Xl_AmtUnitsMinPreu.Amt.Val
            End If

            If CheckBoxForfait.Checked Then
                .Fee = Xl_AmtForfait.Amt.Val
            End If

            .Update()
        End With
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click

    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged, TextBoxUnitsQty.TextChanged, TextBoxUnitsQty.TextChanged, TextBoxText.TextChanged
        SetDirty()
    End Sub

    Private Sub SetDirty()
        If mAllowEvents Then
            Dim IntUnitsQty As Integer = 0
            If IsNumeric(TextBoxUnitsQty.Text) Then
                IntUnitsQty = CInt(TextBoxUnitsQty.Text)
            End If

            TextBoxText.Text = MaxiSrvr.PortsCondicions.GetString(mLang, BLL.FileSystemHelper.OutputFormat.ASCII, _
                Xl_AmtPdcMin.Amt.Val, IntUnitsQty, Xl_AmtUnitsMinPreu.Amt.Val, _
                Xl_AmtForfait.Amt.Val, mCur)
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub RadioButtonPortsDeguts_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonPortsDeguts.CheckedChanged
        Select Case RadioButtonPortsPagats.Checked
            Case True
                GroupBoxPortsPagats.Visible = True
            Case False
                GroupBoxPortsPagats.Visible = False
        End Select
        SetDirty()
    End Sub

    Private Sub CheckBoxPerImport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxPerImport.CheckedChanged
        GroupBoxPdcMinVal.Visible = CheckBoxPerImport.Checked
        SetDirty()
    End Sub

    Private Sub CheckBoxPerQty_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxPerQty.CheckedChanged
        GroupBoxPcs.Visible = CheckBoxPerQty.Checked
        SetDirty()
    End Sub

    Private Sub CheckBoxForfait_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxForfait.CheckedChanged
        GroupBoxForfait.Visible = CheckBoxForfait.Checked
        SetDirty()
    End Sub

    Private Sub Xl_Amt_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtPdcMin.AfterUpdate, Xl_AmtUnitsMinPreu.AfterUpdate, Xl_AmtForfait.AfterUpdate
        SetDirty()
    End Sub

    Private Enum Cols
        StpGuid
        TpaNom
        StpNom
    End Enum

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Stp.Guid,TPA.DSC AS TPADSC, STP.DSC FROM STP INNER JOIN " _
        & "Tpa ON Tpa.Guid = Stp.Brand " _
        & "WHERE TPA.EMP=" & BLLApp.Emp.Id & " AND " _
        & "STP.PORTS=1 AND " _
        & "TPA.OBSOLETO=0 AND " _
        & "STP.OBSOLETO=0 " _
        & "ORDER BY TPA.ORD, TPA.Guid, STP.ORD, STP.Guid"
        mDs =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.StpGuid)
                .Visible = False
            End With
            With .Columns(Cols.TpaNom)
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.StpNom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Static BlPecesDone As Boolean
        Select Case TabControl1.SelectedTab.Text
            Case TabPagePeces.Text
                If BlPecesDone Then
                    RefrescaGrid()
                End If
                BlPecesDone = True
        End Select
    End Sub

    Private Sub RefrescaGrid()
        LoadGrid()
    End Sub

    Private Function CurrentCategory() As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = New DTOProductCategory(oRow.Cells(Cols.StpGuid).Value)
        End If
        Return retval
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_ProductCategory(CurrentCategory)
        oFrm.Show()
    End Sub
End Class
