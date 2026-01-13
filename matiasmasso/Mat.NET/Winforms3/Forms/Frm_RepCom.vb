Public Class Frm_RepCom
    Private _Tag As Object
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal item As DTOPurchaseOrderItem)
        MyBase.New
        InitializeComponent()

        _Tag = item
        TextBoxCaption.Text = DTOPurchaseOrderItem.MultilineFullText(item)
        If item.RepCom IsNot Nothing Then
            Xl_LookupRep1.Rep = item.RepCom.Rep
            Xl_Percent1.Value = item.RepCom.Com
            CheckBoxRepCom.Checked = True
            EnableControls()
        End If
        _AllowEvents = True
    End Sub

    Private Sub CheckBoxRepCom_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRepCom.CheckedChanged
        If _AllowEvents Then
            EnableControls()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub EnableControls()
        Xl_LookupRep1.Enabled = CheckBoxRepCom.Checked
        LabelCom.Enabled = CheckBoxRepCom.Checked
        Xl_Percent1.Enabled = CheckBoxRepCom.Checked
    End Sub

    Private Sub ControlChanged(sender As Object, e As MatEventArgs) Handles _
             Xl_LookupRep1.AfterUpdate,
             Xl_Percent1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oRepCom As DTORepCom = Nothing
        If CheckBoxRepCom.Checked Then
            oRepCom = New DTORepCom
            oRepCom.Rep = Xl_LookupRep1.Rep
            oRepCom.Com = Xl_Percent1.Value
            oRepCom.RepCustom = True
        End If

        _Tag.Repcom = oRepCom
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Tag))
        Me.Close()
    End Sub
End Class