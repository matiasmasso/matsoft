

Public Class Frm_Adr
    Private WithEvents backgroundWorker1 As System.ComponentModel.BackgroundWorker

    Private mAdr As Adr = Nothing
    Private mAdrChanged As Boolean
    Private mCitChanged As Boolean
    Private mAllowEvents As Boolean

    Private _Latitud As Double
    Private _Longitud As Double
    Private _IsNew As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdateAdr(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdateCit(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oAdr As Adr)
        MyBase.New()
        Me.InitializeComponent()
        If oAdr IsNot Nothing Then
            mAdr = oAdr
            If mAdr.Text = "" Then _IsNew = True
            refresca()
            mAllowEvents = True
        End If
    End Sub

    Private Sub refresca()
        TextBoxAdr.Text = mAdr.Text
        TextBoxVia.Text = mAdr.ViaNom
        TextBoxNumero.Text = mAdr.Num
        TextBoxPiso.Text = mAdr.Pis
        Xl_Zip1.Zip = mAdr.Zip
        If mAdr.Coordenadas Is Nothing Then
            Xl_GoogleMaps1.Visible = False
            Xl_StreetView1.Visible = False
        Else
            _Latitud = mAdr.Coordenadas.Latitud
            _Longitud = mAdr.Coordenadas.Longitud
            TextBoxCoordenadas.Text = BLLAddress.CoordenadasToText(_Latitud, _Longitud)
            refrescaMap()
        End If

    End Sub

    Public ReadOnly Property Adr() As Adr
        Get
            Return mAdr
        End Get
    End Property

    Private Sub TextBoxAdr_Changed(ByVal sender As Object, ByVal e As System.EventArgs)

        If mAllowEvents Then
            mAdrChanged = True
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

    Private Sub Xl_Cit1_AfterUpdate(sender As Object, e As System.EventArgs)
        If mAllowEvents Then
            mCitChanged = True
            ButtonOk.Enabled = True
            refrescaMap()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mAdr Is Nothing Then mAdr = New Adr
        With mAdr
            .ViaNom = TextBoxVia.Text
            .Num = TextBoxNumero.Text
            .Pis = TextBoxPiso.Text
            .Text = TextBoxAdr.Text
            .Zip = Xl_Zip1.Zip
        End With
        If mAdrChanged Then
            RaiseEvent AfterUpdateAdr(mAdr, EventArgs.Empty)
        End If
        If mCitChanged Then
            RaiseEvent AfterUpdateCit(mAdr, EventArgs.Empty)
        End If
        RaiseEvent AfterUpdate(mAdr, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCoordenadasReset_Click(sender As Object, e As EventArgs) Handles ButtonCoordenadasReset.Click
        Dim oAddress As New DTOAddress()
        oAddress.ViaNom = TextBoxVia.Text
        oAddress.Num = TextBoxNumero.Text
        oAddress.Zip = New DTOZip(Xl_Zip1.Zip.Guid)
        Dim exs As New List(Of Exception)
        If BLLGoogleMaps.GeoCode(oAddress, exs) Then
            _Latitud = oAddress.Coordenadas.Latitud
            _Longitud = oAddress.Coordenadas.Longitud
            mAdr.Coordenadas = New GeoCoordenadas(_Latitud, _Longitud, GeoCoordenadas.GeoFonts.Auto)
            TextBoxCoordenadas.Text = BLLAddress.CoordenadasToText(_Latitud, _Longitud)
            'GMapControl1.Position = New GMap.NET.PointLatLng(mAdr.Coordenadas.Latitud, mAdr.Coordenadas.Longitud)
            refrescaMap()
            mAdrChanged = True
            ButtonOk.Enabled = True
            RaiseEvent AfterUpdate(mAdr, EventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub Xl_Zip1_AfterUpdate(sender As Object, e As EventArgs)
        refrescaCoordinates()
    End Sub

    Private Async Sub refrescaCoordinates()
        Try
            Await FetchCoordinates()
            TextBoxCoordenadas.Text = BLLAddress.CoordenadasToText(_Latitud, _Longitud)
            ButtonOk.Enabled = True
            refrescaMap()

        Catch ex As Exception
            If ex.Message = "results=0" Then
                UIHelper.WarnError("no s'ha trobat la adreça")
            Else
                UIHelper.WarnError(ex)
            End If
        End Try
    End Sub

    Private Sub refrescaMap()
        Dim oMap As New DTOGoogleMap
        With oMap
            .Latitud = _Latitud
            .Longitud = _Longitud
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
        Dim retval As New DTOAddress()
        With retval
            .ViaNom = TextBoxVia.Text
            .Num = TextBoxNumero.Text
            If Xl_Zip1.Zip IsNot Nothing Then
                .Zip = BLLZip.Find(Xl_Zip1.Zip.Guid)
            End If
            .Text = TextBoxAdr.Text
            .Coordenadas = New DTOGeoCoordenadas(_Latitud, _Longitud)
        End With
        Return retval
    End Function

    Private Function FetchCoordinates() As Task
        Dim oAddress As New DTOAddress()
        oAddress.ViaNom = TextBoxVia.Text
        oAddress.Num = TextBoxNumero.Text
        oAddress.Zip = New DTOZip(Xl_Zip1.Zip.Guid)
        Dim exs As New List(Of Exception)
        If BLLGoogleMaps.GeoCode(oAddress, exs) Then
            _Latitud = oAddress.Coordenadas.Latitud
            _Longitud = oAddress.Coordenadas.Longitud
            mAdr.Coordenadas = New GeoCoordenadas(_Latitud, _Longitud, GeoCoordenadas.GeoFonts.Auto)

            mAdrChanged = True
            Return Task.CompletedTask()
        Else
            Return Task.FromException(exs.First)
        End If
    End Function

    Private Sub TextBoxVia_Validated(sender As Object, e As EventArgs) Handles TextBoxVia.Validated
        If TextBoxVia.Text <> mAdr.NombreDeLaVia Then
            refrescaCoordinates()
            mAdrChanged = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxNumero_Validated(sender As Object, e As EventArgs) Handles TextBoxNumero.Validated
        If TextBoxNumero.Text <> mAdr.NombreDeLaVia Then
            refrescaCoordinates()
            mAdrChanged = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxVia_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxVia.TextChanged,
         TextBoxNumero.TextChanged,
          TextBoxPiso.TextChanged

        If _IsNew Then TextBoxAdr.Text = SuggestedAdr()
    End Sub
End Class