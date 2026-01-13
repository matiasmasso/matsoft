

Public Class Xl_Lookup_TelMissatges
    Inherits Xl_LookupTextboxButton

    Private mMissatge As TelMissatge

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Missatge() As TelMissatge
        Get
            Return mMissatge
        End Get
        Set(ByVal value As TelMissatge)
            mMissatge = value
            If mMissatge Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mMissatge.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Missatge = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Telefons(Frm_Telefons.SelModes.Missatges)
        AddHandler oFrm.AfterSelect, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mMissatge = sender
        MyBase.Text = mMissatge.Nom
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub

End Class