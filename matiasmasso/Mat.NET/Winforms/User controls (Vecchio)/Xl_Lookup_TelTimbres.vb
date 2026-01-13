

Public Class Xl_Lookup_TelTimbres
    Inherits Xl_LookupTextboxButton

    Private mTimbre As TelTimbre

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Timbre() As TelTimbre
        Get
            Return mTimbre
        End Get
        Set(ByVal value As TelTimbre)
            mTimbre = value
            If mTimbre Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mTimbre.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Timbre = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Telefons(Frm_Telefons.SelModes.Timbres)
        AddHandler oFrm.AfterSelect, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mTimbre = sender
        MyBase.Text = mTimbre.Nom
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub
End Class
