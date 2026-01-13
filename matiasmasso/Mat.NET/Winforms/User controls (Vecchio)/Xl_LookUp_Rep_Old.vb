

Public Class Xl_LookUp_Rep_Old
    Inherits Xl_LookupTextboxButton

    Private mRep As Rep

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property Rep() As Rep
        Get
            Return mRep
        End Get
        Set(ByVal value As Rep)
            mRep = value
            If mRep Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mRep.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Rep = Nothing
    End Sub

    Private Sub Xl_Lookup_Rep_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_Contact(mRep)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupRep_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_RepSelection
        AddHandler oFrm.AfterSelect, AddressOf onRepSelected
        oFrm.Show()
    End Sub

    Private Sub onRepSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        mRep = e.Argument
        MyBase.Text = mRep.Abr
        RaiseEvent AfterUpdate(Me, New MatEventArgs(mRep))
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Text = mRep.Nom
    End Sub
End Class

