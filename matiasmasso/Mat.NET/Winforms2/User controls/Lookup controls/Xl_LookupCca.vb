Public Class Xl_LookupCca
    Inherits Xl_LookupTextboxButton

    Private _Cca As DTOCca

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToLookup(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Cca() As DTOCca
        Get
            Return _Cca
        End Get
        Set(ByVal value As DTOCca)
            _Cca = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Cca = Nothing
    End Sub

    Private Sub Xl_LookupCca_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        RaiseEvent RequestToLookup(Me, New MatEventArgs(_Cca))
    End Sub

    Private Sub onCcaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Cca = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Cca Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = Format(_Cca.Fch, "dd/MM/yy") & ": " & _Cca.Concept
            Dim oMenu_Cca As New Menu_Cca(_Cca)
            AddHandler oMenu_Cca.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Cca.Range)
        End If
    End Sub


End Class
