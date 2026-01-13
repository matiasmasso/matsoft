

Public Class Xl_Tpa
    Inherits Xl_LookupTextboxButton
    Private mTpa As tpa

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Tpa As Tpa
        Get
            Return mTpa
        End Get
        Set(ByVal value As Tpa)
            mTpa = value
            If mTpa Is Nothing Then
                MyBase.Text = ""
            Else
                If mTpa.Exists Then
                    MyBase.Text = mTpa.Nom
                Else
                    MyBase.Text = ""
                End If
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Tpa = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oFrm As New Frm_Tpas(oEmp)
        AddHandler oFrm.AfterUpdate, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mTpa = DirectCast(sender, Tpa)
        MyBase.Text = mTpa.Nom
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub

End Class
