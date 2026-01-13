Public Class Xl_LookupPgcCta
    Inherits Xl_LookupTextboxButton

    Private _PgcCta As DTOPgcCta

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property PgcCta() As DTOPgcCta
        Get
            Return _PgcCta
        End Get
        Set(ByVal value As DTOPgcCta)
            _PgcCta = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.PgcCta = Nothing
    End Sub

    Private Sub Xl_LookupPgcCta_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _PgcCta IsNot Nothing Then FEB2.PgcCta.Load(_PgcCta, exs)
        Dim oFrm As New Frm_PgcCtas(_PgcCta, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onPgcCtaSelected
        oFrm.Show()
    End Sub

    Private Sub onPgcCtaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PgcCta = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _PgcCta Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = DTOPgcCta.FullNom(_PgcCta, MyBase.Lang)
            Dim oMenu_PgcCta As New Menu_PgcCta(_PgcCta)
            AddHandler oMenu_PgcCta.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_PgcCta.Range)
        End If
    End Sub

End Class

