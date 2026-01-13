Public Class Frm_AreaProvincias
    Private _Country As DTOCountry
    Private _AllProvincias As List(Of DTOAreaProvincia)
    Private _DefaultValue As DTOAreaProvincia
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oCountry As DTOCountry, Optional oDefaultValue As DTOAreaProvincia = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Country = oCountry
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_AreaProvincias_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = String.Format("Provincies de {0}", _Country.LangNom.Tradueix(Current.Session.Lang))
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Sub Xl_AreaProvincias1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_AreaProvincias1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_AreaProvincias1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AreaProvincias1.RequestToAddNew
        Dim oAreaProvincia As New DTOAreaProvincia
        Dim oFrm As New Frm_AreaProvincia(oAreaProvincia)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_AreaProvincias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaProvincias1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oRegions = Await FEB2.AreaRegions.All(_Country, exs)
        If exs.Count = 0 Then
            Dim oAllRegions As New DTOAreaRegio(Guid.Empty, "(totes les regions)")
            oRegions.Insert(0, oAllRegions)
            Dim oRegio As DTOAreaRegio = oRegions.First
            If _DefaultValue IsNot Nothing Then
                If oRegions.Any(Function(x) x.Equals(_DefaultValue.Regio)) Then
                    oRegio = oRegions.First(Function(x) x.Equals(_DefaultValue.Regio))
                End If
            End If
            Xl_AreaRegions1.Load(oRegions, oRegio, DTO.Defaults.SelectionModes.Browse)
            _AllProvincias = Await FEB2.AreaProvincias.All(_Country, exs)
            If exs.Count = 0 Then
                Xl_AreaProvincias1.Load(FilteredProvincias(), _DefaultValue, _SelectionMode)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_AreaRegions1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.ValueChanged
        If _AllowEvents Then
            Xl_AreaProvincias1.Load(FilteredProvincias(), _DefaultValue, _SelectionMode)
        End If
    End Sub


    Private Function FilteredProvincias() As List(Of DTOAreaProvincia)
        Dim retval As List(Of DTOAreaProvincia) = Nothing
        Dim oRegio = Xl_AreaRegions1.Value
        If oRegio.Guid.Equals(Guid.Empty) Then
            retval = _AllProvincias.OrderBy(Function(x) x.Nom).ToList
        Else
            retval = _AllProvincias.Where(Function(x) x.Regio.Equals(oRegio)).OrderBy(Function(x) x.Nom).ToList
        End If
        Return retval
    End Function
End Class