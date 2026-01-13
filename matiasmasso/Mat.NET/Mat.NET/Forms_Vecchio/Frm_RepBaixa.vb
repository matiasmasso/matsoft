

Public Class Frm_RepBaixa

    Private mRep As Rep = Nothing

    Public Event afterupdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oRep As Rep)
        MyBase.New()
        Me.InitializeComponent()
        mRep = oRep
        TextBoxRepNom.Text = mRep.AliasOrRaoSocial
        DateTimePicker1.Value = Today
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

        Dim exs as New List(Of exception)
        If mRep.DonarDeBaixa(DateTimePicker1.Value, exs) Then
            RaiseEvent afterupdate(mRep, EventArgs.Empty)
            Me.Close()
        Else
            MsgBox("error al donar de baixa el representant" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If

    End Sub
End Class