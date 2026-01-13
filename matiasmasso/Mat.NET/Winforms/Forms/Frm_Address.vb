Public Class Frm_Address
    Private WithEvents backgroundWorker1 As System.ComponentModel.BackgroundWorker

    Private _Address As DTOAddress = Nothing
    Private _AllowEvents As Boolean

    Private _Latitud As Double
    Private _Longitud As Double
    Private _IsNew As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oAddress As DTOAddress)
        MyBase.New()
        Me.InitializeComponent()
        If oAddress IsNot Nothing Then
            _Address = oAddress
            If _Address.Text = "" Then _IsNew = True
            refresca()
            _AllowEvents = True
        End If
    End Sub

    Private Sub refresca()
        TextBoxAdr.Text = _Address.Text
        TextBoxVia.Text = _Address.ViaNom
        TextBoxNumero.Text = _Address.Num
        TextBoxPiso.Text = _Address.Pis
        Xl_Lookup_Zip1.Load(_Address.Zip)
        If _Address.Coordenadas Is Nothing Then
            Xl_GoogleMaps1.Visible = False
            Xl_StreetView1.Visible = False
        Else
            _Latitud = _Address.Coordenadas.Latitud
            _Longitud = _Address.Coordenadas.Longitud
            TextBoxCoordenadas.Text = GeoHelper.Coordenadas.Text(_Latitud, _Longitud)
            refrescaMap()
        End If

    End Sub

    Public ReadOnly Property Adr() As DTOAddress
        Get
            Return _Address
        End Get
    End Property

    Private Sub TextBoxAdr_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Function SuggestedAdr() As String
        Dim sb As New Text.StringBuilder
        sb.Append(TextBoxVia.Text)
        If TextBoxNumero.Text > "" Then
            sb.Append(", " & TextBoxNumero.Text)
        End If
        If TextBoxPiso.Text > "" Then
            sb.Append(" " & TextBoxPiso.Text)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        RaiseEvent AfterUpdate(Me, New MatEventArgs(CurrentAddress))
        Me.Close()
    End Sub

    Private Sub ButtonCoordenadasReset_Click(sender As Object, e As EventArgs) Handles ButtonCoordenadasReset.Click
        Dim exs As New List(Of Exception)
        Dim oAddress As New DTOAddress()
        oAddress.ViaNom = TextBoxVia.Text
        oAddress.Num = TextBoxNumero.Text
        oAddress.Zip = Xl_Lookup_Zip1.Zip
        If oAddress.IsEmpty Then
            UIHelper.WarnError("adreça buida")
        Else
            Dim googleText As String = DTOAddress.GoogleText(oAddress)
            Dim oGeoCoordenadas = GoogleMapsHelper.GeoCode(exs, googleText)
            If exs.Count = 0 Then
                MsgBox("funcionalitat temporalment deshabilitada")
                '_Latitud = oGeoCoordenadas.latitud
                '_Longitud = oGeoCoordenadas.longitud
                '_Address.coordenadas = oGeoCoordenadas
                'TextBoxCoordenadas.Text = GeoHelper.Coordenadas.Text(_Latitud, _Longitud)
                'GMapControl1.Position = New GMap.NET.PointLatLng(_Address.Coordenadas.Latitud, _Address.Coordenadas.Longitud)
                'refrescaMap()
                'ButtonOk.Enabled = True
                'RaiseEvent AfterUpdate(Me, New MatEventArgs(_Address))
            Else
                UIHelper.WarnError(exs)
            End If

        End If
    End Sub



    Private Sub refrescaCoordinates()
        If TextBoxVia.Text > "" And TextBoxNumero.Text > "" Then
            Try
                If FetchCoordinates() Then
                    TextBoxCoordenadas.Text = GeoHelper.Coordenadas.Text(_Latitud, _Longitud)
                    refrescaMap()
                End If
                ButtonOk.Enabled = True

            Catch ex As Exception
                If ex.Message = "results=0" Then
                    UIHelper.WarnError("no s'ha trobat la adreça")
                Else
                    UIHelper.WarnError(ex)
                End If
            End Try

        End If
    End Sub

    Private Sub refrescaMap()
        Dim oMap As New DTOGoogleMap
        With oMap
            .Latitud = _Latitud
            .Longitud = _Longitud
            .MinZoom = 1
            .MaxZoom = 12
            .Zoom = 10
        End With

        Xl_GoogleMaps1.Visible = True
        Xl_GoogleMaps1.Load(oMap)

        If TextBoxAdr.Text = "" Then
            Xl_StreetView1.Visible = False
        Else
            Xl_StreetView1.Visible = True
            Xl_StreetView1.Load(CurrentAddress)
        End If
    End Sub

    Private Function CurrentAddress() As DTOAddress
        With _Address
            .ViaNom = TextBoxVia.Text
            .Num = TextBoxNumero.Text
            .Pis = TextBoxPiso.Text
            .Zip = Xl_Lookup_Zip1.Zip
            .Text = TextBoxAdr.Text
            .Coordenadas = New GeoHelper.Coordenadas(_Latitud, _Longitud)
        End With
        Return _Address
    End Function

    Private Function FetchCoordinates() As Boolean
        Dim retval As Boolean
        Dim oAddress As New DTOAddress()
        oAddress.ViaNom = TextBoxVia.Text
        oAddress.Num = TextBoxNumero.Text
        oAddress.Zip = Xl_Lookup_Zip1.Zip
        Dim googletext = DTOAddress.GoogleNormalized(oAddress)
        If googletext > "" Then
            Dim exs As New List(Of Exception)
            Dim oGeoCoordenadas = GoogleMapsHelper.GeoCode(exs, googletext)
            If exs.Count = 0 Then
                _Latitud = oGeoCoordenadas.Latitud
                _Longitud = oGeoCoordenadas.Longitud
                _Address.Coordenadas = oGeoCoordenadas
                retval = True
                'Return Task.CompletedTask()
            Else
                'Return Task.FromException(exs.First)
            End If

        End If
        Return retval
    End Function

    Private Sub TextBoxVia_Validated(sender As Object, e As EventArgs) Handles TextBoxVia.Validated
        If TextBoxVia.Text <> _Address.ViaNom Then
            refrescaCoordinates()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxNumero_Validated(sender As Object, e As EventArgs) Handles TextBoxNumero.Validated
        If TextBoxNumero.Text <> _Address.Num Then
            refrescaCoordinates()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxVia_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxVia.TextChanged,
         TextBoxNumero.TextChanged,
          TextBoxPiso.TextChanged,
           Xl_Lookup_Zip1.AfterUpdate

        TextBoxAdr.Text = SuggestedAdr()
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_Lookup_Zip1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Lookup_Zip1.AfterUpdate
        RaiseEvent AfterUpdate(Me, New MatEventArgs(CurrentAddress))
        refrescaCoordinates()
    End Sub


End Class