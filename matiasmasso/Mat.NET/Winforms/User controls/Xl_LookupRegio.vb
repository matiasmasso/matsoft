Public Class Xl_LookupRegio
    Inherits Xl_LookupTextboxButton

    Private _Country As DTOCountry
    Private _AreaRegio As DTOAreaRegio
    Private _SelMode As DTOArea.SelectModes = DTOArea.SelectModes.SelectAny
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property AreaRegio() As DTOAreaRegio
        Get
            Return _AreaRegio
        End Get
        Set(ByVal value As DTOAreaRegio)
            _AreaRegio = value
            If _AreaRegio IsNot Nothing Then
                _Country = _AreaRegio.Country
            End If
            refresca()
        End Set
    End Property

    Public Shadows Property Country() As DTOCountry
        Get
            Return _Country
        End Get
        Set(ByVal value As DTOCountry)
            _Country = value
            refresca()
        End Set
    End Property

    Public Property SelMode As DTOArea.SelectModes
        Get
            Return _SelMode
        End Get
        Set(value As DTOArea.SelectModes)
            _SelMode = value
        End Set
    End Property


    Public Sub Clear()
        Me.AreaRegio = Nothing
    End Sub

    Private Sub Xl_LookupAreaRegio_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _AreaRegio IsNot Nothing Then FEB2.AreaRegio.Load(_AreaRegio, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_AreaRegions(_Country, _AreaRegio, DTO.Defaults.SelectionModes.selection)
            AddHandler oFrm.itemSelected, AddressOf onAreaRegioSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub onAreaRegioSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _AreaRegio = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _AreaRegio Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _AreaRegio.Nom
            Dim oMenu_AreaRegio As New Menu_AreaRegio(_AreaRegio)
            AddHandler oMenu_AreaRegio.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_AreaRegio.Range)
        End If
    End Sub

End Class