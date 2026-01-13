Public Class Xl_LookupNif
    Inherits Xl_LookupTextboxButton

    Private _Nif As DTONif

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New()
        MyBase.New
        TextBox1.ReadOnly = True
    End Sub

    Public Shadows Property Nif() As DTONif
        Get
            Return _Nif
        End Get
        Set(ByVal value As DTONif)
            _Nif = value
            refresca()
        End Set
    End Property

    Public Shadows Sub Load(oNif As DTONif)
        _Nif = oNif
        refresca()
    End Sub

    Public Sub Clear()
        Me.Location = Nothing
    End Sub

    Private Sub Xl_Nif(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Nif(_Nif)
        AddHandler oFrm.AfterUpdate, AddressOf onNifUpdated
        oFrm.Show()
    End Sub

    Private Sub onNifUpdated(ByVal sender As Object, ByVal e As MatEventArgs)
        _Nif = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Nif Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _Nif.Value & " " & DTONif.CodNom(_Nif.Cod, Current.Session.Lang)
        End If
    End Sub


End Class


