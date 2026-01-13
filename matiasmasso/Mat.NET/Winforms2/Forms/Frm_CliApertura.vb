Public Class Frm_CliApertura

    Private _CliApertura As DTOCliApertura
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCliApertura)
        MyBase.New()
        Me.InitializeComponent()
        _CliApertura = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.CliApertura.Load(_CliApertura, exs) Then
            With _CliApertura
                DateTimePickerFchCreated.Value = .FchCreated
                TextBoxNom.Text = .Nom
                TextBoxRaoSocial.Text = .RaoSocial
                TextBoxNomComercial.Text = .NomComercial
                TextBoxNIF.Text = .Nif
                TextBoxAdr.Text = .Adr
                TextBoxZip.Text = .Zip
                TextBoxCit.Text = .Cit
                Xl_LookupArea1.Load(.Zona) '.Area = .Zona
                TextBoxTel.Text = .Tel
                TextBoxEmail.Text = .Email
                TextBoxWeb.Text = .Web
                Xl_LookupContactClass1.ContactClass = .ContactClass

                UIHelper.LoadComboFromEnum(ComboBoxCodSuperficie, GetType(DTOCliApertura.CodsSuperficie),, .CodSuperficie)
                UIHelper.LoadComboFromEnum(ComboBoxCodVolum, GetType(DTOCliApertura.CodsVolumen),, .CodVolumen)
                UIHelper.LoadComboFromEnum(ComboBoxCodAntiguedad, GetType(DTOCliApertura.CodsAntiguedad),, .CodAntiguedad)
                UIHelper.LoadComboFromEnum(ComboBoxCodExperiencia, GetType(DTOCliApertura.CodsExperiencia),, .CodExperiencia)
                UIHelper.LoadComboFromEnum(ComboBoxCodSalePoints, GetType(DTOCliApertura.CodsSalePoint),, .CodSalePoint)


                Dim oBrands As New List(Of DTOProductBrand)
                For Each oBrand In .Brands
                    Dim oProductBrand As New DTOProductBrand(oBrand.Guid)
                    oProductBrand.Nom = DTOLangText.Factory(oBrand.Nom)
                    oBrands.Add(oProductBrand)
                Next

                Xl_ProductBrands1.Load(oBrands, False)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        DateTimePickerFchCreated.ValueChanged,
         TextBoxNom.TextChanged,
          TextBoxRaoSocial.TextChanged,
           TextBoxNomComercial.TextChanged,
            TextBoxNIF.TextChanged,
             TextBoxAdr.TextChanged,
              TextBoxZip.TextChanged,
               TextBoxCit.TextChanged,
                Xl_LookupArea1.AfterUpdate,
                 TextBoxTel.TextChanged,
                  TextBoxEmail.TextChanged,
                   TextBoxWeb.TextChanged,
                    Xl_LookupContactClass1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _CliApertura
            .FchCreated = DateTimePickerFchCreated.Value
            .Nom = TextBoxNom.Text
            .RaoSocial = TextBoxRaoSocial.Text
            .NomComercial = TextBoxNomComercial.Text
            .Nif = TextBoxNIF.Text
            .Adr = TextBoxAdr.Text
            .Zip = TextBoxZip.Text
            .Cit = TextBoxCit.Text
            .Zona = Xl_LookupArea1.Area
            .Tel = TextBoxTel.Text
            .Email = TextBoxEmail.Text
            .Web = TextBoxWeb.Text
            .ContactClass = Xl_LookupContactClass1.ContactClass
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.CliApertura.Update(_CliApertura, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CliApertura))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.CliApertura.Delete(_CliApertura, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CliApertura))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona, Xl_LookupArea1.Area)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupArea1.Load(e.Argument)
    End Sub
End Class


