

Public Class Frm_Aeat

    Private mAeat As AEAT
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oAeat As AEAT)
        MyBase.New()
        Me.InitializeComponent()
        mAeat = oAeat
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        With mAeat
            TextBoxModel.Text = "Mod." & .Model.Model & " - " & .Model.Dsc

            DateTimePicker1.MinDate = New Date(.Yea, 1, 1)
            DateTimePicker1.MaxDate = New Date(.Yea, 12, 31)
            Select Case .Fch
                Case Is < DateTimePicker1.MinDate
                    .Fch = DateTimePicker1.MinDate
                Case Is > DateTimePicker1.MaxDate
                    .Fch = DateTimePicker1.MaxDate
            End Select
            DateTimePicker1.Value = .Fch

            If .Period >= 0 Then NumericUpDownPeriod.Value = .Period
            If .Period > 0 Then
                NumericUpDownPeriod.ReadOnly = True
            End If

            Xl_DocFile1.Load(.DocFile)
            ButtonDel.Enabled = .Exists
        End With
    End Sub

  
    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mAeat
            .Period = NumericUpDownPeriod.Value
            .Fch = DateTimePicker1.Value
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If
        End With

        Dim exs as New List(Of exception)
        If mAeat.Update( exs) Then
            RaiseEvent AfterUpdate(mAeat, e)
            Me.Close()
        Else
            MsgBox("error al desar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub



    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Select Case mAeat.Model.TPeriod
            Case AEAT_Mod.PeriodTypes.Mensual
                NumericUpDownPeriod.Value = DateTimePicker1.Value.Month
            Case AEAT_Mod.PeriodTypes.Trimestral
                NumericUpDownPeriod.Value = CInt((DateTimePicker1.Value.Month - 1) / 3)
        End Select

        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub EnableButtons()
        If NumericUpDownPeriod.Value <> 0 Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest document?", MsgBoxStyle.YesNo, mAeat.Model.Model & " " & mAeat.Period.ToString)
        If rc = MsgBoxResult.Yes Then

            Dim exs as New List(Of exception)
            If mAeat.Delete( exs) Then
                RaiseEvent AfterUpdate(sender, System.EventArgs.Empty)
                Me.Close()
            Else
                MsgBox("error al eliminar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub NumericUpDownPeriod_ValueChanged(sender As Object, e As System.EventArgs) Handles NumericUpDownPeriod.ValueChanged
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub Xl_MediaObject1_AfterUpdate(sender As Object, e As EventArgs)
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        EnableButtons()
    End Sub
End Class