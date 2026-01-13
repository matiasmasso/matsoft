Public Class Frm_RepLiq
    Private _RepLiq As DTORepLiq
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(oRepLiq As DTORepLiq)
        MyBase.New()
        Me.InitializeComponent()
        _RepLiq = oRepLiq
    End Sub

    Private Sub Frm_RepLiq_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLL_RepLiq.Load(_RepLiq)

        Me.Text = "LIQUIDACIO " & _RepLiq.Id & " A " & _RepLiq.Rep.NickName
        DateTimePicker1.Value = _RepLiq.Fch
        Xl_RepliqFras1.RepComsLiquidables = _RepLiq.Items
        Dim oBaseFras As DTOAmt = BLL_RepLiq.GetBaseFacturas(_RepLiq)
        Dim oComisions As DTOAmt = BLL_RepLiq.GetTotalComisions(_RepLiq)
        Xl_AmtTotalBaseFras.Value = oBaseFras
        Xl_AmtTotalComisions.Value = oComisions
        If _RepLiq.IVApct > 0 Then
            CheckBoxIVA.Checked = True
            Xl_PercentIVA.Value = _RepLiq.IVApct
        End If
        If _RepLiq.IRPFpct > 0 Then
            CheckBoxIRPF.Checked = True
            Xl_PercentIRPF.Value = _RepLiq.IRPFpct
        End If

        If oComisions.Equals(_RepLiq.BaseImponible) Then
            PictureBoxIcon.Image = My.Resources.Ok
        Else
            PictureBoxIcon.Image = My.Resources.warning
        End If

        Refresca()

        If _RepLiq.Cca IsNot Nothing Then
            Dim oDocFile As DTODocFile = _RepLiq.Cca.DocFile
            If oDocFile IsNot Nothing Then
                Xl_DocFile1.Load(oDocFile)
            End If
        End If
        ButtonDel.Enabled = Not _RepLiq.IsNew
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        Dim oBaseImponible As DTOAmt = Xl_AmtTotalComisions.Value
        Dim oLiquid As DTOAmt = oBaseImponible.Clone
        If CheckBoxIVA.Checked Then
            Xl_PercentIVA.Visible = True
            Xl_AmtIVA.Value = Xl_AmtTotalComisions.Value.Percent(Xl_PercentIVA.Value)
            Xl_AmtIVA.Visible = True
            oLiquid.Add(Xl_AmtIVA.Value)
        Else
            Xl_PercentIVA.Visible = False
            Xl_AmtIVA.Visible = False
        End If
        If CheckBoxIRPF.Checked Then
            Xl_PercentIRPF.Visible = True
            Xl_AmtIRPF.Value = Xl_AmtTotalComisions.Value.Percent(Xl_PercentIRPF.Value)
            Xl_AmtIRPF.Visible = True
            oLiquid.Substract(Xl_AmtIRPF.Value)
        Else
            Xl_PercentIRPF.Visible = False
            Xl_AmtIRPF.Visible = False
        End If

        Xl_AmtLiquid.Value = oLiquid
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _RepLiq
            .Fch = DateTimePicker1.Value
            .IVApct = IIf(CheckBoxIVA.Checked, Xl_PercentIVA.Value, 0)
            .IRPFpct = IIf(CheckBoxIRPF.Checked, Xl_PercentIRPF.Value, 0)
        End With

        Dim exs As New List(Of Exception)
        If BLL_RepLiq.Update(_RepLiq, exs) Then
            RaiseEvent AfterUpdate(Me, e)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("retrocedim liquidació " & _RepLiq.Id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL_RepLiq.Delete(_RepLiq, exs) Then
                MsgBox("liquidació " & _RepLiq.Id & " retrocedida", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        'MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        DateTimePicker1.ValueChanged, _
        CheckBoxIVA.CheckedChanged, _
         CheckBoxIRPF.CheckedChanged, _
          Xl_PercentIVA.AfterUpdate, _
           Xl_PercentIRPF.AfterUpdate

        If _AllowEvents Then
            Refresca()
            ButtonOk.Enabled = True
        End If
    End Sub


End Class