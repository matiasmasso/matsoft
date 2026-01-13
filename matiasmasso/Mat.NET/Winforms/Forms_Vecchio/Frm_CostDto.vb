

Public Class Frm_CostDto

    Private mCostDto As CostDto
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCostDto As CostDto)
        MyBase.New()
        Me.InitializeComponent()
        mCostDto = oCostDto
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mCostDto
            Xl_Lookup_Product1.Product = .Product
            Xl_PercentDto.Value = .Dto
            DateTimePickerFchFrom.Value = .FchFrom
            If .FchTo = Date.MinValue Then
                CheckBoxFchTo.Checked = False
                DateTimePickerFchTo.Visible = False
            Else
                CheckBoxFchTo.Checked = True
                DateTimePickerFchTo.Visible = True
                DateTimePickerFchTo.Value = .FchTo
            End If
            ButtonDel.Enabled = True
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         Xl_Lookup_Product1.AfterUpdate, _
          Xl_PercentDto.AfterUpdate, _
           DateTimePickerFchFrom.ValueChanged, _
            DateTimePickerFchTo.ValueChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mCostDto
            .Product = Xl_Lookup_Product1.Product
            .Dto = Xl_PercentDto.Value
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Date.MinValue
            End If
        End With

        CostDtoLoader.Update(mCostDto)
        RaiseEvent AfterUpdate(mCostDto, System.EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        CostDtoLoader.Delete(mCostDto)
        RaiseEvent AfterUpdate(mCostDto, System.EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchTo.CheckedChanged
        DateTimePickerFchTo.Visible = CheckBoxFchTo.Checked
    End Sub
End Class