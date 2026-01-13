Public Class Xl_LookupProjecte
    Inherits Xl_LookupTextboxButton

    Private _Projecte As DTOProjecte

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToLookup(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Projecte() As DTOProjecte
        Get
            Return _Projecte
        End Get
        Set(ByVal value As DTOProjecte)
            _Projecte = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Projecte = Nothing
    End Sub

    Private Sub Xl_LookupProjecte_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        RaiseEvent RequestToLookup(Me, New MatEventArgs(_Projecte))
    End Sub

    Private Sub onProjecteSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Projecte = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Projecte Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _Projecte.Nom
            Dim oMenu_Projecte As New Menu_Projecte(_Projecte)
            AddHandler oMenu_Projecte.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Projecte.Range)
        End If
    End Sub

    Private Sub Xl_LookupProjecte_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_Projecte(_Projecte)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class

